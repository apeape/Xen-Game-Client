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

        public SimpleTerrain(ContentRegister content, Vector3 position, string modelDataName)
		{
            this.modelDataName = modelDataName;

			Matrix.CreateTranslation(ref position, out this.worldMatrix);

			//A ModelInstance can be created without any content...
			//However it cannot be used until the content is set

			model = new ModelInstance();

			//add to the content register
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
                    //state.RenderState.CurrentRasterState.FillMode = FillMode.WireFrame;
                    model.Draw(state);
                    //state.RenderState.CurrentRasterState.FillMode = FillMode.Solid;
                }
			}
		}

		public void LoadContent(ContentState state)
		{
			//load the model data into the model instance
			model.ModelData = state.Load<Xen.Ex.Graphics.Content.ModelData>(this.modelDataName);
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
