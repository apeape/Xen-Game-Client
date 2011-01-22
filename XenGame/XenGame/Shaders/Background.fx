//CompilerOptions = ParentNamespace

#include "Environment.fx.h"

float4x4 worldViewProj : WORLDVIEWPROJECTION;



//This shader draws the background cubemap as a sphere.




void BackgroundFillVS(	
					float4 position			: POSITION, 
				out float4 positionOut		: POSITION,
				out float3 normalOut		: TEXCOORD0)
{
	// This is a sneaky trick to force geometry to project right to the far clip plane.
	// Using .xyz with w=0 on position prevents the position from being translated (only rotated)
	// As output depth value is Z / W, using .xyww should always generate depth of 1.0 (max depth)
	
	positionOut = mul(float4(position.xyz,0),worldViewProj).xyww;		// * float4(1,1,1,1.000001); // nvidia cards don't like this trick!
	normalOut	= position.xyz;
}

float4 BackgroundFillPS(float3 normal		: TEXCOORD0) : COLOR 
{
	//sample the cubemap background
	float3 sample = SampleCube(normal);
	
	//encode it as RGBM
	return EncodeRGBM(sample);
}

technique BackgroundFill
{
   pass
   {
		VertexShader = compile vs_3_0 BackgroundFillVS();
		PixelShader = compile ps_3_0 BackgroundFillPS();
   }
}


