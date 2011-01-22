//CompilerOptions = ParentNamespace

float4x4 worldViewProj : WORLDVIEWPROJECTION;
float4x4 worldMatrix : WORLD;
//float3 lightDirection = { 1.0f, -0.7f, 1.0f };
float textureScale;
float fogStart;
float fogEnd;
float3 fogColor;
float3 lightDirection;

texture SideTexture;
sampler SideTextureSampler = sampler_state
{
   Texture = <SideTexture>;
   MinFilter = ANISOTROPIC;
   MagFilter = ANISOTROPIC;
   MaxAnisotropy = 4;
   MipFilter = ANISOTROPIC;   
   AddressU  = Wrap;
   AddressV  = Wrap;
};

texture BottomTexture;
sampler BottomTextureSampler = sampler_state
{
   Texture = <BottomTexture>;
   MinFilter = ANISOTROPIC;
   MagFilter = ANISOTROPIC;
   MaxAnisotropy = 4;
   MipFilter = ANISOTROPIC;   
   AddressU  = Wrap;
   AddressV  = Wrap;
};

texture TopTexture;
sampler TopTextureSampler = sampler_state
{
   Texture = <TopTexture>;
   MinFilter = ANISOTROPIC;
   MagFilter = ANISOTROPIC;
   MaxAnisotropy = 4;
   MipFilter = ANISOTROPIC;   
   AddressU  = Wrap;
   AddressV  = Wrap;
};


// TODO: add effect parameters here.

struct VertexShaderInput
{
    float4 Position : POSITION0;
    float3 Normal : NORMAL0;

    // TODO: add input channels such as texture
    // coordinates and vertex colors here.
};

struct VertexShaderOutput
{
    float4 Position : POSITION0;
    float3 Normal : TEXCOORD0;
    float3 worldPosition : TEXCOORD1;
	float Depth : TEXCOORD2;

    // TODO: add vertex shader outputs such as colors and texture
    // coordinates here. These values will automatically be interpolated
    // over the triangle, and provided as input to your pixel shader.
};

VertexShaderOutput VertexShaderFunction(VertexShaderInput input)
{
    VertexShaderOutput output;

    output.Position = mul(input.Position, worldViewProj);

	output.worldPosition = input.Position.xyz / input.Position.w;
	//output.Normal = normalize(input.Normal);
	output.Normal = mul(input.Normal, (float3x3)worldMatrix);
	output.Depth = output.Position.z;
    // TODO: add your vertex shader code here.

    return output;
}

float4 PixelShaderFunction(VertexShaderOutput input) : COLOR0
{
	float blendTweak = 0.6f;
	float3 absNormal = max(abs(input.Normal), blendTweak + 0.0001f);
	float3 blend_weights = absNormal - blendTweak;
	//blend_weights = max(blend_weights, 0);
	// force sum to 1.0
	blend_weights /= (blend_weights.x + blend_weights.y + blend_weights.z).xxx;

	float4 blended_color;
	//float tex_scale = 0.05f;

	float2 coord1 = input.worldPosition.yz * textureScale;
	float2 coord2 = input.worldPosition.zx * textureScale;
	float2 coord3 = input.worldPosition.xy * textureScale;

	float4 col1 = tex2D(SideTextureSampler, coord1); //* 0.01 + float4(1.0,0.0,0.0,1.0); // uncomment to see the blending in red/green/blue only
	//float4 col2 = tex2D(ColorMapSampler2, coord2); //* 0.01 + float4(0.0,1.0,0.0,1.0);
	float4 col2;
	// check if normal faces up or down
	if (input.Normal.y < 0)
		col2 = tex2D(BottomTextureSampler, coord2); //* 0.01 + float4(0.0,1.0,0.0,1.0);
	else
		col2 = tex2D(TopTextureSampler, coord2);
	float4 col3 = tex2D(SideTextureSampler, coord3); //* 0.01 + float4(0.0,0.0,1.0,1.0);

	blended_color = col1.xyzw * blend_weights.xxxx +  
					col2.xyzw * blend_weights.yyyy +  
					col3.xyzw * blend_weights.zzzz;

	// directional lighting
	float3 light = -normalize(lightDirection);
	//light.y *= -1;
	float ldn = max(0, dot(light, input.Normal));
	float ambient = 0.4f;

	// per pixel fog
	float fog = saturate((input.Depth - fogStart) / (fogStart - fogEnd));

	blended_color *= (ambient + ldn);
	blended_color = float4(lerp(fogColor, blended_color, fog), 1);
	return blended_color * 0.1f;

	//return blended_color * 0.06f * (ambient + ldn);
}

technique Triplanar
{
    pass Pass1
    {
        // TODO: set renderstates here.

        VertexShader = compile vs_1_1 VertexShaderFunction();
        PixelShader = compile ps_2_0 PixelShaderFunction();
    }
}
