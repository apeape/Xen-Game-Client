// XenFX
// Assembly = Xen.Graphics.ShaderSystem.CustomTool, Version=7.0.1.1, Culture=neutral, PublicKeyToken=e706afd07878dfca
// SourceFile = Character.fx
// Namespace = GameClient.Shaders

namespace GameClient.Shaders
{
	
	/// <summary><para>Technique 'Character' generated from file 'Character.fx'</para><para>Vertex Shader: approximately 23 instruction slots used, 13 registers</para><para>Pixel Shader: approximately 148 instruction slots used (12 texture, 136 arithmetic), 17 registers</para></summary>
	[System.Diagnostics.DebuggerStepThroughAttribute()]
	[System.CodeDom.Compiler.GeneratedCodeAttribute("Xen.Graphics.ShaderSystem.CustomTool.dll", "5b4ea50c-b2cb-4045-9622-2f93479d1c42")]
	public sealed class Character : Xen.Graphics.ShaderSystem.BaseShader
	{
		/// <summary>Construct an instance of the 'Character' shader</summary>
		public Character()
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
			Character.gd = state.DeviceUniqueIndex;
			this.GraphicsID = state.DeviceUniqueIndex;
			Character.gid0 = state.GetGlobalUniqueID<Microsoft.Xna.Framework.Matrix>("ShadowMapProjection");
			Character.gid1 = state.GetGlobalUniqueID<Microsoft.Xna.Framework.Vector3>("AmbientDiffuseSpecularScale");
			Character.gid2 = state.GetGlobalUniqueID<Microsoft.Xna.Framework.Vector4[]>("EnvironmentSH");
			Character.gid3 = state.GetGlobalUniqueID<Microsoft.Xna.Framework.Vector2>("RgbmImageRenderScale");
			Character.gid4 = state.GetGlobalUniqueID<Microsoft.Xna.Framework.Vector2>("ShadowMapSize");
			Character.gid5 = state.GetGlobalUniqueID<Microsoft.Xna.Framework.Vector3>("SkinLightScatter");
			Character.gid6 = state.GetGlobalUniqueID<Microsoft.Xna.Framework.Vector3>("SunDirection");
			Character.gid7 = state.GetGlobalUniqueID<Microsoft.Xna.Framework.Vector4>("SunRgbIntensity");
			Character.gid8 = state.GetGlobalUniqueID<Microsoft.Xna.Framework.Vector2>("SunSpecularPowerIntensity");
			Character.gid9 = state.GetGlobalUniqueID<Microsoft.Xna.Framework.Vector3>("UseAlbedoOcclusionShadow");
			Character.sid0 = state.GetNameUniqueID("AlbedoSampler");
			Character.sid1 = state.GetNameUniqueID("CubeRgbmSampler");
			Character.sid2 = state.GetNameUniqueID("NormalSampler");
			Character.sid3 = state.GetNameUniqueID("ShadowSampler");
			Character.sid4 = state.GetNameUniqueID("SofSampler");
			Character.tid0 = state.GetGlobalUniqueID<Microsoft.Xna.Framework.Graphics.TextureCube>("CubeRgbmTexture");
			Character.tid1 = state.GetGlobalUniqueID<Microsoft.Xna.Framework.Graphics.Texture2D>("ShadowTexture");
			Character.tid2 = state.GetNameUniqueID("SofTexture");
			Character.tid3 = state.GetNameUniqueID("AlbedoTexture");
			Character.tid4 = state.GetNameUniqueID("NormalTexture");
		}
		/// <summary>Bind the shader, 'ic' indicates the shader instance has changed and 'ec' indicates the extension has changed.</summary><param name="state"/><param name="ic"/><param name="ec"/><param name="ext"/>
		protected override void BeginImpl(Xen.Graphics.ShaderSystem.ShaderSystemBase state, bool ic, bool ec, Xen.Graphics.ShaderSystem.ShaderExtension ext)
		{
			// if the device changed, call Warm()
			if ((state.DeviceUniqueIndex != Character.gd))
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
			this.vreg_change = (this.vreg_change | state.SetGlobalMatrix4(ref this.vreg[0], ref this.vreg[1], ref this.vreg[2], ref this.vreg[3], Character.gid0, ref this.gc0));
			// Set the value for global 'AmbientDiffuseSpecularScale'
			this.preg_change = (this.preg_change | state.SetGlobalVector3(ref this.preg[15], Character.gid1, ref this.gc1));
			// Set the value for global 'EnvironmentSH'
			this.preg_change = (this.preg_change | state.SetGlobalVector4(this.preg, 0, 9, Character.gid2, ref this.gc2));
			// Set the value for global 'RgbmImageRenderScale'
			this.preg_change = (this.preg_change | state.SetGlobalVector2(ref this.preg[9], Character.gid3, ref this.gc3));
			// Set the value for global 'ShadowMapSize'
			this.preg_change = (this.preg_change | state.SetGlobalVector2(ref this.preg[10], Character.gid4, ref this.gc4));
			// Set the value for global 'SkinLightScatter'
			this.preg_change = (this.preg_change | state.SetGlobalVector3(ref this.preg[11], Character.gid5, ref this.gc5));
			// Set the value for global 'SunDirection'
			this.preg_change = (this.preg_change | state.SetGlobalVector3(ref this.preg[13], Character.gid6, ref this.gc6));
			// Set the value for global 'SunRgbIntensity'
			this.preg_change = (this.preg_change | state.SetGlobalVector4(ref this.preg[12], Character.gid7, ref this.gc7));
			// Set the value for global 'SunSpecularPowerIntensity'
			this.preg_change = (this.preg_change | state.SetGlobalVector2(ref this.preg[14], Character.gid8, ref this.gc8));
			// Set the value for global 'UseAlbedoOcclusionShadow'
			this.preg_change = (this.preg_change | state.SetGlobalVector3(ref this.preg[16], Character.gid9, ref this.gc9));
			// Assign global textures
			this.CubeRgbmTexture = state.GetGlobalTextureCube(Character.tid0);
			this.ShadowTexture = state.GetGlobalTexture2D(Character.tid1);
			// Assign pixel shader textures and samplers
			if ((ic | this.ptc))
			{
				state.SetPixelShaderSamplers(this.ptx, this.pts);
				this.ptc = false;
			}
			if ((this.vreg_change == true))
			{
				Character.fx.vs_c.SetValue(this.vreg);
				this.vreg_change = false;
				ic = true;
			}
			if ((this.preg_change == true))
			{
				Character.fx.ps_c.SetValue(this.preg);
				this.preg_change = false;
				ic = true;
			}
			if ((ext == Xen.Graphics.ShaderSystem.ShaderExtension.Blending))
			{
				ic = (ic | state.SetBlendMatricesDirect(Character.fx.vsb_c, ref this.sc3));
			}
			if ((ext == Xen.Graphics.ShaderSystem.ShaderExtension.Instancing))
			{
				this.vireg_change = (this.vireg_change | state.SetViewProjectionMatrix(ref this.vireg[0], ref this.vireg[1], ref this.vireg[2], ref this.vireg[3], ref this.sc4));
				if ((this.vireg_change == true))
				{
					Character.fx.vsi_c.SetValue(this.vireg);
					this.vireg_change = false;
					ic = true;
				}
			}
			// Finally, bind the effect
			if ((ic | ec))
			{
				state.SetEffect(this, ref Character.fx, ext);
			}
		}
		/// <summary>Warm (Preload) the shader</summary><param name="state"/>
		protected override void WarmShader(Xen.Graphics.ShaderSystem.ShaderSystemBase state)
		{
			// Shader is already warmed
			if ((Character.gd == state.DeviceUniqueIndex))
			{
				return;
			}
			// Setup the shader
			if ((Character.gd != state.DeviceUniqueIndex))
			{
				this.gdInit(state);
			}
			Character.fx.Dispose();
			// Create the effect instance
			state.CreateEffect(out Character.fx, Character.fxb, 35, 146);
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
			usage = ((Microsoft.Xna.Framework.Graphics.VertexElementUsage)(Character.vin[i]));
			index = Character.vin[(i + 5)];
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
		/// <summary>TypeID for global attribute 'float3 UseAlbedoOcclusionShadow'</summary>
		private static int gid9;
		/// <summary>Change ID for global attribute 'float3 UseAlbedoOcclusionShadow'</summary>
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
				return new byte[] {36,80,0,0,236,189,7,96,28,73,150,37,38,47,109,202,123,127,74,245,74,215,224,116,161,8,128,96,19,36,216,144,64,16,236,193,136,205,230,146,236,29,105,71,35,41,171,42,129,202,101,86,101,93,102,22,64,204,237,157,188,247,222,123,239,189,247,222,123,239,189,247,186,59,157,78,39,247,223,255,63,92,102,100,1,108,246,206,74,218,201,158,33,128,170,200,31,63,126,124,31,63,34,254,197,223,240,127,250,251,210,95,99,248,249,53,127,236,255,254,191,254,190,223,92,126,255,181,241,55,253,255,127,210,239,126,99,250,255,175,163,159,253,255,237,249,245,232,255,191,255,101,243,251,79,127,13,55,238,173,95,75,190,3,57,254,255,58,238,15,125,152,110,171,144,110,255,210,111,42,223,253,91,191,198,143,232,246,163,231,71,207,143,158,31,61,63,122,126,244,252,232,249,209,243,163,231,71,207,143,158,31,61,63,236,231,215,255,53,56,190,157,32,80,51,113,218,193,111,38,223,253,58,250,255,111,34,78,211,126,10,234,231,119,251,77,126,13,129,251,251,254,102,145,54,20,51,54,59,191,198,175,245,27,105,155,63,104,168,205,238,175,241,227,166,205,95,54,212,102,239,215,248,53,76,155,127,106,168,205,189,95,227,15,50,109,254,187,161,54,251,191,198,175,5,26,224,235,95,71,190,178,15,82,1,8,107,187,159,51,205,254,111,161,233,111,22,249,30,127,199,222,251,49,250,255,147,50,95,206,138,229,197,175,113,242,107,252,26,191,238,192,251,136,175,99,239,255,134,244,255,179,101,211,102,203,41,32,240,24,94,207,179,89,94,11,108,224,5,250,227,115,188,155,122,239,254,31,244,255,223,206,155,236,125,26,220,75,205,117,224,249,247,168,195,255,77,99,120,60,79,127,179,95,227,215,248,189,127,51,247,247,156,126,255,25,239,239,63,142,126,255,139,188,191,255,38,250,253,31,243,254,254,215,232,247,255,202,255,222,203,47,109,233,239,232,254,79,209,239,255,15,106,251,191,209,255,255,84,253,251,183,162,54,191,25,253,255,15,136,180,221,167,207,118,126,115,215,246,57,253,254,109,250,255,95,20,105,219,210,103,43,175,237,31,71,191,255,81,218,238,215,147,31,220,254,255,166,199,144,103,31,132,254,181,255,239,255,251,255,250,191,255,188,95,227,228,205,241,147,223,137,254,60,252,181,228,51,188,243,59,73,179,20,52,252,211,244,253,223,156,254,253,243,232,231,95,71,255,255,251,126,77,35,111,191,214,175,241,143,41,208,127,141,63,251,53,233,191,95,239,215,248,247,244,179,255,142,63,251,181,232,179,228,215,248,229,250,217,175,243,107,225,51,180,252,77,126,141,223,72,231,231,119,226,207,126,29,250,236,183,248,53,126,55,253,76,242,47,127,237,95,251,107,114,219,95,231,215,208,33,253,232,233,60,170,115,254,218,95,135,232,249,107,242,127,222,231,187,248,252,55,234,127,190,55,240,249,189,129,207,247,251,159,211,199,247,126,255,157,95,227,139,98,90,87,77,117,222,166,91,175,238,164,223,126,254,250,121,42,18,155,158,84,139,85,81,210,47,15,199,123,159,142,31,222,223,27,239,29,236,239,255,26,63,65,106,225,55,255,53,126,211,191,136,64,252,30,164,33,233,255,191,198,63,8,120,248,252,183,224,207,87,199,191,198,175,241,123,226,187,163,191,246,175,249,107,254,1,124,254,91,74,251,223,243,215,248,53,254,192,147,63,248,15,250,131,14,209,254,119,33,182,36,253,242,7,17,231,252,73,242,251,175,249,7,253,154,191,198,175,175,191,255,90,127,208,175,101,127,255,181,255,160,95,219,254,254,235,252,65,191,142,253,253,215,253,131,126,221,95,227,55,229,223,9,220,159,245,107,252,6,191,233,95,164,191,255,73,191,166,247,251,175,229,253,254,107,123,191,255,58,244,251,19,22,131,223,148,112,248,207,168,221,127,246,23,253,186,252,247,175,73,127,255,26,127,208,111,250,107,252,53,127,17,179,53,245,249,107,252,26,127,205,31,12,241,253,113,126,247,55,248,131,126,179,95,227,171,191,232,206,175,241,235,254,90,191,153,168,152,191,24,112,126,77,133,243,107,43,28,26,11,141,231,63,227,255,127,235,215,96,221,247,107,225,179,223,156,222,253,132,254,254,189,89,5,254,90,244,206,255,253,7,227,179,95,227,215,248,234,15,18,152,127,13,193,252,107,254,226,95,79,251,250,117,127,141,255,251,79,2,188,95,139,232,64,109,233,239,255,236,79,194,119,36,85,127,80,66,253,203,239,191,22,253,254,213,95,244,235,176,120,254,218,68,187,255,140,250,249,175,160,110,120,12,191,14,211,240,63,227,207,129,199,175,69,99,250,205,127,141,255,251,47,250,253,233,59,162,33,125,254,151,253,65,191,38,225,45,223,253,6,68,227,175,254,32,124,247,235,241,119,127,157,253,142,120,136,254,254,211,248,187,95,159,190,251,117,126,141,255,141,191,251,253,185,15,252,253,203,249,239,95,139,199,255,27,80,159,191,6,253,255,171,63,8,120,201,88,254,154,63,9,99,248,53,105,204,128,135,121,38,252,255,32,249,253,215,249,131,126,125,251,59,112,248,53,254,32,192,249,181,153,174,60,238,63,88,230,227,55,224,177,98,236,191,174,246,139,177,73,27,252,253,187,208,187,24,191,124,255,107,211,223,248,254,183,208,191,13,125,146,95,227,63,251,139,126,75,30,175,252,237,211,238,183,32,248,191,133,206,255,175,197,99,126,202,115,36,239,131,38,87,244,255,228,15,250,49,197,71,222,255,207,248,239,95,83,255,22,122,203,247,102,142,13,93,126,77,253,251,215,117,127,243,255,13,159,224,29,124,47,60,243,27,88,158,121,162,253,131,207,126,29,165,51,201,137,206,247,95,163,227,197,223,95,161,255,63,73,198,244,27,2,15,204,241,159,244,107,211,60,203,103,104,243,215,128,55,255,164,95,139,230,236,119,99,30,18,56,191,1,195,252,53,49,158,63,201,140,1,127,11,140,95,195,190,175,223,253,193,104,135,247,127,29,125,95,120,136,121,159,249,241,215,213,207,49,159,191,70,248,217,95,4,62,251,117,149,246,191,174,192,164,207,254,26,254,12,99,249,245,232,179,95,231,215,248,175,233,255,255,152,109,67,253,129,231,131,247,126,93,254,44,132,245,235,241,103,127,141,253,76,248,250,175,225,255,255,22,196,251,166,221,175,255,107,128,142,255,183,237,211,240,255,175,163,124,43,127,243,184,9,231,255,251,15,54,239,253,6,222,123,191,161,226,138,223,33,87,152,167,95,151,229,129,248,82,245,1,224,144,44,131,71,248,111,197,251,15,18,24,238,111,252,255,247,214,62,126,13,197,255,215,83,220,228,51,55,118,51,247,191,182,71,91,253,236,47,130,220,57,190,22,218,254,218,4,239,215,82,88,191,30,127,246,95,211,255,255,49,219,198,208,214,127,207,208,214,255,204,208,214,125,6,89,255,107,248,255,134,182,104,103,104,107,250,148,118,95,241,255,221,123,191,6,255,223,208,22,239,25,218,250,184,226,247,223,84,105,247,235,211,223,191,17,181,193,255,205,223,212,23,125,246,127,255,69,191,129,237,231,63,251,131,126,99,250,30,127,255,218,191,198,111,206,115,241,27,107,123,232,75,211,111,250,107,64,103,252,230,252,251,111,98,117,6,248,31,127,127,245,23,97,46,126,109,182,9,127,141,190,3,90,8,12,224,248,91,218,119,126,45,254,238,55,85,185,54,127,19,61,254,98,249,251,55,225,191,127,195,95,227,79,82,152,191,9,195,4,191,0,87,145,155,223,206,242,131,177,67,232,19,116,249,53,88,6,255,111,134,47,60,32,178,14,189,241,107,255,26,191,138,255,134,12,194,142,128,95,126,111,165,165,209,77,191,185,55,47,224,159,95,195,155,211,95,67,249,234,215,96,88,254,103,255,25,211,213,204,195,175,193,253,253,103,252,57,120,254,215,100,28,191,226,113,224,239,95,75,113,166,177,252,65,198,126,253,152,210,67,244,164,244,97,254,254,53,152,126,95,177,30,22,88,255,183,213,241,98,39,100,156,191,166,109,251,215,88,88,24,55,96,155,126,12,220,95,83,241,214,247,137,21,126,23,198,249,255,254,191,127,13,126,168,49,251,210,191,134,62,207,105,164,191,198,175,253,127,145,47,189,178,190,244,31,251,107,202,103,104,251,59,113,171,95,35,253,195,232,179,167,250,254,111,76,255,62,167,159,191,47,253,127,196,157,252,198,32,216,175,177,143,54,191,166,89,179,117,190,239,111,108,58,251,255,217,163,49,181,29,231,175,227,190,122,175,167,219,248,242,107,250,166,226,219,253,70,127,208,175,97,253,194,223,152,230,222,252,254,155,16,255,153,223,225,51,88,223,145,248,93,126,255,117,153,223,228,247,95,155,121,88,126,135,78,252,245,245,119,248,40,191,129,247,46,193,250,143,204,187,68,7,253,29,178,250,235,235,239,208,3,230,247,95,155,250,50,191,255,58,212,151,249,253,215,69,95,255,145,216,126,182,27,127,146,240,50,108,253,87,108,167,33,147,242,55,250,252,207,254,36,240,180,251,140,117,177,253,76,124,90,150,101,254,76,252,148,95,243,63,130,92,64,7,202,223,191,22,255,253,155,216,191,127,29,254,251,55,181,127,255,6,252,247,111,246,107,136,239,167,254,44,227,165,246,157,112,254,234,79,194,119,191,182,200,28,227,40,246,129,124,109,213,27,248,94,125,3,243,253,127,68,182,227,79,130,78,114,118,65,224,253,250,4,207,217,14,7,3,159,129,134,191,126,231,61,227,99,255,6,250,30,254,254,13,244,61,67,11,208,251,55,208,247,12,45,126,61,165,197,175,161,99,253,245,148,22,191,166,253,91,104,241,107,217,191,133,22,176,161,208,79,152,55,252,77,246,231,47,22,218,252,218,255,17,252,114,163,85,160,62,126,20,175,255,255,243,249,81,188,254,163,120,253,71,241,250,143,226,245,31,197,235,63,138,215,127,20,175,255,40,94,255,81,188,238,190,251,53,117,156,223,100,188,78,159,5,241,250,62,117,46,241,250,63,244,107,27,95,250,159,253,77,228,51,2,108,125,233,127,236,55,217,28,175,255,91,128,245,107,254,188,139,215,177,214,110,199,249,111,221,248,198,143,158,31,61,63,122,126,244,252,232,249,209,243,163,231,71,207,143,158,31,61,63,122,126,244,252,232,249,38,159,175,187,222,249,19,148,171,255,207,127,141,223,244,47,250,53,126,141,223,243,247,244,225,105,206,252,15,50,235,160,191,46,199,248,110,93,211,172,131,34,119,100,214,65,145,203,53,235,160,18,199,203,239,200,1,152,117,80,133,249,141,173,119,74,110,224,55,253,131,254,115,206,139,255,103,127,210,152,251,248,77,255,166,95,67,242,23,191,14,190,251,117,105,109,239,55,78,255,51,26,227,87,127,19,125,240,235,226,51,249,251,215,248,155,232,143,63,9,109,195,207,255,154,191,73,242,214,221,207,255,239,191,9,185,121,124,46,121,95,228,47,254,179,63,201,244,245,107,105,95,191,137,215,23,62,251,77,188,190,52,255,226,125,46,125,245,63,151,190,126,45,219,215,175,197,125,105,94,230,215,65,142,28,125,253,166,94,95,248,236,55,245,250,210,188,142,247,185,244,213,255,92,250,250,181,109,95,191,14,247,101,114,84,154,139,249,147,36,247,253,107,254,71,178,46,234,214,128,127,77,198,201,173,1,203,186,174,91,3,150,117,89,201,203,253,58,60,207,66,51,249,251,215,226,191,77,30,250,215,161,190,127,45,237,27,127,83,126,233,63,250,117,248,119,121,255,215,162,254,36,239,110,254,254,117,248,239,196,131,255,107,123,240,145,67,251,117,58,253,253,218,94,127,132,63,127,239,247,255,107,123,253,255,26,188,254,228,254,254,181,59,248,252,218,29,124,126,237,62,62,255,209,175,225,181,255,117,116,157,216,180,167,254,248,111,180,55,107,11,160,215,111,104,255,254,53,249,239,223,192,254,253,107,241,223,63,102,255,254,117,248,111,243,254,207,198,186,52,214,12,92,246,236,71,235,210,255,127,125,126,180,46,253,163,117,233,31,173,75,255,104,93,250,71,235,210,63,90,151,254,209,186,244,143,214,165,127,180,46,237,190,251,53,117,156,223,228,186,52,30,127,93,250,247,37,104,178,46,253,149,245,165,127,151,95,83,62,195,143,223,73,154,165,191,29,253,177,243,107,112,103,188,46,125,64,63,191,253,107,252,252,89,131,254,186,185,14,245,183,254,160,175,155,211,208,119,191,177,220,133,216,236,95,243,63,98,27,170,241,216,175,33,241,218,159,228,199,211,191,6,251,89,46,158,198,223,38,158,70,124,76,50,241,39,249,241,49,254,246,227,99,252,157,88,253,243,107,254,71,191,182,215,30,241,235,175,237,181,71,252,250,107,123,237,17,191,254,58,94,123,196,175,191,142,215,30,241,235,175,163,237,141,15,2,252,252,248,245,215,208,247,117,124,252,183,31,191,254,26,222,251,63,155,241,235,255,19,0,0,255,255};
			}
		}
#endif
		/// <summary>Set a shader sampler of type 'TextureSamplerState' by global unique ID, see <see cref="Xen.Graphics.ShaderSystem.ShaderSystemBase.GetNameUniqueID"/> for details.</summary><param name="state"/><param name="id"/><param name="value"/>
		protected override bool SetSamplerStateImpl(Xen.Graphics.ShaderSystem.ShaderSystemBase state, int id, Xen.Graphics.TextureSamplerState value)
		{
			if ((Character.gd != state.DeviceUniqueIndex))
			{
				this.WarmShader(state);
			}
			if ((id == Character.sid0))
			{
				this.AlbedoSampler = value;
				return true;
			}
			if ((id == Character.sid1))
			{
				this.CubeRgbmSampler = value;
				return true;
			}
			if ((id == Character.sid2))
			{
				this.NormalSampler = value;
				return true;
			}
			if ((id == Character.sid3))
			{
				this.ShadowSampler = value;
				return true;
			}
			if ((id == Character.sid4))
			{
				this.SofSampler = value;
				return true;
			}
			return false;
		}
		/// <summary>Set a shader texture of type 'Texture2D' by global unique ID, see <see cref="Xen.Graphics.ShaderSystem.ShaderSystemBase.GetNameUniqueID"/> for details.</summary><param name="state"/><param name="id"/><param name="value"/>
		protected override bool SetTextureImpl(Xen.Graphics.ShaderSystem.ShaderSystemBase state, int id, Microsoft.Xna.Framework.Graphics.Texture2D value)
		{
			if ((Character.gd != state.DeviceUniqueIndex))
			{
				this.WarmShader(state);
			}
			if ((id == Character.tid2))
			{
				this.SofTexture = value;
				return true;
			}
			if ((id == Character.tid3))
			{
				this.AlbedoTexture = value;
				return true;
			}
			if ((id == Character.tid4))
			{
				this.NormalTexture = value;
				return true;
			}
			return false;
		}
	}
}
