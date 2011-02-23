// XenFX
// Assembly = Xen.Graphics.ShaderSystem.CustomTool, Version=7.0.1.1, Culture=neutral, PublicKeyToken=e706afd07878dfca
// SourceFile = SimpleTerrain.fx
// Namespace = GameClient.Shaders

namespace GameClient.Shaders
{
	
	/// <summary><para>Technique 'SimpleTerrain' generated from file 'SimpleTerrain.fx'</para><para>Vertex Shader: approximately 23 instruction slots used, 13 registers</para><para>Pixel Shader: approximately 148 instruction slots used (12 texture, 136 arithmetic), 17 registers</para></summary>
	[System.Diagnostics.DebuggerStepThroughAttribute()]
	[System.CodeDom.Compiler.GeneratedCodeAttribute("Xen.Graphics.ShaderSystem.CustomTool.dll", "47301628-bcee-4c1a-8aea-2eb652e288f0")]
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
		private static void gdInit(Xen.Graphics.ShaderSystem.ShaderSystemBase state)
		{
			// set the graphics ID
			SimpleTerrain.gd = state.DeviceUniqueIndex;
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
		protected override void Begin(Xen.Graphics.ShaderSystem.ShaderSystemBase state, bool ic, bool ec, Xen.Graphics.ShaderSystem.ShaderExtension ext)
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
				SimpleTerrain.gdInit(state);
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
		protected override int GetVertexInputCount()
		{
			return 5;
		}
		/// <summary>Returns a vertex input used by this shader</summary><param name="i"/><param name="usage"/><param name="index"/>
		protected override void GetVertexInput(int i, out Microsoft.Xna.Framework.Graphics.VertexElementUsage usage, out int index)
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
		protected override void GetExtensionSupport(out bool blendingSupport, out bool instancingSupport)
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
				return new byte[] {36,80,0,0,236,189,7,96,28,73,150,37,38,47,109,202,123,127,74,245,74,215,224,116,161,8,128,96,19,36,216,144,64,16,236,193,136,205,230,146,236,29,105,71,35,41,171,42,129,202,101,86,101,93,102,22,64,204,237,157,188,247,222,123,239,189,247,222,123,239,189,247,186,59,157,78,39,247,223,255,63,92,102,100,1,108,246,206,74,218,201,158,33,128,170,200,31,63,126,124,31,63,34,254,197,223,240,127,250,251,210,95,99,248,249,53,127,236,255,254,191,254,190,223,92,126,255,181,241,55,253,255,127,210,239,126,99,250,255,175,163,159,253,255,237,249,245,232,255,191,255,101,243,251,79,127,13,55,238,173,95,75,190,3,57,254,255,58,238,15,125,152,110,171,144,110,255,210,111,42,223,253,91,191,198,143,232,246,163,231,71,207,143,158,31,61,63,122,126,244,252,232,249,209,243,163,231,71,207,143,158,31,61,63,236,231,215,255,53,56,190,157,32,80,51,113,218,193,111,38,223,253,58,250,255,111,34,78,211,126,10,244,243,155,252,26,2,247,247,253,205,34,109,40,102,108,118,126,141,95,227,55,210,54,127,208,80,155,221,95,227,15,50,109,254,178,161,54,123,14,206,63,53,212,230,158,107,243,223,13,181,217,255,53,126,3,208,0,95,255,58,242,149,125,144,10,64,88,219,253,156,105,246,127,11,77,127,179,200,247,248,59,246,222,143,209,255,159,148,249,114,86,44,47,240,242,175,59,240,62,226,235,216,251,191,33,253,255,108,217,180,217,114,10,8,60,134,215,243,108,150,215,2,27,120,129,254,248,28,239,166,222,187,255,7,253,255,183,243,38,123,159,6,247,82,115,29,120,254,61,234,240,127,211,24,30,207,211,223,236,215,248,53,126,239,223,204,253,61,167,223,127,198,251,251,143,163,223,255,34,239,239,191,137,126,255,199,188,191,255,53,250,253,191,242,191,247,242,75,91,250,59,186,255,83,244,251,255,131,218,254,111,244,255,63,85,255,254,173,168,205,111,70,255,255,3,34,109,247,233,179,157,223,220,181,125,78,191,127,155,254,255,23,69,218,182,244,217,202,107,251,199,209,239,127,148,182,251,245,228,7,183,255,191,233,49,228,217,7,161,127,237,255,251,255,254,191,254,239,63,239,215,56,121,115,252,228,119,162,63,15,127,45,249,12,239,252,78,210,44,5,13,255,52,125,255,55,167,127,255,60,250,249,215,209,255,255,190,95,211,200,219,175,245,107,252,99,10,244,95,227,207,126,77,250,239,215,251,53,254,61,253,236,191,227,207,126,45,250,44,249,53,126,185,126,246,235,252,90,248,12,45,127,147,95,227,55,210,249,249,157,248,179,95,135,62,251,45,126,141,223,77,63,147,252,203,95,251,215,254,154,220,246,215,249,53,116,72,63,122,58,143,234,156,191,246,215,33,122,254,154,252,159,247,249,46,62,255,141,250,159,239,13,124,126,111,224,243,253,254,231,244,241,189,223,127,231,215,248,162,152,214,85,83,157,183,233,214,171,59,233,183,159,191,126,158,138,196,166,39,213,98,85,148,244,203,195,241,222,167,227,135,247,247,198,123,7,251,251,191,198,79,144,90,248,205,127,141,223,244,47,34,16,191,7,105,72,250,255,175,241,15,2,30,62,255,45,248,243,213,241,175,241,107,252,158,248,238,232,175,253,107,254,154,127,0,159,255,150,210,254,247,252,53,126,141,63,240,228,15,254,131,254,160,67,180,255,93,136,45,73,191,252,65,196,57,127,146,252,254,107,254,65,191,230,175,241,235,235,239,191,214,31,244,107,217,223,127,237,63,232,215,182,191,255,58,127,208,175,99,127,255,117,255,160,95,247,215,248,77,249,119,2,247,103,253,26,191,193,111,250,23,233,239,127,210,175,233,253,254,107,121,191,255,218,222,239,191,14,253,254,132,197,224,55,37,28,254,51,106,247,159,253,69,191,46,255,253,107,210,223,191,198,31,244,155,254,26,127,205,95,196,108,77,125,254,26,191,198,95,243,7,67,124,127,156,223,253,13,254,160,223,236,215,248,234,47,186,243,107,252,186,191,214,111,38,42,230,47,6,156,95,83,225,252,218,10,135,198,66,227,249,207,248,255,223,250,53,88,247,253,90,248,236,55,167,119,63,161,191,127,111,86,129,191,22,189,243,127,255,193,248,236,215,248,53,190,250,131,4,230,95,67,48,255,154,191,248,215,211,190,126,221,95,227,255,254,147,0,239,215,34,58,80,91,250,251,63,251,147,240,29,73,213,31,148,80,255,242,251,175,69,191,127,245,23,253,58,44,158,191,54,209,238,63,163,126,254,43,168,27,30,195,175,195,52,252,207,248,115,224,241,107,209,152,126,243,95,227,255,254,139,126,127,250,142,104,72,159,255,101,127,208,175,73,120,203,119,191,1,209,248,171,63,8,223,253,122,252,221,95,103,191,35,30,162,191,255,52,254,238,215,167,239,126,157,95,227,127,227,239,126,127,238,3,127,255,114,254,251,215,226,241,255,6,212,231,175,65,255,255,234,15,2,94,50,150,191,230,79,194,24,126,77,26,51,224,97,158,9,255,63,72,126,255,117,254,160,95,223,254,14,28,126,141,63,8,112,126,109,166,43,143,251,15,150,249,248,13,120,172,24,251,175,171,253,98,108,210,6,127,255,46,244,46,198,47,223,255,218,244,55,190,255,45,244,111,67,159,228,215,248,207,254,162,223,146,199,43,127,251,180,251,45,8,254,111,161,243,255,107,241,152,159,242,28,201,251,160,201,21,253,63,249,131,126,76,241,145,247,255,51,254,251,215,212,191,133,222,242,189,153,99,67,151,95,83,255,254,117,221,223,252,127,195,39,120,7,223,11,207,252,6,150,103,158,104,255,224,179,95,71,233,76,114,162,243,253,215,232,120,241,247,87,232,255,79,146,49,253,134,192,3,115,252,39,253,218,52,207,242,25,218,252,53,224,205,63,233,215,162,57,251,221,152,135,4,206,111,192,48,127,77,140,231,79,50,99,192,223,2,227,215,176,239,235,119,127,48,218,225,253,95,71,223,23,30,98,222,103,126,252,117,245,115,204,231,175,17,126,246,23,129,207,126,93,165,253,175,43,48,233,179,191,134,63,195,88,126,61,250,236,215,249,53,254,107,250,255,63,102,219,80,127,224,249,224,189,95,151,63,11,97,253,122,252,217,95,99,63,19,190,254,107,248,255,191,5,241,190,105,247,235,255,26,160,227,255,109,251,52,252,255,235,40,223,202,223,60,110,194,249,255,254,131,205,123,191,129,247,222,111,168,184,226,119,200,21,230,233,215,101,121,32,190,84,125,0,56,36,203,224,17,254,91,241,254,131,4,134,251,27,255,255,189,181,143,95,67,241,255,245,20,55,249,204,141,221,204,253,175,237,209,86,63,251,139,32,119,142,175,133,182,191,54,193,251,181,20,214,175,199,159,253,215,244,255,127,204,182,49,180,245,223,51,180,245,63,51,180,117,159,65,214,255,26,254,191,161,45,218,25,218,154,62,165,221,87,252,127,247,222,175,193,255,55,180,197,123,134,182,62,174,248,253,55,85,218,253,250,244,247,111,68,109,240,127,243,55,245,69,159,253,223,127,209,111,96,251,249,207,254,160,223,152,190,199,223,191,246,175,241,155,243,92,252,198,218,30,250,210,244,155,254,26,208,25,191,57,255,254,155,88,157,1,254,199,223,95,253,69,152,139,95,155,109,194,95,163,239,128,22,2,3,56,254,150,246,157,95,139,191,251,77,85,174,205,223,68,143,191,88,254,254,77,248,239,223,240,215,248,147,20,230,111,194,48,193,47,192,85,228,230,183,179,252,96,236,16,250,4,93,126,13,150,193,255,155,225,11,15,136,172,67,111,252,218,191,198,175,226,191,33,131,176,35,224,151,223,91,105,105,116,211,111,238,205,11,248,231,215,240,230,244,215,80,190,250,53,24,150,255,217,127,198,116,53,243,240,107,112,127,255,25,127,14,158,255,53,25,199,175,120,28,248,251,215,82,156,105,44,127,144,177,95,63,166,244,16,61,41,125,152,191,127,13,166,223,87,172,135,5,214,255,109,117,188,216,9,25,231,175,105,219,254,53,22,22,198,13,216,166,31,3,247,215,84,188,245,125,98,133,223,133,113,254,191,255,239,95,131,31,106,204,190,244,175,161,207,115,26,233,175,241,107,255,95,228,75,175,172,47,253,199,254,154,242,25,218,254,78,220,234,215,72,255,48,250,236,169,190,255,27,211,191,207,233,231,239,75,255,31,113,39,191,49,8,246,107,236,163,205,175,105,214,108,157,239,251,27,155,206,254,127,246,104,76,109,199,249,235,184,175,222,235,233,54,190,252,154,190,169,248,118,191,209,31,244,107,88,191,240,55,166,185,55,191,255,38,196,127,230,119,248,12,214,119,36,126,151,223,127,93,230,55,249,253,215,102,30,150,223,161,19,127,125,253,29,62,202,111,224,189,75,176,254,35,243,46,209,65,127,135,172,254,250,250,59,244,128,249,253,215,166,190,204,239,191,14,245,101,126,255,117,209,215,127,36,182,159,237,198,159,36,188,12,91,255,21,219,105,200,164,252,141,62,255,179,63,9,60,237,62,99,93,108,63,19,159,150,101,153,63,19,63,229,215,252,143,32,23,208,129,242,247,175,197,127,255,38,246,239,95,135,255,254,77,237,223,191,1,255,253,155,253,26,226,251,169,63,203,120,169,125,39,156,191,250,147,240,221,175,45,50,199,56,138,125,32,95,91,245,6,190,87,223,192,124,255,31,145,237,248,147,160,147,156,93,16,120,191,62,193,115,182,195,193,192,103,160,225,175,223,121,207,248,216,191,129,190,135,191,127,3,125,207,208,2,244,254,13,244,61,67,139,95,79,105,241,107,232,88,127,61,165,197,175,105,255,22,90,252,90,246,111,161,5,108,40,244,19,230,13,127,147,253,249,139,133,54,191,246,127,4,191,220,104,21,168,143,31,197,235,255,255,124,126,20,175,255,40,94,255,81,188,254,163,120,253,71,241,250,143,226,245,31,197,235,63,138,215,127,20,175,187,239,126,77,29,231,55,25,175,211,103,65,188,190,79,157,75,188,254,15,253,218,198,151,254,103,127,19,249,140,0,91,95,250,31,251,77,54,199,235,255,22,96,253,154,63,239,226,117,172,181,219,113,254,91,55,190,241,163,231,71,207,143,158,31,61,63,122,126,244,252,232,249,209,243,163,231,71,207,143,158,31,61,63,122,190,201,231,235,174,119,254,4,229,234,255,243,95,227,55,253,139,126,141,95,227,247,252,61,125,120,154,51,255,131,204,58,232,175,203,49,190,91,215,52,235,160,200,29,153,117,80,228,114,205,58,168,196,241,242,59,114,0,102,29,84,97,126,99,235,157,146,27,248,77,255,160,255,156,243,226,255,217,159,52,230,62,126,211,191,233,215,144,252,197,175,131,239,126,93,90,219,251,141,211,255,140,198,248,213,223,68,31,252,186,248,76,254,254,53,254,38,250,227,79,66,219,240,243,191,230,111,146,188,117,247,243,255,251,111,66,110,30,159,75,222,23,249,139,255,236,79,50,125,253,90,218,215,111,226,245,133,207,126,19,175,47,205,191,120,159,75,95,253,207,165,175,95,203,246,245,107,113,95,154,151,249,117,144,35,71,95,191,169,215,23,62,251,77,189,190,52,175,227,125,46,125,245,63,151,190,126,109,219,215,175,195,125,153,28,149,230,98,254,36,201,125,255,154,255,145,172,139,186,53,224,95,147,113,114,107,192,178,174,235,214,128,101,93,86,242,114,191,14,207,179,208,76,254,254,181,248,111,147,135,254,117,168,239,95,75,251,198,223,148,95,250,143,126,29,254,93,222,255,181,168,63,201,187,155,191,127,29,254,59,241,224,255,218,30,124,228,208,126,157,78,127,191,182,215,31,225,207,223,251,253,255,218,94,255,191,6,175,63,185,191,127,237,14,62,191,118,7,159,95,187,143,207,127,244,107,120,237,127,29,93,39,54,237,169,63,254,27,237,205,218,2,232,245,27,218,191,127,77,254,251,55,176,127,255,90,252,247,143,217,191,127,29,254,219,188,255,179,177,46,141,53,3,151,61,251,209,186,244,255,95,159,31,173,75,255,104,93,250,71,235,210,63,90,151,254,209,186,244,143,214,165,127,180,46,253,163,117,233,31,173,75,187,239,126,77,29,231,55,185,46,141,199,95,151,254,125,9,154,172,75,127,101,125,233,223,229,215,148,207,240,227,119,146,102,233,111,71,127,236,252,26,220,25,175,75,31,208,207,111,255,26,63,127,214,160,191,110,174,67,253,173,63,232,235,230,52,244,221,111,44,119,33,54,251,215,252,143,216,134,106,60,246,107,72,188,246,39,249,241,244,175,193,126,150,139,167,241,183,137,167,17,31,147,76,252,73,126,124,140,191,253,248,24,127,39,86,255,252,154,255,209,175,237,181,71,252,250,107,123,237,17,191,254,218,94,123,196,175,191,142,215,30,241,235,175,227,181,71,252,250,235,104,123,227,131,0,63,63,126,253,53,244,125,29,31,255,237,199,175,191,134,247,254,207,102,252,250,255,4,0,0,255,255};
			}
		}
#endif
		/// <summary>Set a shader sampler of type 'TextureSamplerState' by global unique ID, see <see cref="Xen.Graphics.ShaderSystem.ShaderSystemBase.GetNameUniqueID"/> for details.</summary><param name="state"/><param name="id"/><param name="value"/>
		protected override bool SetSamplerState(Xen.Graphics.ShaderSystem.ShaderSystemBase state, int id, Xen.Graphics.TextureSamplerState value)
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
		protected override bool SetTexture(Xen.Graphics.ShaderSystem.ShaderSystemBase state, int id, Microsoft.Xna.Framework.Graphics.Texture2D value)
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
