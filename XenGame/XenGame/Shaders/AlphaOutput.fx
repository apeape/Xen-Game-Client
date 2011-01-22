//CompilerOptions = ParentNamespace

float4x4 worldViewProj : WORLDVIEWPROJECTION;



// This shader simply writes out the alpha values stored in a texture
// It is used to show the Alpha value in the RGBM texture






texture2D DisplayTexture;

sampler2D DisplayTextureSampler = sampler_state
{
	Texture = (DisplayTexture);
};

void AlphaWriteVS(	
					float4 position			: POSITION, 
				out float4 positionOut		: POSITION,
					float2 texCoord			: TEXCOORD0,
				out float2 texCoordOut		: TEXCOORD0)
{
	positionOut = mul(position,worldViewProj);
	texCoordOut = texCoord;
}

float4 AlphaWritePS(float2 texCoord : TEXCOORD0) : COLOR 
{
	return tex2D(DisplayTextureSampler, texCoord).a; // return alpha
}


//--------------------------------------------------------------//
technique AlphaWrite
{
   pass
   {
		VertexShader = compile vs_2_0 AlphaWriteVS();
		PixelShader = compile ps_2_0 AlphaWritePS();
   }
}