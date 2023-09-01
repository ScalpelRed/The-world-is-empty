using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using Game.Main;

namespace Game.Graphics
{
    public class Camera : ObjectModule
    {
        private float near;
        private float far;
        public readonly OpenGL Gl;

        public Vector3 LocalPosition
        {
            get => Transform.LocalPosition;
            set => Transform.LocalPosition = value;
        }

        public Vector3 GlobalPosition
        {
            get => Transform.GlobalPosition;
        }

        public float Near 
        { 
            get => near;
            set
            {
                near = value;
                RecalculateMatrix();
            }
        }

        public float Far 
        { 
            get => far;
            set
            {
                far = value;
                RecalculateMatrix();
            }
        }

        public Camera(WorldObject linkedObject, OpenGL gl, float far, float near = 0.05f) : base(linkedObject)
        {
            Gl = gl;
            Far = far;
            Near = near;
            RecalculateMatrix();

            Gl.Resized += () => RecalculateMatrix();
            Transform.TransformChanged += () => RecalculateMatrix();
        }

        private Matrix4x4 viewMatrix;

        private void RecalculateMatrix()
        {
            Matrix4x4.Invert(Transform.GlobalMatrix, out Matrix4x4 invMatrix);
            viewMatrix = invMatrix * Matrix4x4.CreateOrthographic(Gl.ScreenSize.X, -Gl.ScreenSize.Y, Near, Far);
        }

        public Matrix4x4 GetMatrix()
        {
            return viewMatrix;
        }

        public Vector2 ScreenToWorld(Vector2 screenPos)
        {
            return Vector2.Transform(screenPos, Transform.GlobalMatrix);
        }

        public override void Step()
        {
            
        }
    }
}
