using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

using PolyVoxCore;

using Xen;
using Xen.Graphics;
using Xen.Ex.Material;
using Jitter.LinearMath;
using Jitter.Collision.Shapes;
using Jitter.Dynamics;

namespace GameClient.Terrain
{
    public class TerrainCellMesh : IDraw
    {
        private SurfaceExtractorDensity8 surfaceExtractor;
        private CubicSurfaceExtractorWithNormalsDensity8 cubicSurfaceExtractor;
        private SurfaceMeshPositionMaterialNormal surface;

        public Vector3 Position { get; set; }

        public Vertices<VertexPositionNormalTexture> Vertices { get; set; }
        public Indices<short> Indices { get; set; }

        // physics rigid body
        //public ConvexHullShape ConvexHullShape { get; set; }
        public RigidBody RigidBody { get; set; }

        public bool isEmpty { get; set; }

        //world matrix (position, scale, rotation, etc)
        private Matrix worldMatrix;

        private BoundingBox boundingBox;
        private Vertices<VertexPositionNormalTexture> bbVertexBuffer;
        private Indices<int> bbIndexBuffer;

        public float cellScale { get; set; }

        public TerrainCellMesh(VolumeDensity8 volume, Vector3 pos)
            : this(volume, pos, TerrainManager.defaultCellScale)
        {
        }

        public TerrainCellMesh(VolumeDensity8 volume, Vector3 pos, float scale)
        {
            Position = pos;

            //Setup the world matrix
            this.worldMatrix = Matrix.CreateScale(scale)
                * Matrix.CreateRotationY(MathHelper.Pi)
                * Matrix.CreateTranslation(pos * volume.getWidth());

            surface = new SurfaceMeshPositionMaterialNormal();
            volume.setBorderValue(new Density8(0));
            surfaceExtractor = new SurfaceExtractorDensity8(volume, volume.getEntireVolumePaddedBorder(), surface);
            cubicSurfaceExtractor = new CubicSurfaceExtractorWithNormalsDensity8(volume, volume.getEntireVolumePaddedBorder(), surface);

        }

        public void Calculate(bool cubic)
        {
            if (cubic)
                cubicSurfaceExtractor.execute();
            else
                surfaceExtractor.execute();

            Random random = new Random();

            PositionMaterialNormalVector verts = surface.getVertices();
            isEmpty = (verts.Count == 0);
            if (!isEmpty)
            {
                // TODO: redo polyvox wrapper to generate these instead of using LINQ hilarity

                VertexPositionNormalTexture[] vertsConverted = verts.ToVertexPositionNormalTextureArray();
                Vertices = new Vertices<VertexPositionNormalTexture>(vertsConverted);

                // convert indices to xna format
                Indices = new Indices<short>(surface.getIndices().Select(i => (short)i).ToArray());

                // calculate bounding box for culling
                CreateBoundingBox(vertsConverted.Select(v => v.Position));

                isEmpty = (Indices.Count == 0);
            }
        }

        public void CalculatePhysicsHull()
        {
            List<JOctree.TriangleVertexIndices> collisionIndices = new List<JOctree.TriangleVertexIndices>();
            var indices = surface.getIndices();
            for (int i = 0; i < indices.Count / 3; i++)
            {
                collisionIndices.Add(new JOctree.TriangleVertexIndices((int)indices[i*3], (int)indices[i*3 + 1], (int)indices[i*3 + 2]));
            }

            PositionMaterialNormalVector verts = surface.getVertices();
            List<JVector> collisionVertPositions = verts.Select<PositionMaterialNormal, JVector>(v => v.position.ToJVector()).ToList();

            JOctree collisionVerts = new JOctree(collisionVertPositions, collisionIndices);
            TriangleMeshShape shape = new TriangleMeshShape(collisionVerts);            
            RigidBody = new RigidBody(shape);
            RigidBody.IsStatic = true; // terrain is immovable
        }

        public void Draw(DrawState state)
        {
            if (isEmpty)
                throw new InvalidOperationException("Cell data is null!");

            //First, push the world matrix, multiplying by the current matrix (if there is one).
            //This is very similar to using openGL glPushMatrix() and then glMultMatrix().
            //The DrawState object maintains the world matrix stack, pushing and popping this stack is very fast.
            using (state * this.worldMatrix)
            {
                // we need clockwise culling because polyvox generates vertices in the opengl format
                state.RenderState.CurrentRasterState.CullMode = CullMode.CullClockwiseFace;
                //draw the geometry
                Vertices.Draw(state, Indices, PrimitiveType.TriangleList);
                state.RenderState.CurrentRasterState.CullMode = CullMode.CullCounterClockwiseFace;
            }
        }

        /// <summary>
        /// FrustumCull test the cell
        /// </summary>
        /// <param name="culler"></param>
        /// <returns></returns>
        public bool CullTest(ICuller culler)
        {
            if (isEmpty) return false; // cull empty cells without testing
            return culler.TestBox(ref boundingBox.Min, ref boundingBox.Max, ref this.worldMatrix);
        }

        public void CreateBoundingBox(IEnumerable<Vector3> vertices)
        {
            boundingBox = BoundingBox.CreateFromPoints(vertices);

            VertexPositionNormalTexture[] verts = new VertexPositionNormalTexture[8];
            int[] indices = new int[]
            {
                0, 1,
                1, 2,
                2, 3,
                3, 0,
                0, 4,
                1, 5,
                2, 6,
                3, 7,
                4, 5,
                5, 6,
                6, 7,
                7, 4,
            };

            Vector3[] corners = boundingBox.GetCorners();
            for (int i = 0; i < 8; i++)
            {
                verts[i].Position = corners[i];
                verts[i].Normal = Vector3.Zero;
            }

            bbVertexBuffer = new Vertices<VertexPositionNormalTexture>(verts);
            bbIndexBuffer = new Indices<int>(indices);
        }

        public void DrawBoundingBox(DrawState state)
        {
            state.RenderState.CurrentRasterState.FillMode = FillMode.WireFrame;

            using (state * this.worldMatrix)
                bbVertexBuffer.Draw(state, bbIndexBuffer, PrimitiveType.LineList);

            state.RenderState.CurrentRasterState.FillMode = FillMode.Solid;
 
        }
    }
}
