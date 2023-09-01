using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Game.Graphics.Uniforms
{
    public class Vec4Uniform : Uniform
    {
        public Vector4 Value;

        public Vec4Uniform(string name) : base(name)
        {
        }

        public override void Apply(Model model)
        {
            model.Shader.UniformVec4(Name, Value);
        }

        public override bool SetValue(object value)
        {
            if (value is Vector4 vl)
            {
                Value = vl;
                return true;
            }
            return false;

        }
    }
}
