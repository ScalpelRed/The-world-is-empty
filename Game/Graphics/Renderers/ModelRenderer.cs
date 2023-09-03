using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Game.Main;
using Game.Graphics.Uniforms;

namespace Game.Graphics.Renderers
{
    public class ModelRenderer : Renderer
    {
        public Model Model;

        public ModelRenderer(WorldObject linkedObject, Shader shader) : this(linkedObject, new Model(linkedObject.Game.Core.OpenGL, shader))
        {

        }

        public ModelRenderer(WorldObject linkedObject, Model model) : base(linkedObject)
        {
            Model = model;
        }

        public override void Draw()
        {
            Model.PrepareToDraw();

            Model.Shader.UniformMat4("transform", Transform.GlobalMatrix);
            Model.Shader.UniformMat4("camera", Game.MainCamera.GetMatrix());

            foreach (Uniform uniform in Uniforms.Values) uniform.Apply(Model);

            Model.Draw();
        }

        public override void Step()
        {
            Draw();
        }
    }
}
