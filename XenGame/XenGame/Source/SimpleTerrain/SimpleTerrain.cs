using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xen;
using Xen.Camera;
using Xen.Graphics;
using Xen.Ex.Graphics;
using Xen.Ex.Graphics2D;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using Jitter.LinearMath;
using Jitter.Collision.Shapes;
using Jitter.Dynamics;
using Xen.Ex.Material;

namespace GameClient
{
    public class JMeshData
    {
        public List<JVector> Vertices { get; set; }
        public List<JOctree.TriangleVertexIndices> Indices { get; set; }
    }

    public class SimpleTerrain : IDraw, IContentOwner
    {
		private ModelInstance model;
		private Matrix worldMatrix;
        private string modelDataName;

        // physics rigid body
        //public ConvexHullShape ConvexHullShape { get; set; }
        public RigidBody RigidBody { get; set; }

        private Shaders.SimpleTerrain terrainShader;

        public SimpleTerrain(ContentRegister content, Vector3 position, string modelDataName, MaterialLightCollection lights)
		{
            this.modelDataName = modelDataName;

			Matrix.CreateTranslation(ref position, out this.worldMatrix);

			//A ModelInstance can be created without any content...
			//However it cannot be used until the content is set
			this.model = new ModelInstance();

            this.terrainShader = new Shaders.SimpleTerrain();

            //add to the content register and load textures/etc
            content.Add(this);
		}

		public void Draw(DrawState state)
		{
			using (state.WorldMatrix.Push(ref worldMatrix))
			{
				//ModelInstances automatically setup the default material shaders
				//Custom shaders can be used with model.SetShaderOverride(...)

				//ModelData stores accurate bounding box information
				//the ModelInstance uses this to cull the model

                if (model.CullTest(state))
                {
                    //using (state.Shader.Push(Material))
                    {
                        //state.RenderState.CurrentRasterState.FillMode = FillMode.WireFrame;
                        model.Draw(state);
                        //state.RenderState.CurrentRasterState.FillMode = FillMode.Solid;
                    }
                }
			}
		}

		public void LoadContent(ContentState state)
		{
			//load the model data into the model instance
			model.ModelData = state.Load<Xen.Ex.Graphics.Content.ModelData>(this.modelDataName);

            model.ShaderProvider = new SimpleTerrainShaderProvider(terrainShader, model.ModelData, state.ContentRegister, "TerrainTest");
		}

		public bool CullTest(ICuller culler)
		{
			return true;
		}

        public void CalculatePhysicsHull()
        {
            JMeshData meshData = model.ModelData.ExtractData();
            JOctree collisionVerts = new JOctree(meshData.Vertices, meshData.Indices);
            TriangleMeshShape shape = new TriangleMeshShape(collisionVerts);
            RigidBody = new RigidBody(shape);
            RigidBody.IsStatic = true; // terrain is immovable
        }

    }


    //This class provides the shaders for the character, and also loads extra textures for the character model
    class SimpleTerrainShaderProvider : IModelShaderProvider
    {
        //The character model also has a set of 'SOF' textures. These textures encode three values,
        //S: Specular Intensity (Red)
        //O: Ambient Occlusion (Green)
        //F: Skin / Face regions (Blue)
        private readonly Texture2D[] SofTextures;

        //the shader that is used to display the character
        private readonly Shaders.SimpleTerrain shader;

        //constructor, including shaders
        //This constructor is called from the LoadContent method, the ContentManager is passed in.
        public SimpleTerrainShaderProvider(Shaders.SimpleTerrain shader, Xen.Ex.Graphics.Content.ModelData model, ContentManager manager, string assetLocation)
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
