using Game.Graphics;
using Game.Graphics.Renderers;
using Game.Main;
using Silk.NET.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Game.UI
{
    public sealed class Joystick : MouseInteractable
    {
        readonly WorldObject Stick;

        private bool Interacting;
        private float Radius;

        public Vector2 Value { get; private set; }

        public Joystick(WorldObject linkedObject, WorldObject stick) : base(linkedObject)
        {
            Stick = stick;
            Stick.Transform.Parent = Transform;

            Input.MouseDown += (MouseButton _, Vector2 pos) =>
            {
                if (Hover)
                {
                    Interacting = true;
                    Stick.Transform.LocalPosition2 = (Game.MainCamera.ScreenToWorld(pos) - Transform.GlobalPosition2) / Radius * 0.5f;
                    Value = Stick.Transform.LocalPosition2 * 2;
                }
            };

            Input.MouseUp += (MouseButton _, Vector2 pos) =>
            {
                Interacting = false;
                Stick.Transform.LocalPosition = Vector3.Zero;
                Value = Vector2.Zero;
            };

            Input.MouseMove += (Vector2 pos) =>
            {
                if (Interacting)
                {
                    Stick.Transform.LocalPosition2 = (Game.MainCamera.ScreenToWorld(pos) - Transform.GlobalPosition2) / Radius * 0.5f;

                    if (Stick.Transform.LocalPosition2.LengthSquared() > 0.25f)
                        Stick.Transform.LocalPosition2 = Vector2.Normalize(Stick.Transform.LocalPosition2) * 0.5f;

                    Value = Stick.Transform.LocalPosition2 * 2;
                }
            };


            ModelRenderer? rend = LinkedObject.GetModule<ModelRenderer>();
            if (rend is null)
                rend = new ModelRenderer(linkedObject, new Model(Game.Core.OpenGL, Game.Core.Assets.GetGlMesh("sprite"), Game.Core.Assets.GetShader("meshFlare")));

            rend.AddValue("innerRadius", OpenGL.UniformType.Float);
            rend.AddValue("slopeLength", OpenGL.UniformType.Float);
            rend.AddValue("thickness", OpenGL.UniformType.Float);
            rend.AddValue("inColor", OpenGL.UniformType.Vec4);

            rend.SetValue("innerRadius", 0.89f);
            rend.SetValue("slopeLength", 0.03f);
            rend.SetValue("thickness", 0.05f);
            rend.SetValue("inColor", Vector4.One * 0.5f);
        }

        public override void Step()
        {
            Stick.Step();
        }

        public void SetSize(float size)
        {
            Transform.LocalScale = new Vector3(size);
            BoundsOffset = -0.5f * new Vector2(size);
            Radius = size * 0.5f;
        }
    }
}
