using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Game.Graphics.Uniforms
{
    public class Mat4Uniform : Uniform
    {
        public Matrix4x4 Value;

        public Mat4Uniform(string name) : base(name)
        {
        }

        public override void Apply(Model model)
        {
            model.Shader.UniformMat4(Name, Value);
        }

        public override bool SetValue(object value)
        {
            if (value is Matrix4x4 vl)
            {
                Value = vl;
                return true;
            }
            return false;

        }
    }
}
