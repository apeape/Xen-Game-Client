using System;
using System.Text;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using Xen;
using Xen.Camera;
using Xen.Graphics;
using Xen.Ex.Graphics;
using Xen.Ex.Graphics2D;

using GameClient.Terrain;
using PolyVoxCore;
using System.Threading.Tasks;

/*
 * This sample demonstrates how to accurately represent light for rendering realistic lighting.
 * 
 * Specifically, the folling are demonstrated:
 * 
 *  High dynamic range lighting.
 *  The difference between linear and gamma space light storage.
 *  Tone mapping.
 *  Lens Exposure adjustment.
 *  Gamma correction.
 *  Spherical harmonic indirect lighting.
 *  RGBM HDR texture encoding and decoding.
 *  Bloom.
 *  
 */
namespace GameClient
{

    //This tutorial is split into multiple files, 
    //The following files are also included:
    //
    // RGBM Cubemap.cs    <-- A class for loading an RGBM encoded cubemap
    // Config.cs          <-- Classes that display the on screen config options


    // First, an overview of how this tutorial represents light:

    /*/
     * 
     * An important step to rendering a realistic image is gaining an understanding of how
     * light acts in the physical world.
     * 
     * Key to this understanding is the concept of linear and gamma space lighting.
     * While it may not be immediately obvious, the eye percieves light in a non-linear way.
     * 
     * Consider the example of two identical household lights in a dark room:
     * Turning on the first light will flood the room with bright light.
     * However, turning on the second light will (perceptually) only marginally increase the
     * brightness of the room. Physically, the room *has* doubled in brightness, however our
     * perception is a smaller increase in brightness compared to the first light.
     * 
     * 
     * A typical computer monitor will compensate for this perceptive difference.
     * Most standard computer monitors work with a gamma curve of 2.2.
     * 
     * What does this means?
     * It means that the light intensities sent to the monitor are modified before they are
     * displayed. A gamma curve of 2.2 simple means the following function is applied:
     * 
     * output intensity = pow(input, 2.2)
     * 
     * As seen in the following image:
     * http://en.wikipedia.org/wiki/File:Gamma06_600.png
     * 
     * What does this mean?
     * Simply, if you clear the screen to 0.5, the monitor will output a light intensity of
     * 0.218. If you clear to 0.75, it will output 0.53. Clear to 0.25, and the output is 0.047.
     * 
     * remember, input is the brighness value of the pixel, as sent to the monitor,
     * output is the real world intensity of the light produced by the monitor
     * 
     * input	output
     * 0.00		0.000
     * 0.25		0.047
     * 0.50		0.218
     * 0.75		0.530
     * 1.00		1.000
     * 
     * The key observation is that doubling the colour value of a pixel will actually more that
     * quadruple the intensity of light displayed by the monitor!
     * 
     * 
     * By extending this observation, it becomes clear that you must take gamma correction
     * into account when representing light in your game.
     * 
     * Linear light is very simple to calculate:
     * 0.25 + 0.25  = 0.5
     * 1.0  * 0.5	= 0.5
     * 
     * However this breaks down if you try and do the same thing in gamma space.
     * This can be shown by converting the numbers above using the table:
     * 
     * (numbers converted by the input->output table above):
     * 0.047 + 0.047	= 0.218
     * 1.0	 * 0.5		= 0.218
     * 
     * Clearly this output is totally incorrect, yet *many* games do exactly this!
     * 
     * The solution to this is quite simple:
     * Treat all rendering as Linear Space light, then apply the inverse of the gamma 2.2 function
     * when displaying the final rendered image on screen:
     * 
     * Normally, this would be done with a special hardware feature called sRGB blending and texture
     * sampling. However XNA doesn't expose this capability.
     * The only option is to render linear light directly into a high dynamic range render target,
     * then convert the linear light to gamma space light when displaying it on screen.
     * 
     * output = pow(rendered image, 1.0 / 2.2)
     * 
     * This process is known as Gamma Correction.
     * 
     * --------------------------
     * 
     * A further observation is that almost all texture assets in a game are often stored in
     * in Gamma Space. This is an important observation when dealing with artist drawn textures.
     * However, Computer generated textures (eg Normal Maps) are usually generated in Linear Space.
     * 
     * --------------------------
     * 
     * Summary of the key points:
     * Perform light calculations in linear space (2 + 2 = 4),
     * Apply an inverse gamma curve function when displaying this output to the screen,
     * And finally apply a gamma curve function to your non-linear input (such as colour textures)
     * 
     * 
     * 
     * 
     * 
     * However, that's not the end of the story:
     * There is still the matter of tone mapping.
     * 
     * Tone mapping is an entirely artificial process that is not realistic at all, however it
     * serves a very important purpose.
     * 
     * In the physical world, intensities vary by an enormous degree.
     * Surfaces in a bright office will be lit by approximately 200 LUX. 
     * (LUX being a measure of linear light intensity over an area).
     * However, direct sunlight at midday can be over 100,000 LUX. This is 500x brighter.
     * 
     * These numbers clearly demonstrate how important gamma correction is to realistically capture
     * light intensity. However even with gamma correction, the limited dynamic range of most monitors 
     * simply cannot deal with such an extreme dynamic range.
     * 
     * This is where Tone Mapping comes in. The goal of tone mapping is to display an image with 
     * extreme dynamic range on a monitor with a limited dynamic range.
     * 
     * For example, an image of a scene looking out a window from a dark office.
     * If the view outside the window is ~ 100x brighter than the foreground, then displaying the 
     * image on a display with limited dynamic range becomes quite difficult.
     * Either the office becomes exceptionally dark, or the view out the window is too bright for the
     * monitor, and it simply clamps to it's maximum value (white).
     * 
     * Tone mapping compresses the extreme bright values (and sometimes the extreme dark values) of an 
     * image to allow as much of the limited range of the monitor to be used for displaying the important
     * midtones (in this case, the office). While still retaining enough detail in the highlights and 
     * shadows to produce a recognisable image.
     * 
     * --------------------------
     * 
     * Tone mapping is not just for games, it's also used in movies. The following images are the tone
     * mapping curves for two commercial grade motion film brands:
     * 
     * http://www.fujifilm.com/products/motion_picture/lineup/eterna_cp_3521xd/#h3-2-2
     * http://motion.kodak.com/US/en/motion/Quicklinks/Curves/f002_1254ac.htm
     * 
     * As can be seen, the extreme bright and dark values are compressed to boost the recordable range.
     * 
     * --------------------------
     * 
     * This tutorial uses three tone mapping approximations:
     * 
     * The first is exponetial exposure tonemapping. This uses an exponential curve to compress extreme 
     * bright values. See here for an explaination:
     * http://freespace.virgin.net/hugo.elias/graphics/x_posure.htm
     * 
     * The second is a similar curve to exponetial exposure, 'Inverse +One', defined by (x / (x + 1)).
     * 
     * The final, and most complex, is an approximation to film tone mapping - where both light and dark
     * values are compressed. All credit goes to Jim Hejl of AMD:
     * output = rgb * (0.5 + 6.2 * rgb) / (0.06 + rgb * (1.7 + 6.2 * rgb));
     * Note: This approximation also does gamma correction too!
     * http://home.hejl.com/
     * 
     * 
     * --------------------------
     *
     * One final note:
     * 
     * For reasons of simplicity, this sample uses a gamma curve of 2.0, instead of 2.2.
     * This makes a number of functions much simpler:
     * 
     * Converting from Gamma -> Linear is pow(X,2.0), which is X * X.
     * Converting from Linear -> Gamma is pow(X,0.5), which is sqrt(X).
     * 
     * This is simpler and faster.
     * 
     * Squaring converts from Gamma to Linear,
     * Square Root converts from Linear to Gamma.
     * 
     * --------------------------
     * 
     * 
     * 
     * There are large number of shaders included in this project (in the Shaders directory)
     * 
     * These are:
     * 
     * AlphaOutput.fx		<-- A very simple shader that displays the alpha value of a texture
     * Background.fx		<-- A shader used to display the background cubemap
     * Character.fx			<-- The complex character rendering shader
     * Composite.fx			<-- A set of shaders for compositing to the screen, tone mapping, bloom, etc.
     * 
     * Headers:
     * Environment.fx.h		<-- Helper functions for sampling the Spherical Harmonic and Cubemap
     * RGBM.fx.h			<-- Helper functions for dealing with RGBM encoding/decoding
     * ShadowMap.fx.h		<-- Logic for sampling the character's shadow map
     * 
    /*/


    public class Game : Application
    {
        //This object configures how the scene is drawn
        private RenderConfiguration renderConfig;

        //An object that displays an editing dialog for configuring the rendering
        private RenderConfigEditor configEditor;


        //Current active scene config object
        private SceneConfiguration sceneConfig;

        //Scene specific configs:
        private SceneConfiguration DirtRoadConfig;
        private SceneConfiguration WaterfrontConfig;
        private SceneConfiguration ArchesConfig;
        private SceneConfiguration MillConfig;


        //primary camera
        private Xen.Camera.FirstPersonControlledCamera3D viewCamera;
        private Vector2 lastCameraRotationSensitivity;

        //draw targets:
        private DrawTargetScreen drawToScreen;						//final composite onto the screen,
        private DrawTargetTexture2D drawToRenderTarget;				//initial scene render target
        private DrawTargetTexture2D bloomRenderTarget;				//bloom render target
        private DrawTargetTexture2D bloomIntermediateRenderTarget;	//bloom bounce intermediate render target	

        //shadow mapping
        private Camera3D shadowCamera;								//shadow camera
        private DrawTargetTexture2D shadowMap;						//shadow draw target
        private ShadowMapDrawer shadowDrawer;			            //shadow class borrowed from tutorial 25

        //debug display elements (on screen textures)
        private TexturedElement bloomTextureDisplay;
        private TexturedElement rgbmTextureDisplay;
        private ShaderElement rgbmTextureAlphaDisplay;
        private Shaders.AlphaWrite rgmbTextureAlphaShader;

        //bloom and output shaders
        private Shaders.RgbmDecode outputShader;					//screen composite shader
        private Shaders.RgbmDecodeBloomPass bloomPassShader;		//bloom prepass shader

        //character rendering shader
        private Shaders.Character characterRenderShader;

        //bloom blur filter
        private Xen.Ex.Filters.BlurFilter bloomBlurPass;

        //storage for Spherical Harmonic in GPU format
        private readonly Vector4[] cubeMapGpuSH = new Vector4[9];

        //display model
        private ModelInstance model;
        private AnimationInstance modelAnimation;
        private DrawRotated modelRotation;							//draws the model, but rotating.

        //Draw statistics
        private Xen.Ex.Graphics2D.Statistics.DrawStatisticsDisplay drawStats;

        // Terrain
        private TerrainManager terrainManager;
        private float terrainNoiseDensity = 50.0f;
        private Stopwatch terrainGenerationTimer;

        protected override void Initialise()
        {
            //setup the view camera first
            //--------------------------------------

            viewCamera = new Xen.Camera.FirstPersonControlledCamera3D(this.UpdateManager);
            viewCamera.Projection.FieldOfView *= 0.90f;
            viewCamera.MovementSensitivity *= 0.05f;
            viewCamera.LookAt(new Vector3(-3, 4, 2), new Vector3(6, 6, 2), new Vector3(0, 1, 0));
            viewCamera.Projection.NearClip = 0.1f;

            //shadow map setup:
            //--------------------------------------

            const float shadowArea = 4;
            const int shadowMapResolution = 1024;

            //setup the shadow map rendering camera
            shadowCamera = new Camera3D();

            //setup the shadow map projection to roughly cover the character
            shadowCamera.Projection.Orthographic = true;
            shadowCamera.Projection.NearClip = shadowArea * 2;
            shadowCamera.Projection.FarClip = -shadowArea * 2;
            shadowCamera.Projection.Region = new Vector4(1, -1.8f, -1, 0.2f) * shadowArea;

            //setup the shadow map draw target

            //create the shadow map
            shadowMap = new DrawTargetTexture2D(shadowCamera, shadowMapResolution, shadowMapResolution, SurfaceFormat.HalfSingle, DepthFormat.Depth24);
            shadowMap.ClearBuffer.ClearColour = Color.White;

            //setup the shadow map drawer..
            shadowDrawer = new ShadowMapDrawer(null, new ShadowOutputShaderProvider());
            this.shadowMap.Add(shadowDrawer);



            //create the main draw targets.
            //--------------------------------------

            drawToScreen = new DrawTargetScreen(new Camera2D());
            drawToScreen.ClearBuffer.ClearColourEnabled = false;

            drawToRenderTarget = new DrawTargetTexture2D(viewCamera, this.WindowWidth, this.WindowHeight, SurfaceFormat.Color, DepthFormat.Depth24Stencil8, false, PreferredMultiSampleLevel.FourSamples, RenderTargetUsage.PlatformContents);
            drawToRenderTarget.ClearBuffer.ClearColourEnabled = false;

            //setup the bloom draw targets
            //--------------------------------------

            //scale to reduce the size of the bloom target, compared to main render target
            const int bloomDownsample = 8;	//eight times smaller

            bloomRenderTarget = new DrawTargetTexture2D(new Camera2D(), Math.Max(1, drawToRenderTarget.Width / bloomDownsample), Math.Max(1, drawToRenderTarget.Height / bloomDownsample), SurfaceFormat.Color, DepthFormat.None);
            bloomRenderTarget.ClearBuffer.ClearColourEnabled = false;

            bloomIntermediateRenderTarget = null;

            //the bloom intermediate target is not needed on the xbox, as the full bloom target fits in EDRAM
            bloomIntermediateRenderTarget = new DrawTargetTexture2D(viewCamera, bloomRenderTarget.Width, bloomRenderTarget.Height, SurfaceFormat.Color, DepthFormat.None);
            bloomIntermediateRenderTarget.ClearBuffer.ClearColourEnabled = false;

            //setup the blur filter, with a large 31 sample radius.
            bloomBlurPass = new Xen.Ex.Filters.BlurFilter(Xen.Ex.Filters.BlurFilterFormat.ThirtyOneSampleBlur_FilteredTextureFormat, 1.0f, bloomRenderTarget, bloomIntermediateRenderTarget);


            //setup the character model
            this.model = new ModelInstance();	//(the model is setup in LoadContent)
            this.modelRotation = new DrawRotated(model);
            this.modelRotation.RotationAngle = 3;

            //add the model to be drawn
            drawToRenderTarget.Add(modelRotation);

            //setup the shaders
            this.characterRenderShader = new Shaders.Character();

            //setup the output and bloom shaders
            outputShader = new Shaders.RgbmDecode();
            drawToScreen.Add(new ShaderElement(outputShader, new Vector2(1, 1), true));

            bloomPassShader = new Shaders.RgbmDecodeBloomPass();
            bloomRenderTarget.Add(new ShaderElement(bloomPassShader, new Vector2(1, 1), true));

            //add a background to be drawn
            drawToRenderTarget.Add(new BackgroundDrawer());


            //setup the debug image displays
            //--------------------------------------

            this.rgmbTextureAlphaShader = new Shaders.AlphaWrite();
            this.bloomTextureDisplay = new TexturedElement(this.bloomRenderTarget, new Vector2(0.2f, 0.2f), true);
            this.rgbmTextureDisplay = new TexturedElement(this.drawToRenderTarget, new Vector2(0.2f, 0.2f), true);
            this.rgbmTextureAlphaDisplay = new ShaderElement(this.rgmbTextureAlphaShader, new Vector2(0.2f, 0.2f), true);

            this.rgbmTextureAlphaDisplay.Position = new Vector2(0.7f, 0.2f);
            this.rgbmTextureDisplay.Position = new Vector2(0.7f, 0.4f);
            this.bloomTextureDisplay.Position = new Vector2(0.7f, 0.6f);

            this.drawToScreen.Add(this.rgbmTextureDisplay);
            this.drawToScreen.Add(this.rgbmTextureAlphaDisplay);
            this.drawToScreen.Add(this.bloomTextureDisplay);

            //setup the render config
            this.configEditor = new RenderConfigEditor(this.Content);

            this.drawToScreen.Add(configEditor);
            this.UpdateManager.Add(configEditor);


            //add a statistics overlay.
            drawStats = new Xen.Ex.Graphics2D.Statistics.DrawStatisticsDisplay(this.UpdateManager);
            drawToScreen.Add(drawStats);
        }

        protected override void Frame(FrameState state)
        {
            //setup the shadow map direction
            shadowCamera.LookAt(this.sceneConfig.SunDirection, new Vector3(), new Vector3(0, 1, 0));

            //bind the cubemap and it's SH

            //get the background Spherical Harmonic in GPU format
            this.sceneConfig.BackgroundScene.SphericalHarmonic.CopyToGpuArray(this.cubeMapGpuSH);

            //setup various shader globals
            state.ShaderGlobals.SetShaderGlobal("CubeRgbmTexture", this.sceneConfig.BackgroundScene.CubeMap);
            state.ShaderGlobals.SetShaderGlobal("EnvironmentSH", this.cubeMapGpuSH);

            state.ShaderGlobals.SetShaderGlobal("RgbmImageRenderScale", new Vector2(this.sceneConfig.RgbmImageScale, this.sceneConfig.RgbmRenderScale));

            //setup shader specific values:
            state.ShaderGlobals.SetShaderGlobal("BloomScaleThreshold", new Vector2(this.sceneConfig.BloomScale, this.sceneConfig.BloomThreshold));
            state.ShaderGlobals.SetShaderGlobal("LensExposure", this.renderConfig.LensExposure);

            //output specific constants
            outputShader.UseExposureTonemapping = this.renderConfig.UseExposureTonemapping ? 1 : 0;
            outputShader.UseFilmApproxTonemapping = this.renderConfig.UseFilmApproximationTonemapping ? 1 : 0;
            outputShader.UseInverseOneTonemapping = this.renderConfig.UseInverseOneTonemapping ? 1 : 0;
            outputShader.UseGammaCorrection = this.renderConfig.UseGammaCorrection ? 1 : 0;

            //setup constants specific to the model display shader.
            //These are shared between the two character shaders, so for simplicity they are global.
            state.ShaderGlobals.SetShaderGlobal("SkinLightScatter", this.sceneConfig.SkinLightScattering * this.renderConfig.SkinLightScatteringScale);
            state.ShaderGlobals.SetShaderGlobal("SunRgbIntensity", new Vector4(this.sceneConfig.SunColour, this.sceneConfig.SunIntensity));
            state.ShaderGlobals.SetShaderGlobal("SunDirection", this.sceneConfig.SunDirection);
            state.ShaderGlobals.SetShaderGlobal("SunSpecularPowerIntensity", new Vector2(this.sceneConfig.SunSpecularPower, this.sceneConfig.SunSpecularIntensity));

            state.ShaderGlobals.SetShaderGlobal("AmbientDiffuseSpecularScale", new Vector3(this.renderConfig.AmbientSphericalHarmonicScale, this.renderConfig.DiffuseLightingScale, this.renderConfig.SpecularLightingScale));
            state.ShaderGlobals.SetShaderGlobal("UseAlbedoOcclusionShadow", new Vector3(this.renderConfig.AlbedoTextureScale, this.renderConfig.AmbientOcclusionTextureScale, this.renderConfig.ShadowMapTermScale));


            //Ok, now get on with drawing.


            //set the shadow scene
            shadowDrawer.Scene = this.modelRotation;

            //draw the shadow map first
            if (this.renderConfig.ShadowMapTermScale > 0)
                shadowMap.Draw(state);


            //Set the shadow map projection shader globals:

            //Get the ICamera interface to the shadow camera
            //(ICamera exposes a lot of internally used methods, that are otherwise rarely needed)
            ICamera shadowCameraInterface = this.shadowCamera;

            //get the view*projection matrix of the shadow map
            Matrix view, projection, viewProjection;

            shadowCameraInterface.GetViewMatrix(out view);
            shadowCameraInterface.GetProjectionMatrix(out projection, shadowMap.Size);

            Matrix.Multiply(ref view, ref projection, out viewProjection);

            //set the shader globals
            state.ShaderGlobals.SetShaderGlobal("ShadowMapProjection", ref viewProjection);
            state.ShaderGlobals.SetShaderGlobal("ShadowMapSize", shadowMap.Size);
            state.ShaderGlobals.SetShaderGlobal("ShadowTexture", shadowMap.GetTexture());



            //draw the main render target
            drawToRenderTarget.Draw(state);

            //setup and draw the bloom pass
            bloomPassShader.InputTexture = drawToRenderTarget.GetTexture();
            bloomRenderTarget.Draw(state);

            //blur the bloom pass once
            bloomBlurPass.Draw(state);


            //setup the debug render views
            this.rgmbTextureAlphaShader.DisplayTexture = this.drawToRenderTarget.GetTexture();
            this.bloomTextureDisplay.Visible = this.renderConfig.ShowBloomRenderTarget;
            this.rgbmTextureDisplay.Visible = this.renderConfig.ShowEncodedRgbmRenderTarget;
            this.rgbmTextureAlphaDisplay.Visible = this.renderConfig.ShowEncodedRgbmRenderTarget;

            //setup the output shader
            outputShader.InputTexture = drawToRenderTarget.GetTexture();
            outputShader.BloomTexture = bloomRenderTarget.GetTexture();

            //draw everything else to the screen
            drawToScreen.Draw(state);
        }

        protected override void Update(UpdateState state)
        {
            configEditor.Instance = this.renderConfig;

            //lerp the lens exposure to the curent target exposure, this allows nice exposure transitions
            this.renderConfig.LensExposure = this.renderConfig.LensExposure * 0.95f + this.renderConfig.TargetLensExposure * 0.05f;

            //make sure the model is animating if the user wants it to...
            if ((modelAnimation.ValidAnimation & !modelAnimation.AnimationFinished) == this.renderConfig.PauseModelAnimation)
            {
                //user has changed the animating property..

                //the animated model is 180 degrees rotated, so...
                this.modelRotation.RotationAngle += MathHelper.Pi; //hack!

                if (this.renderConfig.PauseModelAnimation)
                {
                    //stop animation
                    this.modelAnimation.StopAnimation();
                }
                else
                {
                    //play the first animation
                    AnimationController anims = this.model.GetAnimationController();
                    if (anims.AnimationCount > 0)
                        this.modelAnimation = anims.PlayLoopingAnimation(0);
                }
            }

            if (!this.renderConfig.PauseModelRotation)
            {
                this.modelRotation.RotationAngle += state.DeltaTimeSeconds * 0.25f;
            }

            if (state.PlayerInput[PlayerIndex.One].InputState.Buttons.Back.OnPressed)
                this.Shutdown();

            if (state.KeyboardState.IsKeyDown(Keys.LeftControl))
            {
                if (state.PlayerInput[PlayerIndex.One].InputMapper.CentreMouseToWindow)
                {
                    state.PlayerInput[PlayerIndex.One].InputMapper.CentreMouseToWindow = false;
                    state.PlayerInput[PlayerIndex.One].InputMapper.MouseVisible = true;
                    if (viewCamera.RotationSensitivity != Vector2.Zero)
                        this.lastCameraRotationSensitivity = viewCamera.RotationSensitivity;
                    viewCamera.RotationSensitivity = Vector2.Zero;
                }
            }
            else
            {
                if (!state.PlayerInput[PlayerIndex.One].InputMapper.CentreMouseToWindow)
                {
                    state.PlayerInput[PlayerIndex.One].InputMapper.CentreMouseToWindow = true;
                    state.PlayerInput[PlayerIndex.One].InputMapper.MouseVisible = false;
                    viewCamera.RotationSensitivity = this.lastCameraRotationSensitivity;
                }
            }
        }

        protected override void InitialisePlayerInput(Xen.Input.PlayerInputCollection playerInput)
        {
            //setup so pressing Enter is the same as 'A', for interaction with the options menu
            playerInput[0].KeyboardMouseControlMapping.A = Microsoft.Xna.Framework.Input.Keys.Enter;
            playerInput[0].InputMapper.CentreMouseToWindow = true;
        }

        //callback from the render config
        public void ChangeScene()
        {
            //cycle the render config
            if (this.sceneConfig == this.DirtRoadConfig) this.sceneConfig = this.WaterfrontConfig;
            else if (this.sceneConfig == this.WaterfrontConfig) this.sceneConfig = this.ArchesConfig;
            else if (this.sceneConfig == this.ArchesConfig) this.sceneConfig = this.MillConfig;
            else if (this.sceneConfig == this.MillConfig) this.sceneConfig = this.DirtRoadConfig;

            //set the default camera for the scene.
            this.viewCamera.LookAt(this.sceneConfig.DefaultCamViewPos, this.sceneConfig.DefaultCamPos, new Vector3(0, 1, 0));
            //rotate the model a bit, to provide a cleaer transition
            this.modelRotation.RotationAngle += 1;

            this.renderConfig.TargetLensExposure = this.sceneConfig.DefaultLensExposure;

            SetupTerrainShader();
        }

        public void SetupTerrainShader()
        {
            this.terrainManager.terrainShader.LightDirection = this.sceneConfig.SunDirection;
            this.terrainManager.terrainShader.FogColor = new Color(171, 229, 255).ToVector3();
            this.terrainManager.terrainShader.FogStart = 190;
            this.terrainManager.terrainShader.FogEnd = 310;
        }

        //callback from the render config
        public void ScaleExposure(float scale)
        {
            this.renderConfig.TargetLensExposure *= scale;
        }

        protected override void SetupGraphicsDeviceManager(GraphicsDeviceManager graphics, ref RenderTargetUsage presentation)
        {
            if (graphics != null)
            {
                graphics.PreferredBackBufferWidth = 1280;
                graphics.PreferredBackBufferHeight = 720;
            }
        }

        private void SetTriplanarTextureScale()
        {
            //terrainDrawContext.Effect.Parameters["textureScale"].SetValue(0.015f + ((1 / cellRes) / 2.5f));
        }

        private void GenerateTerrain(int seed)
        {
            if (terrainManager != null)
                if (terrainManager.cellsInitialized > 0
                    && (terrainManager.cellsInitialized < terrainManager.terrainCells.Length))
                    return; // currently generating terrain

            const int initialCellRes = 16;
            const int initialTerrainRes = 4;
            terrainManager = new TerrainManager(initialTerrainRes, initialCellRes);
            terrainManager.InitializeCells();
            
            // set initialization state to false until we're actually done generating the cells
            terrainManager.Initialized = false;
            terrainManager.cellsInitialized = 0;

            //set texture scale for shader
            //if (terrainDrawContext != null)
                //SetTriplanarTextureScale();

            object terrainLock = new object();

            terrainGenerationTimer = new Stopwatch();
            terrainGenerationTimer.Start();

            // generate terrain
            Thread terrainProcessing = new Thread(delegate()
            {
                terrainManager.ForEachCell((cellPos, cell) =>
                {
                    // do each cell in its own thread
                    var t = Task.Factory.StartNew(() =>
                        {
                            // generate perlin 3d noise
                            Vector3 noiseOffset = cellPos;
                            noiseOffset.Y *= -1;
                            cell.PerlinNoise(noiseOffset * terrainManager.cellDimensions.X, terrainNoiseDensity, seed);

                            TerrainCellMesh mesh = new TerrainCellMesh(cell, cellPos, terrainManager.cellGap);
                            mesh.Calculate(terrainManager.cubicTerrain);
                            if (mesh == null)
                            {
                                throw new Exception("Problem generating mesh from volume!");
                            }
                            

                            lock (terrainLock)
                            {
                                terrainManager.terrainCellMeshes[(int)cellPos.X, (int)cellPos.Y, (int)cellPos.Z] = mesh;
                                if (mesh.Vertices != null)
                                    terrainManager.vertexCount += mesh.Vertices.Count;

                                terrainManager.cellsInitialized++;
                            }

                            //drawToRenderTarget.Add(new DrawRotated(terrainManager.terrainCellMeshes[(int)pos.X, (int)pos.Y, (int)pos.Z]));
                        });
                });

                // for each cell, create overlapping data with
                // neighboring cells so we can avoid ugly border seams
                // then, generate a mesh for each cell
                /*
                cell.ForEach(innerPos =>
                    {
                        if ((cellPos.X != 0) && (innerPos.X == 0))
                        {
                            // get neighbor cell border density
                            VolumeDensity8 neighborCell = terrainManager.GetCell(cellPos.X - 1, cellPos.Y, cellPos.Z);
                            byte density = neighborCell.getVoxelAt(cell.getWidth(), innerPos.Y, innerPos.Z).getDensity();
                            //byte density2 = cell.getVoxelAt(innerPos).getDensity();
                            //density = (byte)((density + density2) / 2);
                            // set current cell border to match
                            cell.setDensityAt(innerPos.X - 1, innerPos.Y, innerPos.Z, density);
                        }

                        if ((cellPos.X != terrainManager.terrainDimensions.X)
                            && (innerPos.X == cell.getWidth()))
                        {
                            // get neighbor cell border density
                            VolumeDensity8 neighborCell = terrainManager.GetCell(cellPos.X + 1, cellPos.Y, cellPos.Z);
                            byte density = neighborCell.getVoxelAt(0, innerPos.Y, innerPos.Z).getDensity();
                            //byte density2 = cell.getVoxelAt(innerPos).getDensity();
                            //density = (byte)((density + density2) / 2);
                            // set current cell border to match
                            cell.setDensityAt(innerPos.X + 1, innerPos.Y, innerPos.Z, density);
                        }

                        if ((cellPos.Y != 0) && (innerPos.Y == 0))
                        {
                            // get neighbor cell border density
                            VolumeDensity8 neighborCell = terrainManager.GetCell(cellPos.X, cellPos.Y - 1, cellPos.Z);
                            byte density = neighborCell.getVoxelAt(innerPos.X, cell.getHeight(), innerPos.Z).getDensity();
                            //byte density2 = cell.getVoxelAt(innerPos).getDensity();
                            //density = (byte)((density + density2) / 2);
                            // set current cell border to match
                            cell.setDensityAt(innerPos.X, innerPos.Y - 1, innerPos.Z, density);
                        }

                        if ((cellPos.Y != terrainManager.terrainDimensions.Y)
                            && (innerPos.Y == cell.getHeight()))
                        {
                            // get neighbor cell border density
                            VolumeDensity8 neighborCell = terrainManager.GetCell(cellPos.X, cellPos.Y + 1, cellPos.Z);
                            byte density = neighborCell.getVoxelAt(innerPos.X, 0, innerPos.Z).getDensity();
                            //byte density2 = cell.getVoxelAt(innerPos).getDensity();
                            //density = (byte)((density + density2) / 2);
                            // set current cell border to match
                            cell.setDensityAt(innerPos.X, innerPos.Y + 1, innerPos.Z, density);
                        }

                        if ((cellPos.Z != 0) && (innerPos.Z == 0))
                        {
                            // get neighbor cell border density
                            VolumeDensity8 neighborCell = terrainManager.GetCell(cellPos.X, cellPos.Y, cellPos.Z - 1);
                            byte density = neighborCell.getVoxelAt(innerPos.X, innerPos.Y, cell.getDepth()).getDensity();
                            //byte density2 = cell.getVoxelAt(innerPos).getDensity();
                            //density = (byte)((density + density2) / 2);
                            // set current cell border to match
                            cell.setDensityAt(innerPos.X, innerPos.Y, innerPos.Z - 1, density);
                        }
                        if ((cellPos.Z != terrainManager.terrainDimensions.Z)
                            && (innerPos.Z == cell.getDepth()))
                        {
                            // get neighbor cell border density
                            VolumeDensity8 neighborCell = terrainManager.GetCell(cellPos.X, cellPos.Y, cellPos.Z + 1);
                            byte density = neighborCell.getVoxelAt(innerPos.X, innerPos.Y, 0).getDensity();
                            //byte density2 = cell.getVoxelAt(innerPos).getDensity();
                            //density = (byte)((density + density2) / 2);
                            // set current cell border to match
                            cell.setDensityAt(innerPos.X, innerPos.Y, innerPos.Z + 1, density);
                        }
                    });*/

            });
            terrainProcessing.IsBackground = true;
            terrainProcessing.Start();
            drawToRenderTarget.Add(terrainManager);
        }


        protected override void LoadContent(ContentState state)
        {
            //load the default font.
            drawStats.Font = state.Load<SpriteFont>("Arial");

            //load the character model
            model.ModelData = state.Load<Xen.Ex.Graphics.Content.ModelData>(@"xna_dude/dude");

            //set the shader on the model
            model.ShaderProvider = new CharacterShaderProvider(characterRenderShader, model.ModelData, state.ContentRegister, "xna_dude");

            //setup the render configuration
            this.renderConfig = new RenderConfiguration(this);
            renderConfig.UseGammaCorrection = true;
            renderConfig.UseExposureTonemapping = true;
            renderConfig.AmbientSphericalHarmonicScale = 1;
            renderConfig.DiffuseLightingScale = 1;
            renderConfig.SpecularLightingScale = 1;
            renderConfig.AlbedoTextureScale = 1;
            renderConfig.AmbientOcclusionTextureScale = 1;
            renderConfig.ShadowMapTermScale = 1;
            renderConfig.SkinLightScatteringScale = 1;
            renderConfig.ShowBloomRenderTarget = false;
            renderConfig.ShowEncodedRgbmRenderTarget = false;
            renderConfig.PauseModelAnimation = false;
            renderConfig.PauseModelRotation = false;
            renderConfig.TargetLensExposure = 0.3f;


            //setup common defaults for the scene configs.
            SceneConfiguration defaultSC = new SceneConfiguration();
            defaultSC.RgbmImageScale = 20.0f;
            defaultSC.BloomThreshold = 1.5f;
            defaultSC.BloomScale = 1.0f;
            defaultSC.SunSpecularPower = 32.0f;
            defaultSC.SunSpecularIntensity = 30.0f;
            defaultSC.RgbmRenderScale = 200.0f;
            defaultSC.SkinLightScattering = new Vector3(0.15f, 0.02f, 0.002f); // 15% of the red channel is transferred under the skin


            //Load the source cubemap scene textures.

            //Note: To make it easier to view the source images, the RGBM images have been split into two images,
            //One store the RGB portion, one stores the M portion. This makes it much easier to view the textures
            //and see how they are stored - but it is also extremely wasteful, using 2x the texture data.

            //Ideally, the images would be loaded directly as a PNG, however this cannot easily be done on the Xbox.

            //Because these textures are only uses locally as an image data source, they are loaded in a temporary 
            //content manager, which is disposed at the end of this method.
            ContentManager localManager = new ContentManager(this.Services, state.ContentRegister.RootDirectory);

            Texture2D textureDirtroad = localManager.Load<Texture2D>("LightProbes/DirtRoadHDR.rgb");
            Texture2D textureWaterfront = localManager.Load<Texture2D>("LightProbes/WaterfrontHDR.rgb");
            Texture2D textureArches = localManager.Load<Texture2D>("LightProbes/ArchesHDR.rgb");
            Texture2D textureMill = localManager.Load<Texture2D>("LightProbes/MillHDR.rgb");

            Texture2D textureDirtroadAlpha = localManager.Load<Texture2D>("LightProbes/DirtRoadHDR.m");
            Texture2D textureWaterfrontAlpha = localManager.Load<Texture2D>("LightProbes/WaterfrontHDR.m");
            Texture2D textureArchesAlpha = localManager.Load<Texture2D>("LightProbes/ArchesHDR.m");
            Texture2D textureMillAlpha = localManager.Load<Texture2D>("LightProbes/MillHDR.m");

            //setup DirtRoadHDR specifics
            this.DirtRoadConfig = defaultSC.Clone();
            this.DirtRoadConfig.BackgroundScene = new RgbmCubeMap(textureDirtroad, textureDirtroadAlpha, this.DirtRoadConfig.RgbmImageScale);
            this.DirtRoadConfig.SunColour = new Vector3(1, 0.9f, 0.75f);
            this.DirtRoadConfig.SunDirection = Vector3.Normalize(new Vector3(0, 0.1f, 1));
            this.DirtRoadConfig.SunIntensity = 20.0f;
            this.DirtRoadConfig.DefaultLensExposure = 0.15f;
            this.DirtRoadConfig.DefaultCamPos = new Vector3(6.062489f, 4.9959f, -0.6131198f);
            this.DirtRoadConfig.DefaultCamViewPos = new Vector3(5.147717f, 5.02338f, -0.2100832f);

            //setup Arches specifics
            this.ArchesConfig = defaultSC.Clone();
            this.ArchesConfig.BackgroundScene = new RgbmCubeMap(textureArches, textureArchesAlpha, this.ArchesConfig.RgbmImageScale);
            this.ArchesConfig.SunColour = new Vector3(1, 1, 1);
            this.ArchesConfig.SunDirection = Vector3.Normalize(new Vector3(-0.4f, 0.4f, 0.5f));
            this.ArchesConfig.SunIntensity = 15.0f;
            this.ArchesConfig.DefaultLensExposure = 0.15f;
            this.ArchesConfig.DefaultCamPos = new Vector3(-2.667145f, 6.280345f, 4.98485f);
            this.ArchesConfig.DefaultCamViewPos = new Vector3(-2.470318f, 6.176862f, 4.009888f);
            //this.ArchesConfig.BloomScale = 0f; // temporary bloom disable for wireframe

            //setup WaterfrontHDR specifics
            this.WaterfrontConfig = defaultSC.Clone();
            this.WaterfrontConfig.BackgroundScene = new RgbmCubeMap(textureWaterfront, textureWaterfrontAlpha, this.WaterfrontConfig.RgbmImageScale);
            this.WaterfrontConfig.SunColour = new Vector3(0.9f, 0.95f, 1);
            this.WaterfrontConfig.SunDirection = Vector3.Normalize(new Vector3(-0.2f, 1, 0));
            this.WaterfrontConfig.SunIntensity = 15.0f;
            this.WaterfrontConfig.DefaultLensExposure = 0.35f;
            this.WaterfrontConfig.DefaultCamPos = new Vector3(5.251021f, 5.877438f, -2.74239f);
            this.WaterfrontConfig.DefaultCamViewPos = new Vector3(4.579107f, 5.783906f, -2.007691f);

            //setup MillHDR specifics
            this.MillConfig = defaultSC.Clone();
            this.MillConfig.BackgroundScene = new RgbmCubeMap(textureMill, textureMillAlpha, this.MillConfig.RgbmImageScale);
            this.MillConfig.SunColour = new Vector3(1, 0.975f, 0.95f);
            this.MillConfig.SunDirection = Vector3.Normalize(new Vector3(-1, 1, -1));
            this.MillConfig.SunIntensity = 25.0f;
            this.MillConfig.DefaultLensExposure = 0.5f;
            this.MillConfig.BloomScale = 0.5f;
            this.MillConfig.BloomThreshold = 1.0f;
            this.MillConfig.DefaultCamPos = new Vector3(6.087461f, 6.132507f, -0.8147218f);
            this.MillConfig.DefaultCamViewPos = new Vector3(5.203656f, 5.989332f, -0.3693113f);


            this.sceneConfig = this.ArchesConfig;

            //Textures are no longer needed. 
            localManager.Dispose();
            localManager = new ContentManager(this.Services, state.ContentRegister.RootDirectory);

            // Generate terrain
            const int terrainSeed = 70;
            GenerateTerrain(terrainSeed);

            terrainManager.SideTexture = localManager.Load<Texture2D>("TerrainTextures/cliffs2");
            terrainManager.BottomTexture = localManager.Load<Texture2D>("TerrainTextures/rock1");
            terrainManager.TopTexture = localManager.Load<Texture2D>("TerrainTextures/grassyrock2");
            
            terrainManager.terrainShader.SideTexture = terrainManager.SideTexture;
            terrainManager.terrainShader.BottomTexture = terrainManager.BottomTexture;
            terrainManager.terrainShader.TopTexture = terrainManager.TopTexture;

            terrainManager.terrainShader.TextureScale = 0.1f + ((1 / terrainManager.cellDimensions.X) / 2.5f);

            SetupTerrainShader();
        }
    }



    //Helper classes:

    //draw the character with a specific rotation
    class DrawRotated : IDraw
    {
        public float RotationAngle;
        private readonly IDraw item;

        public DrawRotated(IDraw item)
        {
            this.item = item;
        }

        public void Draw(DrawState state)
        {
            //generate the rotation matrix for the object.
            Matrix basis = new Matrix(1, 0, 0, 0, 0, 0, -1, 0, 0, 1, 0, 0, 0, 0, 0, 1); //xna_dude model is on his side

            using (state.WorldMatrix.PushMultiply(ref basis))
            {
                Matrix.CreateRotationZ(RotationAngle, out basis); // generate the rotation.
                state.WorldMatrix.Multiply(ref basis);

                if (item.CullTest(state))
                    item.Draw(state);
            }
        }

        bool ICullable.CullTest(ICuller culler)
        {
            return true;
        }
    }


    //This class provides the shaders for the character, and also loads extra textures for the character model
    class CharacterShaderProvider : IModelShaderProvider
    {
        //The character model also has a set of 'SOF' textures. These textures encode three values,
        //S: Specular Intensity (Red)
        //O: Ambient Occlusion (Green)
        //F: Skin / Face regions (Blue)
        private readonly Texture2D[] SofTextures;

        //the shader that is used to display the character
        private readonly Shaders.Character shader;

        //constructor, including shaders
        //This constructor is called from the LoadContent method, the ContentManager is passed in.
        public CharacterShaderProvider(Shaders.Character shader, Xen.Ex.Graphics.Content.ModelData model, ContentManager manager, string assetLocation)
        {
            //Generate a list of SOF textures for this model
            List<Texture2D> textures = new List<Texture2D>();

            assetLocation = assetLocation ?? "";
            if (assetLocation.Length > 0)
                assetLocation += "/";

            //loop through all the geometries in the model
            foreach (Xen.Ex.Graphics.Content.MeshData mesh in model.Meshes)
            {
                foreach (Xen.Ex.Graphics.Content.GeometryData geom in mesh.Geometry)
                {
                    //take the existing texture file name and add 'SOF' on the end, to find the skin/occlusion/face+skin texture

                    //Normally, XNA would add '_0' to the end of every texture asset, so each model has a unique copy of the
                    //asset (incase a model sets up different processing options for the asset).

                    //Xen supports the ability to set 'Manual Texture Import' property to true.
                    //When this is set to true, the model will not directly process the textures, it
                    //will simply assume they are processed. This requires each texture is manually added to the project.
                    //When this is true, the textures are not renamed.

                    //Try and load a texture with 'SOF' on the end.
                    Texture2D texture = manager.Load<Texture2D>(assetLocation + geom.MaterialData.TextureFileName + "SOF");
                    textures.Add(texture);
                }
            }

            //store the textures
            this.SofTextures = textures.ToArray();

            this.shader = shader;
        }

        //IModelShaderProvider implementation:

        public IShader BeginModel(DrawState state, Xen.Ex.Material.MaterialLightCollection lights)
        {
            return null;
        }
        public void EndModel(DrawState state)
        {
        }

        //get the shader specific to this geometry.
        public IShader BeginGeometry(DrawState state, Xen.Ex.Graphics.Content.GeometryData geometry)
        {
            //setup the shader specifics, including texture, normal map and SOF texture
            shader.AlbedoTexture = geometry.MaterialData.Texture;
            shader.NormalTexture = geometry.MaterialData.NormalMap;
            shader.SofTexture = this.SofTextures[geometry.Index];
            return shader;
        }
    }
}
