using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;

namespace Game.Util
{
    public static class Util
    {
        public static Vector2 XY(this Vector3 vec) => new(vec.X, vec.Y);

        public static Vector2 ToNumerics(this Microsoft.Xna.Framework.Vector2 vec) => new(vec.X, vec.Y);

        public static Microsoft.Xna.Framework.Vector2 ToXna(this Vector2 vec) => new(vec.X, vec.Y);

        public static MemoryStream StreamReaderToMemoryStream(StreamReader streamReader)
        {
            MemoryStream res = new();
            byte[] buffer = new byte[1024];
            int bytesRead;
            while ((bytesRead = streamReader.BaseStream.Read(buffer, 0, buffer.Length)) > 0)
            {
                res.Write(buffer, 0, bytesRead);
            }

            return res;
        }

        public static void WriteMissingBytes(MemoryStream stream, int varBytes)
        {
            int byteCount = (int)(varBytes - stream.Length % varBytes);
            byte[] buffer = new byte[byteCount];
            stream.Write(buffer, 0, byteCount);
        }
    }
}
