using System.Diagnostics;
using System.Numerics;
using Game.Graphics;
using Game.Graphics.Renderers;
using Game.Main;
using Game.Phys;
using Silk.NET.Input;

namespace Game.Main
{
    public class GameController
    {
        public readonly GameCore Core;
        public Camera MainCamera;
        public PhysicalWorld PhysicalWorld;

        public GameController(GameCore core)
        {
            Core = core;
            MainCamera = new Camera(new WorldObject(Vector3.Zero, this), core.OpenGL, 10);
            PhysicalWorld = new PhysicalWorld(new WorldObject(Vector3.Zero, this), Vector2.Zero);
           
        }

        public void Step()
        {
            PhysicalWorld.LinkedObject.Step();
        }
    }

}
