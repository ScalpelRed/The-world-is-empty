using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Game.Graphics.Uniforms
{
    public class Vec3Uniform : Uniform
    {
        public Vector3 Value;

        public Vec3Uniform(string name) : base(name)
        {
        }

        public override void Apply(Model model)
        {
            model.Shader.UniformVec3(Name, Value);
        }

        public override bool SetValue(object value)
        {
            if (value is Vector3 vl)
            {
                Value = vl;
                return true;
            }
            return false;

        }
    }
}
