// XenFX
// Assembly = Xen.Graphics.ShaderSystem.CustomTool, Version=7.0.1.1, Culture=neutral, PublicKeyToken=e706afd07878dfca
// SourceFile = ShadowShader.fx
// Namespace = GameClient.Shaders

namespace GameClient.Shaders
{
	
	/// <summary><para>Technique 'ShadowShader' generated from file 'ShadowShader.fx'</para><para>Vertex Shader: approximately 21 instruction slots used, 14 registers</para><para>Pixel Shader: approximately 13 instruction slots used (2 texture, 11 arithmetic), 0 registers</para></summary>
	[System.Diagnostics.DebuggerStepThroughAttribute()]
	[System.CodeDom.Compiler.GeneratedCodeAttribute("Xen.Graphics.ShaderSystem.CustomTool.dll", "47301628-bcee-4c1a-8aea-2eb652e288f0")]
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
		private static void gdInit(Xen.Graphics.ShaderSystem.ShaderSystemBase state)
		{
			// set the graphics ID
			ShadowShader.gd = state.DeviceUniqueIndex;
			ShadowShader.cid0 = state.GetNameUniqueID("lightColour");
			ShadowShader.cid1 = state.GetNameUniqueID("shadowMapProjection");
			ShadowShader.cid2 = state.GetNameUniqueID("shadowViewDirection");
			ShadowShader.sid0 = state.GetNameUniqueID("ShadowSampler");
			ShadowShader.sid1 = state.GetNameUniqueID("TextureSampler");
			ShadowShader.tid0 = state.GetNameUniqueID("ShadowMap");
			ShadowShader.tid1 = state.GetNameUniqueID("TextureMap");
		}
		/// <summary>Bind the shader, 'ic' indicates the shader instance has changed and 'ec' indicates the extension has changed.</summary><param name="state"/><param name="ic"/><param name="ec"/><param name="ext"/>
		protected override void Begin(Xen.Graphics.ShaderSystem.ShaderSystemBase state, bool ic, bool ec, Xen.Graphics.ShaderSystem.ShaderExtension ext)
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
				ShadowShader.gdInit(state);
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
		protected override int GetVertexInputCount()
		{
			return 3;
		}
		/// <summary>Returns a vertex input used by this shader</summary><param name="i"/><param name="usage"/><param name="index"/>
		protected override void GetVertexInput(int i, out Microsoft.Xna.Framework.Graphics.VertexElementUsage usage, out int index)
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
		protected override void GetExtensionSupport(out bool blendingSupport, out bool instancingSupport)
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
				return new byte[] {64,48,0,0,236,189,7,96,28,73,150,37,38,47,109,202,123,127,74,245,74,215,224,116,161,8,128,96,19,36,216,144,64,16,236,193,136,205,230,146,236,29,105,71,35,41,171,42,129,202,101,86,101,93,102,22,64,204,237,157,188,247,222,123,239,189,247,222,123,239,189,247,186,59,157,78,39,247,223,255,63,92,102,100,1,108,246,206,74,218,201,158,33,128,170,200,31,63,126,124,31,63,34,254,197,223,240,127,250,251,210,95,99,248,249,53,127,236,255,254,191,182,126,51,249,253,215,198,223,191,134,249,231,215,248,53,126,19,250,255,175,227,254,252,255,245,243,235,209,255,127,255,203,230,247,159,254,26,142,14,127,213,111,34,223,253,91,191,198,207,31,58,252,232,249,209,243,163,231,71,207,143,158,31,61,63,122,126,244,252,232,249,209,243,163,231,71,207,255,91,158,95,255,215,224,56,109,130,64,205,196,105,191,217,111,42,223,253,58,250,255,111,34,78,211,126,10,244,243,27,253,26,2,119,95,251,9,218,172,154,223,191,217,113,109,126,239,161,54,187,191,198,31,196,184,106,59,255,249,181,232,255,120,173,251,185,25,7,198,249,155,69,190,199,223,177,247,126,140,254,255,164,204,151,179,98,121,1,224,191,238,192,251,136,121,99,239,255,134,244,255,179,101,211,102,203,41,32,240,24,94,207,179,89,94,255,26,191,62,96,253,154,218,6,159,227,221,212,123,247,55,162,47,183,188,9,248,219,40,134,254,151,52,142,198,243,59,81,135,59,30,141,126,79,250,253,141,247,247,239,244,155,201,79,140,249,79,211,207,65,159,63,69,191,159,211,103,51,250,255,159,170,127,255,65,244,251,207,208,255,255,185,72,219,191,136,62,251,243,188,182,127,23,253,254,183,209,255,127,163,223,172,223,246,63,162,207,255,61,175,237,255,70,191,255,114,133,249,235,201,15,110,255,127,211,99,134,247,159,225,151,95,251,255,254,191,255,175,255,123,244,107,156,188,57,126,242,59,209,159,191,248,215,144,207,208,246,119,226,86,191,70,218,210,63,79,127,13,195,175,191,214,175,241,92,223,255,125,249,179,95,147,254,251,245,126,141,153,126,166,252,244,215,254,58,196,81,191,38,255,231,125,190,219,255,156,62,190,247,251,239,252,26,95,20,211,186,106,170,243,54,221,122,117,39,253,246,243,215,207,83,153,177,244,164,90,172,138,146,126,121,56,222,251,116,252,240,254,222,120,239,96,127,255,215,248,9,102,139,223,244,47,34,16,191,7,253,255,31,172,126,139,63,244,239,249,191,158,254,102,39,248,252,215,228,207,255,160,223,67,122,144,231,119,161,225,211,11,127,16,97,252,39,201,239,191,38,241,243,111,170,191,255,90,127,208,175,245,107,252,250,252,59,53,253,147,126,141,223,224,55,253,139,204,239,191,38,253,254,235,241,239,104,255,127,255,73,191,46,147,225,215,35,56,144,135,127,233,79,250,117,152,133,240,221,95,243,39,253,26,12,255,175,249,139,228,51,180,249,207,232,255,255,18,112,252,139,158,208,123,212,31,253,253,95,255,65,191,198,111,240,159,253,69,191,22,195,249,53,1,231,15,38,90,252,65,191,174,251,155,254,255,127,255,69,191,9,247,249,155,243,223,238,221,255,140,240,249,207,254,34,105,251,155,80,159,127,18,225,253,39,9,78,191,193,175,175,239,254,42,200,233,175,245,107,252,6,191,1,126,254,69,255,247,255,205,195,183,115,255,107,232,243,183,97,50,127,237,255,139,230,190,181,115,255,231,254,154,242,153,63,247,127,218,175,41,115,143,207,126,147,95,227,183,224,185,199,188,63,230,78,126,19,150,159,167,244,251,155,95,211,228,159,254,218,191,246,215,100,174,248,117,126,13,79,108,254,127,253,168,158,181,227,238,234,163,175,251,92,126,77,217,16,222,253,141,254,160,95,67,249,251,215,248,53,126,99,203,235,224,155,95,203,254,254,155,254,65,191,182,253,253,215,248,131,126,29,253,253,215,102,126,52,178,241,107,252,65,191,158,215,134,222,249,143,204,231,52,94,253,253,215,4,76,253,253,215,34,152,191,254,127,244,107,42,252,95,135,120,246,215,181,252,251,21,189,243,159,177,204,200,223,128,71,60,77,114,226,62,251,107,8,150,251,236,215,224,207,254,111,130,41,159,253,152,200,201,127,4,217,250,77,126,141,255,236,47,146,191,127,45,254,251,55,181,127,255,58,252,247,111,102,255,254,13,248,239,223,92,255,254,181,244,253,95,199,254,45,239,255,186,246,111,121,255,215,179,127,203,251,191,62,253,45,227,250,245,209,86,199,5,185,115,227,34,203,162,227,250,203,8,223,191,76,199,128,54,50,46,210,202,127,208,239,70,48,228,189,255,236,15,250,13,84,238,49,182,223,232,215,248,207,254,98,192,4,253,0,227,55,214,254,64,103,194,229,79,50,210,11,49,245,245,248,255,132,95,126,173,136,30,255,181,126,168,122,124,239,235,240,234,79,124,13,61,254,107,136,30,255,155,204,239,212,206,254,78,122,220,252,62,160,199,161,23,255,239,191,73,116,232,175,253,7,129,183,136,246,127,147,204,211,175,67,191,255,53,127,19,62,115,122,28,109,254,51,252,31,56,122,122,252,63,99,61,142,191,127,45,249,251,111,130,110,254,181,248,239,223,0,239,91,189,142,191,127,45,134,41,122,253,215,250,53,126,59,254,91,112,248,245,193,23,248,191,226,244,235,107,219,255,236,15,18,94,19,61,206,191,255,6,210,111,151,15,126,13,125,246,127,11,250,231,215,130,78,255,199,126,109,195,7,255,238,111,34,159,249,124,240,175,253,38,190,78,255,181,122,58,253,223,250,53,126,164,211,213,71,183,227,254,183,110,124,227,71,207,143,158,31,61,63,122,126,244,124,147,207,143,96,253,232,249,209,243,163,231,71,207,143,158,31,61,63,122,46,63,32,207,240,95,112,62,225,247,252,61,125,120,94,94,203,230,190,92,94,24,249,4,249,29,177,188,201,149,253,90,154,43,147,120,93,114,92,255,5,197,232,99,201,121,253,77,146,75,146,220,213,175,253,107,124,245,39,253,38,41,114,7,95,33,199,128,92,199,31,36,127,255,26,127,211,175,141,28,133,228,185,188,207,255,26,250,252,175,137,124,254,127,211,231,255,55,127,254,99,156,131,248,53,255,32,228,206,58,57,164,63,201,244,253,107,105,223,191,169,215,55,62,251,77,189,190,127,45,237,195,125,46,125,247,63,151,190,127,45,219,247,175,197,125,255,90,182,239,95,139,251,254,181,58,125,255,102,157,190,127,179,129,190,127,179,129,190,127,179,104,223,191,78,167,239,95,199,246,45,185,178,223,128,190,255,191,255,164,31,19,154,252,67,200,171,33,119,35,127,255,90,252,247,175,233,242,132,252,247,175,229,242,132,252,247,175,237,242,132,104,75,253,216,60,33,255,253,99,46,79,200,127,39,46,79,200,127,255,134,250,55,205,209,127,36,227,49,127,255,90,252,183,201,51,210,88,248,111,147,103,36,220,249,111,228,25,127,3,121,255,15,146,249,53,127,51,221,185,127,249,155,105,193,253,255,110,154,151,20,124,133,31,240,157,201,41,254,90,157,156,34,125,132,190,108,78,17,207,143,114,138,63,202,41,226,241,115,138,127,31,70,202,57,197,223,199,242,193,221,95,83,62,195,15,195,7,91,244,199,206,175,193,192,57,167,120,64,63,191,253,107,252,252,205,31,126,93,59,117,27,123,100,116,155,234,127,171,219,240,183,175,219,84,79,90,221,134,191,141,110,83,253,240,39,25,221,246,107,136,110,249,147,140,110,251,53,68,183,252,73,70,183,129,103,240,183,175,219,196,70,57,221,38,182,199,233,54,252,237,235,54,252,109,116,155,177,87,70,183,25,27,98,116,155,209,235,70,183,109,90,47,217,164,219,254,159,0,0,0,255,255};
			}
		}
#endif
		/// <summary>Set a shader attribute of type 'Vector3' by global unique ID, see <see cref="Xen.Graphics.ShaderSystem.ShaderSystemBase.GetNameUniqueID"/> for details.</summary><param name="state"/><param name="id"/><param name="value"/>
		protected override bool SetAttribute(Xen.Graphics.ShaderSystem.ShaderSystemBase state, int id, ref Microsoft.Xna.Framework.Vector3 value)
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
		protected override bool SetAttribute(Xen.Graphics.ShaderSystem.ShaderSystemBase state, int id, ref Microsoft.Xna.Framework.Matrix value)
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
		protected override bool SetSamplerState(Xen.Graphics.ShaderSystem.ShaderSystemBase state, int id, Xen.Graphics.TextureSamplerState value)
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
		protected override bool SetTexture(Xen.Graphics.ShaderSystem.ShaderSystemBase state, int id, Microsoft.Xna.Framework.Graphics.Texture2D value)
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