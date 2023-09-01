using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Game.Graphics.Uniforms
{
    public class Vec2Uniform : Uniform
    {
        public Vector2 Value;

        public Vec2Uniform(string name) : base(name)
        {
        }

        public override void Apply(Model model)
        {
            model.Shader.UniformVec2(Name, Value);
        }

        public override bool SetValue(object value)
        {
            if (value is Vector2 vl)
            {
                Value = vl;
                return true;
            }
            return false;

        }
    }
}
