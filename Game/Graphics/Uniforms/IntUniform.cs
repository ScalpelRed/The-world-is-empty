using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Graphics.Uniforms
{
    public class IntUniform : Uniform
    {
        public int Value;

        public IntUniform(string name) : base(name)
        {
        }

        public override void Apply(Model model)
        {
            model.Shader.UniformInt(Name, Value);
        }

        public override bool SetValue(object value)
        {
            if (value is int vl)
            {
                Value = vl;
                return true;
            }
            return false;
        }
    }
}
