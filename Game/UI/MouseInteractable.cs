using Game.Main;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Silk.NET.Input;

namespace Game.UI
{
    public class MouseInteractable : ObjectModule
    {
        protected Input Input;

        protected Vector2 BoundsOffset;

        protected bool Hover = false;

        public event Action<Vector2>? MouseIn;
        public event Action<Vector2>? MouseOut;
        //public event Action<MouseButton, Vector2>? MouseDown;
        //public event Action<MouseButton, Vector2>? MouseUp;

        public MouseInteractable(WorldObject linkedObject) : base(linkedObject)
        {
            Input = linkedObject.Game.Core.Input;

            Input.MouseMove += (Vector2 pos) =>
            {
                pos = Game.MainCamera.ScreenToWorld(pos);

                pos -= BoundsOffset + Transform.GlobalPosition2;

                if (pos.X >= 0 && pos.Y >= 0 && pos.X <= Transform.LocalScale.X && pos.Y <= Transform.LocalScale.Y)
                {
                    if (!Hover)
                    {
                        Hover = true;
                        MouseIn?.Invoke(pos);
                        Console.WriteLine(pos);
                    }
                }
                else
                {
                    if (Hover)
                    {
                        Hover = false;
                        MouseOut?.Invoke(pos);
                        Console.WriteLine(pos);
                    }
                }
            };
        }

        public override void Step()
        {
            
        }
    }
}
