using Silk.NET.OpenGL;
using Silk.NET.Windowing;
using System.Numerics;
using Game.Main;
using Game.OtherAssets;

namespace Game.Graphics
{
    public class OpenGL
    {
        public IView View;
        public GL Api;
        public GameCore Core;

        public Vector2 ScreenSize;
        public Vector2 PixelSize;
        public float AspectRatio;

        public float TargetAspectRatio;

        private int frameCounter;
        private float deltaTime;

        public int FrameCounter { get => frameCounter; set => frameCounter = value; }
        public float DeltaTime { get => deltaTime; set => deltaTime = value; }

        public OpenGL(IView view, GameCore core)
        {
            Core = core;

            View = view;
            Api = GL.GetApi(view);

            string openglInfo = "OpenGL info:";
            openglInfo += $"\n   Version: {Api.GetStringS(StringName.Version)}";
            openglInfo += $"\n   GLSL version: {Api.GetStringS(StringName.ShadingLanguageVersion)}";
            openglInfo += $"\n   Renderer: {Api.GetStringS(StringName.Renderer)}";
            openglInfo += $"\n   Vendor: {Api.GetStringS(StringName.Vendor)}";
            Api.GetInteger(GetPName.MaxTextureSize, out int maxTexSize);
            openglInfo += $"\n   Max texture size: {maxTexSize}";
            Api.GetInteger(GetPName.Max3DTextureSize, out int max3DTexSize);
            openglInfo += $"\n   Max 3D texture size: {max3DTexSize}";
            openglInfo += $"\n   Extensions: {Api.GetStringS(StringName.Extensions)?.Replace(' ', '\n')}";
            Core.Log(openglInfo);

            ScreenSize = new Vector2(View.Size.X, View.Size.Y);

            TargetAspectRatio = 480f / 800;

            PixelSize = 2 * Vector2.One / ScreenSize;
            AspectRatio = ScreenSize.Y / ScreenSize.X;

            Api.ClearColor(0.3f, 0.3f, 0.3f, 1);
            Api.Clear(ClearBufferMask.ColorBufferBit);
            Api.Clear(ClearBufferMask.DepthBufferBit);

            Api.Enable(EnableCap.Blend);
            Api.BlendFunc(BlendingFactor.SrcAlpha,
                BlendingFactor.OneMinusSrcAlpha);

            /*Api.Enable(EnableCap.DepthTest);
            Api.DepthFunc(DepthFunction.Lequal);*/

            Api.Enable(EnableCap.Multisample);
            

            View.Render += (double t) => RenderFrame(t);
            View.Resize += (Silk.NET.Maths.Vector2D<int> size) =>
            {
                ScreenSize = new Vector2(size.X, size.Y);
                PixelSize = 2 * Vector2.One / ScreenSize;
                AspectRatio = ScreenSize.Y / ScreenSize.X;
                Api.Viewport(0, 0, (uint)size.X, (uint)size.Y);
                Resized?.Invoke();
            };
        }

        public void RenderFrame(double delta)
        {
            deltaTime = (float)delta;

            Api.Clear(ClearBufferMask.ColorBufferBit);
            //Api.Clear(ClearBufferMask.DepthBufferBit);

            Core.Game.Step();

            View.SwapBuffers();

            FrameCounter++;
        }

        public event Action? Resized;

        public enum UniformType
        {
            Int,
            Float,
            Double,
            Vec2,
            Vec3,
            Vec4,
            Mat4
        }

        

        static void Main()
        {
            // unused
        }
    }
}                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                       