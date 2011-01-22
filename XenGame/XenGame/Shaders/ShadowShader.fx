//CompilerOptions = ParentNamespace

float4x4 worldViewProjection	: WORLDVIEWPROJECTION;
float4x4 viewProjection			: VIEWPROJECTION;

float4x4 shadowMapProjection;
float4x4 worldMatrix			: WORLD;

float3 shadowViewDirection;
float3 lightColour;


// --------------------------------
// --------------------------------
//
// This shader performs simple exponential shadow mapping
//
// --------------------------------
// --------------------------------

texture2D ShadowMap;
sampler2D ShadowSampler = sampler_state
{
	Texture = (ShadowMap);
	AddressU = Clamp;
	AddressV = Clamp;
};

texture2D TextureMap;
sampler2D TextureSampler = sampler_state
{
	Texture = (TextureMap);
	AddressU = Clamp;
	AddressV = Clamp;
};






void ShadowVS(
		float4	positionIn	: POSITION,
	out	float4	positionOut	: POSITION,
	
		float3	normalIn	: NORMAL,
	
		float2	texCoord	: TEXCOORD0,
	out	float2	texCoordOut	: TEXCOORD0,
	out	float4	shadowMap	: TEXCOORD1,
	out	float3	colour		: TEXCOORD2
		)
{
	positionOut			= mul(positionIn, worldViewProjection);
	
	float4 worldPosition = mul(positionIn, worldMatrix);
	
	//shadow map projection
	shadowMap			= mul(worldPosition, shadowMapProjection);

	//basic lighting
	float3 normal		= mul(normalIn, (float3x3)worldMatrix);
	colour				= dot(normalize(normal), -shadowViewDirection) * lightColour;
	texCoordOut			= texCoord;
}

//the shadow pixel shader
float4 ShadowPS(float2 texCoord : TEXCOORD0, float4 shadowMapCoord : TEXCOORD1, float3 colour : TEXCOORD2) : COLOR0
{
	//projected texture coordinate
	float3 lookupCoord = shadowMapCoord.xyz / shadowMapCoord.w;
	
	//convert the shadow map projection XY into texture range (so convert from [-1,1] to [0,1])
	float2 shadowCoord = lookupCoord.xy * float2(0.5,-0.5) + 0.5;
	
	//sample the shadow map
	float occlusion = tex2D(ShadowSampler, shadowCoord).r;
	
	//apply a very small bias, to account for sampling errors
	float depth = lookupCoord.z - 0.001;
	
	//difference with real depth
	float difference = occlusion - depth;
	
	//shadow term
	float shadow = saturate(exp(difference * 100));
	
	//sample texture
	colour *= tex2D(TextureSampler, texCoord).rgb;
	colour *= shadow;
	
	return float4(colour,1);
}




technique ShadowShader
{
   pass
   {
		VertexShader = compile vs_2_0 ShadowVS();
		PixelShader = compile ps_2_0 ShadowPS();
   }
}