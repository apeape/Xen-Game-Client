//CompilerOptions = ParentNamespace

// This shader performs complex character relighting.


#include "Environment.fx.h"
#include "ShadowMap.fx.h"

float4x4 worldViewProj : WORLDVIEWPROJECTION;
float4x4 world : WORLD;
float4	viewPoint : VIEWPOINT;


//Various tweakables to control rendering
float2	BloomScaleThreshold				: GLOBAL;
float3	SkinLightScatter				: GLOBAL;

float4	SunRgbIntensity					: GLOBAL;
float3	SunDirection					: GLOBAL;
float2	SunSpecularPowerIntensity		: GLOBAL;

float3	AmbientDiffuseSpecularScale		: GLOBAL;
float3	UseAlbedoOcclusionShadow_SimpleTerrain		: GLOBAL; // if these values are 1, then albedo/occlusion/shadow is used


//The 'Sof' texture stores details about the character surfaces:
//S: Specular Intensity (Red)
//O: Ambient Occlusion (Green)
//F: Skin / Face regions (Blue)
texture2D SofTexture;
sampler2D SofSampler = sampler_state
{
	Texture = (SofTexture);
};

//Albedo RGB texture (in Gamma Space)
texture2D AlbedoTexture;
sampler2D AlbedoSampler = sampler_state
{
	Texture = (AlbedoTexture);
	MagFilter = Linear;
	MinFilter = Anisotropic;
	MipFilter = Linear;
	MaxAnisotropy  = 8;
};

//Normal map texture
texture2D NormalTexture;
sampler2D NormalSampler = sampler_state
{
	Texture = (NormalTexture);
	MagFilter = Linear;
	MinFilter = Anisotropic;
	MipFilter = Linear;
	MaxAnisotropy  = 8;
};


//Standard vertex shader for the character
//Takes all the inputs, and mostly passes them through to the pixel shader
void SimpleTerrainVS(	
					float4 position			: POSITION, 
				out float4 positionOut		: POSITION,
					float2 texCoordIn		: TEXCOORD0,
				out float2 texCoordOut		: TEXCOORD0,
					float3 normal			: NORMAL,
					float3 tangent			: TANGENT,
					float3 binorm			: BINORMAL,
				out float3 normalOut		: TEXCOORD1,
				out float3 tangentOut		: TEXCOORD2,
				out float3 binormOut		: TEXCOORD3,
				out float3 viewDirOut		: TEXCOORD4,
				out float4 shadowMapSample	: TEXCOORD5)
{
	positionOut		= mul(position,worldViewProj);
	float4 worldPos	= mul(position,world);
	texCoordOut		= texCoordIn;
	
	//convert normals/tangents into world space
	normalOut		= mul(normal.xyz, (float3x3)world);
	tangentOut		= mul(tangent.xyz,(float3x3)world);
	binormOut		= mul(binorm.xyz, (float3x3)world);
	
	//calculate shadow map projection
	shadowMapSample	= mul(worldPos,ShadowMapProjection);
	
	viewDirOut		= worldPos.xyz - viewPoint.xyz;
}


float4 SimpleTerrainPS(	float2 texCoordIn		: TEXCOORD0,
					float3 normal			: TEXCOORD1,
					float3 tangent			: TEXCOORD2,
					float3 binorm			: TEXCOORD3,
					float3 viewDir			: TEXCOORD4,
					float4 shadowMapSample	: TEXCOORD5) : COLOR 
{
	//This shader is quite complex.
	//It could be a lot simpler if the optional components were removed, split out, etc.
	

	//combine the normal,binorml and tangent into a tangent space matrix.
	float3x3 tangentSpace = float3x3(tangent,binorm,normal);

	//extract the scale constants
	float ambientScale	= AmbientDiffuseSpecularScale.x;
	float diffuseScale	= AmbientDiffuseSpecularScale.y;
	float specularScale	= AmbientDiffuseSpecularScale.z;
	
	//sample of SOF texture
	float3 sof			= tex2D(SofSampler, texCoordIn).xyz;

	//extract the components from the SOF texture
	float specular		= sof.x * specularScale;
	bool isSkin			= sof.z > 0.5;
	
	
	float4	albedo		= 1;
	float	occlusion	= 1;
	float2	shadowOcclusion = 1;
	
	
	//Should we be using the albedo texture? If so, sample albedo when desired.
	if (UseAlbedoOcclusionShadow_SimpleTerrain.x)
	{
		albedo			= tex2D(AlbedoSampler, texCoordIn);
		
		//convert albedo to linear colour
		albedo.rgb		*= albedo.rgb;
	}
	
	//How about occlusion? Store occlusion when desired.
	if (UseAlbedoOcclusionShadow_SimpleTerrain.y)
	{
		//Remember, the occlusion part of the texture stores an *Ambient* occlusion factor.
		//The occlusion should NOT be applied to direct lighting, that is the job of the
		//shadow map and normal mapping! It should only be applied to indirect lighting.
		//The same rule applies if you are using SSAO, you should not apply SSAO to direct lighting!
	
		occlusion		= sof.y;
	}
	
	//sample the shadow map when desired.
	if (UseAlbedoOcclusionShadow_SimpleTerrain.z)
	{
		//this is actually quite complex!
		shadowOcclusion	= SampleShadowMap(shadowMapSample);
	}
	
	
	
	
	//sample the normal map. Bias by 0.5 as it's stored in [0,1] range.
	float3 normalMap	= tex2D(NormalSampler, texCoordIn).xyz - 0.5;
	
	//convert the normal into tangent space
	normalMap			= mul(normalMap, tangentSpace);
	
	//normalise the normal.
	normalMap			= normalize(normalMap);
	
	
	
	//calculate the reflection direction from the view direction and normal direction
	float3 reflectionDir = reflect(viewDir, normalMap);
	
	//normalise it.
	reflectionDir		= normalize(reflectionDir);
	
	
	//sample the reflection...
	float3 reflectionRgb;
	
	if (isSkin)
	{
		//sample the SH for a soft reflection, as reflected by character skin.
		reflectionRgb = SampleSH(reflectionDir);
	}
	else
	{
		//sample the cubemap for a 'sharp' reflection, as reflected by the characters armor
		reflectionRgb = SampleCube(reflectionDir);
	}
	
	//scale by the specular intensity
	reflectionRgb		 *= specular;
	
	
	
	//sample the SH for the ambient colour
	float3 ambient		= SampleSH(normalMap) * ambientScale;
	
	
	
	//get the sun colour (not including intensity!)
	float3 sunColour	= SunRgbIntensity.rgb;
	
	//convert the rgb colour into linear
	sunColour			*= sunColour;
	
	//apply sun intensity scale
	sunColour			*= SunRgbIntensity.a;
	
	
	//extract the sun values
	float sunSpecular	= SunSpecularPowerIntensity.x;
	float sunSpecularIntensity = SunSpecularPowerIntensity.y;
	float3 sunDir		= SunDirection;
	
	
	
	//Compute the dot product of the light direction and normal (L dot N)
	//When the normal is facing the light, the output is 1.0, when facing away, -1.0.
	float LdotN			= dot(normalMap, sunDir);
	
	//Do the same, but for the reflection direction and normal. Clamp to [0,1] range.
	float LdotR			= dot(reflectionDir, sunDir);
	LdotR				= saturate(LdotR);
	
	
	//scale the direct reflected specular by ambient occlusion
	float3 reflectedSpecular = reflectionRgb * occlusion;


	//calculate the specular light
	float specularLight;
	
	if (isSkin)
	{
		//Direct light specular for the skin is very subtle. Just the equivalent of specular exponent of 2.
		specularLight	= LdotR * LdotR;
	}
	else
	{
		//Calculate the direct specular from the directional light (apply power function to LdotR, scale)
		specularLight = saturate(pow(LdotR, sunSpecular)) * sunSpecularIntensity;
	}
	
	
	float3 skinLightScattering	= 0;
	
	if (isSkin)
	{
		//calculate the skin light scattering. This is done by using a very approximate lighting function,
		//Search for 'Half Lambert' for an overview. This is actually 1/3rd Lambert (Hence the 2 / 3).
		//Scale by the scattering RGB term and and diffuse scale.
		skinLightScattering		= SkinLightScatter * (((LdotN + 2.0) / 3.0) * diffuseScale);
	}
	
	//add up the diffuse and specular intensity terms.
	float diffusePlusSpecular	= saturate(LdotN) * diffuseScale + (specularLight * specular);
	
	
	//apply the shadow terms
	diffusePlusSpecular			*= shadowOcclusion.x;
	skinLightScattering			*= shadowOcclusion.y;
	
	
	//add everything up!
	float3 sample				= ambient * occlusion + reflectedSpecular + (diffusePlusSpecular + skinLightScattering) * sunColour;
	
	//and finally apply the albedo colour
	float3 light				= sample * albedo.rgb;
	
	//Done! encode the colour to write out.
	return EncodeRGBM(light);
}


//Setup the character techniques
technique SimpleTerrain
{
   pass
   {
		VertexShader = compile vs_3_0 SimpleTerrainVS();
		PixelShader = compile ps_3_0 SimpleTerrainPS();
   }
}
