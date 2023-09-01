using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Graphics.Uniforms
{
    public class FloatUniform : Uniform
    {
        public float Value;

        public FloatUniform(string name) : base(name)
        {
        }

        public override void Apply(Model model)
        {
            model.Shader.UniformFloat(Name, Value);
        }

        public override bool SetValue(object value)
        {
            if (value is float vl)
            {
                Value = vl;
                return true;
            }
            return false;

        }
    }
}
