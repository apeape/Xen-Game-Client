//CompilerOptions = ParentNamespace

// This file defines three key opertaions: 
// Output tone mapping is defined,
// Bloom logic is definde,
// And final screen composite step is defined


#include "RGBM.fx.h"

float4x4 worldViewProj				: WORLDVIEWPROJECTION;


//bloom global values (scale and threshold)
float2	BloomScaleThreshold			: GLOBAL;

//lens eposure (fraction of light to display)
float	LensExposure				: GLOBAL;

//the shader compiler will typically turn these values into floating point constants
//It does this by deciding if the overhead of branching would compensate for inlining
//as a float. Unless the branched logic is highly complex, it usually ends up as a float.
bool	UseExposureTonemapping;
bool	UseGammaCorrection;
bool	UseFilmApproxTonemapping;
bool	UseInverseOneTonemapping;



//Input texture for compositing (in RGBM format)
texture2D InputTexture;
sampler2D InputSampler = sampler_state
{
	Texture = (InputTexture);
    AddressU = Clamp;
    AddressV = Clamp;
};

//Input texture for generating bloom (in RGB + Intensity format)
texture2D BloomTexture;
sampler2D BloomSampler = sampler_state
{
	Texture = (BloomTexture);
    AddressU = Clamp;
    AddressV = Clamp;
};



//This function takes an input colour in linear space, and converts it to a tone mapped
//gamma corrected colour output

float4 OutputColour(float3 colour)
{
	// Multiply the incomming light by the lens exposure value. Think of this in terms of a camera:
	// Exposure time on a camera adjusts how long the camera collects light on the main sensor.
	// This is a simple multiplication factor of the incomming light.
	
	colour.rgb *= LensExposure;
		
	// Apply one of the three exposure functions:
	// See the comments in HDR Lighting.cs for more details.
		
	if (UseExposureTonemapping)
	{
		colour.rgb = 1 - exp(-colour.rgb);
	}
	
	if (UseFilmApproxTonemapping)
	{
		//Note: the 0.6 here is to compensate for this method producing slighty brighter output than the other two
		//Normally this isn't needed.
		colour.rgb *= 0.6;

		//run the approximation
		colour.rgb = colour.rgb * (0.5 + 6.2 * colour.rgb) / (0.06 + colour.rgb * (1.7 + 6.2 * colour.rgb));
		
		//this approximation already performs gamma correction!, so convert back to linear
		//this is obviously highly redundant! but for this sample it's OK
		colour.rgb *= colour.rgb;
	}
	
	if (UseInverseOneTonemapping)
	{
		colour.rgb = colour.rgb / (1 + colour.rgb);
	}
	
	
	// The light is in linear colour space, so, convert it to gamma space.
	// The Human eye sees light in approximately gamma 2.2, so technically, this should be 
	// pow(colour.rgb, 1.0f / 2.2f), however sqrt is very similar and ~ 3x faster.
	
	if (UseGammaCorrection)
	{
		colour.rgb = sqrt(colour.rgb);
	}
	
	// Don't worry too much about performance, both exp() and sqrt() are generally quite fast
	// on most graphics hardware. Even the film approximation is quite fast.
		
	//horray! the final colour output
	return float4(colour,1);
}



//Vertex shader for the two screen space decode operations.
//Just passes through a texture coordinate

void RgbmDecodeVS(	
					float4 position			: POSITION, 
				out float4 positionOut		: POSITION,
					float2 texCoordIn		: TEXCOORD0,
				out float2 texCoordOut		: TEXCOORD0)
{
	positionOut = mul(position,worldViewProj);
	texCoordOut	= texCoordIn;
}


//Decode the incoming RGBM colour value, add bloom and then output.
//This happens when compositing to the screen.

float4 RgbmDecodePS(float2 texCoord : TEXCOORD0) : COLOR0
{
	//Read the rendered linear ouput (in RGBM)
	float4 rgbm = tex2D(InputSampler, texCoord);
	
	//convert to linear RGB
	float3 rgb = DecodeRgbmRender(rgbm); 
	
	//geneate the tonemapped colour output
	float4 output = OutputColour(rgb);
	
	
	//Bloom:
	
	//sample the bloom value.
	float4 bloomSample = tex2D(BloomSampler, texCoord);
	
	//because the bloom texture is 8bit RGBA and is heavily blurred, it's
	//very tricky to store the difference between a moderate bloom and an extremely bright bloom spot (such as the sun)
	//So, the alpha channel of the bloom texture stores a 'bloom booster', storing bloom intensity * 0.1, so extra bright bloom is kept.
	
	float bloomBoost = bloomSample.a * 10;
	float3 bloom = bloomSample.rgb * BloomScaleThreshold.x * (1 + bloomBoost);
	
	//add bloom, using additive.
	//Except, in order to prevent the bloom from blowing out, multiply it by the inverse
	//of the colour it's being added to. So the output won't go above 1.
	//
	output.rgb += bloom * saturate(1-output.rgb);

	//Return the output!
	return output;
}


//Generate the bloom source. Output the bright pixels that will bloom.
//This ouput will later be blurred to form the bloom texture.

float4 RgbmDecodeBloomPassPS(float2 texCoord : TEXCOORD0) : COLOR0
{
	//Sample the output from the render
	float4 rgbm = tex2D(InputSampler, texCoord);
	
	//convert it from RGBM to linear RGB
	float3 rgb = DecodeRgbmRender(rgbm); 
	
	//apply lens exposure and gamma correction, but NO tone mapping.
	float3 bloom = (rgb.rgb * LensExposure);
	
	//work out the intensity of the colour
	float brightness = dot(bloom,float3(0.3,0.6,0.1));
	
	
	//work out the gamma space colour of the bloom (so it just stores the colour of the bloom, not intensity)
	float3 bloomColour = sqrt(bloom / brightness);
	
	//work out the intensity of the output bloom, based on the threshold.
	//anything below the threshold will be zero.
	float bloomScale = max(0, (brightness - BloomScaleThreshold.y) / BloomScaleThreshold.y);
	
	//if you don't separate colour from intensity, then you can get ulgy highly sautrated bloom.
	//This happens when a colour such as orange (1.0, 0.5, 0.0) goes over a threshold in just
	//one colour channel (eg, just the red channel). You get ulgy red bloom from an orange source.
	
	//multiply this with the bloom colour and output
	float3 bloomRgb = bloomColour * saturate(bloomScale);
	
	//store the intensity 'booster' for bright bloom spots, as this will render to an 8bit RGBA
	//texture, and get heavily blurred - so it's very difficult to store spot bloom.
	float bloomIntensityBoost = saturate((bloomScale - 1) * 0.1);
	
	return float4(bloomRgb, bloomIntensityBoost);
}


//define the techniques for the shaders:

technique RgbmDecode
{
   pass
   {
		VertexShader = compile vs_3_0 RgbmDecodeVS();
		PixelShader = compile ps_3_0 RgbmDecodePS();
   }
}

technique RgbmDecodeBloomPass
{
   pass
   {
		VertexShader = compile vs_3_0 RgbmDecodeVS();
		PixelShader = compile ps_3_0 RgbmDecodeBloomPassPS();
   }
}
