// XenFX
// Assembly = Xen.Graphics.ShaderSystem.CustomTool, Version=7.0.1.1, Culture=neutral, PublicKeyToken=e706afd07878dfca
// SourceFile = SimpleTerrain.fx
// Namespace = GameClient.Shaders

namespace GameClient.Shaders
{
	
	/// <summary><para>Technique 'SimpleTerrain' generated from file 'SimpleTerrain.fx'</para><para>Vertex Shader: approximately 23 instruction slots used, 13 registers</para><para>Pixel Shader: approximately 148 instruction slots used (12 texture, 136 arithmetic), 17 registers</para></summary>
	[System.Diagnostics.DebuggerStepThroughAttribute()]
	[System.CodeDom.Compiler.GeneratedCodeAttribute("Xen.Graphics.ShaderSystem.CustomTool.dll", "5b4ea50c-b2cb-4045-9622-2f93479d1c42")]
	public sealed class SimpleTerrain : Xen.Graphics.ShaderSystem.BaseShader
	{
		/// <summary>Construct an instance of the 'SimpleTerrain' shader</summary>
		public SimpleTerrain()
		{
			this.sc0 = -1;
			this.sc1 = -1;
			this.sc2 = -1;
			this.sc3 = -1;
			this.sc4 = -1;
			this.gc0 = -1;
			this.gc1 = -1;
			this.gc2 = -1;
			this.gc3 = -1;
			this.gc4 = -1;
			this.gc5 = -1;
			this.gc6 = -1;
			this.gc7 = -1;
			this.gc8 = -1;
			this.gc9 = -1;
			this.pts[3] = ((Xen.Graphics.TextureSamplerState)(7296));
			this.pts[0] = ((Xen.Graphics.TextureSamplerState)(213));
			this.pts[4] = ((Xen.Graphics.TextureSamplerState)(7296));
			this.pts[1] = ((Xen.Graphics.TextureSamplerState)(64));
			this.pts[2] = ((Xen.Graphics.TextureSamplerState)(192));
		}
		/// <summary>Setup shader static values</summary><param name="state"/>
		private void gdInit(Xen.Graphics.ShaderSystem.ShaderSystemBase state)
		{
			// set the graphics ID
			SimpleTerrain.gd = state.DeviceUniqueIndex;
			this.GraphicsID = state.DeviceUniqueIndex;
			SimpleTerrain.gid0 = state.GetGlobalUniqueID<Microsoft.Xna.Framework.Matrix>("ShadowMapProjection");
			SimpleTerrain.gid1 = state.GetGlobalUniqueID<Microsoft.Xna.Framework.Vector3>("AmbientDiffuseSpecularScale");
			SimpleTerrain.gid2 = state.GetGlobalUniqueID<Microsoft.Xna.Framework.Vector4[]>("EnvironmentSH");
			SimpleTerrain.gid3 = state.GetGlobalUniqueID<Microsoft.Xna.Framework.Vector2>("RgbmImageRenderScale");
			SimpleTerrain.gid4 = state.GetGlobalUniqueID<Microsoft.Xna.Framework.Vector2>("ShadowMapSize");
			SimpleTerrain.gid5 = state.GetGlobalUniqueID<Microsoft.Xna.Framework.Vector3>("SkinLightScatter");
			SimpleTerrain.gid6 = state.GetGlobalUniqueID<Microsoft.Xna.Framework.Vector3>("SunDirection");
			SimpleTerrain.gid7 = state.GetGlobalUniqueID<Microsoft.Xna.Framework.Vector4>("SunRgbIntensity");
			SimpleTerrain.gid8 = state.GetGlobalUniqueID<Microsoft.Xna.Framework.Vector2>("SunSpecularPowerIntensity");
			SimpleTerrain.gid9 = state.GetGlobalUniqueID<Microsoft.Xna.Framework.Vector3>("UseAlbedoOcclusionShadow_SimpleTerrain");
			SimpleTerrain.sid0 = state.GetNameUniqueID("AlbedoSampler");
			SimpleTerrain.sid1 = state.GetNameUniqueID("CubeRgbmSampler");
			SimpleTerrain.sid2 = state.GetNameUniqueID("NormalSampler");
			SimpleTerrain.sid3 = state.GetNameUniqueID("ShadowSampler");
			SimpleTerrain.sid4 = state.GetNameUniqueID("SofSampler");
			SimpleTerrain.tid0 = state.GetGlobalUniqueID<Microsoft.Xna.Framework.Graphics.TextureCube>("CubeRgbmTexture");
			SimpleTerrain.tid1 = state.GetGlobalUniqueID<Microsoft.Xna.Framework.Graphics.Texture2D>("ShadowTexture");
			SimpleTerrain.tid2 = state.GetNameUniqueID("SofTexture");
			SimpleTerrain.tid3 = state.GetNameUniqueID("AlbedoTexture");
			SimpleTerrain.tid4 = state.GetNameUniqueID("NormalTexture");
		}
		/// <summary>Bind the shader, 'ic' indicates the shader instance has changed and 'ec' indicates the extension has changed.</summary><param name="state"/><param name="ic"/><param name="ec"/><param name="ext"/>
		protected override void BeginImpl(Xen.Graphics.ShaderSystem.ShaderSystemBase state, bool ic, bool ec, Xen.Graphics.ShaderSystem.ShaderExtension ext)
		{
			// if the device changed, call Warm()
			if ((state.DeviceUniqueIndex != SimpleTerrain.gd))
			{
				this.WarmShader(state);
				ic = true;
			}
			// Force updating if the instance has changed
			this.vreg_change = (this.vreg_change | ic);
			this.preg_change = (this.preg_change | ic);
			this.vbreg_change = (this.vbreg_change | ic);
			this.vireg_change = (this.vireg_change | ic);
			// Set the value for attribute 'viewPoint'
			this.vreg_change = (this.vreg_change | state.SetViewPointVector4(ref this.vreg[12], ref this.sc0));
			// Set the value for attribute 'world'
			this.vreg_change = (this.vreg_change | state.SetWorldMatrix(ref this.vreg[8], ref this.vreg[9], ref this.vreg[10], ref this.vreg[11], ref this.sc1));
			// Set the value for attribute 'worldViewProj'
			this.vreg_change = (this.vreg_change | state.SetWorldViewProjectionMatrix(ref this.vreg[4], ref this.vreg[5], ref this.vreg[6], ref this.vreg[7], ref this.sc2));
			// Set the value for global 'ShadowMapProjection'
			this.vreg_change = (this.vreg_change | state.SetGlobalMatrix4(ref this.vreg[0], ref this.vreg[1], ref this.vreg[2], ref this.vreg[3], SimpleTerrain.gid0, ref this.gc0));
			// Set the value for global 'AmbientDiffuseSpecularScale'
			this.preg_change = (this.preg_change | state.SetGlobalVector3(ref this.preg[15], SimpleTerrain.gid1, ref this.gc1));
			// Set the value for global 'EnvironmentSH'
			this.preg_change = (this.preg_change | state.SetGlobalVector4(this.preg, 0, 9, SimpleTerrain.gid2, ref this.gc2));
			// Set the value for global 'RgbmImageRenderScale'
			this.preg_change = (this.preg_change | state.SetGlobalVector2(ref this.preg[9], SimpleTerrain.gid3, ref this.gc3));
			// Set the value for global 'ShadowMapSize'
			this.preg_change = (this.preg_change | state.SetGlobalVector2(ref this.preg[10], SimpleTerrain.gid4, ref this.gc4));
			// Set the value for global 'SkinLightScatter'
			this.preg_change = (this.preg_change | state.SetGlobalVector3(ref this.preg[11], SimpleTerrain.gid5, ref this.gc5));
			// Set the value for global 'SunDirection'
			this.preg_change = (this.preg_change | state.SetGlobalVector3(ref this.preg[13], SimpleTerrain.gid6, ref this.gc6));
			// Set the value for global 'SunRgbIntensity'
			this.preg_change = (this.preg_change | state.SetGlobalVector4(ref this.preg[12], SimpleTerrain.gid7, ref this.gc7));
			// Set the value for global 'SunSpecularPowerIntensity'
			this.preg_change = (this.preg_change | state.SetGlobalVector2(ref this.preg[14], SimpleTerrain.gid8, ref this.gc8));
			// Set the value for global 'UseAlbedoOcclusionShadow_SimpleTerrain'
			this.preg_change = (this.preg_change | state.SetGlobalVector3(ref this.preg[16], SimpleTerrain.gid9, ref this.gc9));
			// Assign global textures
			this.CubeRgbmTexture = state.GetGlobalTextureCube(SimpleTerrain.tid0);
			this.ShadowTexture = state.GetGlobalTexture2D(SimpleTerrain.tid1);
			// Assign pixel shader textures and samplers
			if ((ic | this.ptc))
			{
				state.SetPixelShaderSamplers(this.ptx, this.pts);
				this.ptc = false;
			}
			if ((this.vreg_change == true))
			{
				SimpleTerrain.fx.vs_c.SetValue(this.vreg);
				this.vreg_change = false;
				ic = true;
			}
			if ((this.preg_change == true))
			{
				SimpleTerrain.fx.ps_c.SetValue(this.preg);
				this.preg_change = false;
				ic = true;
			}
			if ((ext == Xen.Graphics.ShaderSystem.ShaderExtension.Blending))
			{
				ic = (ic | state.SetBlendMatricesDirect(SimpleTerrain.fx.vsb_c, ref this.sc3));
			}
			if ((ext == Xen.Graphics.ShaderSystem.ShaderExtension.Instancing))
			{
				this.vireg_change = (this.vireg_change | state.SetViewProjectionMatrix(ref this.vireg[0], ref this.vireg[1], ref this.vireg[2], ref this.vireg[3], ref this.sc4));
				if ((this.vireg_change == true))
				{
					SimpleTerrain.fx.vsi_c.SetValue(this.vireg);
					this.vireg_change = false;
					ic = true;
				}
			}
			// Finally, bind the effect
			if ((ic | ec))
			{
				state.SetEffect(this, ref SimpleTerrain.fx, ext);
			}
		}
		/// <summary>Warm (Preload) the shader</summary><param name="state"/>
		protected override void WarmShader(Xen.Graphics.ShaderSystem.ShaderSystemBase state)
		{
			// Shader is already warmed
			if ((SimpleTerrain.gd == state.DeviceUniqueIndex))
			{
				return;
			}
			// Setup the shader
			if ((SimpleTerrain.gd != state.DeviceUniqueIndex))
			{
				this.gdInit(state);
			}
			SimpleTerrain.fx.Dispose();
			// Create the effect instance
			state.CreateEffect(out SimpleTerrain.fx, SimpleTerrain.fxb, 35, 146);
		}
		/// <summary>True if a shader constant has changed since the last Bind()</summary>
		protected override bool Changed()
		{
			return ((this.vreg_change | this.preg_change) 
						| this.ptc);
		}
		/// <summary>Returns the number of vertex inputs used by this shader</summary>
		protected override int GetVertexInputCountImpl()
		{
			return 5;
		}
		/// <summary>Returns a vertex input used by this shader</summary><param name="i"/><param name="usage"/><param name="index"/>
		protected override void GetVertexInputImpl(int i, out Microsoft.Xna.Framework.Graphics.VertexElementUsage usage, out int index)
		{
			usage = ((Microsoft.Xna.Framework.Graphics.VertexElementUsage)(SimpleTerrain.vin[i]));
			index = SimpleTerrain.vin[(i + 5)];
		}
		/// <summary>Static graphics ID</summary>
		private static int gd;
		/// <summary>Static effect container instance</summary>
		private static Xen.Graphics.ShaderSystem.ShaderEffect fx;
		/// <summary/>
		private bool vreg_change;
		/// <summary/>
		private bool preg_change;
		/// <summary/>
		private bool vbreg_change;
		/// <summary/>
		private bool vireg_change;
		/// <summary>Return the supported modes for this shader</summary><param name="blendingSupport"/><param name="instancingSupport"/>
		protected override void GetExtensionSupportImpl(out bool blendingSupport, out bool instancingSupport)
		{
			blendingSupport = true;
			instancingSupport = true;
		}
		/// <summary>Change ID for Semantic bound attribute 'viewPoint'</summary>
		private int sc0;
		/// <summary>Change ID for Semantic bound attribute 'world'</summary>
		private int sc1;
		/// <summary>Change ID for Semantic bound attribute 'worldViewProj'</summary>
		private int sc2;
		/// <summary>Change ID for Semantic bound attribute '__BLENDMATRICES__GENMATRIX'</summary>
		private int sc3;
		/// <summary>Change ID for Semantic bound attribute '__VIEWPROJECTION__GENMATRIX'</summary>
		private int sc4;
		/// <summary>TypeID for global attribute 'float4x4 ShadowMapProjection'</summary>
		private static int gid0;
		/// <summary>Change ID for global attribute 'float4x4 ShadowMapProjection'</summary>
		private int gc0;
		/// <summary>TypeID for global attribute 'float3 AmbientDiffuseSpecularScale'</summary>
		private static int gid1;
		/// <summary>Change ID for global attribute 'float3 AmbientDiffuseSpecularScale'</summary>
		private int gc1;
		/// <summary>TypeID for global attribute 'float4 EnvironmentSH'</summary>
		private static int gid2;
		/// <summary>Change ID for global attribute 'float4 EnvironmentSH'</summary>
		private int gc2;
		/// <summary>TypeID for global attribute 'float2 RgbmImageRenderScale'</summary>
		private static int gid3;
		/// <summary>Change ID for global attribute 'float2 RgbmImageRenderScale'</summary>
		private int gc3;
		/// <summary>TypeID for global attribute 'float2 ShadowMapSize'</summary>
		private static int gid4;
		/// <summary>Change ID for global attribute 'float2 ShadowMapSize'</summary>
		private int gc4;
		/// <summary>TypeID for global attribute 'float3 SkinLightScatter'</summary>
		private static int gid5;
		/// <summary>Change ID for global attribute 'float3 SkinLightScatter'</summary>
		private int gc5;
		/// <summary>TypeID for global attribute 'float3 SunDirection'</summary>
		private static int gid6;
		/// <summary>Change ID for global attribute 'float3 SunDirection'</summary>
		private int gc6;
		/// <summary>TypeID for global attribute 'float4 SunRgbIntensity'</summary>
		private static int gid7;
		/// <summary>Change ID for global attribute 'float4 SunRgbIntensity'</summary>
		private int gc7;
		/// <summary>TypeID for global attribute 'float2 SunSpecularPowerIntensity'</summary>
		private static int gid8;
		/// <summary>Change ID for global attribute 'float2 SunSpecularPowerIntensity'</summary>
		private int gc8;
		/// <summary>TypeID for global attribute 'float3 UseAlbedoOcclusionShadow_SimpleTerrain'</summary>
		private static int gid9;
		/// <summary>Change ID for global attribute 'float3 UseAlbedoOcclusionShadow_SimpleTerrain'</summary>
		private int gc9;
		/// <summary>Get/Set the Texture Sampler State for 'Sampler2D AlbedoSampler'</summary>
		public Xen.Graphics.TextureSamplerState AlbedoSampler
		{
			get
			{
				return this.pts[3];
			}
			set
			{
				if ((value != this.pts[3]))
				{
					this.pts[3] = value;
					this.ptc = true;
				}
			}
		}
		/// <summary>Get/Set the Texture Sampler State for 'SamplerCUBE CubeRgbmSampler'</summary>
		public Xen.Graphics.TextureSamplerState CubeRgbmSampler
		{
			get
			{
				return this.pts[0];
			}
			set
			{
				if ((value != this.pts[0]))
				{
					this.pts[0] = value;
					this.ptc = true;
				}
			}
		}
		/// <summary>Get/Set the Texture Sampler State for 'Sampler2D NormalSampler'</summary>
		public Xen.Graphics.TextureSamplerState NormalSampler
		{
			get
			{
				return this.pts[4];
			}
			set
			{
				if ((value != this.pts[4]))
				{
					this.pts[4] = value;
					this.ptc = true;
				}
			}
		}
		/// <summary>Get/Set the Texture Sampler State for 'Sampler2D ShadowSampler'</summary>
		public Xen.Graphics.TextureSamplerState ShadowSampler
		{
			get
			{
				return this.pts[1];
			}
			set
			{
				if ((value != this.pts[1]))
				{
					this.pts[1] = value;
					this.ptc = true;
				}
			}
		}
		/// <summary>Get/Set the Texture Sampler State for 'Sampler2D SofSampler'</summary>
		public Xen.Graphics.TextureSamplerState SofSampler
		{
			get
			{
				return this.pts[2];
			}
			set
			{
				if ((value != this.pts[2]))
				{
					this.pts[2] = value;
					this.ptc = true;
				}
			}
		}
		/// <summary>Get/Set the Bound texture for 'TextureCube CubeRgbmTexture'</summary>
		private Microsoft.Xna.Framework.Graphics.TextureCube CubeRgbmTexture
		{
			get
			{
				return ((Microsoft.Xna.Framework.Graphics.TextureCube)(this.ptx[0]));
			}
			set
			{
				if ((value != this.ptx[0]))
				{
					this.ptc = true;
					this.ptx[0] = value;
				}
			}
		}
		/// <summary>Get/Set the Bound texture for 'Texture2D ShadowTexture'</summary>
		private Microsoft.Xna.Framework.Graphics.Texture2D ShadowTexture
		{
			get
			{
				return ((Microsoft.Xna.Framework.Graphics.Texture2D)(this.ptx[1]));
			}
			set
			{
				if ((value != this.ptx[1]))
				{
					this.ptc = true;
					this.ptx[1] = value;
				}
			}
		}
		/// <summary>Get/Set the Bound texture for 'Texture2D SofTexture'</summary>
		public Microsoft.Xna.Framework.Graphics.Texture2D SofTexture
		{
			get
			{
				return ((Microsoft.Xna.Framework.Graphics.Texture2D)(this.ptx[2]));
			}
			set
			{
				if ((value != this.ptx[2]))
				{
					this.ptc = true;
					this.ptx[2] = value;
				}
			}
		}
		/// <summary>Get/Set the Bound texture for 'Texture2D AlbedoTexture'</summary>
		public Microsoft.Xna.Framework.Graphics.Texture2D AlbedoTexture
		{
			get
			{
				return ((Microsoft.Xna.Framework.Graphics.Texture2D)(this.ptx[3]));
			}
			set
			{
				if ((value != this.ptx[3]))
				{
					this.ptc = true;
					this.ptx[3] = value;
				}
			}
		}
		/// <summary>Get/Set the Bound texture for 'Texture2D NormalTexture'</summary>
		public Microsoft.Xna.Framework.Graphics.Texture2D NormalTexture
		{
			get
			{
				return ((Microsoft.Xna.Framework.Graphics.Texture2D)(this.ptx[4]));
			}
			set
			{
				if ((value != this.ptx[4]))
				{
					this.ptc = true;
					this.ptx[4] = value;
				}
			}
		}
		/// <summary>Name uid for sampler for 'Sampler2D AlbedoSampler'</summary>
		static int sid0;
		/// <summary>Name uid for sampler for 'SamplerCUBE CubeRgbmSampler'</summary>
		static int sid1;
		/// <summary>Name uid for sampler for 'Sampler2D NormalSampler'</summary>
		static int sid2;
		/// <summary>Name uid for sampler for 'Sampler2D ShadowSampler'</summary>
		static int sid3;
		/// <summary>Name uid for sampler for 'Sampler2D SofSampler'</summary>
		static int sid4;
		/// <summary>Name uid for texture for 'TextureCube CubeRgbmTexture'</summary>
		static int tid0;
		/// <summary>Name uid for texture for 'Texture2D ShadowTexture'</summary>
		static int tid1;
		/// <summary>Name uid for texture for 'Texture2D SofTexture'</summary>
		static int tid2;
		/// <summary>Name uid for texture for 'Texture2D AlbedoTexture'</summary>
		static int tid3;
		/// <summary>Name uid for texture for 'Texture2D NormalTexture'</summary>
		static int tid4;
		/// <summary>Pixel samplers/textures changed</summary>
		bool ptc;
		/// <summary>array storing vertex usages, and element indices</summary>
readonly 
		private static int[] vin = new int[] {0,2,3,4,5,0,0,0,0,0};
		/// <summary>Vertex shader register storage</summary>
readonly 
		private Microsoft.Xna.Framework.Vector4[] vreg = new Microsoft.Xna.Framework.Vector4[13];
		/// <summary>Pixel shader register storage</summary>
readonly 
		private Microsoft.Xna.Framework.Vector4[] preg = new Microsoft.Xna.Framework.Vector4[17];
		/// <summary>Instancing shader register storage</summary>
readonly 
		private Microsoft.Xna.Framework.Vector4[] vireg = new Microsoft.Xna.Framework.Vector4[4];
		/// <summary>Bound pixel textures</summary>
readonly 
		Microsoft.Xna.Framework.Graphics.Texture[] ptx = new Microsoft.Xna.Framework.Graphics.Texture[5];
		/// <summary>Bound pixel samplers</summary>
readonly 
		Xen.Graphics.TextureSamplerState[] pts = new Xen.Graphics.TextureSamplerState[5];
#if XBOX360
		/// <summary>Static RLE compressed shader byte code (Xbox360)</summary>
		private static byte[] fxb
		{
			get
			{
				return new byte[] {4,188,240,11,207,131,0,1,32,152,0,8,254,255,9,1,0,0,17,184,135,0,1,3,131,0,1,1,131,0,1,240,135,0,1,13,131,0,1,4,131,0,1,1,229,0,0,229,0,0,137,0,0,1,6,1,95,1,118,1,115,1,95,1,99,134,0,0,1,3,131,0,0,1,1,1,0,1,0,1,2,1,40,135,0,0,1,17,131,0,0,1,4,131,0,0,1,1,229,0,0,229,0,0,201,0,0,1,6,1,95,1,112,1,115,1,95,1,99,134,0,0,1,3,131,0,0,1,1,1,0,1,0,1,15,1,208,135,0,0,1,216,131,0,0,1,4,131,0,0,1,1,229,0,0,229,0,0,229,0,0,229,0,0,229,0,0,229,0,0,229,0,0,229,0,0,229,0,0,229,0,0,229,0,0,229,0,0,229,0,0,229,0,0,229,0,0,229,0,0,229,0,0,229,0,0,229,0,0,229,0,0,229,0,0,229,0,0,229,0,0,229,0,0,229,0,0,229,0,0,229,0,0,229,0,0,229,0,0,229,0,0,229,0,0,229,0,0,229,0,0,229,0,0,153,0,0,1,7,1,95,1,118,1,115,1,98,1,95,1,99,133,0,0,1,3,131,0,0,1,1,1,0,1,0,1,16,1,56,135,0,0,1,4,131,0,0,1,4,131,0,0,1,1,195,0,0,1,7,1,95,1,118,1,115,1,105,1,95,1,99,133,0,0,1,14,131,0,0,1,4,1,0,1,0,1,16,1,92,143,0,0,1,7,1,95,1,112,1,115,1,95,1,115,1,48,133,0,0,1,12,131,0,0,1,4,1,0,1,0,1,16,1,128,143,0,0,1,7,1,95,1,112,1,115,1,95,1,115,1,49,133,0,0,1,12,131,0,0,1,4,1,0,1,0,1,16,1,164,143,0,0,1,7,1,95,1,112,1,115,1,95,1,115,1,50,133,0,0,1,12,131,0,0,1,4,1,0,1,0,1,16,1,200,143,0,0,1,7,1,95,1,112,1,115,1,95,1,115,1,51,133,0,0,1,12,131,0,0,1,4,1,0,1,0,1,16,1,236,143,0,0,1,7,1,95,1,112,1,115,1,95,1,115,1,52,133,0,0,1,1,131,0,0,1,16,131,0,0,1,4,143,0,0,1,2,131,0,0,1,15,131,0,0,1,4,147,0,0,1,3,131,0,0,1,16,131,0,0,1,4,143,0,0,1,4,131,0,0,1,15,131,0,0,1,4,143,0,0,1,9,1,66,1,108,1,101,1,110,1,100,1,105,1,110,1,103,135,0,0,1,5,131,0,0,1,16,131,0,0,1,4,143,0,0,1,6,131,0,0,1,15,131,0,0,1,4,143,0,0,1,11,1,73,1,110,1,115,1,116,1,97,1,110,1,99,1,105,1,110,1,103,133,0,0,1,7,1,83,1,104,1,97,1,100,1,101,1,114,133,0,0,1,9,131,0,0,1,1,131,0,0,1,14,131,0,0,1,7,131,0,0,1,4,131,0,0,1,32,139,0,0,1,252,1,0,1,0,1,1,1,24,138,0,0,1,2,1,52,1,0,1,0,1,2,1,80,138,0,0,1,15,1,220,1,0,1,0,1,15,1,248,138,0,0,1,16,1,68,1,0,1,0,1,16,1,88,138,0,0,1,16,1,104,1,0,1,0,1,16,1,124,138,0,0,1,16,1,140,1,0,1,0,1,16,1,160,138,0,0,1,16,1,176,1,0,1,0,1,16,1,196,138,0,0,1,16,1,212,1,0,1,0,1,16,1,232,138,0,0,1,17,1,172,135,0,0,1,3,1,0,1,0,1,17,1,40,135,0,0,1,2,131,0,0,1,92,134,0,0,1,16,1,252,1,0,1,0,1,16,1,248,131,0,0,1,93,134,0,0,1,17,1,20,1,0,1,0,1,17,1,16,1,0,1,0,1,17,1,92,135,0,0,1,2,131,0,0,1,92,134,0,0,1,17,1,48,1,0,1,0,1,17,1,44,131,0,0,1,93,134,0,0,1,17,1,72,1,0,1,0,1,17,1,68,1,0,1,0,1,17,1,156,135,0,0,1,2,131,0,0,1,92,134,0,0,1,17,1,112,1,0,1,0,1,17,1,108,131,0,0,1,93,134,0,0,1,17,1,136,1,0,1,0,1,17,1,132,135,0,0,1,6,135,0,0,1,2,132,255,0,131,0,0,1,1,134,0,0,1,8,1,88,1,16,1,42,1,17,131,0,0,1,2,1,156,1,0,1,0,1,5,1,188,135,0,0,1,36,1,0,1,0,1,2,1,60,1,0,1,0,1,2,1,100,138,0,0,1,2,1,20,131,0,0,1,28,1,0,1,0,1,2,1,8,1,255,1,255,1,3,132,0,0,1,6,131,0,0,1,28,134,0,0,1,2,1,1,131,0,0,1,148,1,0,1,2,131,0,0,1,17,133,0,0,1,156,131,0,0,1,172,1,0,1,0,1,1,1,188,1,0,1,3,131,0,0,1,1,132,0,0,1,1,1,196,134,0,0,1,1,1,212,1,0,1,3,1,0,1,1,1,0,1,1,132,0,0,1,1,1,220,134,0,0,1,1,1,236,1,0,1,3,1,0,1,2,1,0,1,1,132,0,0,1,1,1,220,134,0,0,1,1,1,243,1,0,1,3,1,0,1,3,1,0,1,1,132,0,0,1,1,1,220,134,0,0,1,1,1,250,1,0,1,3,1,0,1,4,1,0,1,1,132,0,0,1,1,1,220,132,0,0,1,95,1,112,1,115,1,95,1,99,1,0,1,171,1,171,1,0,1,1,1,0,1,3,1,0,1,1,1,0,1,4,1,0,1,17,229,0,0,229,0,0,204,0,0,1,95,1,112,1,115,1,95,1,115,1,48,1,0,1,171,1,0,1,4,1,0,1,14,1,0,1,1,1,0,1,1,1,0,1,1,134,0,0,1,95,1,112,1,115,1,95,1,115,1,49,1,0,1,171,1,0,1,4,1,0,1,12,1,0,1,1,1,0,1,1,1,0,1,1,134,0,0,1,95,1,112,1,115,1,95,1,115,1,50,1,0,1,95,1,112,1,115,1,95,1,115,1,51,1,0,1,95,1,112,1,115,1,95,1,115,1,52,1,0,1,112,1,115,1,95,1,51,1,95,1,48,1,0,1,50,1,46,1,48,1,46,1,49,1,49,1,54,1,50,1,54,1,46,1,48,136,0,0,1,1,139,0,0,1,20,1,1,1,252,1,0,1,16,147,0,0,1,64,1,0,1,0,1,5,1,124,1,16,1,0,1,12,132,0,0,1,4,134,0,0,1,72,1,198,1,0,1,63,1,0,1,63,131,0,0,1,1,1,0,1,0,1,48,1,80,1,0,1,0,1,113,1,81,1,0,1,0,1,114,1,82,1,0,1,0,1,115,1,83,1,0,1,0,1,116,1,84,1,0,1,0,1,245,1,85,1,62,1,170,1,170,1,171,144,0,0,1,65,1,112,1,0,1,0,1,62,1,128,1,0,1,0,1,63,1,64,1,0,1,0,1,190,1,170,1,170,1,171,1,63,1,42,1,170,1,171,1,63,1,192,1,0,1,0,1,59,1,128,1,128,1,129,1,63,131,0,0,1,191,131,0,0,1,63,1,128,1,0,1,0,1,67,1,127,131,0,0,1,9,1,80,1,12,1,32,1,17,1,16,1,0,1,86,1,0,1,0,1,9,1,0,1,0,1,32,1,19,1,64,1,8,1,16,1,0,1,176,131,0,0,1,4,1,0,1,96,1,21,1,96,1,27,1,18,1,0,1,18,1,0,1,0,1,149,1,0,1,0,1,96,1,33,1,48,1,39,1,18,1,0,1,18,132,0,0,1,9,1,96,1,42,1,96,1,48,1,18,1,0,1,18,133,0,0,1,32,1,54,1,64,1,56,1,16,1,0,1,82,1,0,1,0,1,64,132,0,0,1,48,1,60,1,196,1,0,1,82,133,0,0,1,96,1,63,1,96,1,69,1,86,1,0,1,86,133,0,0,1,96,1,75,1,96,1,81,1,18,1,0,1,18,133,0,0,1,96,1,87,1,96,1,93,1,18,1,0,1,18,133,0,0,1,96,1,99,1,96,1,105,1,18,1,0,1,18,133,0,0,1,80,1,111,1,0,1,0,1,34,133,0,0,1,16,1,32,1,96,1,1,1,31,1,31,1,254,1,136,1,0,1,0,1,64,1,0,1,116,1,0,1,0,1,128,131,0,0,1,108,1,194,1,0,1,0,1,16,1,200,1,7,1,0,1,8,1,0,1,198,1,198,1,0,1,34,1,255,1,255,1,0,1,200,1,7,1,0,1,7,1,0,1,198,1,198,1,0,1,34,1,255,1,255,1,0,1,168,1,136,1,4,1,3,1,0,1,198,1,108,1,196,1,165,1,6,1,255,1,15,1,16,1,56,1,112,1,1,1,159,1,31,1,254,1,136,1,128,1,0,1,64,1,0,1,200,1,7,1,0,1,7,1,24,1,192,1,192,1,0,1,225,1,7,1,7,1,0,1,200,1,8,1,0,1,130,1,0,1,177,1,177,1,198,1,110,1,16,1,6,1,8,1,116,1,0,1,0,1,128,131,0,0,1,198,1,194,1,0,1,0,1,16,1,76,1,64,133,0,0,1,27,1,226,1,0,1,0,1,5,1,200,1,7,1,0,1,6,1,0,1,198,1,192,1,0,1,161,1,0,1,255,1,0,1,76,1,71,1,0,1,5,1,0,1,205,1,205,1,108,1,193,1,6,1,5,1,10,1,76,1,137,1,0,1,8,1,0,1,109,1,108,1,177,1,128,1,5,1,255,1,10,1,0,1,134,1,0,1,8,1,0,1,107,1,203,1,203,1,224,1,8,1,0,1,0,1,152,1,24,1,97,1,1,1,15,1,31,1,255,1,248,1,0,1,0,1,64,1,0,1,184,1,24,1,97,1,1,1,15,1,31,1,255,1,199,1,0,1,0,1,64,1,0,1,16,1,24,1,97,1,1,1,15,1,31,1,254,1,63,1,0,1,0,1,64,1,0,1,48,1,24,1,1,1,1,1,15,1,31,1,254,1,63,1,0,1,0,1,64,1,0,1,0,1,72,1,0,1,1,1,6,1,27,1,198,1,97,1,192,1,0,1,0,1,10,1,20,1,8,1,0,1,0,1,4,1,27,1,198,1,27,1,224,1,0,1,5,1,1,1,4,1,131,1,6,1,5,1,0,1,111,1,109,1,198,1,161,1,8,1,10,1,5,1,44,1,23,1,5,1,6,1,2,1,27,1,192,1,108,1,224,1,0,1,6,1,5,1,45,1,47,1,5,1,8,1,0,1,0,1,177,1,177,1,161,1,6,1,253,1,5,1,200,1,3,1,0,1,5,1,0,1,176,1,27,1,198,1,139,1,5,1,253,1,253,1,188,1,79,1,5,1,6,1,0,1,0,1,198,1,196,1,193,1,6,1,0,1,255,1,189,1,143,1,5,1,6,1,0,1,0,1,108,1,197,1,129,1,6,1,255,1,255,1,200,1,15,1,0,1,5,1,0,1,51,1,78,1,0,1,225,1,5,1,5,1,0,1,200,1,8,131,0,0,1,37,1,208,1,0,1,239,1,5,1,8,1,0,1,184,1,72,1,0,1,1,1,0,1,37,1,208,1,195,1,207,1,5,1,6,1,255,1,188,1,33,1,8,1,8,1,0,1,198,1,198,1,195,1,193,1,0,1,0,1,255,1,16,1,64,1,0,1,1,1,31,1,31,1,254,1,136,1,0,1,0,1,64,1,0,1,200,1,11,131,0,0,1,98,1,177,1,0,1,160,1,0,1,255,1,0,1,200,1,7,1,0,1,1,1,0,1,108,1,192,1,0,1,225,1,0,1,1,1,0,1,200,1,7,131,0,0,1,177,1,192,1,192,1,235,1,0,1,3,1,1,1,200,1,14,131,0,0,1,27,1,140,1,140,1,235,1,0,1,2,1,0,1,200,1,1,131,0,0,1,18,1,18,1,0,1,240,131,0,0,1,88,1,16,133,0,0,1,108,1,226,1,0,1,0,1,128,1,200,1,7,1,0,1,5,1,0,1,201,1,108,1,0,1,225,131,0,0,1,200,1,1,131,0,0,1,190,1,190,1,0,1,240,1,5,1,4,1,0,1,0,1,16,133,0,0,1,108,1,226,131,0,0,1,200,1,14,1,0,1,0,1,4,1,252,1,108,1,252,1,235,1,5,1,0,1,4,1,200,1,1,131,0,0,1,195,1,195,1,0,1,240,131,0,0,1,88,1,16,133,0,0,1,108,1,226,1,0,1,0,1,128,1,112,1,7,1,0,1,2,1,0,1,21,1,108,1,27,1,225,1,0,1,0,1,3,1,200,1,15,1,0,1,0,1,16,1,166,1,205,1,0,1,242,1,2,1,2,1,0,1,76,1,20,1,1,1,1,1,16,1,27,1,27,1,198,1,226,1,0,1,0,1,128,1,200,1,3,1,0,1,1,1,16,1,109,1,108,1,198,1,203,1,0,1,1,1,254,1,144,1,8,1,0,1,33,1,159,1,31,1,246,1,136,1,0,1,0,1,192,1,0,1,168,1,16,1,1,1,0,1,16,1,0,1,0,1,67,1,194,1,0,1,0,1,9,1,200,1,7,1,0,1,0,1,16,1,108,1,192,1,0,1,225,1,1,1,0,1,0,1,200,1,7,1,0,1,4,1,16,1,192,1,192,1,0,1,225,131,0,0,1,200,1,7,1,0,1,0,1,24,1,108,1,98,1,98,1,139,1,2,1,1,1,0,1,200,1,7,1,0,1,0,1,24,1,177,1,190,1,180,1,171,1,2,1,2,1,0,1,200,1,3,1,0,1,1,1,24,1,108,1,196,1,0,1,225,1,2,1,2,1,0,1,200,1,15,1,0,1,6,1,24,1,114,1,130,1,0,1,225,1,2,1,2,1,0,1,200,1,8,1,0,1,0,1,26,1,108,1,177,1,0,1,224,1,1,1,6,1,0,1,200,1,1,1,0,1,1,1,26,1,108,1,108,1,0,1,160,1,6,1,252,1,0,1,200,1,7,1,0,1,0,1,24,1,198,1,98,1,180,1,171,1,2,1,3,1,0,1,200,1,7,1,0,1,0,1,24,1,27,1,190,1,180,1,171,1,6,1,4,1,0,1,200,1,7,1,0,1,0,1,24,1,198,1,98,1,180,1,171,1,6,1,5,1,0,1,200,1,7,1,0,1,0,1,24,1,177,1,190,1,180,1,171,1,1,1,6,1,0,1,200,1,7,1,0,1,0,1,24,1,108,1,98,1,180,1,171,1,1,1,7,1,0,1,200,1,7,1,0,1,0,1,24,1,27,1,190,1,180,1,171,1,0,1,8,1,0,1,200,1,7,1,0,1,4,1,24,1,101,1,108,1,0,1,162,1,0,1,253,1,0,1,20,1,14,1,0,1,6,1,0,1,108,1,140,1,108,1,129,1,5,1,6,1,11,1,12,1,135,1,1,1,10,1,0,1,177,1,180,1,177,1,129,1,5,1,5,1,15,1,200,1,13,131,0,0,1,108,1,114,1,114,1,139,1,5,1,1,1,0,1,76,1,39,1,0,1,1,1,0,1,98,1,98,1,177,1,1,1,12,1,12,1,9,1,172,1,23,1,3,1,11,1,0,1,108,1,180,1,2,1,129,1,5,1,4,1,12,1,200,1,7,1,0,1,12,1,0,1,177,1,190,1,200,1,171,1,5,1,2,1,0,1,172,1,39,1,3,1,9,1,0,1,190,1,190,1,0,1,193,1,5,1,5,1,12,1,173,1,72,1,3,1,0,1,0,1,190,1,190,1,1,1,144,1,5,1,13,1,12,1,101,1,17,1,6,1,0,1,0,1,190,1,190,1,182,1,176,1,2,1,13,1,9,1,8,1,68,1,1,1,0,1,0,1,190,1,190,1,108,1,176,1,5,1,13,1,0,1,200,1,7,1,0,1,2,1,0,1,198,1,98,1,180,1,171,1,5,1,3,1,12,1,64,1,72,1,0,1,9,1,0,1,198,1,108,1,108,1,161,1,0,1,252,1,0,1,200,1,7,1,0,1,2,1,0,1,205,1,177,1,180,1,235,1,11,1,5,1,2,1,200,1,7,1,0,1,2,1,0,1,101,1,198,1,180,1,235,1,10,1,5,1,2,1,168,1,19,1,0,1,1,1,0,1,111,1,109,1,66,1,128,1,9,1,254,1,14,1,58,1,24,1,0,1,1,1,0,1,27,1,108,1,108,1,225,1,1,1,1,1,0,1,200,1,7,1,0,1,5,1,0,1,18,1,198,1,180,1,235,1,6,1,5,1,2,1,172,1,20,131,0,0,1,108,1,177,1,128,1,129,1,0,1,14,1,15,1,200,1,8,1,0,1,5,1,0,1,27,1,198,1,198,1,236,1,3,1,0,1,1,1,168,1,23,1,1,1,2,1,0,1,27,1,192,1,192,1,193,1,4,1,4,1,11,1,200,1,7,1,0,1,4,1,0,1,177,1,98,1,180,1,171,1,1,1,7,1,5,1,200,1,7,1,0,1,4,1,0,1,108,1,190,1,180,1,171,1,6,1,8,1,4,1,168,1,39,1,1,1,5,1,0,1,101,1,108,1,128,1,130,1,4,1,253,1,11,1,200,1,7,1,0,1,1,1,0,1,27,1,108,1,99,1,172,1,3,1,253,1,1,1,168,1,23,1,0,1,1,1,0,1,192,1,108,1,131,1,193,1,1,1,8,1,15,1,200,1,1,131,0,0,1,27,1,27,1,108,1,235,1,4,1,5,1,0,1,200,1,13,131,0,0,1,108,1,177,1,240,1,235,1,0,1,8,1,1,1,168,1,23,1,0,1,1,1,0,1,180,1,20,1,67,1,225,1,3,1,0,1,15,1,200,1,7,1,0,1,1,1,0,1,192,1,27,1,192,1,235,1,2,1,2,1,1,1,200,1,13,131,0,0,1,108,1,240,1,240,1,235,1,0,1,5,1,1,1,200,1,7,1,0,1,1,1,0,1,20,1,192,1,0,1,225,1,0,1,7,1,0,1,20,1,16,133,0,0,1,182,1,226,1,0,1,0,1,1,1,168,1,33,131,0,0,1,108,1,108,1,1,1,194,1,0,1,1,1,255,1,200,1,1,131,0,0,1,177,1,108,1,0,1,225,131,0,0,1,52,1,16,1,0,1,0,1,1,1,0,1,0,1,108,1,226,131,0,0,1,201,1,8,1,128,1,0,1,4,1,108,1,27,1,0,1,161,1,0,1,254,1,0,1,200,1,1,1,0,1,0,1,4,1,108,1,27,1,0,1,161,1,0,1,254,1,0,1,168,1,16,133,0,0,1,128,1,194,1,0,1,0,1,9,1,76,1,16,133,0,0,1,108,1,226,131,0,0,1,201,1,7,1,128,1,0,1,0,1,192,1,108,1,0,1,225,1,1,149,0,0,1,2,132,255,0,138,0,0,1,4,1,16,1,16,1,42,1,17,1,1,1,0,1,0,1,2,1,72,1,0,1,0,1,1,1,200,135,0,0,1,36,134,0,0,1,1,1,192,138,0,0,1,1,1,152,131,0,0,1,28,1,0,1,0,1,1,1,139,1,255,1,254,1,3,132,0,0,1,2,131,0,0,1,28,134,0,0,1,1,1,132,131,0,0,1,68,1,0,1,2,131,0,0,1,13,133,0,0,1,76,131,0,0,1,92,1,0,1,0,1,1,1,44,1,0,1,2,1,0,1,13,1,0,1,4,132,0,0,1,1,1,52,1,0,1,0,1,1,1,68,1,95,1,118,1,115,1,95,1,99,1,0,1,171,1,171,1,0,1,1,1,0,1,3,1,0,1,1,1,0,1,4,1,0,1,13,229,0,0,229,0,0,140,0,0,1,95,1,118,1,115,1,105,1,95,1,99,1,0,1,171,1,0,1,1,1,0,1,3,1,0,1,1,1,0,1,4,1,0,1,4,198,0,0,1,118,1,115,1,95,1,51,1,95,1,48,1,0,1,50,1,46,1,48,1,46,1,49,1,49,1,54,1,50,1,54,1,46,1,48,1,0,1,171,134,0,0,1,1,1,200,1,0,1,81,1,0,1,9,138,0,0,1,72,1,198,131,0,0,1,1,131,0,0,1,9,131,0,0,1,9,1,0,1,0,1,2,1,144,1,0,1,16,1,0,1,5,1,0,1,0,1,80,1,6,1,0,1,0,1,48,1,7,1,0,1,0,1,96,1,8,1,0,1,0,1,112,1,9,1,0,1,12,1,0,1,10,1,0,1,13,1,0,1,11,1,0,1,14,1,0,1,12,1,0,1,63,1,0,1,13,1,0,1,0,1,48,1,80,1,0,1,1,1,113,1,81,1,0,1,2,1,114,1,82,1,0,1,3,1,115,1,83,1,0,1,4,1,116,1,84,1,0,1,5,1,245,1,85,1,0,1,0,1,16,1,32,1,0,1,0,1,16,1,29,1,0,1,0,1,16,1,30,1,0,1,0,1,16,1,31,1,0,1,0,1,16,1,28,131,0,0,1,33,131,0,0,1,34,131,0,0,1,35,1,0,1,0,1,16,1,36,1,245,1,85,1,96,1,5,1,48,1,11,1,18,1,3,1,18,1,0,1,112,1,21,132,0,0,1,96,1,14,1,194,1,0,1,18,133,0,0,1,32,1,20,1,0,1,0,1,18,1,0,1,196,133,0,0,1,96,1,22,1,96,1,28,1,18,1,0,1,18,133,0,0,1,48,1,34,1,0,1,0,1,34,133,0,0,1,5,1,248,1,96,131,0,0,1,6,1,136,132,0,0,1,5,1,248,1,16,131,0,0,1,15,1,200,132,0,0,1,5,1,248,1,80,131,0,0,1,1,1,209,132,0,0,1,5,1,248,1,64,131,0,0,1,1,1,209,132,0,0,1,5,1,248,1,32,131,0,0,1,1,1,209,132,0,0,1,5,1,248,1,48,131,0,0,1,6,1,136,132,0,0,1,5,1,248,1,112,131,0,0,1,6,1,136,132,0,0,1,5,1,248,1,144,131,0,0,1,6,1,136,132,0,0,1,5,1,248,132,0,0,1,6,1,136,132,0,0,1,200,1,15,131,0,0,1,27,1,0,1,0,1,225,1,6,1,0,1,0,1,200,1,15,131,0,0,1,198,1,0,1,0,1,235,1,6,1,9,1,0,1,200,1,15,131,0,0,1,177,1,148,1,148,1,235,1,6,1,7,1,0,1,200,1,15,131,0,0,1,108,1,248,1,148,1,235,1,6,1,3,1,0,1,200,1,1,1,128,1,62,1,0,1,233,1,167,1,0,1,175,1,0,1,13,1,0,1,200,1,2,1,128,1,62,1,0,1,233,1,167,1,0,1,175,1,0,1,14,1,0,1,200,1,4,1,128,1,62,1,0,1,233,1,167,1,0,1,175,1,0,1,15,1,0,1,200,1,8,1,128,1,62,1,0,1,233,1,167,1,0,1,175,1,0,1,16,1,0,1,200,1,7,1,0,1,6,1,0,1,177,1,180,1,0,1,225,1,5,1,9,1,0,1,200,1,7,1,0,1,8,1,0,1,177,1,180,1,0,1,225,1,4,1,9,1,0,1,200,1,7,1,0,1,9,1,0,1,177,1,180,1,0,1,225,1,2,1,9,1,0,1,200,1,7,1,0,1,2,1,0,1,108,1,180,1,192,1,235,1,2,1,7,1,9,1,200,1,7,1,0,1,4,1,0,1,108,1,180,1,192,1,235,1,4,1,7,1,8,1,200,1,7,1,0,1,5,1,0,1,108,1,180,1,192,1,235,1,5,1,7,1,6,1,200,1,7,1,128,1,4,1,2,1,20,1,192,1,0,1,160,1,0,1,12,1,0,1,200,1,7,1,128,1,1,1,0,1,27,1,192,1,180,1,235,1,5,1,3,1,5,1,200,1,7,1,128,1,2,1,0,1,27,1,192,1,180,1,235,1,4,1,3,1,4,1,200,1,7,1,128,1,3,1,0,1,27,1,192,1,180,1,235,1,2,1,3,1,2,1,200,1,3,1,128,1,0,1,0,1,176,1,176,1,0,1,226,1,1,1,1,1,0,1,200,1,1,1,128,1,5,1,0,1,233,1,167,1,0,1,175,131,0,0,1,200,1,2,1,128,1,5,1,0,1,233,1,167,1,0,1,175,1,0,1,1,1,0,1,200,1,4,1,128,1,5,1,0,1,233,1,167,1,0,1,175,1,0,1,2,1,0,1,200,1,8,1,128,1,5,1,0,1,233,1,167,1,0,1,175,1,0,1,3,148,0,0,1,1,132,255,0,131,0,0,1,1,134,0,0,1,8,1,88,1,16,1,42,1,17,131,0,0,1,2,1,156,1,0,1,0,1,5,1,188,135,0,0,1,36,1,0,1,0,1,2,1,60,1,0,1,0,1,2,1,100,138,0,0,1,2,1,20,131,0,0,1,28,1,0,1,0,1,2,1,8,1,255,1,255,1,3,132,0,0,1,6,131,0,0,1,28,134,0,0,1,2,1,1,131,0,0,1,148,1,0,1,2,131,0,0,1,17,133,0,0,1,156,131,0,0,1,172,1,0,1,0,1,1,1,188,1,0,1,3,131,0,0,1,1,132,0,0,1,1,1,196,134,0,0,1,1,1,212,1,0,1,3,1,0,1,1,1,0,1,1,132,0,0,1,1,1,220,134,0,0,1,1,1,236,1,0,1,3,1,0,1,2,1,0,1,1,132,0,0,1,1,1,220,134,0,0,1,1,1,243,1,0,1,3,1,0,1,3,1,0,1,1,132,0,0,1,1,1,220,134,0,0,1,1,1,250,1,0,1,3,1,0,1,4,1,0,1,1,132,0,0,1,1,1,220,132,0,0,1,95,1,112,1,115,1,95,1,99,1,0,1,171,1,171,1,0,1,1,1,0,1,3,1,0,1,1,1,0,1,4,1,0,1,17,229,0,0,229,0,0,204,0,0,1,95,1,112,1,115,1,95,1,115,1,48,1,0,1,171,1,0,1,4,1,0,1,14,1,0,1,1,1,0,1,1,1,0,1,1,134,0,0,1,95,1,112,1,115,1,95,1,115,1,49,1,0,1,171,1,0,1,4,1,0,1,12,1,0,1,1,1,0,1,1,1,0,1,1,134,0,0,1,95,1,112,1,115,1,95,1,115,1,50,1,0,1,95,1,112,1,115,1,95,1,115,1,51,1,0,1,95,1,112,1,115,1,95,1,115,1,52,1,0,1,112,1,115,1,95,1,51,1,95,1,48,1,0,1,50,1,46,1,48,1,46,1,49,1,49,1,54,1,50,1,54,1,46,1,48,136,0,0,1,1,139,0,0,1,20,1,1,1,252,1,0,1,16,147,0,0,1,64,1,0,1,0,1,5,1,124,1,16,1,0,1,12,132,0,0,1,4,134,0,0,1,72,1,198,1,0,1,63,1,0,1,63,131,0,0,1,1,1,0,1,0,1,48,1,80,1,0,1,0,1,113,1,81,1,0,1,0,1,114,1,82,1,0,1,0,1,115,1,83,1,0,1,0,1,116,1,84,1,0,1,0,1,245,1,85,1,62,1,170,1,170,1,171,144,0,0,1,65,1,112,1,0,1,0,1,62,1,128,1,0,1,0,1,63,1,64,1,0,1,0,1,190,1,170,1,170,1,171,1,63,1,42,1,170,1,171,1,63,1,192,1,0,1,0,1,59,1,128,1,128,1,129,1,63,131,0,0,1,191,131,0,0,1,63,1,128,1,0,1,0,1,67,1,127,131,0,0,1,9,1,80,1,12,1,32,1,17,1,16,1,0,1,86,1,0,1,0,1,9,1,0,1,0,1,32,1,19,1,64,1,8,1,16,1,0,1,176,131,0,0,1,4,1,0,1,96,1,21,1,96,1,27,1,18,1,0,1,18,1,0,1,0,1,149,1,0,1,0,1,96,1,33,1,48,1,39,1,18,1,0,1,18,132,0,0,1,9,1,96,1,42,1,96,1,48,1,18,1,0,1,18,133,0,0,1,32,1,54,1,64,1,56,1,16,1,0,1,82,1,0,1,0,1,64,132,0,0,1,48,1,60,1,196,1,0,1,82,133,0,0,1,96,1,63,1,96,1,69,1,86,1,0,1,86,133,0,0,1,96,1,75,1,96,1,81,1,18,1,0,1,18,133,0,0,1,96,1,87,1,96,1,93,1,18,1,0,1,18,133,0,0,1,96,1,99,1,96,1,105,1,18,1,0,1,18,133,0,0,1,80,1,111,1,0,1,0,1,34,133,0,0,1,16,1,32,1,96,1,1,1,31,1,31,1,254,1,136,1,0,1,0,1,64,1,0,1,116,1,0,1,0,1,128,131,0,0,1,108,1,194,1,0,1,0,1,16,1,200,1,7,1,0,1,8,1,0,1,198,1,198,1,0,1,34,1,255,1,255,1,0,1,200,1,7,1,0,1,7,1,0,1,198,1,198,1,0,1,34,1,255,1,255,1,0,1,168,1,136,1,4,1,3,1,0,1,198,1,108,1,196,1,165,1,6,1,255,1,15,1,16,1,56,1,112,1,1,1,159,1,31,1,254,1,136,1,128,1,0,1,64,1,0,1,200,1,7,1,0,1,7,1,24,1,192,1,192,1,0,1,225,1,7,1,7,1,0,1,200,1,8,1,0,1,130,1,0,1,177,1,177,1,198,1,110,1,16,1,6,1,8,1,116,1,0,1,0,1,128,131,0,0,1,198,1,194,1,0,1,0,1,16,1,76,1,64,133,0,0,1,27,1,226,1,0,1,0,1,5,1,200,1,7,1,0,1,6,1,0,1,198,1,192,1,0,1,161,1,0,1,255,1,0,1,76,1,71,1,0,1,5,1,0,1,205,1,205,1,108,1,193,1,6,1,5,1,10,1,76,1,137,1,0,1,8,1,0,1,109,1,108,1,177,1,128,1,5,1,255,1,10,1,0,1,134,1,0,1,8,1,0,1,107,1,203,1,203,1,224,1,8,1,0,1,0,1,152,1,24,1,97,1,1,1,15,1,31,1,255,1,248,1,0,1,0,1,64,1,0,1,184,1,24,1,97,1,1,1,15,1,31,1,255,1,199,1,0,1,0,1,64,1,0,1,16,1,24,1,97,1,1,1,15,1,31,1,254,1,63,1,0,1,0,1,64,1,0,1,48,1,24,1,1,1,1,1,15,1,31,1,254,1,63,1,0,1,0,1,64,1,0,1,0,1,72,1,0,1,1,1,6,1,27,1,198,1,97,1,192,1,0,1,0,1,10,1,20,1,8,1,0,1,0,1,4,1,27,1,198,1,27,1,224,1,0,1,5,1,1,1,4,1,131,1,6,1,5,1,0,1,111,1,109,1,198,1,161,1,8,1,10,1,5,1,44,1,23,1,5,1,6,1,2,1,27,1,192,1,108,1,224,1,0,1,6,1,5,1,45,1,47,1,5,1,8,1,0,1,0,1,177,1,177,1,161,1,6,1,253,1,5,1,200,1,3,1,0,1,5,1,0,1,176,1,27,1,198,1,139,1,5,1,253,1,253,1,188,1,79,1,5,1,6,1,0,1,0,1,198,1,196,1,193,1,6,1,0,1,255,1,189,1,143,1,5,1,6,1,0,1,0,1,108,1,197,1,129,1,6,1,255,1,255,1,200,1,15,1,0,1,5,1,0,1,51,1,78,1,0,1,225,1,5,1,5,1,0,1,200,1,8,131,0,0,1,37,1,208,1,0,1,239,1,5,1,8,1,0,1,184,1,72,1,0,1,1,1,0,1,37,1,208,1,195,1,207,1,5,1,6,1,255,1,188,1,33,1,8,1,8,1,0,1,198,1,198,1,195,1,193,1,0,1,0,1,255,1,16,1,64,1,0,1,1,1,31,1,31,1,254,1,136,1,0,1,0,1,64,1,0,1,200,1,11,131,0,0,1,98,1,177,1,0,1,160,1,0,1,255,1,0,1,200,1,7,1,0,1,1,1,0,1,108,1,192,1,0,1,225,1,0,1,1,1,0,1,200,1,7,131,0,0,1,177,1,192,1,192,1,235,1,0,1,3,1,1,1,200,1,14,131,0,0,1,27,1,140,1,140,1,235,1,0,1,2,1,0,1,200,1,1,131,0,0,1,18,1,18,1,0,1,240,131,0,0,1,88,1,16,133,0,0,1,108,1,226,1,0,1,0,1,128,1,200,1,7,1,0,1,5,1,0,1,201,1,108,1,0,1,225,131,0,0,1,200,1,1,131,0,0,1,190,1,190,1,0,1,240,1,5,1,4,1,0,1,0,1,16,133,0,0,1,108,1,226,131,0,0,1,200,1,14,1,0,1,0,1,4,1,252,1,108,1,252,1,235,1,5,1,0,1,4,1,200,1,1,131,0,0,1,195,1,195,1,0,1,240,131,0,0,1,88,1,16,133,0,0,1,108,1,226,1,0,1,0,1,128,1,112,1,7,1,0,1,2,1,0,1,21,1,108,1,27,1,225,1,0,1,0,1,3,1,200,1,15,1,0,1,0,1,16,1,166,1,205,1,0,1,242,1,2,1,2,1,0,1,76,1,20,1,1,1,1,1,16,1,27,1,27,1,198,1,226,1,0,1,0,1,128,1,200,1,3,1,0,1,1,1,16,1,109,1,108,1,198,1,203,1,0,1,1,1,254,1,144,1,8,1,0,1,33,1,159,1,31,1,246,1,136,1,0,1,0,1,192,1,0,1,168,1,16,1,1,1,0,1,16,1,0,1,0,1,67,1,194,1,0,1,0,1,9,1,200,1,7,1,0,1,0,1,16,1,108,1,192,1,0,1,225,1,1,1,0,1,0,1,200,1,7,1,0,1,4,1,16,1,192,1,192,1,0,1,225,131,0,0,1,200,1,7,1,0,1,0,1,24,1,108,1,98,1,98,1,139,1,2,1,1,1,0,1,200,1,7,1,0,1,0,1,24,1,177,1,190,1,180,1,171,1,2,1,2,1,0,1,200,1,3,1,0,1,1,1,24,1,108,1,196,1,0,1,225,1,2,1,2,1,0,1,200,1,15,1,0,1,6,1,24,1,114,1,130,1,0,1,225,1,2,1,2,1,0,1,200,1,8,1,0,1,0,1,26,1,108,1,177,1,0,1,224,1,1,1,6,1,0,1,200,1,1,1,0,1,1,1,26,1,108,1,108,1,0,1,160,1,6,1,252,1,0,1,200,1,7,1,0,1,0,1,24,1,198,1,98,1,180,1,171,1,2,1,3,1,0,1,200,1,7,1,0,1,0,1,24,1,27,1,190,1,180,1,171,1,6,1,4,1,0,1,200,1,7,1,0,1,0,1,24,1,198,1,98,1,180,1,171,1,6,1,5,1,0,1,200,1,7,1,0,1,0,1,24,1,177,1,190,1,180,1,171,1,1,1,6,1,0,1,200,1,7,1,0,1,0,1,24,1,108,1,98,1,180,1,171,1,1,1,7,1,0,1,200,1,7,1,0,1,0,1,24,1,27,1,190,1,180,1,171,1,0,1,8,1,0,1,200,1,7,1,0,1,4,1,24,1,101,1,108,1,0,1,162,1,0,1,253,1,0,1,20,1,14,1,0,1,6,1,0,1,108,1,140,1,108,1,129,1,5,1,6,1,11,1,12,1,135,1,1,1,10,1,0,1,177,1,180,1,177,1,129,1,5,1,5,1,15,1,200,1,13,131,0,0,1,108,1,114,1,114,1,139,1,5,1,1,1,0,1,76,1,39,1,0,1,1,1,0,1,98,1,98,1,177,1,1,1,12,1,12,1,9,1,172,1,23,1,3,1,11,1,0,1,108,1,180,1,2,1,129,1,5,1,4,1,12,1,200,1,7,1,0,1,12,1,0,1,177,1,190,1,200,1,171,1,5,1,2,1,0,1,172,1,39,1,3,1,9,1,0,1,190,1,190,1,0,1,193,1,5,1,5,1,12,1,173,1,72,1,3,1,0,1,0,1,190,1,190,1,1,1,144,1,5,1,13,1,12,1,101,1,17,1,6,1,0,1,0,1,190,1,190,1,182,1,176,1,2,1,13,1,9,1,8,1,68,1,1,1,0,1,0,1,190,1,190,1,108,1,176,1,5,1,13,1,0,1,200,1,7,1,0,1,2,1,0,1,198,1,98,1,180,1,171,1,5,1,3,1,12,1,64,1,72,1,0,1,9,1,0,1,198,1,108,1,108,1,161,1,0,1,252,1,0,1,200,1,7,1,0,1,2,1,0,1,205,1,177,1,180,1,235,1,11,1,5,1,2,1,200,1,7,1,0,1,2,1,0,1,101,1,198,1,180,1,235,1,10,1,5,1,2,1,168,1,19,1,0,1,1,1,0,1,111,1,109,1,66,1,128,1,9,1,254,1,14,1,58,1,24,1,0,1,1,1,0,1,27,1,108,1,108,1,225,1,1,1,1,1,0,1,200,1,7,1,0,1,5,1,0,1,18,1,198,1,180,1,235,1,6,1,5,1,2,1,172,1,20,131,0,0,1,108,1,177,1,128,1,129,1,0,1,14,1,15,1,200,1,8,1,0,1,5,1,0,1,27,1,198,1,198,1,236,1,3,1,0,1,1,1,168,1,23,1,1,1,2,1,0,1,27,1,192,1,192,1,193,1,4,1,4,1,11,1,200,1,7,1,0,1,4,1,0,1,177,1,98,1,180,1,171,1,1,1,7,1,5,1,200,1,7,1,0,1,4,1,0,1,108,1,190,1,180,1,171,1,6,1,8,1,4,1,168,1,39,1,1,1,5,1,0,1,101,1,108,1,128,1,130,1,4,1,253,1,11,1,200,1,7,1,0,1,1,1,0,1,27,1,108,1,99,1,172,1,3,1,253,1,1,1,168,1,23,1,0,1,1,1,0,1,192,1,108,1,131,1,193,1,1,1,8,1,15,1,200,1,1,131,0,0,1,27,1,27,1,108,1,235,1,4,1,5,1,0,1,200,1,13,131,0,0,1,108,1,177,1,240,1,235,1,0,1,8,1,1,1,168,1,23,1,0,1,1,1,0,1,180,1,20,1,67,1,225,1,3,1,0,1,15,1,200,1,7,1,0,1,1,1,0,1,192,1,27,1,192,1,235,1,2,1,2,1,1,1,200,1,13,131,0,0,1,108,1,240,1,240,1,235,1,0,1,5,1,1,1,200,1,7,1,0,1,1,1,0,1,20,1,192,1,0,1,225,1,0,1,7,1,0,1,20,1,16,133,0,0,1,182,1,226,1,0,1,0,1,1,1,168,1,33,131,0,0,1,108,1,108,1,1,1,194,1,0,1,1,1,255,1,200,1,1,131,0,0,1,177,1,108,1,0,1,225,131,0,0,1,52,1,16,1,0,1,0,1,1,1,0,1,0,1,108,1,226,131,0,0,1,201,1,8,1,128,1,0,1,4,1,108,1,27,1,0,1,161,1,0,1,254,1,0,1,200,1,1,1,0,1,0,1,4,1,108,1,27,1,0,1,161,1,0,1,254,1,0,1,168,1,16,133,0,0,1,128,1,194,1,0,1,0,1,9,1,76,1,16,133,0,0,1,108,1,226,131,0,0,1,201,1,7,1,128,1,0,1,0,1,192,1,108,1,0,1,225,1,1,149,0,0,1,1,132,255,0,138,0,0,1,19,1,72,1,16,1,42,1,17,1,1,1,0,1,0,1,15,1,192,1,0,1,0,1,3,1,136,135,0,0,1,36,1,0,1,0,1,15,131,0,0,1,15,1,40,138,0,0,1,14,1,216,131,0,0,1,28,1,0,1,0,1,14,1,203,1,255,1,254,1,3,132,0,0,1,2,131,0,0,1,28,134,0,0,1,14,1,196,131,0,0,1,68,1,0,1,2,131,0,0,1,13,133,0,0,1,76,131,0,0,1,92,1,0,1,0,1,1,1,44,1,0,1,2,1,0,1,13,1,0,1,216,132,0,0,1,1,1,52,1,0,1,0,1,1,1,68,1,95,1,118,1,115,1,95,1,99,1,0,1,171,1,171,1,0,1,1,1,0,1,3,1,0,1,1,1,0,1,4,1,0,1,13,229,0,0,229,0,0,140,0,0,1,95,1,118,1,115,1,98,1,95,1,99,1,0,1,171,1,0,1,1,1,0,1,3,1,0,1,1,1,0,1,4,1,0,1,216,229,0,0,229,0,0,229,0,0,229,0,0,229,0,0,229,0,0,229,0,0,229,0,0,229,0,0,229,0,0,229,0,0,229,0,0,229,0,0,229,0,0,229,0,0,229,0,0,229,0,0,229,0,0,229,0,0,229,0,0,229,0,0,229,0,0,229,0,0,229,0,0,229,0,0,229,0,0,229,0,0,229,0,0,229,0,0,229,0,0,229,0,0,229,0,0,229,0,0,229,0,0,156,0,0,1,118,1,115,1,95,1,51,1,95,1,48,1,0,1,50,1,46,1,48,1,46,1,49,1,49,1,54,1,50,1,54,1,46,1,48,1,0,1,171,135,0,0,1,1,139,0,0,1,20,1,0,1,252,1,0,1,16,147,0,0,1,64,1,0,1,0,1,3,1,72,1,0,1,81,1,0,1,11,138,0,0,1,72,1,198,131,0,0,1,1,131,0,0,1,7,131,0,0,1,15,1,0,1,0,1,2,1,144,1,0,1,16,1,0,1,7,1,0,1,0,1,80,1,8,1,0,1,0,1,48,1,9,1,0,1,0,1,96,1,10,1,0,1,0,1,112,1,11,1,0,1,0,1,16,1,12,1,0,1,48,1,32,1,13,1,0,1,0,1,48,1,80,1,0,1,1,1,113,1,81,1,0,1,4,1,114,1,82,1,0,1,7,1,115,1,83,1,0,1,10,1,116,1,84,1,0,1,11,1,245,1,85,1,0,1,0,1,16,1,54,131,0,0,1,55,131,0,0,1,56,1,0,1,0,1,16,1,57,131,0,0,1,58,131,0,0,1,59,1,0,1,0,1,16,1,60,131,0,0,1,61,131,0,0,1,62,1,0,1,0,1,16,1,63,1,0,1,0,1,16,1,64,131,0,0,1,65,131,0,0,1,66,131,0,0,1,67,1,0,1,0,1,16,1,68,180,0,0,1,63,1,128,1,0,1,0,1,64,1,64,134,0,0,1,245,1,85,1,96,1,7,1,16,1,13,1,18,1,3,1,18,1,0,1,16,1,1,132,0,0,1,96,1,14,1,194,1,0,1,18,133,0,0,1,96,1,20,1,96,1,26,1,18,1,0,1,18,133,0,0,1,96,1,32,1,16,1,38,1,18,1,0,1,18,135,0,0,1,96,1,39,1,196,1,0,1,18,133,0,0,1,96,1,45,1,96,1,51,1,18,1,0,1,18,133,0,0,1,96,1,57,1,96,1,63,1,18,1,0,1,34,131,0,0,1,5,1,248,1,16,131,0,0,1,4,1,88,132,0,0,1,5,1,248,1,64,131,0,0,1,15,1,200,132,0,0,1,5,1,248,1,80,131,0,0,1,14,1,136,132,0,0,1,5,1,248,1,144,131,0,0,1,14,1,136,132,0,0,1,5,1,248,1,32,131,0,0,1,14,1,136,132,0,0,1,5,1,248,1,96,131,0,0,1,6,1,136,132,0,0,1,5,1,248,132,0,0,1,6,1,136,132,0,0,1,200,1,15,1,0,1,8,1,0,1,0,1,198,1,0,1,161,1,0,1,255,1,0,1,92,1,8,1,0,1,10,1,0,1,177,1,27,1,27,1,161,1,1,1,7,1,8,1,200,1,15,1,0,1,0,1,160,1,27,1,136,1,0,1,161,1,6,1,13,1,0,1,200,1,15,1,0,1,3,1,160,1,27,1,136,1,0,1,161,1,6,1,14,1,0,1,92,1,15,1,0,1,7,1,160,1,27,1,136,1,198,1,161,1,6,1,15,1,8,1,200,1,15,1,0,1,7,1,160,1,198,1,136,1,0,1,171,1,6,1,15,1,7,1,200,1,15,1,0,1,3,1,160,1,198,1,136,1,0,1,171,1,6,1,14,1,3,1,200,1,15,1,0,1,0,1,160,1,198,1,136,1,0,1,171,1,6,1,13,1,0,1,92,1,2,1,0,1,11,1,0,1,177,1,27,1,177,1,161,1,1,1,5,1,8,1,200,1,15,1,0,1,0,1,160,1,177,1,52,1,148,1,171,1,6,1,13,1,0,1,200,1,15,1,0,1,3,1,160,1,177,1,52,1,148,1,171,1,6,1,14,1,3,1,200,1,15,1,0,1,7,1,160,1,177,1,52,1,148,1,171,1,6,1,15,1,7,1,92,1,8,1,0,1,11,1,0,1,177,1,27,1,108,1,161,1,1,1,4,1,8,1,200,1,15,1,0,1,7,1,160,1,108,1,208,1,148,1,171,1,6,1,15,1,7,1,200,1,15,1,0,1,3,1,160,1,108,1,255,1,143,1,171,1,6,1,14,1,3,1,200,1,15,1,0,1,8,1,160,1,108,1,208,1,148,1,171,1,6,1,13,1,0,1,200,1,1,1,0,1,6,1,0,1,170,1,233,1,0,1,239,1,8,1,1,1,0,1,200,1,2,1,0,1,6,1,0,1,248,1,233,1,0,1,239,1,3,1,1,1,0,1,200,1,4,1,0,1,6,1,0,1,170,1,233,1,0,1,239,1,7,1,1,1,0,1,200,1,1,1,0,1,10,1,0,1,190,1,190,1,0,1,176,1,6,1,6,1,0,1,200,1,4,1,0,1,10,1,0,1,190,1,190,1,0,1,176,1,6,1,7,1,0,1,20,1,17,1,0,1,11,1,0,1,190,1,190,1,177,1,176,1,6,1,5,1,1,1,168,1,36,1,10,1,11,1,0,1,190,1,190,1,0,1,144,1,6,1,4,1,6,1,200,1,3,1,128,1,62,1,0,1,110,1,179,1,0,1,224,1,11,1,11,1,0,1,200,1,12,1,128,1,62,1,0,1,236,1,49,1,0,1,224,1,10,1,10,1,0,1,200,1,2,131,0,0,1,191,1,190,1,0,1,240,1,8,1,2,1,0,1,200,1,4,131,0,0,1,195,1,190,1,0,1,240,1,3,1,2,1,0,1,200,1,8,131,0,0,1,191,1,190,1,0,1,240,1,7,1,2,1,0,1,200,1,1,1,0,1,2,1,0,1,191,1,190,1,0,1,240,1,8,1,9,1,0,1,200,1,2,1,0,1,2,1,0,1,195,1,190,1,0,1,240,1,3,1,9,1,0,1,200,1,4,1,0,1,2,1,0,1,191,1,190,1,0,1,240,1,7,1,9,1,0,1,200,1,1,1,0,1,3,1,0,1,191,1,190,1,0,1,240,1,8,1,5,1,0,1,20,1,18,1,0,1,3,1,0,1,195,1,190,1,177,1,240,1,3,1,5,1,1,1,168,1,20,1,1,1,3,1,0,1,191,1,190,1,0,1,208,1,7,1,5,1,8,1,20,1,17,1,0,1,5,1,0,1,190,1,190,1,177,1,176,1,6,1,8,1,1,1,168,1,66,1,1,1,5,1,0,1,190,1,190,1,0,1,144,1,6,1,10,1,10,1,20,1,20,1,0,1,5,1,0,1,190,1,190,1,177,1,176,1,6,1,11,1,1,1,200,1,15,1,0,1,6,1,0,1,176,1,177,1,166,1,108,1,255,1,1,1,6,1,168,1,130,1,1,1,1,1,0,1,85,1,62,1,0,1,143,1,6,1,9,1,11,1,200,1,13,1,0,1,1,1,0,1,240,1,4,1,0,1,224,1,5,1,1,1,0,1,200,1,3,1,128,1,0,1,0,1,176,1,176,1,0,1,226,1,4,1,4,1,0,1,200,1,1,1,128,1,1,1,0,1,190,1,190,1,0,1,176,1,3,1,8,1,0,1,200,1,2,1,128,1,1,1,0,1,190,1,190,1,0,1,176,1,3,1,9,1,0,1,200,1,4,1,128,1,1,1,0,1,190,1,190,1,0,1,176,1,3,1,10,1,0,1,200,1,1,1,128,1,2,1,0,1,190,1,190,1,0,1,176,1,2,1,8,1,0,1,200,1,2,1,128,1,2,1,0,1,190,1,190,1,0,1,176,1,2,1,9,1,0,1,200,1,4,1,128,1,2,1,0,1,190,1,190,1,0,1,176,1,2,1,10,1,0,1,200,1,1,1,128,1,3,1,0,1,195,1,190,1,0,1,176,1,0,1,8,1,0,1,200,1,2,1,128,1,3,1,0,1,195,1,190,1,0,1,176,1,0,1,9,1,0,1,200,1,4,1,128,1,3,1,0,1,195,1,190,1,0,1,176,1,0,1,10,1,0,1,200,1,7,1,128,1,4,1,2,1,192,1,192,1,0,1,160,1,1,1,12,1,0,1,200,1,1,1,128,1,5,1,0,1,167,1,167,1,0,1,175,1,1,1,0,1,0,1,200,1,2,1,128,1,5,1,0,1,167,1,167,1,0,1,175,1,1,1,1,1,0,1,200,1,4,1,128,1,5,1,0,1,167,1,167,1,0,1,175,1,1,1,2,1,0,1,200,1,8,1,128,1,5,1,0,1,167,1,167,1,0,1,175,1,1,1,3,149,0,0,132,255,0,131,0,0,1,1,134,0,0,1,8,1,88,1,16,1,42,1,17,131,0,0,1,2,1,156,1,0,1,0,1,5,1,188,135,0,0,1,36,1,0,1,0,1,2,1,60,1,0,1,0,1,2,1,100,138,0,0,1,2,1,20,131,0,0,1,28,1,0,1,0,1,2,1,8,1,255,1,255,1,3,132,0,0,1,6,131,0,0,1,28,134,0,0,1,2,1,1,131,0,0,1,148,1,0,1,2,131,0,0,1,17,133,0,0,1,156,131,0,0,1,172,1,0,1,0,1,1,1,188,1,0,1,3,131,0,0,1,1,132,0,0,1,1,1,196,134,0,0,1,1,1,212,1,0,1,3,1,0,1,1,1,0,1,1,132,0,0,1,1,1,220,134,0,0,1,1,1,236,1,0,1,3,1,0,1,2,1,0,1,1,132,0,0,1,1,1,220,134,0,0,1,1,1,243,1,0,1,3,1,0,1,3,1,0,1,1,132,0,0,1,1,1,220,134,0,0,1,1,1,250,1,0,1,3,1,0,1,4,1,0,1,1,132,0,0,1,1,1,220,132,0,0,1,95,1,112,1,115,1,95,1,99,1,0,1,171,1,171,1,0,1,1,1,0,1,3,1,0,1,1,1,0,1,4,1,0,1,17,229,0,0,229,0,0,204,0,0,1,95,1,112,1,115,1,95,1,115,1,48,1,0,1,171,1,0,1,4,1,0,1,14,1,0,1,1,1,0,1,1,1,0,1,1,134,0,0,1,95,1,112,1,115,1,95,1,115,1,49,1,0,1,171,1,0,1,4,1,0,1,12,1,0,1,1,1,0,1,1,1,0,1,1,134,0,0,1,95,1,112,1,115,1,95,1,115,1,50,1,0,1,95,1,112,1,115,1,95,1,115,1,51,1,0,1,95,1,112,1,115,1,95,1,115,1,52,1,0,1,112,1,115,1,95,1,51,1,95,1,48,1,0,1,50,1,46,1,48,1,46,1,49,1,49,1,54,1,50,1,54,1,46,1,48,136,0,0,1,1,139,0,0,1,20,1,1,1,252,1,0,1,16,147,0,0,1,64,1,0,1,0,1,5,1,124,1,16,1,0,1,12,132,0,0,1,4,134,0,0,1,72,1,198,1,0,1,63,1,0,1,63,131,0,0,1,1,1,0,1,0,1,48,1,80,1,0,1,0,1,113,1,81,1,0,1,0,1,114,1,82,1,0,1,0,1,115,1,83,1,0,1,0,1,116,1,84,1,0,1,0,1,245,1,85,1,62,1,170,1,170,1,171,144,0,0,1,65,1,112,1,0,1,0,1,62,1,128,1,0,1,0,1,63,1,64,1,0,1,0,1,190,1,170,1,170,1,171,1,63,1,42,1,170,1,171,1,63,1,192,1,0,1,0,1,59,1,128,1,128,1,129,1,63,131,0,0,1,191,131,0,0,1,63,1,128,1,0,1,0,1,67,1,127,131,0,0,1,9,1,80,1,12,1,32,1,17,1,16,1,0,1,86,1,0,1,0,1,9,1,0,1,0,1,32,1,19,1,64,1,8,1,16,1,0,1,176,131,0,0,1,4,1,0,1,96,1,21,1,96,1,27,1,18,1,0,1,18,1,0,1,0,1,149,1,0,1,0,1,96,1,33,1,48,1,39,1,18,1,0,1,18,132,0,0,1,9,1,96,1,42,1,96,1,48,1,18,1,0,1,18,133,0,0,1,32,1,54,1,64,1,56,1,16,1,0,1,82,1,0,1,0,1,64,132,0,0,1,48,1,60,1,196,1,0,1,82,133,0,0,1,96,1,63,1,96,1,69,1,86,1,0,1,86,133,0,0,1,96,1,75,1,96,1,81,1,18,1,0,1,18,133,0,0,1,96,1,87,1,96,1,93,1,18,1,0,1,18,133,0,0,1,96,1,99,1,96,1,105,1,18,1,0,1,18,133,0,0,1,80,1,111,1,0,1,0,1,34,133,0,0,1,16,1,32,1,96,1,1,1,31,1,31,1,254,1,136,1,0,1,0,1,64,1,0,1,116,1,0,1,0,1,128,131,0,0,1,108,1,194,1,0,1,0,1,16,1,200,1,7,1,0,1,8,1,0,1,198,1,198,1,0,1,34,1,255,1,255,1,0,1,200,1,7,1,0,1,7,1,0,1,198,1,198,1,0,1,34,1,255,1,255,1,0,1,168,1,136,1,4,1,3,1,0,1,198,1,108,1,196,1,165,1,6,1,255,1,15,1,16,1,56,1,112,1,1,1,159,1,31,1,254,1,136,1,128,1,0,1,64,1,0,1,200,1,7,1,0,1,7,1,24,1,192,1,192,1,0,1,225,1,7,1,7,1,0,1,200,1,8,1,0,1,130,1,0,1,177,1,177,1,198,1,110,1,16,1,6,1,8,1,116,1,0,1,0,1,128,131,0,0,1,198,1,194,1,0,1,0,1,16,1,76,1,64,133,0,0,1,27,1,226,1,0,1,0,1,5,1,200,1,7,1,0,1,6,1,0,1,198,1,192,1,0,1,161,1,0,1,255,1,0,1,76,1,71,1,0,1,5,1,0,1,205,1,205,1,108,1,193,1,6,1,5,1,10,1,76,1,137,1,0,1,8,1,0,1,109,1,108,1,177,1,128,1,5,1,255,1,10,1,0,1,134,1,0,1,8,1,0,1,107,1,203,1,203,1,224,1,8,1,0,1,0,1,152,1,24,1,97,1,1,1,15,1,31,1,255,1,248,1,0,1,0,1,64,1,0,1,184,1,24,1,97,1,1,1,15,1,31,1,255,1,199,1,0,1,0,1,64,1,0,1,16,1,24,1,97,1,1,1,15,1,31,1,254,1,63,1,0,1,0,1,64,1,0,1,48,1,24,1,1,1,1,1,15,1,31,1,254,1,63,1,0,1,0,1,64,1,0,1,0,1,72,1,0,1,1,1,6,1,27,1,198,1,97,1,192,1,0,1,0,1,10,1,20,1,8,1,0,1,0,1,4,1,27,1,198,1,27,1,224,1,0,1,5,1,1,1,4,1,131,1,6,1,5,1,0,1,111,1,109,1,198,1,161,1,8,1,10,1,5,1,44,1,23,1,5,1,6,1,2,1,27,1,192,1,108,1,224,1,0,1,6,1,5,1,45,1,47,1,5,1,8,1,0,1,0,1,177,1,177,1,161,1,6,1,253,1,5,1,200,1,3,1,0,1,5,1,0,1,176,1,27,1,198,1,139,1,5,1,253,1,253,1,188,1,79,1,5,1,6,1,0,1,0,1,198,1,196,1,193,1,6,1,0,1,255,1,189,1,143,1,5,1,6,1,0,1,0,1,108,1,197,1,129,1,6,1,255,1,255,1,200,1,15,1,0,1,5,1,0,1,51,1,78,1,0,1,225,1,5,1,5,1,0,1,200,1,8,131,0,0,1,37,1,208,1,0,1,239,1,5,1,8,1,0,1,184,1,72,1,0,1,1,1,0,1,37,1,208,1,195,1,207,1,5,1,6,1,255,1,188,1,33,1,8,1,8,1,0,1,198,1,198,1,195,1,193,1,0,1,0,1,255,1,16,1,64,1,0,1,1,1,31,1,31,1,254,1,136,1,0,1,0,1,64,1,0,1,200,1,11,131,0,0,1,98,1,177,1,0,1,160,1,0,1,255,1,0,1,200,1,7,1,0,1,1,1,0,1,108,1,192,1,0,1,225,1,0,1,1,1,0,1,200,1,7,131,0,0,1,177,1,192,1,192,1,235,1,0,1,3,1,1,1,200,1,14,131,0,0,1,27,1,140,1,140,1,235,1,0,1,2,1,0,1,200,1,1,131,0,0,1,18,1,18,1,0,1,240,131,0,0,1,88,1,16,133,0,0,1,108,1,226,1,0,1,0,1,128,1,200,1,7,1,0,1,5,1,0,1,201,1,108,1,0,1,225,131,0,0,1,200,1,1,131,0,0,1,190,1,190,1,0,1,240,1,5,1,4,1,0,1,0,1,16,133,0,0,1,108,1,226,131,0,0,1,200,1,14,1,0,1,0,1,4,1,252,1,108,1,252,1,235,1,5,1,0,1,4,1,200,1,1,131,0,0,1,195,1,195,1,0,1,240,131,0,0,1,88,1,16,133,0,0,1,108,1,226,1,0,1,0,1,128,1,112,1,7,1,0,1,2,1,0,1,21,1,108,1,27,1,225,1,0,1,0,1,3,1,200,1,15,1,0,1,0,1,16,1,166,1,205,1,0,1,242,1,2,1,2,1,0,1,76,1,20,1,1,1,1,1,16,1,27,1,27,1,198,1,226,1,0,1,0,1,128,1,200,1,3,1,0,1,1,1,16,1,109,1,108,1,198,1,203,1,0,1,1,1,254,1,144,1,8,1,0,1,33,1,159,1,31,1,246,1,136,1,0,1,0,1,192,1,0,1,168,1,16,1,1,1,0,1,16,1,0,1,0,1,67,1,194,1,0,1,0,1,9,1,200,1,7,1,0,1,0,1,16,1,108,1,192,1,0,1,225,1,1,1,0,1,0,1,200,1,7,1,0,1,4,1,16,1,192,1,192,1,0,1,225,131,0,0,1,200,1,7,1,0,1,0,1,24,1,108,1,98,1,98,1,139,1,2,1,1,1,0,1,200,1,7,1,0,1,0,1,24,1,177,1,190,1,180,1,171,1,2,1,2,1,0,1,200,1,3,1,0,1,1,1,24,1,108,1,196,1,0,1,225,1,2,1,2,1,0,1,200,1,15,1,0,1,6,1,24,1,114,1,130,1,0,1,225,1,2,1,2,1,0,1,200,1,8,1,0,1,0,1,26,1,108,1,177,1,0,1,224,1,1,1,6,1,0,1,200,1,1,1,0,1,1,1,26,1,108,1,108,1,0,1,160,1,6,1,252,1,0,1,200,1,7,1,0,1,0,1,24,1,198,1,98,1,180,1,171,1,2,1,3,1,0,1,200,1,7,1,0,1,0,1,24,1,27,1,190,1,180,1,171,1,6,1,4,1,0,1,200,1,7,1,0,1,0,1,24,1,198,1,98,1,180,1,171,1,6,1,5,1,0,1,200,1,7,1,0,1,0,1,24,1,177,1,190,1,180,1,171,1,1,1,6,1,0,1,200,1,7,1,0,1,0,1,24,1,108,1,98,1,180,1,171,1,1,1,7,1,0,1,200,1,7,1,0,1,0,1,24,1,27,1,190,1,180,1,171,1,0,1,8,1,0,1,200,1,7,1,0,1,4,1,24,1,101,1,108,1,0,1,162,1,0,1,253,1,0,1,20,1,14,1,0,1,6,1,0,1,108,1,140,1,108,1,129,1,5,1,6,1,11,1,12,1,135,1,1,1,10,1,0,1,177,1,180,1,177,1,129,1,5,1,5,1,15,1,200,1,13,131,0,0,1,108,1,114,1,114,1,139,1,5,1,1,1,0,1,76,1,39,1,0,1,1,1,0,1,98,1,98,1,177,1,1,1,12,1,12,1,9,1,172,1,23,1,3,1,11,1,0,1,108,1,180,1,2,1,129,1,5,1,4,1,12,1,200,1,7,1,0,1,12,1,0,1,177,1,190,1,200,1,171,1,5,1,2,1,0,1,172,1,39,1,3,1,9,1,0,1,190,1,190,1,0,1,193,1,5,1,5,1,12,1,173,1,72,1,3,1,0,1,0,1,190,1,190,1,1,1,144,1,5,1,13,1,12,1,101,1,17,1,6,1,0,1,0,1,190,1,190,1,182,1,176,1,2,1,13,1,9,1,8,1,68,1,1,1,0,1,0,1,190,1,190,1,108,1,176,1,5,1,13,1,0,1,200,1,7,1,0,1,2,1,0,1,198,1,98,1,180,1,171,1,5,1,3,1,12,1,64,1,72,1,0,1,9,1,0,1,198,1,108,1,108,1,161,1,0,1,252,1,0,1,200,1,7,1,0,1,2,1,0,1,205,1,177,1,180,1,235,1,11,1,5,1,2,1,200,1,7,1,0,1,2,1,0,1,101,1,198,1,180,1,235,1,10,1,5,1,2,1,168,1,19,1,0,1,1,1,0,1,111,1,109,1,66,1,128,1,9,1,254,1,14,1,58,1,24,1,0,1,1,1,0,1,27,1,108,1,108,1,225,1,1,1,1,1,0,1,200,1,7,1,0,1,5,1,0,1,18,1,198,1,180,1,235,1,6,1,5,1,2,1,172,1,20,131,0,0,1,108,1,177,1,128,1,129,1,0,1,14,1,15,1,200,1,8,1,0,1,5,1,0,1,27,1,198,1,198,1,236,1,3,1,0,1,1,1,168,1,23,1,1,1,2,1,0,1,27,1,192,1,192,1,193,1,4,1,4,1,11,1,200,1,7,1,0,1,4,1,0,1,177,1,98,1,180,1,171,1,1,1,7,1,5,1,200,1,7,1,0,1,4,1,0,1,108,1,190,1,180,1,171,1,6,1,8,1,4,1,168,1,39,1,1,1,5,1,0,1,101,1,108,1,128,1,130,1,4,1,253,1,11,1,200,1,7,1,0,1,1,1,0,1,27,1,108,1,99,1,172,1,3,1,253,1,1,1,168,1,23,1,0,1,1,1,0,1,192,1,108,1,131,1,193,1,1,1,8,1,15,1,200,1,1,131,0,0,1,27,1,27,1,108,1,235,1,4,1,5,1,0,1,200,1,13,131,0,0,1,108,1,177,1,240,1,235,1,0,1,8,1,1,1,168,1,23,1,0,1,1,1,0,1,180,1,20,1,67,1,225,1,3,1,0,1,15,1,200,1,7,1,0,1,1,1,0,1,192,1,27,1,192,1,235,1,2,1,2,1,1,1,200,1,13,131,0,0,1,108,1,240,1,240,1,235,1,0,1,5,1,1,1,200,1,7,1,0,1,1,1,0,1,20,1,192,1,0,1,225,1,0,1,7,1,0,1,20,1,16,133,0,0,1,182,1,226,1,0,1,0,1,1,1,168,1,33,131,0,0,1,108,1,108,1,1,1,194,1,0,1,1,1,255,1,200,1,1,131,0,0,1,177,1,108,1,0,1,225,131,0,0,1,52,1,16,1,0,1,0,1,1,1,0,1,0,1,108,1,226,131,0,0,1,201,1,8,1,128,1,0,1,4,1,108,1,27,1,0,1,161,1,0,1,254,1,0,1,200,1,1,1,0,1,0,1,4,1,108,1,27,1,0,1,161,1,0,1,254,1,0,1,168,1,16,133,0,0,1,128,1,194,1,0,1,0,1,9,1,76,1,16,133,0,0,1,108,1,226,131,0,0,1,201,1,7,1,128,1,0,1,0,1,192,1,108,1,0,1,225,1,1,150,0,0,132,255,0,138,0,0,1,3,1,112,1,16,1,42,1,17,1,1,1,0,1,0,1,1,1,228,1,0,1,0,1,1,1,140,135,0,0,1,36,134,0,0,1,1,1,84,138,0,0,1,1,1,44,131,0,0,1,28,1,0,1,0,1,1,1,31,1,255,1,254,1,3,132,0,0,1,1,131,0,0,1,28,134,0,0,1,1,1,24,131,0,0,1,48,1,0,1,2,131,0,0,1,13,133,0,0,1,56,131,0,0,1,72,1,95,1,118,1,115,1,95,1,99,1,0,1,171,1,171,1,0,1,1,1,0,1,3,1,0,1,1,1,0,1,4,1,0,1,13,229,0,0,229,0,0,140,0,0,1,118,1,115,1,95,1,51,1,95,1,48,1,0,1,50,1,46,1,48,1,46,1,49,1,49,1,54,1,50,1,54,1,46,1,48,1,0,1,171,134,0,0,1,1,1,140,1,0,1,81,1,0,1,5,138,0,0,1,72,1,198,131,0,0,1,1,131,0,0,1,5,131,0,0,1,15,1,0,1,0,1,2,1,144,1,0,1,16,1,0,1,4,1,0,1,0,1,80,1,5,1,0,1,0,1,48,1,6,1,0,1,0,1,96,1,7,1,0,1,32,1,112,1,8,1,0,1,0,1,48,1,80,1,0,1,1,1,113,1,81,1,0,1,4,1,114,1,82,1,0,1,7,1,115,1,83,1,0,1,10,1,116,1,84,1,0,1,11,1,245,1,85,1,0,1,0,1,16,1,17,131,0,0,1,18,131,0,0,1,19,1,0,1,0,1,16,1,20,131,0,0,1,21,131,0,0,1,22,1,0,1,0,1,16,1,23,131,0,0,1,24,131,0,0,1,25,1,0,1,0,1,16,1,26,1,0,1,0,1,16,1,27,131,0,0,1,28,131,0,0,1,29,131,0,0,1,30,1,0,1,0,1,16,1,31,1,241,1,85,1,80,1,4,1,0,1,0,1,18,1,1,1,194,133,0,0,1,64,1,9,1,0,1,0,1,18,1,0,1,196,133,0,0,1,96,1,13,1,96,1,19,1,18,1,0,1,18,133,0,0,1,96,1,25,1,16,1,31,1,18,1,0,1,34,131,0,0,1,5,1,248,1,80,131,0,0,1,6,1,136,132,0,0,1,5,1,248,1,64,131,0,0,1,15,1,200,132,0,0,1,5,1,248,1,48,131,0,0,1,14,1,136,132,0,0,1,5,1,248,1,32,131,0,0,1,14,1,136,132,0,0,1,5,1,248,1,16,131,0,0,1,14,1,136,132,0,0,1,200,1,1,1,128,1,62,1,0,1,167,1,167,1,0,1,175,1,5,1,4,1,0,1,200,1,2,1,128,1,62,1,0,1,167,1,167,1,0,1,175,1,5,1,5,1,0,1,200,1,4,1,128,1,62,1,0,1,167,1,167,1,0,1,175,1,5,1,6,1,0,1,200,1,8,1,128,1,62,1,0,1,167,1,167,1,0,1,175,1,5,1,7,1,0,1,200,1,1,131,0,0,1,167,1,167,1,0,1,175,1,5,1,8,1,0,1,200,1,2,131,0,0,1,167,1,167,1,0,1,175,1,5,1,9,1,0,1,200,1,4,131,0,0,1,167,1,167,1,0,1,175,1,5,1,10,1,0,1,200,1,8,131,0,0,1,167,1,167,1,0,1,175,1,5,1,11,1,0,1,200,1,3,1,128,1,0,1,0,1,176,1,176,1,0,1,226,1,4,1,4,1,0,1,200,1,1,1,128,1,1,1,0,1,190,1,190,1,0,1,176,1,3,1,8,1,0,1,200,1,2,1,128,1,1,1,0,1,190,1,190,1,0,1,176,1,3,1,9,1,0,1,200,1,4,1,128,1,1,1,0,1,190,1,190,1,0,1,176,1,3,1,10,1,0,1,200,1,1,1,128,1,2,1,0,1,190,1,190,1,0,1,176,1,2,1,8,1,0,1,200,1,2,1,128,1,2,1,0,1,190,1,190,1,0,1,176,1,2,1,9,1,0,1,200,1,4,1,128,1,2,1,0,1,190,1,190,1,0,1,176,1,2,1,10,1,0,1,200,1,1,1,128,1,3,1,0,1,190,1,190,1,0,1,176,1,1,1,8,1,0,1,200,1,2,1,128,1,3,1,0,1,190,1,190,1,0,1,176,1,1,1,9,1,0,1,200,1,4,1,128,1,3,1,0,1,190,1,190,1,0,1,176,1,1,1,10,1,0,1,200,1,7,1,128,1,4,1,2,1,192,1,192,1,0,1,160,1,0,1,12,1,0,1,200,1,1,1,128,1,5,1,0,1,167,1,167,1,0,1,175,131,0,0,1,200,1,2,1,128,1,5,1,0,1,167,1,167,1,0,1,175,1,0,1,1,1,0,1,200,1,4,1,128,1,5,1,0,1,167,1,167,1,0,1,175,1,0,1,2,1,0,1,200,1,8,1,128,1,5,1,0,1,167,1,167,1,0,1,175,1,0,1,3,140,0,0,1,0};
			}
		}
#else
		/// <summary>Static Length+DeflateStream compressed shader byte code (Windows)</summary>
		private static byte[] fxb
		{
			get
			{
				return new byte[] {36,80,0,0,236,189,7,96,28,73,150,37,38,47,109,202,123,127,74,245,74,215,224,116,161,8,128,96,19,36,216,144,64,16,236,193,136,205,230,146,236,29,105,71,35,41,171,42,129,202,101,86,101,93,102,22,64,204,237,157,188,247,222,123,239,189,247,222,123,239,189,247,186,59,157,78,39,247,223,255,63,92,102,100,1,108,246,206,74,218,201,158,33,128,170,200,31,63,126,124,31,63,34,254,197,223,240,127,250,251,210,95,99,248,249,53,127,236,255,254,191,254,190,223,92,126,255,181,241,55,253,255,127,210,239,126,99,250,255,175,163,159,253,255,237,249,245,232,255,191,255,101,243,251,79,127,13,55,238,173,95,75,190,3,57,254,255,58,238,15,125,152,110,171,144,110,255,210,111,42,223,253,91,191,198,143,232,246,163,231,71,207,143,158,31,61,63,122,126,244,252,232,249,209,243,163,231,71,207,143,158,31,61,63,236,231,215,255,53,56,190,157,32,80,51,113,218,193,111,38,223,253,58,250,255,111,34,78,211,126,10,234,231,215,250,77,126,13,129,251,251,254,102,145,54,20,51,54,59,191,198,175,245,27,105,155,63,104,168,205,174,107,243,151,13,181,217,251,53,126,13,211,230,159,26,106,115,239,215,248,131,76,155,255,110,168,205,190,208,0,95,255,58,242,149,125,144,10,64,88,219,253,220,208,12,52,253,205,34,223,227,239,216,123,63,70,255,127,82,230,203,89,177,188,248,53,78,126,141,95,227,215,29,120,31,241,117,236,253,223,144,254,127,182,108,218,108,57,5,132,31,96,12,175,231,217,44,175,5,54,240,2,253,241,57,222,77,189,119,255,15,250,255,111,231,77,246,62,13,238,165,230,58,240,252,123,212,225,255,166,49,60,158,167,191,217,175,241,107,252,222,191,153,251,123,78,191,255,140,247,247,31,71,191,255,69,222,223,127,19,253,254,143,121,127,255,107,244,251,127,229,127,239,229,151,182,244,119,116,255,167,232,247,255,7,181,253,223,232,255,127,170,254,253,91,81,155,223,140,254,255,7,68,218,238,211,103,59,191,185,107,251,156,126,255,54,253,255,47,138,180,109,233,179,149,215,246,143,163,223,255,40,109,247,235,201,15,110,255,127,211,99,200,179,15,66,255,218,255,247,255,253,127,253,223,127,222,175,113,242,230,248,201,239,68,127,30,254,90,242,25,222,249,157,164,89,10,26,254,105,250,254,111,78,255,254,121,244,243,175,163,255,255,125,191,166,145,183,95,235,215,248,199,20,232,191,198,159,253,154,244,223,175,247,107,252,123,250,217,127,199,159,253,90,244,89,242,107,252,114,253,236,215,249,181,240,25,90,254,38,191,198,111,164,243,243,59,241,103,191,14,125,246,91,252,26,191,155,126,38,249,151,191,246,175,253,53,185,237,175,243,107,232,144,126,244,116,30,213,57,127,237,175,67,244,252,53,249,63,239,243,93,124,254,27,245,63,223,27,248,252,222,192,231,251,253,207,233,227,123,191,255,206,175,241,69,49,173,171,166,58,111,211,173,87,119,210,111,63,127,253,60,21,137,77,79,170,197,170,40,233,151,135,227,189,79,199,15,239,239,141,247,14,246,247,127,141,159,32,181,240,155,255,26,191,233,95,68,32,126,15,210,144,244,255,95,227,31,4,60,124,254,91,240,231,171,227,95,227,215,248,61,241,221,209,95,251,215,252,53,255,0,62,255,45,165,253,239,249,107,252,26,127,224,201,31,252,7,253,65,135,104,255,187,16,91,146,126,249,131,136,115,254,36,249,253,215,252,131,126,205,95,227,215,215,223,127,173,63,232,215,178,191,255,218,127,208,175,109,127,255,117,254,160,95,199,254,254,235,254,65,191,238,175,241,155,242,239,4,238,207,250,53,126,131,223,244,47,210,223,255,164,95,211,251,253,215,242,126,255,181,189,223,127,29,250,253,9,139,193,111,74,56,252,103,212,238,63,251,139,126,93,254,251,215,164,191,127,141,63,232,55,253,53,254,154,191,136,217,154,250,252,53,126,141,191,230,15,134,248,254,56,191,251,27,252,65,191,217,175,241,213,95,116,231,215,248,117,127,173,223,76,84,204,95,12,56,191,166,194,249,181,21,14,141,133,198,243,159,241,255,191,245,107,176,238,251,181,240,217,111,78,239,126,66,127,255,222,172,2,127,45,122,231,255,254,131,241,217,175,241,107,124,245,7,9,204,191,134,96,254,53,127,241,175,167,125,253,186,191,198,255,253,39,1,222,175,69,116,160,182,244,247,127,246,39,225,59,146,170,63,40,161,254,229,247,95,139,126,255,234,47,250,117,88,60,127,109,162,221,127,70,253,252,87,80,55,60,134,95,135,105,248,159,241,231,192,227,215,162,49,253,230,191,198,255,253,23,253,254,244,29,209,144,62,255,203,254,160,95,147,240,150,239,126,3,162,241,87,127,16,190,251,245,248,187,191,206,126,71,60,68,127,255,105,252,221,175,79,223,253,58,191,198,255,198,223,253,254,220,7,254,254,229,252,247,175,197,227,255,13,168,207,95,131,254,255,213,31,4,188,100,44,127,205,159,132,49,252,154,52,102,192,195,60,19,254,127,144,252,254,235,252,65,191,190,253,29,56,252,26,127,16,224,252,218,76,87,30,247,31,44,243,241,27,240,88,49,246,95,87,251,197,216,164,13,254,254,93,232,93,140,95,190,255,181,233,111,124,255,91,232,223,134,62,201,175,241,159,253,69,191,37,143,87,254,246,105,247,91,16,252,223,66,231,255,215,226,49,63,229,57,146,247,65,147,43,250,127,242,7,253,152,226,35,239,255,103,252,247,175,169,127,11,189,229,123,51,199,134,46,191,166,254,253,235,186,191,249,255,134,79,240,14,190,23,158,249,13,44,207,60,209,254,193,103,191,142,210,153,228,68,231,251,175,209,241,226,239,175,208,255,159,36,99,250,13,129,7,230,248,79,250,181,105,158,229,51,180,249,107,192,155,127,210,175,69,115,246,187,49,15,9,156,223,128,97,254,154,24,207,159,100,198,128,191,5,198,175,97,223,215,239,254,96,180,195,251,191,142,190,47,60,196,188,207,252,248,235,234,231,152,207,95,35,252,236,47,2,159,253,186,74,251,95,87,96,210,103,127,13,127,134,177,252,122,244,217,175,243,107,252,215,244,255,127,204,182,161,254,192,243,193,123,191,46,127,22,194,250,245,248,179,191,198,126,38,124,253,215,240,255,127,11,226,125,211,238,215,255,53,64,199,255,219,246,105,248,255,215,81,190,149,191,121,220,132,243,255,253,7,155,247,126,3,239,189,223,80,113,197,239,144,43,204,211,175,203,242,64,124,169,250,0,112,72,150,193,35,252,183,226,253,7,9,12,247,55,254,255,123,107,31,191,134,226,255,235,41,110,242,153,27,187,153,251,95,219,163,173,126,246,23,65,238,28,95,11,109,127,109,130,247,107,41,172,95,143,63,251,175,233,255,255,152,109,99,104,235,191,103,104,235,127,102,104,235,62,131,172,255,53,252,127,67,91,180,51,180,53,125,74,187,175,248,255,238,189,95,131,255,111,104,139,247,12,109,125,92,241,251,111,170,180,251,245,233,239,223,136,218,224,255,230,111,234,139,62,251,191,255,162,223,192,246,243,159,253,65,191,49,125,143,191,127,237,95,227,55,231,185,248,141,181,61,244,165,233,55,253,53,160,51,126,115,254,253,55,177,58,3,252,143,191,191,250,139,48,23,191,54,219,132,191,70,223,1,45,4,6,112,252,45,237,59,191,22,127,247,155,170,92,155,191,137,30,127,177,252,253,155,240,223,191,225,175,241,39,41,204,223,132,97,130,95,128,171,200,205,111,103,249,193,216,33,244,9,186,252,26,44,131,255,55,195,23,30,16,89,135,222,248,181,127,141,95,197,127,67,6,97,71,192,47,191,183,210,210,232,166,223,220,155,23,240,207,175,225,205,233,175,161,124,245,107,48,44,255,179,255,140,233,106,230,225,215,224,254,254,51,254,28,60,255,107,50,142,95,241,56,240,247,175,165,56,211,88,254,32,99,191,126,76,233,33,122,82,250,48,127,255,26,76,191,175,88,15,11,172,255,219,234,120,177,19,50,206,95,211,182,253,107,44,44,140,27,176,77,63,6,238,175,169,120,235,251,196,10,191,11,227,252,127,255,223,191,6,63,212,152,125,233,95,67,159,231,52,210,95,227,215,254,191,200,151,94,89,95,250,143,253,53,229,51,180,253,157,184,213,175,145,254,97,244,217,83,125,255,55,166,127,159,211,207,223,151,254,63,226,78,126,99,16,236,215,216,71,155,95,211,172,217,58,223,247,55,54,157,253,255,236,209,152,218,142,243,215,113,95,189,215,211,109,124,249,53,125,83,241,237,126,163,63,232,215,176,126,225,111,76,115,111,126,255,77,136,255,204,239,240,25,172,239,72,252,46,191,255,186,204,111,242,251,175,205,60,44,191,67,39,254,250,250,59,124,148,223,192,123,151,96,253,71,230,93,162,131,254,14,89,253,245,245,119,232,1,243,251,175,77,125,153,223,127,29,234,203,252,254,235,162,175,255,72,108,63,219,141,63,73,120,25,182,254,43,182,211,144,73,249,27,125,254,103,127,18,120,218,125,198,186,216,126,38,62,45,203,50,127,38,126,202,175,249,31,65,46,160,3,229,239,95,139,255,254,77,236,223,191,14,255,253,155,218,191,127,3,254,251,55,251,53,196,247,83,127,150,241,82,251,78,56,127,245,39,225,187,95,91,100,142,113,20,251,64,190,182,234,13,124,175,190,129,249,254,63,34,219,241,39,65,39,57,187,32,240,126,125,130,231,108,135,131,129,207,64,195,95,191,243,158,241,177,127,3,125,15,127,255,6,250,158,161,5,232,253,27,232,123,134,22,191,158,210,226,215,208,177,254,122,74,139,95,211,254,45,180,248,181,236,223,66,11,216,80,232,39,204,27,254,38,251,243,23,11,109,126,237,255,8,126,185,209,42,80,31,63,138,215,255,255,249,252,40,94,255,81,188,254,163,120,253,71,241,250,143,226,245,31,197,235,63,138,215,127,20,175,255,40,94,119,223,253,154,58,206,111,50,94,167,207,130,120,125,159,58,151,120,253,31,250,181,141,47,253,207,254,38,242,25,1,182,190,244,63,246,155,108,142,215,255,45,192,250,53,127,222,197,235,88,107,183,227,252,183,110,124,227,71,207,143,158,31,61,63,122,126,244,252,232,249,209,243,163,231,71,207,143,158,31,61,63,122,126,244,124,147,207,215,93,239,252,9,202,213,255,231,191,198,111,250,23,253,26,191,198,239,249,123,250,240,52,103,254,7,153,117,208,95,151,99,124,183,174,105,214,65,145,59,50,235,160,200,229,154,117,80,137,227,229,119,228,0,204,58,168,194,252,198,214,59,37,55,240,155,254,65,255,57,231,197,255,179,63,105,204,125,252,166,127,211,175,33,249,139,95,7,223,253,186,180,182,247,27,167,255,25,141,241,171,191,137,62,248,117,241,153,252,253,107,252,77,244,199,159,132,182,225,231,127,205,223,36,121,235,238,231,255,247,223,132,220,60,62,151,188,47,242,23,255,217,159,100,250,250,181,180,175,223,196,235,11,159,253,38,94,95,154,127,241,62,151,190,250,159,75,95,191,150,237,235,215,226,190,52,47,243,235,32,71,142,190,126,83,175,47,124,246,155,122,125,105,94,199,251,92,250,234,127,46,125,253,218,182,175,95,135,251,50,57,42,205,197,252,73,146,251,254,53,255,35,89,23,117,107,192,191,38,227,228,214,128,101,93,215,173,1,203,186,172,228,229,126,29,158,103,161,153,252,253,107,241,223,38,15,253,235,80,223,191,150,246,141,191,41,191,244,31,253,58,252,187,188,255,107,81,127,146,119,55,127,255,58,252,119,226,193,255,181,61,248,200,161,253,58,157,254,126,109,175,63,194,159,191,247,251,255,181,189,254,127,13,94,127,114,127,255,218,29,124,126,237,14,62,191,118,31,159,255,232,215,240,218,255,58,186,78,108,218,83,127,252,55,218,155,181,5,208,235,55,180,127,255,154,252,247,111,96,255,254,181,248,239,31,179,127,255,58,252,183,121,255,103,99,93,26,107,6,46,123,246,163,117,233,255,191,62,63,90,151,254,209,186,244,143,214,165,127,180,46,253,163,117,233,31,173,75,255,104,93,250,71,235,210,63,90,151,118,223,253,154,58,206,111,114,93,26,143,191,46,253,251,18,52,89,151,254,202,250,210,191,203,175,41,159,225,199,239,36,205,210,223,142,254,216,249,53,184,51,94,151,62,160,159,223,254,53,126,254,172,65,127,221,92,135,250,91,127,208,215,205,105,232,187,223,88,238,66,108,246,175,249,31,177,13,213,120,236,215,144,120,237,79,242,227,233,95,131,253,44,23,79,227,111,19,79,35,62,38,153,248,147,252,248,24,127,251,241,49,254,78,172,254,249,53,255,163,95,219,107,143,248,245,215,246,218,35,126,253,181,189,246,136,95,127,29,175,61,226,215,95,199,107,143,248,245,215,209,246,198,7,1,126,126,252,250,107,232,251,58,62,254,219,143,95,127,13,239,253,159,205,248,245,255,9,0,0,255,255};
			}
		}
#endif
		/// <summary>Set a shader sampler of type 'TextureSamplerState' by global unique ID, see <see cref="Xen.Graphics.ShaderSystem.ShaderSystemBase.GetNameUniqueID"/> for details.</summary><param name="state"/><param name="id"/><param name="value"/>
		protected override bool SetSamplerStateImpl(Xen.Graphics.ShaderSystem.ShaderSystemBase state, int id, Xen.Graphics.TextureSamplerState value)
		{
			if ((SimpleTerrain.gd != state.DeviceUniqueIndex))
			{
				this.WarmShader(state);
			}
			if ((id == SimpleTerrain.sid0))
			{
				this.AlbedoSampler = value;
				return true;
			}
			if ((id == SimpleTerrain.sid1))
			{
				this.CubeRgbmSampler = value;
				return true;
			}
			if ((id == SimpleTerrain.sid2))
			{
				this.NormalSampler = value;
				return true;
			}
			if ((id == SimpleTerrain.sid3))
			{
				this.ShadowSampler = value;
				return true;
			}
			if ((id == SimpleTerrain.sid4))
			{
				this.SofSampler = value;
				return true;
			}
			return false;
		}
		/// <summary>Set a shader texture of type 'Texture2D' by global unique ID, see <see cref="Xen.Graphics.ShaderSystem.ShaderSystemBase.GetNameUniqueID"/> for details.</summary><param name="state"/><param name="id"/><param name="value"/>
		protected override bool SetTextureImpl(Xen.Graphics.ShaderSystem.ShaderSystemBase state, int id, Microsoft.Xna.Framework.Graphics.Texture2D value)
		{
			if ((SimpleTerrain.gd != state.DeviceUniqueIndex))
			{
				this.WarmShader(state);
			}
			if ((id == SimpleTerrain.tid2))
			{
				this.SofTexture = value;
				return true;
			}
			if ((id == SimpleTerrain.tid3))
			{
				this.AlbedoTexture = value;
				return true;
			}
			if ((id == SimpleTerrain.tid4))
			{
				this.NormalTexture = value;
				return true;
			}
			return false;
		}
	}
}
