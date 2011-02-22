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

        private readonly MaterialShader Material;

        public SimpleTerrain(ContentRegister content, Vector3 position, string modelDataName, MaterialLightCollection lights)
		{
            this.modelDataName = modelDataName;

			Matrix.CreateTranslation(ref position, out this.worldMatrix);

			//A ModelInstance can be created without any content...
			//However it cannot be used until the content is set
			model = new ModelInstance();

            //create the custom material for this geometry
            //the light collection has been passed into the constructor, although it
            //could easily be changed later (by changing material.Lights)
            this.Material = new MaterialShader(lights);

            //give the disk really bright specular for effect
            Material.SpecularColour = new Vector3(1, 1, 1);
            Material.DiffuseColour = new Vector3(0.6f, 0.6f, 0.6f);
            Material.SpecularPower = 64;

            //setup the texture samples to use high quality anisotropic filtering
            //the textures are assigned in LoadContent
            Material.Textures = new MaterialTextures();
            Material.Textures.TextureMapSampler = TextureSamplerState.AnisotropicHighFiltering;
            Material.Textures.NormalMapSampler = TextureSamplerState.AnisotropicLowFiltering;

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
                    using (state.Shader.Push(Material))
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

            //load the box texture, and it's normal map.
            Material.Textures.TextureMap = state.Load<Texture2D>(@"TerrainTest\terrain_desert_diffuse");
            Material.Textures.NormalMap = state.Load<Texture2D>(@"TerrainTest\terrain_desert_normal");
            //model.ShaderProvider = new Xen.Graphics.ShaderSystem.
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
}
