
//constants required for the shadow map
float4x4	ShadowMapProjection		: GLOBAL;
float2		ShadowMapSize			: GLOBAL;

//texture storing the shadow map
texture2D ShadowTexture : GLOBAL;
sampler2D ShadowSampler = sampler_state
{
	Texture = (ShadowTexture);
	MagFilter = POINT;
	MinFilter = POINT;
	MipFilter = POINT;
};


//Shadow map function, outputs two values.
//first output is the direct shadowing term,
//the second output is the skin scattering shadow term

float2 SampleShadowMap(float4 shadowMapLookup)
{
	//perform perspective divide
	shadowMapLookup		/= shadowMapLookup.w;
	
	//extract the depth and XY
	float depth			= shadowMapLookup.z;
	float2 sampleXY		= shadowMapLookup.xy;
	
	
	float2 inverseSize	= 1 / ShadowMapSize;
	
	//In direct3D, the texture coordinate you pass in samples based on the top corner
	//of the texel in the texture, not the centre of the texel as might otherwise be expected
	//(note, OpenGL samples in the centre).
	//What this means, is that in order to get the correct centre point, you need to offset
	//by half a texel in each direction. However, the size of the texel is determined by the
	//size of the texture.
	//So in this case, to get the centre of the texel would be:
	//
	// sampleXY += inverseSize * 0.5;
	//
	//In general, getting texture sampling for a shadow map correct is *very* tricky.
	//However, in this example, the 4 sourrounding texels are desired:
	
	//Output from the shadow map is in the range [-1,1]. But texture lookups are [0,1].
	//Y is also flipped, so convert it (this could be done by modifying the input matrix):
	sampleXY			= sampleXY * float2(0.5,-0.5) + 0.5;
	
	float2 offsetXY		= sampleXY + inverseSize;
	
	//take 4 depth samples from the nearest texels in the shadow map texture, fetch4 would be good right now!
	
	float sample0		= tex2Dlod(ShadowSampler, float4(sampleXY.x,sampleXY.y,0,0)).x; //top left
	float sample1		= tex2Dlod(ShadowSampler, float4(sampleXY.x,offsetXY.y,0,0)).x; //btm left
	float sample2		= tex2Dlod(ShadowSampler, float4(offsetXY.x,sampleXY.y,0,0)).x; //top right
	float sample3		= tex2Dlod(ShadowSampler, float4(offsetXY.x,offsetXY.y,0,0)).x; //btm right
	
	
	//Now that there are 4 samples of the nearest pixels,
	//we want to perform a bilinear interpolation between them.
	//
	//In order to perform this (without using hardware), would look something
	//like the following:
	//
	//
	//	//work out how close to each texel the sample is
	//	float2 xy_lerp		= frac(sampleXY * ShadowMapSize);
	//	float4 lerp			= float4(1-xy_lerp,xy_lerp);
	//	
	//	//work out how close to each texel the sample is
	//	float2 xy_lerp		= frac(sampleXY * ShadowMapSize);
	//	float4 lerp			= float4(1-xy_lerp,xy_lerp);
	//	
	//	float top = sample0 * lerp.x + sample2 * lerp.z;
	//	float btm = sample1 * lerp.x + sample3 * lerp.z;
	//	float mid = top * lerp.y + btm * lerp.w;
	//	
	//	float bilinearDepth = mid;
	//
	//However, instead of interpolating the samples, it is desired to interpolate
	//the depth difference

	//pack the samples together
	float4 samples		= float4(sample0,sample1,sample2,sample3);
	
	//bias the depth value by the texel size, to reduce visual artifacts.
	//Ideally do this based on the gradient of the samples, but that's too much for this example.
	depth				-= (inverseSize.x + inverseSize.y);
	
	//work out the difference in sample depth and actual depth.
	//if actual depth is greater than sampled depth, then it's in shadow.
	float4 depthDiff	= depth - samples;
	
	//However, the values are approximate...
	//Work out smooth shadow terms based on depth compared to the size of the texels.
	//So instead of instantly switching to shadow, smoothly transition from 0->1. Base this distance on the texel size.
	float4 shadowTerms	=  saturate(depthDiff * (ShadowMapSize.x+ShadowMapSize.y) * 0.5f);
	
	//The skin term is a constant distance that is a much longer fade in. (so light passes through thin surfaces)
	float4 skinTerms	=  saturate(depthDiff * 15);

	//work out how close to each texel is to the sample.
	float2 xy_lerp		= frac(sampleXY * ShadowMapSize);
	
	//true bilinear doesn't actually look all that great. But odly, half bilinear looks quite good
	xy_lerp				= xy_lerp * 0.75 + 0.25;
	
	//work out the bilinear constants
	float4 lerp			= float4(1-xy_lerp,xy_lerp);
	lerp				= lerp.xxzz * lerp.ywyw;
	
	//interpolate the shadow terms
	//where 1 represents full shadow
	float shadowTerm = dot(lerp,shadowTerms);
	float skinShadowTerm = dot(lerp,skinTerms);
	
	//invert the values, so 1 represents no shadow, 0 represents shadow
	shadowTerm = 1 - shadowTerm;
	skinShadowTerm = 1 - skinShadowTerm;
	
	//square the skin for nicer falloff.
	return float2(shadowTerm,skinShadowTerm * skinShadowTerm);
}

