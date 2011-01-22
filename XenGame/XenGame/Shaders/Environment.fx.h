
#include "RGBM.fx.h"

//Storage for environment spherical harmonic
float4	EnvironmentSH[9]			: GLOBAL;


//Texture for an RGBM encoded cubemap
textureCUBE CubeRgbmTexture : GLOBAL;
samplerCUBE CubeRgbmSampler = sampler_state
{
	Texture = (CubeRgbmTexture);
};



//sample the cubemap, converting to linear light
float3 SampleCube(float3 normal)
{
	//sample the RGBM cubemap
	float4 rgbm = texCUBE(CubeRgbmSampler,normal);
	
	//convert to RGB
	float3 rgb = DecodeRgbmImage(rgbm);
	
	//convert it to linear colour space (it is stored in gamma space)
	return rgb * rgb;
}



//sample the spherical harmonic, outputting linear light
float3 SampleSH(float3 normal)
{
	// normal = normalize(normal);		// all calls to SampleSH() pass in a normalised vector

	float3 light = 
		EnvironmentSH[0].xyz + 
		EnvironmentSH[1].xyz * normal.x +  
		EnvironmentSH[2].xyz * normal.y + 
		EnvironmentSH[3].xyz * normal.z + 
		EnvironmentSH[4].xyz * (normal.x * normal.y) +
		EnvironmentSH[5].xyz * (normal.y * normal.z) + 
		EnvironmentSH[6].xyz * (normal.x * normal.z) + 
		EnvironmentSH[7].xyz * ((normal.z * normal.z) - (1.0f / 3.0f)) + 
		EnvironmentSH[8].xyz * ((normal.x * normal.x) - (normal.y * normal.y));

	//clamp to zero
	light = max(0,light);
	
	return light;
}