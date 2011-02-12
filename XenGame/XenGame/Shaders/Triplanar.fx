//CompilerOptions = ParentNamespace

float4x4 worldViewProj : WORLDVIEWPROJECTION;
float4x4 worldMatrix : WORLD;
//float3 lightDirection = { 1.0f, -0.7f, 1.0f };
float textureScale;
float fogStart;
float fogEnd;
float3 fogColor;
float3 lightDirection;
float3 cameraPosition;

texture SideColor;
texture SideNormal;
texture BottomColor;
texture BottomNormal;
texture TopColor;
texture TopNormal;

sampler2D SideColorSampler = sampler_state
{
   Texture = <SideColor>;
   MinFilter = ANISOTROPIC;
   MagFilter = ANISOTROPIC;
   MaxAnisotropy = 4;
   MipFilter = ANISOTROPIC;   
   AddressU  = Wrap;
   AddressV  = Wrap;
};


sampler2D SideNormalSampler = sampler_state
{
   Texture = <SideNormal>;
   MinFilter = ANISOTROPIC;
   MagFilter = ANISOTROPIC;
   MaxAnisotropy = 4;
   MipFilter = ANISOTROPIC;   
   AddressU  = Wrap;
   AddressV  = Wrap;
};


sampler2D BottomColorSampler = sampler_state
{
   Texture = <BottomColor>;
   MinFilter = ANISOTROPIC;
   MagFilter = ANISOTROPIC;
   MaxAnisotropy = 4;
   MipFilter = ANISOTROPIC;   
   AddressU  = Wrap;
   AddressV  = Wrap;
};


sampler2D BottomNormalSampler = sampler_state
{
   Texture = <BottomNormal>;
   MinFilter = ANISOTROPIC;
   MagFilter = ANISOTROPIC;
   MaxAnisotropy = 4;
   MipFilter = ANISOTROPIC;   
   AddressU  = Wrap;
   AddressV  = Wrap;
};


sampler2D TopColorSampler = sampler_state
{
   Texture = <TopColor>;
   MinFilter = ANISOTROPIC;
   MagFilter = ANISOTROPIC;
   MaxAnisotropy = 4;
   MipFilter = ANISOTROPIC;   
   AddressU  = Wrap;
   AddressV  = Wrap;
};


sampler2D TopNormalSampler = sampler_state
{
   Texture = <TopNormal>;
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
    float3 WorldPosition : TEXCOORD1;
	float Depth : TEXCOORD2;

	// tangents for normal mapping
	float3 LightTan1 : TEXCOORD3;
	float2 LightTan2 : TEXCOORD4;
	float3 CameraTan1 : TEXCOORD5;
	float2 CameraTan2 : TEXCOORD6;

	float3 CameraView : TEXCOORD7;

    // TODO: add vertex shader outputs such as colors and texture
    // coordinates here. These values will automatically be interpolated
    // over the triangle, and provided as input to your pixel shader.
};

VertexShaderOutput VertexShaderFunction(VertexShaderInput input)
{
    VertexShaderOutput output;

    output.Position = mul(input.Position, worldViewProj);

	output.WorldPosition = input.Position.xyz / input.Position.w;
	output.Normal = normalize(input.Normal);
	//output.Normal = mul(input.Normal, (float3x3)worldMatrix);
	output.Depth = output.Position.z;
    
	// calculate normal mapping tangents for light and camera
	// mag = max((nx * nx + ny * ny, nx * nx + nz * nz), 1/32)
	float3 mag = input.Normal * input.Normal;
	mag.xy = mag.x + mag.yz;
	mag.xy = max(mag.xy, 0.03125);
	
	// calculate V x N one time
	float3 vxn = cross(lightDirection, input.Normal);
	float4 temp;
	temp.xy = input.Normal.xz * lightDirection.yz - input.Normal.yx * lightDirection.xz;
	temp.zw = input.Normal.xz * vxn.yx - input.Normal.yx * vxn.xz;
	output.LightTan1.xy = temp.xz * rsqrt(mag.x);
	output.LightTan2 = temp.yw, rsqrt(mag.y);
	output.LightTan1.z = dot(input.Normal, lightDirection);

    // Get the vector from the camera to the vertex for the specular component by
    // subtracting the world position from the camera
    output.CameraView = normalize( cameraPosition - output.WorldPosition );

	vxn = cross(output.CameraView, input.Normal);
	temp.xy = input.Normal.xz * output.CameraView.yz - input.Normal.yx * output.CameraView.xz;
	temp.zw = input.Normal.xz * vxn.yx - input.Normal.yx * vxn.xz;
	output.CameraTan1.xy = temp.xz * rsqrt(mag.x);
	output.CameraTan2 = temp.yw, rsqrt(mag.y);
	output.CameraTan1.z = dot(input.Normal, output.CameraView);

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

	float2 coord1 = input.WorldPosition.yz * textureScale;
	float2 coord2 = input.WorldPosition.zx * textureScale;
	float2 coord3 = input.WorldPosition.xy * textureScale;

	float4 col1 = tex2D(SideColorSampler, coord1);// * 0.01 + float4(1.0,0.0,0.0,1.0); // uncomment to see the blending in red/green/blue only
	//float4 col2 = tex2D(ColorMapSampler2, coord2); //* 0.01 + float4(0.0,1.0,0.0,1.0);
	float4 col2;
	// check if normal faces up or down
	if (input.Normal.y < 0)
		col2 = tex2D(BottomColorSampler, coord2);// * 0.01 + float4(0.0,1.0,0.0,1.0);
	else
		col2 = tex2D(TopColorSampler, coord2);// * 0.01 + float4(0.0,1.0,0.0,1.0);
	float4 col3 = tex2D(SideColorSampler, coord3);// * 0.01 + float4(0.0,0.0,1.0,1.0);

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

	////////////////////////////
	// tangent space normal mapping
	float3 l1 = normalize(input.LightTan1);
	float3 l2 = normalize(float3(input.LightTan2.x, input.LightTan2.y, input.LightTan1.z));

	float3 h1 = normalize(l1 + normalize(input.CameraTan1));
	float3 h2 = normalize(l2 + normalize(float3(input.CameraTan2.x, input.CameraTan2.y, input.CameraTan1.z)));

	// sample normal map textures and blend them together
	float4 ncol1 = tex2D(SideNormalSampler, coord1);
	float4 ncol2;
	// check if normal faces up or down
	if (input.Normal.y < 0)
		ncol2 = tex2D(BottomNormalSampler, coord2);
	else
		ncol2 = tex2D(TopNormalSampler, coord2);
	float4 ncol3 = tex2D(SideNormalSampler, coord3);

	float4 blended_ncolor = ncol1.xyzw * blend_weights.xxxx +  
					ncol2.xyzw * blend_weights.yyyy +  
					ncol3.xyzw * blend_weights.zzzz;

	blended_ncolor = normalize(blended_ncolor);

    // use the normal we looked up to do phong diffuse style lighting.    
    float nDotL = max(dot(blended_ncolor, normalize(lightDirection)), 0);
    float4 diffuse = blended_color * nDotL;
	return diffuse * 0.15f;
	//return blended_ncolor;

	//////////////////////////////

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
