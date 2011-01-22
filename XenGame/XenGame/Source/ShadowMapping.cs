using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xen;
using Xen.Camera;
using Xen.Graphics;
using Xen.Ex.Graphics;
using Xen.Ex.Graphics2D;
using Microsoft.Xna.Framework.Graphics;
using Xen.Ex.Graphics.Content;
using Xen.Ex.Material;



namespace GameClient
{

    //here, a custom Enum is created, which will flag the rendering mode of the tutorial.
    //This will be used as a DrawFlag
    enum RenderMode
    {
        Default,
        DepthOutput,	// depth will be drawn into the shadow map
        DrawShadow		// the shadow effect will be drawn
    }

    // This is the object that draws to the shadow map texture.
    // This class controls the rendering of the scene,
    // it sets up the draw flags that make the scene render Depth values
    class ShadowMapDrawer : IDraw
    {
        private ShadowOutputShaderProvider shaderProvider;
        private IDraw scene;

        public ShadowMapDrawer(IDraw scene, ShadowOutputShaderProvider shaderProvider)
        {
            this.scene = scene;
            this.shaderProvider = shaderProvider;
        }

        //get/set the scene
        public IDraw Scene
        {
            get { return scene; }
            set { if (value == null) throw new ArgumentNullException(); scene = value; }
        }

        public void Draw(DrawState state)
        {
            //set the draw flags up, which will control rendering
            //this will make the models render depth
            using (state.DrawFlags.Push(new ModelShaderProviderFlag(this.shaderProvider)))
            using (state.DrawFlags.Push(RenderMode.DepthOutput))
            {
                //draw the scene
                scene.Draw(state);

            }
        }

        public bool CullTest(ICuller culler)
        {
            return scene.CullTest(culler);
        }
    }


    //This class is a ShaderProvider, it overrides the shaders used by a ModelInstance.
    //This class will query the TutorialRenderMode, and bind the required shader.
    //
    //DepthOutput mode will draw the models using the Xen.Ex.Shaders.NonLinearDepthOutRg shaders,
    //these shaders ouput non-linear depth to RGB.
    //Non linear is easier to manage, as it's the natural output of the shadow map projection matrix.
    //
    //The linear versions of these shaders output linear depth to Red, and depth squared to Green.
    //
    sealed class ShadowOutputShaderProvider : Xen.Ex.Graphics.IModelShaderProvider
    {
        //no change to the shader:
        public IShader BeginModel(DrawState state, MaterialLightCollection lights)
        {
            return null;
        }
        public void EndModel(DrawState state)
        {
        }

        //return the shader to use:
        public IShader BeginGeometry(DrawState state, GeometryData geometry)
        {	//query the draw flag, 
            switch (state.DrawFlags.GetFlag<RenderMode>())
            {
                case RenderMode.DrawShadow:
                    {
                        //return the shader shader
                        Shaders.ShadowShader shader = state.GetShader<Shaders.ShadowShader>();

                        shader.TextureMap = geometry.MaterialData.Texture;

                        return shader;
                    }

                case RenderMode.DepthOutput:
                    return state.GetShader<Xen.Ex.Shaders.NonLinearDepthOut>();

                default:
                    return null;	//no change
            }
        }
    }
}
