using Game.OtherAssets;
using Silk.NET.OpenGL;

namespace Game.Graphics
{
    public class GlMesh
    {
        public readonly uint VBOHandle;
        public readonly uint VAOHandle;
        public readonly uint EBOHandle;

        public readonly uint VertexCount;
        public readonly int VertsPerPolygon;

        public unsafe GlMesh(OpenGL gl, Mesh source)
        {
            float[] vertexArray = source.GetVertexDataArray();
            int[] indexArray = source.GetIndexArray();

            VAOHandle = gl.Api.GenVertexArray();
            VBOHandle = gl.Api.GenBuffer();
            EBOHandle = gl.Api.GenBuffer();

            gl.Api.BindVertexArray(VAOHandle);

            fixed (void* d = &vertexArray[0])
            {
                gl.Api.BindBuffer(BufferTargetARB.ArrayBuffer,
                    VBOHandle);
                gl.Api.BufferData(BufferTargetARB.ArrayBuffer,
                (nuint)(vertexArray.Length * sizeof(float)), d,
                BufferUsageARB.StaticDraw);
            }

            gl.Api.VertexAttribPointer(0, 3,
                VertexAttribPointerType.Float,
                false, 8 * sizeof(float), (void*)0);
            gl.Api.VertexAttribPointer(1, 2,
                VertexAttribPointerType.Float,
                false, 8 * sizeof(float), (void*)(3 * sizeof(float)));
            gl.Api.VertexAttribPointer(2, 3,
                VertexAttribPointerType.Float,
                false, 8 * sizeof(float), (void*)(5 * sizeof(float)));

            gl.Api.EnableVertexAttribArray(0);
            gl.Api.EnableVertexAttribArray(1);
            gl.Api.EnableVertexAttribArray(2);

            fixed (void* d = &indexArray[0])
            {
                gl.Api.BindBuffer(BufferTargetARB.ElementArrayBuffer,
                    EBOHandle);
                gl.Api.BufferData(BufferTargetARB.ElementArrayBuffer,
                (nuint)(indexArray.Length * sizeof(int)), d,
                BufferUsageARB.StaticDraw);
            }

            gl.Api.BindBuffer(BufferTargetARB.ArrayBuffer, 0);
            gl.Api.BindVertexArray(0);

            VertexCount = (uint)indexArray.Length;
            VertsPerPolygon = source.VertsPerPolygon;
        }
    }
}
