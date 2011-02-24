// XenFX
// Assembly = Xen.Graphics.ShaderSystem.CustomTool, Version=7.0.1.1, Culture=neutral, PublicKeyToken=e706afd07878dfca
// SourceFile = ShadowShader.fx
// Namespace = GameClient.Shaders

namespace GameClient.Shaders
{
	
	/// <summary><para>Technique 'ShadowShader' generated from file 'ShadowShader.fx'</para><para>Vertex Shader: approximately 21 instruction slots used, 14 registers</para><para>Pixel Shader: approximately 13 instruction slots used (2 texture, 11 arithmetic), 0 registers</para></summary>
	[System.Diagnostics.DebuggerStepThroughAttribute()]
	[System.CodeDom.Compiler.GeneratedCodeAttribute("Xen.Graphics.ShaderSystem.CustomTool.dll", "5b4ea50c-b2cb-4045-9622-2f93479d1c42")]
	public sealed class ShadowShader : Xen.Graphics.ShaderSystem.BaseShader
	{
		/// <summary>Construct an instance of the 'ShadowShader' shader</summary>
		public ShadowShader()
		{
			this.sc0 = -1;
			this.sc1 = -1;
			this.sc2 = -1;
			this.sc3 = -1;
			this.pts[0] = ((Xen.Graphics.TextureSamplerState)(197));
			this.pts[1] = ((Xen.Graphics.TextureSamplerState)(197));
		}
		/// <summary>Setup shader static values</summary><param name="state"/>
		private void gdInit(Xen.Graphics.ShaderSystem.ShaderSystemBase state)
		{
			// set the graphics ID
			ShadowShader.gd = state.DeviceUniqueIndex;
			this.GraphicsID = state.DeviceUniqueIndex;
			ShadowShader.cid0 = state.GetNameUniqueID("lightColour");
			ShadowShader.cid1 = state.GetNameUniqueID("shadowMapProjection");
			ShadowShader.cid2 = state.GetNameUniqueID("shadowViewDirection");
			ShadowShader.sid0 = state.GetNameUniqueID("ShadowSampler");
			ShadowShader.sid1 = state.GetNameUniqueID("TextureSampler");
			ShadowShader.tid0 = state.GetNameUniqueID("ShadowMap");
			ShadowShader.tid1 = state.GetNameUniqueID("TextureMap");
		}
		/// <summary>Bind the shader, 'ic' indicates the shader instance has changed and 'ec' indicates the extension has changed.</summary><param name="state"/><param name="ic"/><param name="ec"/><param name="ext"/>
		protected override void BeginImpl(Xen.Graphics.ShaderSystem.ShaderSystemBase state, bool ic, bool ec, Xen.Graphics.ShaderSystem.ShaderExtension ext)
		{
			// if the device changed, call Warm()
			if ((state.DeviceUniqueIndex != ShadowShader.gd))
			{
				this.WarmShader(state);
				ic = true;
			}
			// Force updating if the instance has changed
			this.vreg_change = (this.vreg_change | ic);
			this.vbreg_change = (this.vbreg_change | ic);
			this.vireg_change = (this.vireg_change | ic);
			// Set the value for attribute 'worldMatrix'
			this.vreg_change = (this.vreg_change | state.SetWorldMatrix(ref this.vreg[8], ref this.vreg[9], ref this.vreg[10], ref this.vreg[11], ref this.sc0));
			// Set the value for attribute 'worldViewProjection'
			this.vreg_change = (this.vreg_change | state.SetWorldViewProjectionMatrix(ref this.vreg[0], ref this.vreg[1], ref this.vreg[2], ref this.vreg[3], ref this.sc1));
			// Assign pixel shader textures and samplers
			if ((ic | this.ptc))
			{
				state.SetPixelShaderSamplers(this.ptx, this.pts);
				this.ptc = false;
			}
			if ((this.vreg_change == true))
			{
				ShadowShader.fx.vs_c.SetValue(this.vreg);
				this.vreg_change = false;
				ic = true;
			}
			if ((ext == Xen.Graphics.ShaderSystem.ShaderExtension.Blending))
			{
				ic = (ic | state.SetBlendMatricesDirect(ShadowShader.fx.vsb_c, ref this.sc2));
			}
			if ((ext == Xen.Graphics.ShaderSystem.ShaderExtension.Instancing))
			{
				this.vireg_change = (this.vireg_change | state.SetViewProjectionMatrix(ref this.vireg[0], ref this.vireg[1], ref this.vireg[2], ref this.vireg[3], ref this.sc3));
				if ((this.vireg_change == true))
				{
					ShadowShader.fx.vsi_c.SetValue(this.vireg);
					this.vireg_change = false;
					ic = true;
				}
			}
			// Finally, bind the effect
			if ((ic | ec))
			{
				state.SetEffect(this, ref ShadowShader.fx, ext);
			}
		}
		/// <summary>Warm (Preload) the shader</summary><param name="state"/>
		protected override void WarmShader(Xen.Graphics.ShaderSystem.ShaderSystemBase state)
		{
			// Shader is already warmed
			if ((ShadowShader.gd == state.DeviceUniqueIndex))
			{
				return;
			}
			// Setup the shader
			if ((ShadowShader.gd != state.DeviceUniqueIndex))
			{
				this.gdInit(state);
			}
			ShadowShader.fx.Dispose();
			// Create the effect instance
			state.CreateEffect(out ShadowShader.fx, ShadowShader.fxb, 22, 20);
		}
		/// <summary>True if a shader constant has changed since the last Bind()</summary>
		protected override bool Changed()
		{
			return (this.vreg_change | this.ptc);
		}
		/// <summary>Returns the number of vertex inputs used by this shader</summary>
		protected override int GetVertexInputCountImpl()
		{
			return 3;
		}
		/// <summary>Returns a vertex input used by this shader</summary><param name="i"/><param name="usage"/><param name="index"/>
		protected override void GetVertexInputImpl(int i, out Microsoft.Xna.Framework.Graphics.VertexElementUsage usage, out int index)
		{
			usage = ((Microsoft.Xna.Framework.Graphics.VertexElementUsage)(ShadowShader.vin[i]));
			index = ShadowShader.vin[(i + 3)];
		}
		/// <summary>Static graphics ID</summary>
		private static int gd;
		/// <summary>Static effect container instance</summary>
		private static Xen.Graphics.ShaderSystem.ShaderEffect fx;
		/// <summary/>
		private bool vreg_change;
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
		/// <summary>Name ID for 'lightColour'</summary>
		private static int cid0;
		/// <summary>Set the shader value 'float3 lightColour'</summary><param name="value"/>
		public void SetLightColour(ref Microsoft.Xna.Framework.Vector3 value)
		{
			this.vreg[13] = new Microsoft.Xna.Framework.Vector4(value.X, value.Y, value.Z, 0F);
			this.vreg_change = true;
		}
		/// <summary>Assign the shader value 'float3 lightColour'</summary>
		public Microsoft.Xna.Framework.Vector3 LightColour
		{
			set
			{
				this.SetLightColour(ref value);
			}
		}
		/// <summary>Name ID for 'shadowMapProjection'</summary>
		private static int cid1;
		/// <summary>Set the shader value 'float4x4 shadowMapProjection'</summary><param name="value"/>
		public void SetShadowMapProjection(ref Microsoft.Xna.Framework.Matrix value)
		{
			this.vreg[4] = new Microsoft.Xna.Framework.Vector4(value.M11, value.M21, value.M31, value.M41);
			this.vreg[5] = new Microsoft.Xna.Framework.Vector4(value.M12, value.M22, value.M32, value.M42);
			this.vreg[6] = new Microsoft.Xna.Framework.Vector4(value.M13, value.M23, value.M33, value.M43);
			this.vreg[7] = new Microsoft.Xna.Framework.Vector4(value.M14, value.M24, value.M34, value.M44);
			this.vreg_change = true;
		}
		/// <summary>Assign the shader value 'float4x4 shadowMapProjection'</summary>
		public Microsoft.Xna.Framework.Matrix ShadowMapProjection
		{
			set
			{
				this.SetShadowMapProjection(ref value);
			}
		}
		/// <summary>Name ID for 'shadowViewDirection'</summary>
		private static int cid2;
		/// <summary>Set the shader value 'float3 shadowViewDirection'</summary><param name="value"/>
		public void SetShadowViewDirection(ref Microsoft.Xna.Framework.Vector3 value)
		{
			this.vreg[12] = new Microsoft.Xna.Framework.Vector4(value.X, value.Y, value.Z, 0F);
			this.vreg_change = true;
		}
		/// <summary>Assign the shader value 'float3 shadowViewDirection'</summary>
		public Microsoft.Xna.Framework.Vector3 ShadowViewDirection
		{
			set
			{
				this.SetShadowViewDirection(ref value);
			}
		}
		/// <summary>Change ID for Semantic bound attribute 'worldMatrix'</summary>
		private int sc0;
		/// <summary>Change ID for Semantic bound attribute 'worldViewProjection'</summary>
		private int sc1;
		/// <summary>Change ID for Semantic bound attribute '__BLENDMATRICES__GENMATRIX'</summary>
		private int sc2;
		/// <summary>Change ID for Semantic bound attribute 'viewProjection'</summary>
		private int sc3;
		/// <summary>Get/Set the Texture Sampler State for 'Sampler2D ShadowSampler'</summary>
		public Xen.Graphics.TextureSamplerState ShadowSampler
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
		/// <summary>Get/Set the Texture Sampler State for 'Sampler2D TextureSampler'</summary>
		public Xen.Graphics.TextureSamplerState TextureSampler
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
		/// <summary>Get/Set the Bound texture for 'Texture2D ShadowMap'</summary>
		public Microsoft.Xna.Framework.Graphics.Texture2D ShadowMap
		{
			get
			{
				return ((Microsoft.Xna.Framework.Graphics.Texture2D)(this.ptx[0]));
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
		/// <summary>Get/Set the Bound texture for 'Texture2D TextureMap'</summary>
		public Microsoft.Xna.Framework.Graphics.Texture2D TextureMap
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
		/// <summary>Name uid for sampler for 'Sampler2D ShadowSampler'</summary>
		static int sid0;
		/// <summary>Name uid for sampler for 'Sampler2D TextureSampler'</summary>
		static int sid1;
		/// <summary>Name uid for texture for 'Texture2D ShadowMap'</summary>
		static int tid0;
		/// <summary>Name uid for texture for 'Texture2D TextureMap'</summary>
		static int tid1;
		/// <summary>Pixel samplers/textures changed</summary>
		bool ptc;
		/// <summary>array storing vertex usages, and element indices</summary>
readonly 
		private static int[] vin = new int[] {0,2,3,0,0,0};
		/// <summary>Vertex shader register storage</summary>
readonly 
		private Microsoft.Xna.Framework.Vector4[] vreg = new Microsoft.Xna.Framework.Vector4[14];
		/// <summary>Instancing shader register storage</summary>
readonly 
		private Microsoft.Xna.Framework.Vector4[] vireg = new Microsoft.Xna.Framework.Vector4[4];
		/// <summary>Bound pixel textures</summary>
readonly 
		Microsoft.Xna.Framework.Graphics.Texture[] ptx = new Microsoft.Xna.Framework.Graphics.Texture[2];
		/// <summary>Bound pixel samplers</summary>
readonly 
		Xen.Graphics.TextureSamplerState[] pts = new Xen.Graphics.TextureSamplerState[2];
#if XBOX360
		/// <summary>Static RLE compressed shader byte code (Xbox360)</summary>
		private static byte[] fxb
		{
			get
			{
				return new byte[] {4,188,240,11,207,131,0,1,32,152,0,8,254,255,9,1,0,0,16,36,135,0,1,3,131,0,4,1,0,0,1,136,0,1,14,131,0,1,4,131,0,1,1,229,0,0,229,0,0,153,0,0,1,6,1,95,1,118,1,115,1,95,1,99,134,0,0,1,3,131,0,0,1,1,1,0,1,0,1,14,1,168,135,0,0,1,216,131,0,0,1,4,131,0,0,1,1,229,0,0,229,0,0,229,0,0,229,0,0,229,0,0,229,0,0,229,0,0,229,0,0,229,0,0,229,0,0,229,0,0,229,0,0,229,0,0,229,0,0,229,0,0,229,0,0,229,0,0,229,0,0,229,0,0,229,0,0,229,0,0,229,0,0,229,0,0,229,0,0,229,0,0,229,0,0,229,0,0,229,0,0,229,0,0,229,0,0,229,0,0,229,0,0,229,0,0,229,0,0,153,0,0,1,7,1,95,1,118,1,115,1,98,1,95,1,99,133,0,0,1,3,131,0,0,1,1,1,0,1,0,1,15,1,16,135,0,0,1,4,131,0,0,1,4,131,0,0,1,1,195,0,0,1,7,1,95,1,118,1,115,1,105,1,95,1,99,133,0,0,1,12,131,0,0,1,4,1,0,1,0,1,15,1,52,143,0,0,1,7,1,95,1,112,1,115,1,95,1,115,1,48,133,0,0,1,12,131,0,0,1,4,1,0,1,0,1,15,1,88,143,0,0,1,7,1,95,1,112,1,115,1,95,1,115,1,49,133,0,0,1,1,131,0,0,1,16,131,0,0,1,4,143,0,0,1,2,131,0,0,1,15,131,0,0,1,4,147,0,0,1,3,131,0,0,1,16,131,0,0,1,4,143,0,0,1,4,131,0,0,1,15,131,0,0,1,4,143,0,0,1,9,1,66,1,108,1,101,1,110,1,100,1,105,1,110,1,103,135,0,0,1,5,131,0,0,1,16,131,0,0,1,4,143,0,0,1,6,131,0,0,1,15,131,0,0,1,4,143,0,0,1,11,1,73,1,110,1,115,1,116,1,97,1,110,1,99,1,105,1,110,1,103,133,0,0,1,7,1,83,1,104,1,97,1,100,1,101,1,114,133,0,0,1,5,131,0,0,1,1,131,0,0,1,11,131,0,0,1,7,131,0,0,1,4,131,0,0,1,32,138,0,0,1,1,1,12,1,0,1,0,1,1,1,40,138,0,0,1,14,1,180,1,0,1,0,1,14,1,208,138,0,0,1,15,1,28,1,0,1,0,1,15,1,48,138,0,0,1,15,1,64,1,0,1,0,1,15,1,84,138,0,0,1,16,1,24,135,0,0,1,3,1,0,1,0,1,15,1,148,135,0,0,1,2,131,0,0,1,92,134,0,0,1,15,1,104,1,0,1,0,1,15,1,100,131,0,0,1,93,134,0,0,1,15,1,128,1,0,1,0,1,15,1,124,1,0,1,0,1,15,1,200,135,0,0,1,2,131,0,0,1,92,134,0,0,1,15,1,156,1,0,1,0,1,15,1,152,131,0,0,1,93,134,0,0,1,15,1,180,1,0,1,0,1,15,1,176,1,0,1,0,1,16,1,8,135,0,0,1,2,131,0,0,1,92,134,0,0,1,15,1,220,1,0,1,0,1,15,1,216,131,0,0,1,93,134,0,0,1,15,1,244,1,0,1,0,1,15,1,240,135,0,0,1,6,135,0,0,1,2,132,255,0,131,0,0,1,1,134,0,0,1,1,1,196,1,16,1,42,1,17,132,0,0,1,244,131,0,0,1,208,135,0,0,1,36,131,0,0,1,160,131,0,0,1,200,139,0,0,1,120,131,0,0,1,28,131,0,0,1,106,1,255,1,255,1,3,132,0,0,1,2,131,0,0,1,28,135,0,0,1,99,131,0,0,1,68,1,0,1,3,131,0,0,1,1,133,0,0,1,76,135,0,0,1,92,1,0,1,3,1,0,1,1,1,0,1,1,133,0,0,1,76,132,0,0,1,95,1,112,1,115,1,95,1,115,1,48,1,0,1,171,1,0,1,4,1,0,1,12,1,0,1,1,1,0,1,1,1,0,1,1,134,0,0,1,95,1,112,1,115,1,95,1,115,1,49,1,0,1,112,1,115,1,95,1,51,1,95,1,48,1,0,1,50,1,46,1,48,1,46,1,49,1,49,1,54,1,50,1,54,1,46,1,48,1,0,1,171,1,171,135,0,0,1,1,139,0,0,1,20,1,1,1,252,1,0,1,16,147,0,0,1,64,131,0,0,1,144,1,16,1,0,1,3,132,0,0,1,4,134,0,0,1,36,1,99,1,0,1,7,1,0,1,7,131,0,0,1,1,1,0,1,0,1,48,1,80,1,0,1,0,1,241,1,81,1,0,1,0,1,114,1,82,160,0,0,1,63,131,0,0,1,191,131,0,0,1,63,1,128,1,0,1,0,1,186,1,131,1,18,1,111,1,67,1,16,1,68,1,254,140,0,0,1,1,1,64,1,80,1,2,1,0,1,0,1,18,1,0,1,196,133,0,0,1,64,1,7,1,0,1,0,1,34,133,0,0,1,76,1,64,133,0,0,1,27,1,226,1,0,1,0,1,1,1,200,1,7,1,0,1,3,1,0,1,198,1,192,1,0,1,161,1,0,1,254,1,0,1,200,1,7,1,0,1,3,1,0,1,190,1,190,1,111,1,203,1,3,1,1,1,254,1,16,1,24,1,16,1,1,1,31,1,31,1,254,1,136,1,0,1,0,1,64,1,0,1,100,1,8,1,0,1,97,1,31,1,31,1,255,1,248,1,0,1,0,1,64,1,0,1,200,1,2,1,0,1,0,1,4,1,108,1,108,1,0,1,224,1,3,1,0,1,0,1,168,1,16,133,0,0,1,65,1,194,1,0,1,0,1,255,1,58,1,30,131,0,0,1,252,1,252,1,108,1,225,1,1,1,2,1,0,1,200,1,143,1,192,1,0,1,0,1,21,1,108,1,0,1,225,150,0,0,1,2,132,255,0,138,0,0,1,3,1,208,1,16,1,42,1,17,1,1,1,0,1,0,1,2,1,56,1,0,1,0,1,1,1,152,135,0,0,1,36,134,0,0,1,1,1,208,138,0,0,1,1,1,168,131,0,0,1,28,1,0,1,0,1,1,1,155,1,255,1,254,1,3,132,0,0,1,2,131,0,0,1,28,134,0,0,1,1,1,148,131,0,0,1,68,1,0,1,2,131,0,0,1,14,133,0,0,1,76,131,0,0,1,92,1,0,1,0,1,1,1,60,1,0,1,2,1,0,1,14,1,0,1,4,132,0,0,1,1,1,68,1,0,1,0,1,1,1,84,1,95,1,118,1,115,1,95,1,99,1,0,1,171,1,171,1,0,1,1,1,0,1,3,1,0,1,1,1,0,1,4,1,0,1,14,229,0,0,229,0,0,156,0,0,1,95,1,118,1,115,1,105,1,95,1,99,1,0,1,171,1,0,1,1,1,0,1,3,1,0,1,1,1,0,1,4,1,0,1,4,198,0,0,1,118,1,115,1,95,1,51,1,95,1,48,1,0,1,50,1,46,1,48,1,46,1,49,1,49,1,54,1,50,1,54,1,46,1,48,1,0,1,171,134,0,0,1,1,1,152,1,0,1,33,1,0,1,6,138,0,0,1,36,1,99,131,0,0,1,1,131,0,0,1,7,131,0,0,1,6,1,0,1,0,1,2,1,144,1,0,1,16,1,0,1,5,1,0,1,0,1,48,1,6,1,0,1,0,1,80,1,7,1,0,1,12,1,0,1,8,1,0,1,13,1,0,1,9,1,0,1,14,1,0,1,10,1,0,1,47,1,0,1,11,1,0,1,0,1,48,1,80,1,0,1,1,1,241,1,81,1,0,1,5,1,114,1,82,1,0,1,0,1,16,1,25,131,0,0,1,26,131,0,0,1,27,131,0,0,1,28,1,0,1,0,1,16,1,29,1,0,1,0,1,16,1,32,1,245,1,85,1,96,1,5,1,16,1,11,1,18,1,3,1,18,1,0,1,16,1,1,132,0,0,1,96,1,12,1,194,1,0,1,18,133,0,0,1,32,1,18,1,0,1,0,1,18,1,0,1,196,133,0,0,1,96,1,20,1,96,1,26,1,18,1,0,1,18,133,0,0,1,16,1,32,1,0,1,0,1,34,133,0,0,1,5,1,248,1,96,131,0,0,1,6,1,136,132,0,0,1,5,1,248,133,0,0,1,143,132,0,0,1,5,1,248,1,32,131,0,0,1,15,1,200,132,0,0,1,5,1,248,1,48,131,0,0,1,6,1,136,132,0,0,1,5,1,248,1,80,131,0,0,1,6,1,136,132,0,0,1,5,1,248,1,64,131,0,0,1,6,1,136,132,0,0,1,5,1,248,1,16,131,0,0,1,6,1,136,132,0,0,1,200,1,15,1,0,1,1,1,0,1,27,1,0,1,0,1,225,1,6,1,1,1,0,1,200,1,15,1,0,1,1,1,0,1,198,1,0,1,0,1,235,1,6,1,4,1,1,1,200,1,15,1,0,1,1,1,0,1,177,1,148,1,148,1,235,1,6,1,5,1,1,1,200,1,15,1,0,1,1,1,0,1,108,1,248,1,148,1,235,1,6,1,3,1,1,1,200,1,1,1,128,1,62,1,0,1,233,1,167,1,0,1,175,1,1,1,14,1,0,1,200,1,2,1,128,1,62,1,0,1,233,1,167,1,0,1,175,1,1,1,15,1,0,1,200,1,4,1,128,1,62,1,0,1,233,1,167,1,0,1,175,1,1,1,16,1,0,1,200,1,8,1,128,1,62,1,0,1,233,1,167,1,0,1,175,1,1,1,17,1,0,1,200,1,7,1,0,1,4,1,0,1,198,1,180,1,0,1,225,1,0,1,4,1,0,1,200,1,7,131,0,0,1,177,1,180,1,192,1,235,1,0,1,5,1,4,1,200,1,14,131,0,0,1,27,1,252,1,140,1,235,1,0,1,3,1,0,1,200,1,1,131,0,0,1,195,1,195,1,0,1,240,131,0,0,1,88,1,16,133,0,0,1,108,1,226,1,0,1,0,1,128,1,200,1,3,1,128,1,0,1,0,1,176,1,176,1,0,1,226,1,2,1,2,1,0,1,200,1,1,1,128,1,1,1,0,1,233,1,167,1,0,1,175,1,1,1,4,1,0,1,200,1,2,1,128,1,1,1,0,1,233,1,167,1,0,1,175,1,1,1,5,1,0,1,200,1,4,1,128,1,1,1,0,1,233,1,167,1,0,1,175,1,1,1,6,1,0,1,200,1,8,1,128,1,1,1,0,1,233,1,167,1,0,1,175,1,1,1,7,1,0,1,200,1,7,131,0,0,1,21,1,108,1,0,1,225,131,0,0,1,200,1,1,1,0,1,0,1,2,1,190,1,190,1,0,1,176,1,0,1,12,1,0,1,200,1,7,1,128,1,2,1,0,1,108,1,192,1,0,1,161,1,0,1,13,148,0,0,1,1,132,255,0,131,0,0,1,1,134,0,0,1,1,1,196,1,16,1,42,1,17,132,0,0,1,244,131,0,0,1,208,135,0,0,1,36,131,0,0,1,160,131,0,0,1,200,139,0,0,1,120,131,0,0,1,28,131,0,0,1,106,1,255,1,255,1,3,132,0,0,1,2,131,0,0,1,28,135,0,0,1,99,131,0,0,1,68,1,0,1,3,131,0,0,1,1,133,0,0,1,76,135,0,0,1,92,1,0,1,3,1,0,1,1,1,0,1,1,133,0,0,1,76,132,0,0,1,95,1,112,1,115,1,95,1,115,1,48,1,0,1,171,1,0,1,4,1,0,1,12,1,0,1,1,1,0,1,1,1,0,1,1,134,0,0,1,95,1,112,1,115,1,95,1,115,1,49,1,0,1,112,1,115,1,95,1,51,1,95,1,48,1,0,1,50,1,46,1,48,1,46,1,49,1,49,1,54,1,50,1,54,1,46,1,48,1,0,1,171,1,171,135,0,0,1,1,139,0,0,1,20,1,1,1,252,1,0,1,16,147,0,0,1,64,131,0,0,1,144,1,16,1,0,1,3,132,0,0,1,4,134,0,0,1,36,1,99,1,0,1,7,1,0,1,7,131,0,0,1,1,1,0,1,0,1,48,1,80,1,0,1,0,1,241,1,81,1,0,1,0,1,114,1,82,160,0,0,1,63,131,0,0,1,191,131,0,0,1,63,1,128,1,0,1,0,1,186,1,131,1,18,1,111,1,67,1,16,1,68,1,254,140,0,0,1,1,1,64,1,80,1,2,1,0,1,0,1,18,1,0,1,196,133,0,0,1,64,1,7,1,0,1,0,1,34,133,0,0,1,76,1,64,133,0,0,1,27,1,226,1,0,1,0,1,1,1,200,1,7,1,0,1,3,1,0,1,198,1,192,1,0,1,161,1,0,1,254,1,0,1,200,1,7,1,0,1,3,1,0,1,190,1,190,1,111,1,203,1,3,1,1,1,254,1,16,1,24,1,16,1,1,1,31,1,31,1,254,1,136,1,0,1,0,1,64,1,0,1,100,1,8,1,0,1,97,1,31,1,31,1,255,1,248,1,0,1,0,1,64,1,0,1,200,1,2,1,0,1,0,1,4,1,108,1,108,1,0,1,224,1,3,1,0,1,0,1,168,1,16,133,0,0,1,65,1,194,1,0,1,0,1,255,1,58,1,30,131,0,0,1,252,1,252,1,108,1,225,1,1,1,2,1,0,1,200,1,143,1,192,1,0,1,0,1,21,1,108,1,0,1,225,150,0,0,1,1,132,255,0,138,0,0,1,18,1,144,1,16,1,42,1,17,1,1,1,0,1,0,1,15,1,152,1,0,1,0,1,2,1,248,135,0,0,1,36,1,0,1,0,1,15,1,16,1,0,1,0,1,15,1,56,138,0,0,1,14,1,232,131,0,0,1,28,1,0,1,0,1,14,1,219,1,255,1,254,1,3,132,0,0,1,2,131,0,0,1,28,134,0,0,1,14,1,212,131,0,0,1,68,1,0,1,2,131,0,0,1,14,133,0,0,1,76,131,0,0,1,92,1,0,1,0,1,1,1,60,1,0,1,2,1,0,1,14,1,0,1,216,132,0,0,1,1,1,68,1,0,1,0,1,1,1,84,1,95,1,118,1,115,1,95,1,99,1,0,1,171,1,171,1,0,1,1,1,0,1,3,1,0,1,1,1,0,1,4,1,0,1,14,229,0,0,229,0,0,156,0,0,1,95,1,118,1,115,1,98,1,95,1,99,1,0,1,171,1,0,1,1,1,0,1,3,1,0,1,1,1,0,1,4,1,0,1,216,229,0,0,229,0,0,229,0,0,229,0,0,229,0,0,229,0,0,229,0,0,229,0,0,229,0,0,229,0,0,229,0,0,229,0,0,229,0,0,229,0,0,229,0,0,229,0,0,229,0,0,229,0,0,229,0,0,229,0,0,229,0,0,229,0,0,229,0,0,229,0,0,229,0,0,229,0,0,229,0,0,229,0,0,229,0,0,229,0,0,229,0,0,229,0,0,229,0,0,229,0,0,156,0,0,1,118,1,115,1,95,1,51,1,95,1,48,1,0,1,50,1,46,1,48,1,46,1,49,1,49,1,54,1,50,1,54,1,46,1,48,1,0,1,171,135,0,0,1,1,139,0,0,1,20,1,0,1,252,1,0,1,16,147,0,0,1,64,1,0,1,0,1,2,1,184,1,0,1,33,1,0,1,8,138,0,0,1,36,1,99,131,0,0,1,1,131,0,0,1,5,131,0,0,1,6,1,0,1,0,1,2,1,144,1,0,1,16,1,0,1,6,1,0,1,0,1,48,1,7,1,0,1,0,1,80,1,8,1,0,1,0,1,16,1,9,1,0,1,32,1,32,1,10,1,0,1,0,1,48,1,80,1,0,1,1,1,241,1,81,1,0,1,5,1,114,1,82,1,0,1,0,1,16,1,48,131,0,0,1,50,131,0,0,1,51,131,0,0,1,52,1,0,1,0,1,16,1,53,1,0,1,0,1,16,1,56,180,0,0,1,63,1,128,1,0,1,0,1,64,1,64,134,0,0,1,241,1,85,1,80,1,6,1,0,1,0,1,18,1,1,1,194,133,0,0,1,96,1,11,1,96,1,17,1,18,1,0,1,18,133,0,0,1,96,1,23,1,96,1,29,1,18,1,0,1,18,133,0,0,1,16,1,35,1,0,1,0,1,18,1,0,1,196,133,0,0,1,96,1,36,1,96,1,42,1,18,1,0,1,18,133,0,0,1,96,1,48,1,48,1,54,1,18,1,0,1,34,131,0,0,1,5,1,248,1,16,131,0,0,1,6,1,136,132,0,0,1,5,1,248,132,0,0,1,4,1,71,132,0,0,1,5,1,248,1,48,131,0,0,1,15,1,200,132,0,0,1,5,1,248,1,96,131,0,0,1,6,1,136,132,0,0,1,5,1,248,1,32,131,0,0,1,6,1,136,132,0,0,1,200,1,15,1,0,1,8,1,0,1,148,1,198,1,0,1,161,1,2,1,255,1,0,1,92,1,8,1,0,1,7,1,0,1,27,1,27,1,198,1,161,1,1,1,2,1,8,1,200,1,15,1,0,1,4,1,160,1,27,1,136,1,0,1,161,1,6,1,14,1,0,1,200,1,15,1,0,1,5,1,160,1,27,1,136,1,0,1,161,1,6,1,15,1,0,1,92,1,15,1,0,1,2,1,160,1,27,1,136,1,177,1,161,1,6,1,16,1,8,1,200,1,15,1,0,1,2,1,160,1,198,1,136,1,0,1,171,1,6,1,16,1,2,1,200,1,15,1,0,1,5,1,160,1,198,1,136,1,0,1,171,1,6,1,15,1,5,1,200,1,15,1,0,1,4,1,160,1,198,1,136,1,0,1,171,1,6,1,14,1,4,1,92,1,2,1,0,1,8,1,0,131,27,0,1,161,1,1,1,0,1,8,1,200,1,15,1,0,1,4,1,160,1,177,1,52,1,148,1,171,1,6,1,14,1,4,1,200,1,15,1,0,1,5,1,160,1,177,1,52,1,148,1,171,1,6,1,15,1,5,1,200,1,15,1,0,1,2,1,160,1,177,1,52,1,148,1,171,1,6,1,16,1,2,1,92,1,8,1,0,1,8,1,0,1,27,1,27,1,108,1,161,1,1,1,1,1,8,1,200,1,15,1,0,1,2,1,160,1,108,1,208,1,148,1,171,1,6,1,16,1,2,1,200,1,15,1,0,1,5,1,160,1,108,1,255,1,143,1,171,1,6,1,15,1,5,1,200,1,15,1,0,1,6,1,160,1,108,1,208,1,148,1,171,1,6,1,14,1,4,1,200,1,1,1,0,1,4,1,0,1,170,1,167,1,0,1,239,1,6,1,1,1,0,1,200,1,2,1,0,1,4,1,0,1,248,1,167,1,0,1,239,1,5,1,1,1,0,1,200,1,4,1,0,1,4,1,0,1,170,1,167,1,0,1,239,1,2,1,1,1,0,1,200,1,1,1,0,1,7,1,0,1,190,1,190,1,0,1,176,1,4,1,3,1,0,1,200,1,4,1,0,1,7,1,0,1,190,1,190,1,0,1,176,1,4,1,2,1,0,1,20,1,17,1,0,1,8,1,0,1,190,1,190,1,27,1,176,1,4,1,0,1,1,1,168,1,36,1,7,1,8,1,0,1,190,1,190,1,0,1,144,1,4,1,1,1,3,1,200,1,3,1,128,1,62,1,0,1,196,1,25,1,0,1,224,1,8,1,8,1,0,1,200,1,12,1,128,1,62,1,0,1,70,1,155,1,0,1,224,1,7,1,7,1,0,1,200,1,1,1,0,1,5,1,0,1,191,1,195,1,0,1,240,1,6,1,0,1,0,1,200,1,2,1,0,1,5,1,0,1,195,1,195,1,0,1,240,1,5,1,0,1,0,1,200,1,4,1,0,1,5,1,0,1,191,1,195,1,0,1,240,1,2,1,0,1,0,1,200,1,2,1,0,1,2,1,0,1,190,1,190,1,0,1,176,1,4,1,8,1,0,1,200,1,4,1,0,1,2,1,0,1,190,1,190,1,0,1,176,1,4,1,10,1,0,1,20,1,24,1,0,1,2,1,0,1,190,1,190,1,27,1,176,1,4,1,11,1,1,1,168,1,18,1,1,1,0,1,0,1,190,1,190,1,0,1,144,1,5,1,8,1,8,1,20,1,20,131,0,0,1,190,1,190,1,27,1,176,1,5,1,9,1,1,1,168,1,72,1,1,1,0,1,0,1,190,1,190,1,0,1,144,1,5,1,10,1,10,1,200,1,15,1,0,1,4,1,0,1,176,1,27,1,166,1,108,1,255,1,1,1,4,1,20,1,18,1,0,1,1,1,0,1,85,1,62,1,27,1,175,1,4,1,9,1,1,1,168,1,129,1,1,1,2,1,0,1,195,1,195,1,0,1,208,1,0,1,0,1,11,1,200,1,3,1,128,1,0,1,0,1,176,1,176,1,0,1,226,1,3,1,3,1,0,1,88,1,29,1,0,1,1,1,0,1,5,1,4,1,108,1,224,1,2,1,1,1,130,1,200,1,1,1,128,1,1,1,0,1,167,1,167,1,0,1,175,1,1,1,4,1,0,1,200,1,2,1,128,1,1,1,0,1,167,1,167,1,0,1,175,1,1,1,5,1,0,1,200,1,4,1,128,1,1,1,0,1,167,1,167,1,0,1,175,1,1,1,6,1,0,1,200,1,8,1,128,1,1,1,0,1,167,1,167,1,0,1,175,1,1,1,7,1,0,1,200,1,7,131,0,0,1,21,1,108,1,0,1,225,131,0,0,1,200,1,1,1,0,1,0,1,2,1,190,1,190,1,0,1,176,1,0,1,12,1,0,1,200,1,7,1,128,1,2,1,0,1,108,1,192,1,0,1,161,1,0,1,13,149,0,0,132,255,0,131,0,0,1,1,134,0,0,1,1,1,196,1,16,1,42,1,17,132,0,0,1,244,131,0,0,1,208,135,0,0,1,36,131,0,0,1,160,131,0,0,1,200,139,0,0,1,120,131,0,0,1,28,131,0,0,1,106,1,255,1,255,1,3,132,0,0,1,2,131,0,0,1,28,135,0,0,1,99,131,0,0,1,68,1,0,1,3,131,0,0,1,1,133,0,0,1,76,135,0,0,1,92,1,0,1,3,1,0,1,1,1,0,1,1,133,0,0,1,76,132,0,0,1,95,1,112,1,115,1,95,1,115,1,48,1,0,1,171,1,0,1,4,1,0,1,12,1,0,1,1,1,0,1,1,1,0,1,1,134,0,0,1,95,1,112,1,115,1,95,1,115,1,49,1,0,1,112,1,115,1,95,1,51,1,95,1,48,1,0,1,50,1,46,1,48,1,46,1,49,1,49,1,54,1,50,1,54,1,46,1,48,1,0,1,171,1,171,135,0,0,1,1,139,0,0,1,20,1,1,1,252,1,0,1,16,147,0,0,1,64,131,0,0,1,144,1,16,1,0,1,3,132,0,0,1,4,134,0,0,1,36,1,99,1,0,1,7,1,0,1,7,131,0,0,1,1,1,0,1,0,1,48,1,80,1,0,1,0,1,241,1,81,1,0,1,0,1,114,1,82,160,0,0,1,63,131,0,0,1,191,131,0,0,1,63,1,128,1,0,1,0,1,186,1,131,1,18,1,111,1,67,1,16,1,68,1,254,140,0,0,1,1,1,64,1,80,1,2,1,0,1,0,1,18,1,0,1,196,133,0,0,1,64,1,7,1,0,1,0,1,34,133,0,0,1,76,1,64,133,0,0,1,27,1,226,1,0,1,0,1,1,1,200,1,7,1,0,1,3,1,0,1,198,1,192,1,0,1,161,1,0,1,254,1,0,1,200,1,7,1,0,1,3,1,0,1,190,1,190,1,111,1,203,1,3,1,1,1,254,1,16,1,24,1,16,1,1,1,31,1,31,1,254,1,136,1,0,1,0,1,64,1,0,1,100,1,8,1,0,1,97,1,31,1,31,1,255,1,248,1,0,1,0,1,64,1,0,1,200,1,2,1,0,1,0,1,4,1,108,1,108,1,0,1,224,1,3,1,0,1,0,1,168,1,16,133,0,0,1,65,1,194,1,0,1,0,1,255,1,58,1,30,131,0,0,1,252,1,252,1,108,1,225,1,1,1,2,1,0,1,200,1,143,1,192,1,0,1,0,1,21,1,108,1,0,1,225,151,0,0,132,255,0,138,0,0,1,3,1,24,1,16,1,42,1,17,1,1,1,0,1,0,1,1,1,188,1,0,1,0,1,1,1,92,135,0,0,1,36,134,0,0,1,1,1,100,138,0,0,1,1,1,60,131,0,0,1,28,1,0,1,0,1,1,1,47,1,255,1,254,1,3,132,0,0,1,1,131,0,0,1,28,134,0,0,1,1,1,40,131,0,0,1,48,1,0,1,2,131,0,0,1,14,133,0,0,1,56,131,0,0,1,72,1,95,1,118,1,115,1,95,1,99,1,0,1,171,1,171,1,0,1,1,1,0,1,3,1,0,1,1,1,0,1,4,1,0,1,14,229,0,0,229,0,0,156,0,0,1,118,1,115,1,95,1,51,1,95,1,48,1,0,1,50,1,46,1,48,1,46,1,49,1,49,1,54,1,50,1,54,1,46,1,48,1,0,1,171,134,0,0,1,1,1,92,1,0,1,33,1,0,1,3,138,0,0,1,36,1,99,131,0,0,1,1,131,0,0,1,3,131,0,0,1,6,1,0,1,0,1,2,1,144,1,0,1,16,1,0,1,4,1,0,1,0,1,48,1,5,1,0,1,32,1,80,1,6,1,0,1,0,1,48,1,80,1,0,1,1,1,241,1,81,1,0,1,5,1,114,1,82,1,0,1,0,1,16,1,18,131,0,0,1,19,131,0,0,1,20,131,0,0,1,21,1,0,1,0,1,16,1,22,1,0,1,0,1,16,1,27,1,112,1,21,1,48,1,4,1,0,1,0,1,18,1,0,1,194,133,0,0,1,64,1,7,1,0,1,0,1,18,1,0,1,196,133,0,0,1,96,1,11,1,96,1,17,1,18,1,0,1,18,133,0,0,1,80,1,23,1,0,1,0,1,34,133,0,0,1,5,1,248,1,48,131,0,0,1,6,1,136,132,0,0,1,5,1,248,1,16,131,0,0,1,14,1,136,132,0,0,1,5,1,248,1,32,131,0,0,1,15,1,200,132,0,0,1,200,1,1,1,128,1,62,1,0,1,167,1,167,1,0,1,175,1,3,1,0,1,0,1,200,1,2,1,128,1,62,1,0,1,167,1,167,1,0,1,175,1,3,1,1,1,0,1,200,1,4,1,128,1,62,1,0,1,167,1,167,1,0,1,175,1,3,1,2,1,0,1,200,1,8,1,128,1,62,1,0,1,167,1,167,1,0,1,175,1,3,1,3,1,0,1,200,1,2,131,0,0,1,190,1,190,1,0,1,176,1,1,1,8,1,0,1,200,1,4,131,0,0,1,190,1,190,1,0,1,176,1,1,1,9,1,0,1,200,1,8,131,0,0,1,190,1,190,1,0,1,176,1,1,1,10,1,0,1,200,1,1,1,0,1,1,1,0,1,167,1,167,1,0,1,175,1,3,1,8,1,0,1,200,1,2,1,0,1,1,1,0,1,167,1,167,1,0,1,175,1,3,1,9,1,0,1,200,1,4,1,0,1,1,1,0,1,167,1,167,1,0,1,175,1,3,1,10,1,0,1,200,1,8,1,0,1,1,1,0,1,167,1,167,1,0,1,175,1,3,1,11,1,0,1,200,1,3,1,128,1,0,1,0,1,176,1,176,1,0,1,226,1,2,1,2,1,0,1,200,1,1,1,128,1,1,1,0,1,167,1,167,1,0,1,175,1,1,1,4,1,0,1,200,1,2,1,128,1,1,1,0,1,167,1,167,1,0,1,175,1,1,1,5,1,0,1,200,1,4,1,128,1,1,1,0,1,167,1,167,1,0,1,175,1,1,1,6,1,0,1,200,1,8,1,128,1,1,1,0,1,167,1,167,1,0,1,175,1,1,1,7,1,0,1,200,1,1,131,0,0,1,195,1,195,1,0,1,240,131,0,0,1,88,1,16,133,0,0,1,108,1,226,1,0,1,0,1,128,1,200,1,7,131,0,0,1,21,1,108,1,0,1,225,131,0,0,1,200,1,1,1,0,1,0,1,2,1,190,1,190,1,0,1,176,1,0,1,12,1,0,1,200,1,7,1,128,1,2,1,0,1,108,1,192,1,0,1,161,1,0,1,13,140,0,0,1,0};
			}
		}
#else
		/// <summary>Static Length+DeflateStream compressed shader byte code (Windows)</summary>
		private static byte[] fxb
		{
			get
			{
				return new byte[] {64,48,0,0,236,189,7,96,28,73,150,37,38,47,109,202,123,127,74,245,74,215,224,116,161,8,128,96,19,36,216,144,64,16,236,193,136,205,230,146,236,29,105,71,35,41,171,42,129,202,101,86,101,93,102,22,64,204,237,157,188,247,222,123,239,189,247,222,123,239,189,247,186,59,157,78,39,247,223,255,63,92,102,100,1,108,246,206,74,218,201,158,33,128,170,200,31,63,126,124,31,63,34,254,197,223,240,127,250,251,210,95,99,248,249,53,127,236,255,254,191,182,126,51,249,253,215,198,223,191,134,249,231,215,248,53,126,19,250,255,175,227,254,252,255,245,243,235,209,255,127,255,203,230,247,159,254,26,142,14,127,213,111,34,223,253,91,191,198,207,31,58,252,232,249,209,243,163,231,71,207,143,158,31,61,63,122,126,244,252,232,249,209,243,163,231,71,207,255,91,158,95,255,215,224,56,109,66,129,218,31,100,226,180,223,236,55,149,239,126,29,253,255,55,17,167,105,63,5,2,194,223,232,215,16,184,251,218,79,208,102,213,252,254,205,206,175,241,227,166,205,239,61,212,102,87,113,213,118,254,243,107,209,255,241,90,247,115,51,14,140,243,55,139,124,143,191,99,239,253,24,253,255,73,153,47,103,197,242,2,47,255,186,3,239,35,230,141,189,255,27,210,255,207,150,77,155,45,167,128,192,99,120,61,207,102,121,253,107,252,14,128,245,107,106,27,124,142,119,83,239,221,223,136,190,220,242,38,224,111,163,24,250,95,210,56,26,207,239,68,29,238,120,52,250,61,233,247,55,222,223,191,211,111,38,63,49,230,63,77,63,7,125,254,20,253,126,78,159,205,232,255,127,170,254,253,7,209,239,63,67,255,255,231,34,109,255,34,250,236,207,243,218,254,93,244,251,223,70,255,255,141,126,179,126,219,255,136,62,255,247,188,182,255,27,253,254,203,21,230,175,39,63,184,253,255,77,143,25,222,127,134,95,126,237,255,251,255,254,191,254,239,209,175,113,242,230,248,201,239,68,127,254,226,95,67,62,67,219,223,137,91,253,26,105,75,255,60,253,53,76,94,225,215,250,53,158,235,251,191,47,127,246,107,210,127,191,222,175,49,211,207,148,159,254,218,95,135,184,238,215,228,255,188,207,119,251,159,211,199,247,126,255,157,95,227,139,98,90,87,77,117,222,166,91,175,238,164,223,126,254,250,121,42,51,150,158,84,139,85,81,210,47,15,199,123,159,142,31,222,223,27,239,29,236,239,255,26,63,193,108,241,155,254,69,4,226,247,160,255,255,131,213,111,241,135,254,61,255,215,211,223,236,4,159,255,154,252,249,31,244,123,72,15,242,252,46,52,124,122,225,15,34,140,255,36,249,253,215,252,131,168,157,254,254,107,253,65,191,214,175,241,235,243,239,212,244,79,250,53,126,131,223,244,47,50,191,255,154,244,251,175,199,191,163,253,255,253,39,253,186,76,134,95,143,224,252,26,244,247,191,244,39,253,58,204,66,248,238,175,249,147,126,13,134,255,215,252,69,242,25,218,252,103,244,255,127,9,56,254,69,79,232,61,234,143,254,254,175,255,160,95,227,55,248,207,254,162,95,139,225,252,154,128,243,7,19,45,254,160,95,215,253,77,255,255,191,255,162,223,132,251,252,205,249,111,247,238,127,70,248,252,103,127,145,180,253,77,168,207,63,137,240,254,147,4,167,223,224,215,215,119,127,21,125,78,239,254,6,191,1,126,254,69,255,247,255,205,195,183,115,255,107,232,243,183,97,50,127,237,255,139,230,190,181,115,255,231,254,154,242,153,63,247,127,218,175,41,115,143,207,126,147,95,227,183,224,185,199,188,63,230,78,126,19,150,159,167,244,251,155,95,211,228,159,254,218,191,246,215,100,174,248,117,126,13,79,108,254,127,253,168,158,181,227,238,234,163,175,251,92,126,77,217,16,222,253,141,254,160,95,67,249,251,215,248,53,126,99,203,235,224,155,95,203,254,254,155,146,13,50,191,255,26,127,208,175,163,191,255,218,204,143,70,54,126,141,63,232,215,243,218,208,59,255,145,249,156,198,171,191,255,154,128,169,191,255,90,4,243,215,255,143,126,77,133,255,235,16,207,254,186,150,127,191,162,119,254,51,150,25,249,27,240,136,167,73,78,220,103,127,13,193,114,159,253,26,252,217,255,77,48,229,179,31,19,57,249,143,32,91,191,201,175,241,159,253,69,242,247,175,197,127,255,166,246,239,95,135,255,254,205,236,223,191,1,255,253,155,235,223,191,150,190,255,235,216,191,229,253,95,215,254,45,239,255,122,246,111,121,255,215,167,191,101,92,191,62,218,234,184,32,119,110,92,100,89,116,92,127,25,225,251,151,233,24,208,70,198,69,90,249,15,250,221,8,134,188,247,159,253,65,191,129,202,61,198,246,27,253,26,255,217,95,12,152,160,31,96,252,198,218,31,232,76,184,252,73,70,122,33,166,190,30,255,159,240,203,175,21,209,227,191,214,15,85,143,239,125,29,94,253,137,175,161,199,127,13,209,227,127,147,249,157,218,217,223,73,143,155,223,7,244,56,244,226,255,253,55,137,14,253,181,255,32,240,22,209,254,111,146,121,250,117,232,247,191,230,111,194,103,78,143,163,205,127,134,255,3,71,79,143,255,103,172,199,241,247,175,37,127,255,77,208,205,191,22,255,253,27,224,125,171,215,241,247,175,197,48,69,175,255,90,191,198,111,199,127,11,14,191,62,248,2,255,87,156,126,125,109,251,159,253,65,194,107,162,199,249,247,223,64,250,237,242,193,175,161,207,254,111,65,255,252,90,208,233,255,216,175,109,248,224,223,253,77,228,51,159,15,254,181,223,196,215,233,191,86,79,167,255,91,191,198,143,116,186,250,232,118,220,255,214,141,111,252,232,249,209,243,163,231,71,207,143,158,111,242,249,17,172,31,61,63,122,126,244,252,232,249,209,243,163,231,71,207,229,7,228,25,254,11,206,39,252,158,191,167,15,207,203,107,217,220,151,203,11,35,159,32,191,35,150,55,185,178,95,75,115,101,18,175,75,142,235,191,160,24,125,44,57,175,191,73,114,73,146,187,250,181,127,141,175,254,164,223,36,69,238,224,43,228,24,144,235,248,131,228,239,95,227,111,250,181,145,163,144,60,151,247,249,95,67,159,255,53,145,207,255,111,250,252,255,230,207,127,140,115,16,191,230,31,132,220,89,39,135,244,39,153,190,127,45,237,251,55,245,250,198,103,191,169,215,247,175,165,125,184,207,165,239,254,231,210,247,175,101,251,254,181,184,239,95,203,246,253,107,113,223,191,86,167,239,223,172,211,247,111,54,208,247,111,54,208,247,111,22,237,251,215,233,244,253,235,216,190,37,87,246,27,208,247,255,247,159,244,99,66,147,127,8,121,53,228,110,228,239,95,139,255,254,53,93,158,144,255,254,181,92,158,144,255,254,181,93,158,16,109,169,31,155,39,228,191,127,204,229,9,249,239,196,229,9,249,239,223,80,255,166,57,250,143,100,60,230,239,95,139,255,54,121,70,26,11,255,109,242,140,132,59,255,141,60,227,111,32,239,255,65,50,191,230,111,166,59,247,47,127,51,45,184,255,223,77,243,146,130,175,240,3,190,51,57,197,95,171,147,83,164,143,208,151,205,41,226,249,81,78,241,71,57,69,60,126,78,241,239,195,72,57,167,248,251,88,62,184,251,107,202,103,248,97,248,96,139,254,216,249,53,24,56,231,20,15,232,231,183,127,141,159,191,249,195,175,107,167,110,99,143,140,110,83,253,111,117,27,254,246,117,155,234,73,171,219,240,183,209,109,170,31,254,36,163,219,126,13,209,45,127,146,209,109,191,134,232,150,63,201,232,54,240,12,254,246,117,155,216,40,167,219,196,246,56,221,134,191,125,221,134,191,141,110,51,246,202,232,54,99,67,140,110,51,122,221,232,182,77,235,37,155,116,219,255,19,0,0,255,255};
			}
		}
#endif
		/// <summary>Set a shader attribute of type 'Vector3' by global unique ID, see <see cref="Xen.Graphics.ShaderSystem.ShaderSystemBase.GetNameUniqueID"/> for details.</summary><param name="state"/><param name="id"/><param name="value"/>
		protected override bool SetAttributeImpl(Xen.Graphics.ShaderSystem.ShaderSystemBase state, int id, ref Microsoft.Xna.Framework.Vector3 value)
		{
			if ((ShadowShader.gd != state.DeviceUniqueIndex))
			{
				this.WarmShader(state);
			}
			if ((id == ShadowShader.cid0))
			{
				this.SetLightColour(ref value);
				return true;
			}
			if ((id == ShadowShader.cid2))
			{
				this.SetShadowViewDirection(ref value);
				return true;
			}
			return false;
		}
		/// <summary>Set a shader attribute of type 'Matrix' by global unique ID, see <see cref="Xen.Graphics.ShaderSystem.ShaderSystemBase.GetNameUniqueID"/> for details.</summary><param name="state"/><param name="id"/><param name="value"/>
		protected override bool SetAttributeImpl(Xen.Graphics.ShaderSystem.ShaderSystemBase state, int id, ref Microsoft.Xna.Framework.Matrix value)
		{
			if ((ShadowShader.gd != state.DeviceUniqueIndex))
			{
				this.WarmShader(state);
			}
			if ((id == ShadowShader.cid1))
			{
				this.SetShadowMapProjection(ref value);
				return true;
			}
			return false;
		}
		/// <summary>Set a shader sampler of type 'TextureSamplerState' by global unique ID, see <see cref="Xen.Graphics.ShaderSystem.ShaderSystemBase.GetNameUniqueID"/> for details.</summary><param name="state"/><param name="id"/><param name="value"/>
		protected override bool SetSamplerStateImpl(Xen.Graphics.ShaderSystem.ShaderSystemBase state, int id, Xen.Graphics.TextureSamplerState value)
		{
			if ((ShadowShader.gd != state.DeviceUniqueIndex))
			{
				this.WarmShader(state);
			}
			if ((id == ShadowShader.sid0))
			{
				this.ShadowSampler = value;
				return true;
			}
			if ((id == ShadowShader.sid1))
			{
				this.TextureSampler = value;
				return true;
			}
			return false;
		}
		/// <summary>Set a shader texture of type 'Texture2D' by global unique ID, see <see cref="Xen.Graphics.ShaderSystem.ShaderSystemBase.GetNameUniqueID"/> for details.</summary><param name="state"/><param name="id"/><param name="value"/>
		protected override bool SetTextureImpl(Xen.Graphics.ShaderSystem.ShaderSystemBase state, int id, Microsoft.Xna.Framework.Graphics.Texture2D value)
		{
			if ((ShadowShader.gd != state.DeviceUniqueIndex))
			{
				this.WarmShader(state);
			}
			if ((id == ShadowShader.tid0))
			{
				this.ShadowMap = value;
				return true;
			}
			if ((id == ShadowShader.tid1))
			{
				this.TextureMap = value;
				return true;
			}
			return false;
		}
	}
}
