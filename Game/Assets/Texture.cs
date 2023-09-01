using System.Numerics;
using Silk.NET.OpenGL;
using Game.Main;
using Game.Graphics;

namespace Game.Assets
{
    public class Texture
    {
        public uint Handle = 0;
        public int SizeX = 0;
        public int SizeY = 0;
        public Vector2 Size
        {
            get
            {
                return new Vector2(SizeX, SizeY);
            }
        }

        public string Name;

        private readonly OpenGL? Gl;

        public Texture()
        {
            Name = "Empty";
            SizeX = SizeY = 0;
            Handle = 0;
        }

        public unsafe Texture(string name, OpenGL opengl)
        {
            Gl = opengl;

            (int, int, byte[]) tex;

            try
            {
                tex = opengl.Core.Platform.GetTextureData("textures/" + name + ".png");

            }
            catch
            {
                throw new AssetNotFoundException("texture", name);
            }

            Name = name;
            SizeX = tex.Item1;
            SizeY = tex.Item2;

            Handle = opengl.Api.GenTexture();
            opengl.Api.BindTexture(TextureTarget.Texture2D, Handle);
            fixed (void* d = &tex.Item3[0])
            {
                opengl.Api.TexImage2D(TextureTarget.Texture2D, 0,
                    InternalFormat.Rgba, (uint)SizeX, (uint)SizeY,
                    0, PixelFormat.Rgba, PixelType.UnsignedByte, d);
            }

            opengl.Api.GenerateMipmap(TextureTarget.Texture2D);

            opengl.Api.TextureParameter(Handle,
                TextureParameterName.TextureMinFilter,
                (int)TextureMinFilter.Linear);
            opengl.Api.TextureParameter(Handle,
                TextureParameterName.TextureMagFilter,
                (int)TextureMinFilter.Linear);

            opengl.Api.TextureParameter(Handle,
                TextureParameterName.TextureWrapS,
                (int)TextureWrapMode.Repeat);
            opengl.Api.TextureParameter(Handle,
                TextureParameterName.TextureWrapT,
                (int)TextureWrapMode.Repeat);
        }
    }
}
