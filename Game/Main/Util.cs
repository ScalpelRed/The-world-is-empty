using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;

namespace Game.Main
{
    public static class Util
    {
        public static Vector2 XY(this Vector3 vec) => new(vec.X, vec.Y);

        public static Vector2 ToNumerics(this Microsoft.Xna.Framework.Vector2 vec) => new(vec.X, vec.Y);

        public static Microsoft.Xna.Framework.Vector2 ToXna(this Vector2 vec) => new(vec.X, vec.Y);
    }
}
