using System.Diagnostics;
using System.Numerics;
using Silk.NET.OpenGL;

namespace Game.Graphics
{
    public class Model
    {
        private static uint currentBuffer;
        private static uint currentTexture;
        private static uint currentShader;

        public readonly OpenGL Gl;

        public GlMesh Mesh;
        public Texture Texture;
        public Shader Shader;
        public PrimitiveType RenderMode;


        public static bool IsCurrentShader(Shader shader)
        {
            return currentShader == shader.Handle;
        }


        public Model(OpenGL gl, GlMesh mesh, Texture texture, Shader shader)
        {
            Gl = gl;

            Mesh = mesh;
            Shader = shader;
            Texture = texture;
            ResetRenderMode();
        }

        public Model(OpenGL gl, GlMesh mesh, Shader shader) : this(gl, mesh, gl.Core.Assets.GetTexture(""), shader)
        {
            
        }

        public Model(OpenGL gl, Shader shader) : this(gl, gl.Core.Assets.GetGlMesh("sprite"), shader)
        {

        }

        public Model(OpenGL gl, Texture texture, Shader shader) : this(gl, gl.Core.Assets.GetGlMesh("sprite"), texture, shader)
        {

        }


        public unsafe void ResetRenderMode()
        {
            RenderMode = Mesh.VertsPerPolygon switch
            {
                1 => PrimitiveType.Points,
                2 => PrimitiveType.Lines,
                3 => PrimitiveType.Triangles,
                4 => PrimitiveType.Quads, // does not work :(
                _ => PrimitiveType.Triangles,
            };
        }

        public unsafe void PrepareToDraw()
        {
            if (Mesh.VAOHandle != currentBuffer)
            {
                Gl.Api.BindVertexArray(Mesh.VAOHandle);
                currentBuffer = Mesh.VAOHandle;
            }

            if (Texture.Handle != currentTexture)
            {
                Gl.Api.BindTexture(TextureTarget.Texture2D, Texture.Handle);
                currentTexture = Texture.Handle;
            }

            if (Shader.Handle != currentShader)
            {
                Gl.Api.UseProgram(Shader.Handle);
                currentShader = Shader.Handle;
            }

            /*Gl.Api.BindVertexArray(VAOHandle);
            Gl.Api.BindTexture(TextureTarget.Texture2D, Texture.Handle);
            Gl.Api.UseProgram(Shader.Handle);*/
        }

        public unsafe void Draw()
        {
            if (Shader.Handle == 0) return;

            Gl.Api.DrawElements(RenderMode, Mesh.VertexCount, DrawElementsType.UnsignedInt, (void*)null);

            //Gl.Core.Log(Gl.Api.GetError());
        }
    }
}
