using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PolyVoxCore;
using Microsoft.Xna.Framework;
using GameClient.Util;
using GameClient.Terrain;
using Microsoft.Xna.Framework.Graphics;
using Jitter.LinearMath;
using Xen.Graphics;
using Xen.Ex.Graphics.Content;

namespace GameClient
{
    public static class Extensions
    {
        /// <summary>
        /// Apply an action to each voxel in the volume
        /// </summary>
        /// <param name="volume"></param>
        /// <param name="action"></param>
        public static void ForEach(this VolumeDensity8 volume, Action<Vector3> action)
        {
            Vector3 pos;
            // loop over entire volume
            for (ushort z = 0; z < volume.getDepth(); z++)
                for (ushort y = 0; y < volume.getHeight(); y++)
                    for (ushort x = 0; x < volume.getWidth(); x++)
                    {
                        pos = new Vector3(x, y, z);
                        action(pos);
                    }
        }

        public static void setDensityAt(this VolumeDensity8 volume, int X, int Y, int Z, byte density)
        {
            //Get the old voxel
            Density8 voxel = volume.getVoxelAt(X, Y, Z);

            //Modify the density
            voxel.setDensity(density);

            //Write the voxel value into the volume
            volume.setVoxelAt(X, Y, Z, voxel);
        }

        public static void setDensityAt(this VolumeDensity8 volume, float X, float Y, float Z, byte density)
        {
            volume.setDensityAt((int)X, (int)Y, (int)Z, density);
        }

        public static void setDensityAt(this VolumeDensity8 volume, Vector3 point, byte density)
        {
            volume.setDensityAt(point.X, point.Y, point.Z, density);
        }

        public static Density8 getVoxelAt(this VolumeDensity8 volume, Vector3 point)
        {
            return volume.getVoxelAt(point.X, point.Y, point.Z);
        }

        public static Density8 getVoxelAt(this VolumeDensity8 volume, int X, int Y, int Z)
        {
            return volume.getVoxelAt((ushort)X, (ushort)Y, (ushort)Z);
        }

        public static Density8 getVoxelAt(this VolumeDensity8 volume, float X, float Y, float Z)
        {
            return volume.getVoxelAt((ushort)X, (ushort)Y, (ushort)Z);
        }

        public static bool setVoxelAt(this VolumeDensity8 volume, int X, int Y, int Z, Density8 density)
        {
            return volume.setVoxelAt((ushort)X, (ushort)Y, (ushort)Z, density);
        }

        public static bool setVoxelAt(this VolumeDensity8 volume, float X, float Y, float Z, Density8 density)
        {
            return volume.setVoxelAt((ushort)X, (ushort)Y, (ushort)Z, density);
        }

        public static bool setVoxelAt(this VolumeDensity8 volume, Vector3 point, Density8 density)
        {
            return volume.setVoxelAt((ushort)point.X, (ushort)point.Y, (ushort)point.Z, density);
        }

        public static void CreateSphere(this VolumeDensity8 volume, float radius, byte density)
        {
            Vector3 volumeCenter = new Vector3(volume.getWidth() / 2, volume.getHeight() / 2, volume.getDepth() / 2);
            volume.CreateSphere(volumeCenter, radius, density);
        }

        public static void CreateSphere(this VolumeDensity8 volume, Vector3 center, float radius, byte density)
        {
            volume.ForEach(
                currentPos =>
                {
                    //Compute how far the current position is from the center of the volume
                    float distToCenter = (currentPos - center).Length();

                    //If the current voxel is less than 'radius' units from the center then we make it solid.
                    if (distToCenter <= radius)
                        volume.setDensityAt(currentPos, density);
                });
        }

        public static void DeleteRandomCells(this VolumeDensity8 volume, byte percentToDelete)
        {
            Random random = new Random();
            volume.ForEach(
                currentPos =>
                {
                    if (random.Next(100) <= percentToDelete)
                        volume.setDensityAt(currentPos, 0);
                });
        }

        public static void PerlinNoise(this VolumeDensity8 volume, double densityDivisor)
        {
            volume.PerlinNoise(densityDivisor, 99);
        }

        public static void PerlinNoise(this VolumeDensity8 volume, Vector3 offset, double densityDivisor)
        {
            volume.PerlinNoise(offset, densityDivisor, 99);
        }

        public static void PerlinNoise(this VolumeDensity8 volume, double densityDivisor, int seed)
        {
            volume.ForEach(curPos => volume.setDensityAt(curPos, PerlinNoise(curPos, densityDivisor, seed)));
        }

        public static void PerlinNoise(this VolumeDensity8 volume, Vector3 offset, double densityDivisor, int seed)
        {
            volume.ForEach(curPos => volume.setDensityAt(curPos, PerlinNoise(curPos - offset, densityDivisor, seed)));
        }

        public static byte PerlinNoise(Vector3 pos, double densityDivisor)
        {
            return PerlinNoise(pos, densityDivisor, 99);
        }

        public static byte PerlinNoise(Vector3 pos, double densityDivisor, int seed)
        {
            PerlinNoise perlinNoise = new PerlinNoise(seed);

            //Vector3 warp;
            //warp.X = (float)perlinNoise.Noise(pos.X * 0.004, pos.X * 0.004, pos.X * 0.004);
            //warp.Y = (float)perlinNoise.Noise(pos.Y * 0.004, pos.Y * 0.004, pos.Y * 0.004);
            //warp.Z = (float)perlinNoise.Noise(pos.Z * 0.004, pos.Z * 0.004, pos.Z * 0.004);
            //pos += warp * 8;

            

            double widthDivisor = 1 / (double)densityDivisor / 2;
            double heightDivisor = 1 / (double)densityDivisor * 3;
            double depthDivisor = 1 / (double)densityDivisor / 2;
            double v =
                // First octave
                (perlinNoise.Noise(2 * pos.X * widthDivisor, 2 * pos.Y * heightDivisor, -2 * pos.Z * depthDivisor) + 1) / 2 * 0.7 +
                // Second octave
                (perlinNoise.Noise(-8 * pos.X * widthDivisor, 4 * pos.Y * heightDivisor, 8 * pos.Z * depthDivisor) + 1) / 2 * 0.2 +
                // Third octave
                (perlinNoise.Noise(16 * pos.X * widthDivisor, 8 * pos.Y * heightDivisor, -16 * pos.Z * depthDivisor) + 1) / 2 * 0.1;

            // generate a sphere
            //float rad = 30;
            //float dist = rad - (pos - new Vector3(0, rad, 0)).Length();
            //dist = dist.Scale(0, rad, 0, 0.5f);
            //v = dist + v;

            // clamp to 0 - 1
            //v = Math.Min(1, Math.Max(0, v));
            v = MathHelper.Clamp((float)v, 0, 1);
            byte density = (byte)(v * 255);
            

            return density;
        }

        public static float Scale(this float value, float min, float max, float minScale, float maxScale)
        {
            float scaled = minScale + (float)(value - min) / (max - min) * (maxScale - minScale);
            return scaled;
        }

        public static VolumeDensity8 VolumeDensity8FromVector3(Vector3 dimensions)
        {
            return new VolumeDensity8((ushort)dimensions.X, (ushort)dimensions.Y, (ushort)dimensions.Z, (ushort)Math.Max(Math.Max(dimensions.X, dimensions.Y), dimensions.Z));
        }

        public static VolumeDensity8 VolumeDensity8Cubic(int cubicSize)
        {
            return new VolumeDensity8((ushort)cubicSize, (ushort)cubicSize, (ushort)cubicSize, (ushort)cubicSize);
        }

        public static Region getEntireVolume(this VolumeDensity8 volume)
        {
            return new Region(new Vector3DInt16(0, 0, 0),
                new Vector3DInt16((short)volume.getWidth(), (short)volume.getHeight(), (short)volume.getDepth()));
        }

        public static Region getEntireVolumePaddedBorder(this VolumeDensity8 volume)
        {
            const short padding = 1;
            return new Region(new Vector3DInt16(-padding, -padding, -padding),
                new Vector3DInt16((short)(volume.getWidth() + padding), (short)(volume.getHeight() + padding), (short)(volume.getDepth() + padding)));
        }

        public static TerrainCellMesh GetMesh(this VolumeDensity8 volume, Vector3 pos, bool cubic, GraphicsDevice graphicsDevice)
        {
            TerrainCellMesh mesh = new TerrainCellMesh(volume, pos);
            mesh.Calculate(cubic);
            return mesh;
        }

        public static Vector3 ToVector3(this Vector3DFloat v)
        {
            return new Vector3(v.getX(), v.getY(), v.getZ());
        }

        public static Vector3 ToVector3(this JVector v)
        {
            return new Vector3(v.X, v.Y, v.Z);
        }

        public static JVector ToJVector(this Vector3DFloat v)
        {
            return new JVector(v.getX(), v.getY(), v.getZ());
        }

        public static JVector ToJVector(this Vector3 v)
        {
            return new JVector(v.X, v.Y, v.Z);
        }

        public static IEnumerable<JVector> ToJVector(this Vector3[] v)
        {
            return v.Select<Vector3, JVector>(q => q.ToJVector());
        }

        public static Matrix ToXNAMatrix(this JMatrix matrix)
        {
            return new Matrix(matrix.M11,
                            matrix.M12,
                            matrix.M13,
                            0.0f,
                            matrix.M21,
                            matrix.M22,
                            matrix.M23,
                            0.0f,
                            matrix.M31,
                            matrix.M32,
                            matrix.M33,
                            0.0f, 0.0f, 0.0f, 0.0f, 1.0f);
        }

        public static JMatrix ToJitterMatrix(this Matrix matrix)
        {
            JMatrix result;
            result.M11 = matrix.M11;
            result.M12 = matrix.M12;
            result.M13 = matrix.M13;
            result.M21 = matrix.M21;
            result.M22 = matrix.M22;
            result.M23 = matrix.M23;
            result.M31 = matrix.M31;
            result.M32 = matrix.M32;
            result.M33 = matrix.M33;
            return result;

        }

        public static JMeshData ExtractData(this ModelData model)
        {
            JMeshData jMeshData = new JMeshData();
            jMeshData.Vertices = new List<JVector>();
            jMeshData.Indices = new List<JOctree.TriangleVertexIndices>();

            foreach (MeshData m in model.Meshes)
            {
                foreach (var g in m.Geometry)
                {
                    Vector3[] verts = new Vector3[g.Vertices.Count];
                    g.Vertices.TryExtractVertexData<Vector3>(VertexElementUsage.Position, 0, verts);

                    int[] indices = new int[g.Indices.Count];
                    g.Indices.ExtractIndexData(indices);

                    jMeshData.Vertices.AddRange(verts.ToJVector());

                    for (int i = 0; i < g.Indices.Count / 3; i++)
                    {
                        jMeshData.Indices.Add(new JOctree.TriangleVertexIndices((int)indices[i * 3], (int)indices[i * 3 + 1], (int)indices[i * 3 + 2]));
                    }
                }
            }

            return jMeshData;
        }

        public static VertexPositionNormalTexture[] ToVertexPositionNormalTextureArray(this PositionMaterialNormalVector verts)
        {
            return verts.Select<PositionMaterialNormal, VertexPositionNormalTexture>(v =>
            {
                return new VertexPositionNormalTexture(v.position.ToVector3(), v.getNormal().ToVector3(), new Vector2(0, 0));
            }).ToArray();
        }

        public static Vertices<VertexPositionNormalTexture> ToVertices(this PositionMaterialNormalVector verts)
        {
            VertexPositionNormalTexture[] vertsConverted = verts.ToVertexPositionNormalTextureArray();
            return new Vertices<VertexPositionNormalTexture>(vertsConverted);
        }
    }
}
