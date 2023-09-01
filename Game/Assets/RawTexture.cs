using Game.Main;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Assets
{
    public class RawTexture
    {
        public readonly int SizeX;
        public readonly int SizeY;

        private byte[] data;

        public byte[] Data
        {
            get
            {
                byte[] res = new byte[data.Length];
                data.CopyTo(res, 0);
                return res;
            }
        }

        public RawTexture(string name, GameCore core)
        {
            (int sizeX, int sizeY, byte[] data) tex;

            try
            {
                tex = core.Platform.GetTextureData("textures/" + name + ".png");

            }
            catch
            {
                throw new AssetNotFoundException("texture", name);
            }

            SizeX = tex.sizeX;
            SizeY = tex.sizeY;
            data = tex.data;
        }

        public (byte R, byte G, byte B, byte A) GetPixel(int x, int y)
        {
            int pos0 = (y * SizeX + x) * 4;
            return (data[pos0], data[pos0 + 1], data[pos0 + 2], data[pos0 + 3]);
        }
    }
}
