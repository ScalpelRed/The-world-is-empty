using Game.Main;
using System.Numerics;

namespace Game.Graphics.Renderers
{
    public class SpriteRenderer : ModelRenderer
    {

        public SpriteRenderer(WorldObject linkedObject, Texture texture) 
            : base(linkedObject, new Model(linkedObject.Game.Core.OpenGL, texture, linkedObject.Game.Core.Assets.GetShader("spriteTextured")))
        {
            
        }

        public override void Draw()
        {
            Model.PrepareToDraw();

            Model.Shader.UniformMat4("transform", Transform.LocalMatrix);
            Model.Shader.UniformMat4("camera", Game.MainCamera.GetMatrix());
            Model.Shader.UniformVec2("textureSize", Model.Texture.Size);
            Model.Shader.UniformVec2("geometryScale", GeometryScale);

            Model.Draw();
        }

        public Vector2 GeometryScale = Vector2.One;
    }
}
