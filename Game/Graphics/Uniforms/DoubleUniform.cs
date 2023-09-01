using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Graphics.Uniforms
{
    public class DoubleUniform : Uniform
    {
        public double Value;

        public DoubleUniform(string name) : base(name)
        {
        }

        public override void Apply(Model model)
        {
            model.Shader.UniformDouble(Name, Value);
        }

        public override bool SetValue(object value)
        {
            if (value is double vl)
            {
                Value = vl;
                return true;
            }
            return false;

        }
    }
}
