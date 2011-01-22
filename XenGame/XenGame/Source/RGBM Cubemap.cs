using System;
using System.Collections.Generic;

using Xen;
using Xen.Graphics;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.IO;


namespace GameClient
{
    /*/
     * 
     * The following class loads a cubemap from an RGBM encoded 2D image.
     * The source images used by this example can be found in 'Content/LightProbes'.
     * 
     * These light probes are based off source HDR probes available on the website:
     * http://www.hdrlabs.com/sibl/archive.html
     * 
     * They are distributed under the Creative Commons Attribution-Noncommercial-Share 
     * Alike 3.0 License. They cannot be used for commercial use.
     * http://creativecommons.org/licenses/by-nc-sa/3.0/us/
     * 
     * 
     * They have been converted into CubeMaps. Each face of the cubemap is stored next to 
     * each other, left to right. So the final image is 1200x200 (ie, 6 200x200 cube faces).
     * 
     * The source images were originally stored in a highly accurate floating point format.
     * 
     *
     * ------------------------------------
     * 
     * So, why would you want to store a texture in floating point?
     * 
     * For a lot of cases, you don't. However, in a number of situations it is desriable to
     * be able to represent numbers outside the normal [0,1] range of 8bit RGBA textures.
     * 
     * LightProbes / Skyboxes are a good example, as they typically store a very high dynamic range.
     * 
     * However, an albedo texture almost always only needs [0,1] range.
     * (An albedo texture is a name for a standard colour texture used on a model).
     * 
     * The reason for this requirement is very simple; albedo represents how much a 
     * surface reflects diffuse light. Ie, a black surface (RGB = 0.0), reflects no light.
     * A perfect pure white surface (RGB = 1.0) reflects ALL light.
     * It can be easily seen that a value above 1.0 is physically not possible for albedo.
     * 
     * Aside:	It's worth noting: In the real world, albedo is generally never above ~0.7
     *			in Linear Colour space.
     * 
     * 
     * Therefore, HDR textures are only really useful for non-albedo textures.
     * Perfect candidates for HDR textures are skyboxes / light probes / environment maps.
     * 
     * ------------------------------------
     * 
     * So, why not store the images in full floating point?...
     * 
     * Few games will ever truely need FP32 for their HDR game assets. Even FP16 (16 bit 
     * floating point), is usually overkill for most scenarios.
     * Also, not all hardware supports texture filtering for FP16 textures.
     * 
     * The xbox 360, for example, only partially supports FP16 filtering by approximating 
     * the result using int16 filtering. It does not support FP32 filtering.
     * 
     * Furthermore, FP16 and FP32 textures are simply too large. FP16 is twice the size of
     * standard 8bit RGBA. FP32 is twice that again. They are often 2x/4x as expensive to
     * sample as well.
     * 
     * The dynamic range requirement can be reduced by storing the image in Gamma Space,
     * however this is usually not enough to get good results from an 8bit RGBA image.
     * 
     * This is where RGBM comes in. RGBM is a nice alternative, providing a usable dynamic
     * range while being the same size as 8bit RGBA (and supporting filtering).
     * 
     * 
     * ------------------------------------
     * 
     * How it works;
     * It's very simple. Normal RGBA textures store 8bits of red, blue, green and alpha.
     * RGBM simpy swaps alpha for a general 'multiplier'.
     * 
     * Normally, your textures are [0,1] range with 8 bits of precision.
     * For HDR light probes, we need more range and a bit more precision.
     * 
     * In this tutorial, we need a range of [0,50].
     * [0,50] is more than enough range for the light probes used (when stored in gamma space).
     * 
     * So why use RGBM?
     * 
     * We could just multiply the image by 50.0 in the shader, however, because the texture
     * is stored in 8bit precision it can only store 256 shades for each R/G/B.
     * This would mean the smallest non-zero value it could store would be 0.195.
     * Around 10% of the light probe image colour values are between 0 and 0.195!
     * 
     * Multiplying by 50.0 solves the range problem. But you lose too much precision.
     * This is where RGBM comes in.
     * 
     * RGBM's added multiplier allows higher precision to be maintained for smaller values.
     * So you get similar colour precision, but more range.
     * 
     * ------------------------------------
     * 
     * How does it get encoded?
     * For the given RGB input, the RGB floating point values are scaled so they fit 
     * in [0,1] for the desired output range.
     * 
     * Remember, we want the output texture to have a range of [0,50]. To get them to fit
     * in [0,1], we divide by 50.0.
     * 
     * However this doesn't help precision. This is where the M in RGBM comes in:
     * The floating point Maximum of R,G and B is found.
     * 
     * R, G and B are then all divided by the maximum value. This means the largest value
     * becomes 1.0.
     * 
     * 
     * For example:
     * Given the following inputs, you get the following outputs.
     * 
     * 
     * ------------------------------------
     * - Example encode:
     * ------------------------------------
     * 
     * Large number:
     * 
     * MaxRange		= 50
     * 
     * RGB input	= { 40, 20, 1 }
     * max(RGB)		= 40
     * M			= 40 / MaxRange
     *				= 0.8
     *				= 205 rounded up in 8bit [0,255]
     * 
     * Red channel	= R / (205 / 255 * MaxRange)
     *				= 40 / 40.196
     *				= 0.995
     *				= 254 (rounded nearest 8bit)
     * 
     * RGBM (8bit)	= { 254, 127, 6, 205 }
     * 
     * Decoded		= RGB * M * MaxRange
     *				= 40.04, 20.02, 0.946
     * 
     * ------------------------------------
     * 
     * Smaller numbers:
     * 
     * RGB			= { 0.4, 0.3, 0.6 }
     * M			= 4
     * RGBM (8bit)	= { 130, 98, 195, 4 }
     * 
     * Decoded		= 0.3998, 0.3014, 0.5998
     * 
     * ------------------------------------
     * 
     * As can be seen, precision is approximately 8bit compared to the maximum value.
     * 
     * ------------------------------------
     * 
     * Pixel shader decoding:
     * 
     * float4 rgbm = tex2D(Sampler,texCoord);
     * float3 rgb = rgbm.rgb * rgbm.a * MaxRange;
     * 
     * 
     * That's it. Simple!
     * 
     * In summary, RGB stores the normalised colour, M (Alpha) stores the intensity.
     * 
    /*/

    /// <summary>
    /// Useful tools to encode and decode RGBM.
    /// These may not be suitable for realtime use, as they aren't terribly efficient.
    /// Note, a shader version is also provided in RGBM.fx.h. Which is obviously a *lot* faster.
    /// </summary>
    public static class RgbmTools
    {
        public static Color EncodeRGBM(float R, float G, float B, float MaxRange)
        {
            R = Math.Max(0, R);						// catch negative numbers..
            G = Math.Max(0, G);
            B = Math.Max(0, B);

            float maxRGB = Math.Max(R, Math.Max(G, B));
            float M = Math.Min(maxRGB / MaxRange, 1.0f);	// catch max above MaxRange

            M = (float)Math.Ceiling(M * 255.0f);		// make sure to round up
            byte m = (byte)M; //round up to 8bit

            M = (M / 255.0f) * (MaxRange / 255.0f);
            R = R / M;
            G = G / M;
            B = B / M;

            R = Math.Min(255.25f, R);					// clamp numbers > MaxRange
            G = Math.Min(255.25f, G);
            B = Math.Min(255.25f, B);

            byte r = (byte)(R + 0.5f);						// cast will truncate, +0.5 makes it round to nearest
            byte g = (byte)(G + 0.5f);
            byte b = (byte)(B + 0.5f);

            return new Color(r, g, b, m);
        }

        public static Vector3 DecodeRGBM(Color rgbm, float MaxRange)
        {
            float r = (float)rgbm.R;
            float g = (float)rgbm.G;
            float b = (float)rgbm.B;
            float m = (float)rgbm.A;

            m *= MaxRange / 65025.0f;

            r *= m;
            g *= m;
            b *= m;

            return new Vector3(r, g, b);
        }
    }


    /// <summary>
    /// <para>This class reads in a 2D texture in RGBM format, and extracts the cube faces.</para>
    /// <para>It also generates a Spherical Harmonic from the input, to approximate indirect lighting</para>
    /// </summary>
    public sealed class RgbmCubeMap : IDisposable
    {
        //Check out the remaks for the Spherical Harmonics class for a detailed description.
        public readonly Xen.Ex.SphericalHarmonicL2RGB SphericalHarmonic;

        private readonly TextureCube cubeMap;
        public readonly float MaxRange;

        /// <summary>
        /// Returns the RGBM cubemap, stored in Gamma Colour Space
        /// </summary>
        public TextureCube CubeMap { get { if (cubeMap.IsDisposed) return null; return cubeMap; } }

        /// <summary>
        /// <para>Reads in a 2D texture in RGBM format, and extracts the cube faces.</para>
        /// <para>It also generates a Spherical Harmonic from the input, to approximate indirect lighting</para>
        /// </summary>
        public RgbmCubeMap(Texture2D sourceRgbmImage2D, Texture2D sourceRgbmAlphaImage2D, float maxRange)
        {
            //the two textures represent the RGB and M values of an RGBM texture. They have
            //been separated to make it easier to view their colour channels. 
            //This is somewhat wasteful however. Normally it would be wise to combine them into one texture.

            if (sourceRgbmImage2D == null || sourceRgbmAlphaImage2D == null)
                throw new ArgumentNullException();
            if (sourceRgbmImage2D.Width != sourceRgbmImage2D.Height * 6 ||
                sourceRgbmAlphaImage2D.Width != sourceRgbmImage2D.Width ||
                sourceRgbmAlphaImage2D.Height != sourceRgbmImage2D.Height)
                throw new ArgumentException("Invalid size");	//expected to have 6 cube faces
            if (sourceRgbmImage2D.Format != SurfaceFormat.Color ||
                sourceRgbmAlphaImage2D.Format != SurfaceFormat.Color)
                throw new ArgumentException("Unexpected image format");

            if (maxRange <= 0.0f)
                throw new ArgumentException("MaxRange");

            this.MaxRange = maxRange;

            //extract the RGB pixels
            Color[] pixelDataRGB = new Color[sourceRgbmImage2D.Width * sourceRgbmImage2D.Height];
            sourceRgbmImage2D.GetData(pixelDataRGB);

            //extract the M pixels
            Color[] pixelDataM = new Color[sourceRgbmAlphaImage2D.Width * sourceRgbmAlphaImage2D.Height];
            sourceRgbmAlphaImage2D.GetData(pixelDataM);

            //at this point, the source textures are no longer needed


            //extract the faces from the cubemap,
            //and generate a spherical harmonic based off it's colours

            //width/height of each face of the cubemap
            int cubeFaceSize = sourceRgbmImage2D.Height;

            this.SphericalHarmonic = new Xen.Ex.SphericalHarmonicL2RGB();
            this.cubeMap = new TextureCube(sourceRgbmImage2D.GraphicsDevice, cubeFaceSize, false, SurfaceFormat.Color);

            int textureLineStride = sourceRgbmImage2D.Width;

            //storage for the decoded cubemap faces
            Vector3[][] cubeFaces = new Vector3[6][];

            //temporary storage of a single face of the cube
            Color[] singleFaceRGBM = new Color[cubeFaceSize * cubeFaceSize];

            //extract the 6 faces of the cubemap.
            for (int face = 0; face < 6; face++)
            {
                CubeMapFace faceId = (CubeMapFace)face;

                //cube face data
                Vector3[] singleFaceRGB = new Vector3[cubeFaceSize * cubeFaceSize];
                cubeFaces[face] = singleFaceRGB;

                //each face is stored next to each other in the 2D texture
                int startPixel = cubeFaceSize * face;

                //copy the face from the 2D texture data into singleFace

                int writeIndex = 0;
                int readIndex = startPixel;

                for (int y = 0; y < cubeFaceSize; y++) // each hoizontal line
                {
                    for (int x = 0; x < cubeFaceSize; x++) // each pixel in the line
                    {
                        Color encodedRgb = pixelDataRGB[readIndex + x];
                        Color encodedM = pixelDataM[readIndex + x];

                        //combine to get the RGBM value
                        Color rgbm = new Color(encodedRgb.R, encodedRgb.G, encodedRgb.B, encodedM.R);

                        //decode the pixel
                        Vector3 rgb = RgbmTools.DecodeRGBM(rgbm, maxRange);

                        //convert to linear
                        rgb = rgb * rgb;

                        //store
                        singleFaceRGB[writeIndex + x] = rgb;
                        singleFaceRGBM[writeIndex + x] = rgbm;
                    }

                    //jump to the next line
                    readIndex += textureLineStride;
                    writeIndex += cubeFaceSize;
                }

                //write the pixels into the cubemap
                cubeMap.SetData(faceId, singleFaceRGBM);
            }

            //Generate the SH from the cubemap faces
            this.SphericalHarmonic = Xen.Ex.SphericalHarmonicL2RGB.GenerateSphericalHarmonicFromCubeMap(cubeFaces);
        }

        /// <summary>
        /// Dispose the cube map
        /// </summary>
        public void Dispose()
        {
            cubeMap.Dispose();
        }
    }


    //this really couldn't be much simpler :-)
    //draw the background cubemap to the screen.
    public sealed class BackgroundDrawer : IDraw
    {
        //create an inverted sphere
        private IDraw geometry = new Xen.Ex.Geometry.Sphere(new Vector3(-1, -1, -1), 8, true, false, false);

        public void Draw(DrawState state)
        {
            //bind the background filling shader

            using (state.Shader.Push(state.GetShader<Shaders.BackgroundFill>()))
            {
                geometry.Draw(state);
            }
        }

        bool ICullable.CullTest(ICuller culler)
        {
            return true;
        }
    }
}
