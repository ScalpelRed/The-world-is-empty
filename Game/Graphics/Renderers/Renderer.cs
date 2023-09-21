using Game.Graphics.Uniforms;
using Game.Main;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Game.Graphics.OpenGL;

namespace Game.Graphics.Renderers
{
    public abstract class Renderer : ObjectModule
    {

        protected Renderer(WorldObject linkedObject) : base(linkedObject)
        {
            
        }

        public override void Step()
        {

        }

        public abstract void Draw();

        protected SortedDictionary<string, Uniform> Uniforms = new();

        public void AddValue(string name, UniformType type)
        {
            switch (type)
            {
                case UniformType.Int:
                    Uniforms.Add(name, new IntUniform(name));
                    break;
                case UniformType.Float:
                    Uniforms.Add(name, new FloatUniform(name));
                    break;
                case UniformType.Double:
                    Uniforms.Add(name, new DoubleUniform(name));
                    break;
                case UniformType.Vec2:
                    Uniforms.Add(name, new Vec2Uniform(name));
                    break;
                case UniformType.Vec3:
                    Uniforms.Add(name, new Vec3Uniform(name));
                    break;
                case UniformType.Vec4:
                    Uniforms.Add(name, new Vec4Uniform(name));
                    break;
                case UniformType.Mat4:
                    Uniforms.Add(name, new Mat4Uniform(name));
                    break;
            }
        }

        public bool SetValue<T>(string name, T value) where T : struct
        {
            if (Uniforms.TryGetValue(name, out Uniform uniform))
            {
                return uniform!.SetValue(value);
            }
            else throw new Exception($"This renderer has no value \"{name}\"");
        }

        public void AddValue<T>(string name, UniformType type, T value) where T : struct
        {
            AddValue(name, type);
            SetValue(name, value);
        }
    }
}
