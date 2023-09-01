using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Game.Graphics.Uniforms
{
    public abstract class Uniform
    {
        public readonly string Name;

        public Uniform(string name)
        {
            Name = name;
        }

        public abstract bool SetValue(object value);

        public abstract void Apply(Model model);
    }
}
