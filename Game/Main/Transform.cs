using System.Numerics;

namespace Game.Main
{
    public sealed class Transform
    {

        private Vector3 localPosition;
        private Matrix4x4 localPosMatrix;
        public event Action<Vector3>? LocalPosChanged;

        public Vector3 LocalPosition
        {
            get => localPosition;
            set
            {
                localPosition = value;
                localPosMatrix = Matrix4x4.CreateTranslation(localPosition);
                LocalPosChanged?.Invoke(localPosition);
                RecalculateLocalMatrix();
            }
        }

        public Vector2 LocalPosition2
        {
            get => localPosition.XY();
            set
            {
                localPosition.X = value.X;
                localPosition.Y = value.Y;
                localPosMatrix = Matrix4x4.CreateTranslation(localPosition);
                LocalPosChanged?.Invoke(localPosition);
                RecalculateLocalMatrix();
            }
        }

        public Vector3 GlobalPosition
        {
            get => NoParent ? localPosition : localPosition + Parent!.GlobalPosition;
        }

        public Vector2 GlobalPosition2
        {
            get => NoParent ? LocalPosition2 : LocalPosition2 + Parent!.GlobalPosition2;
        }



        private Vector3 localRotation;
        private Matrix4x4 localRotMatrix;
        public event Action<Vector3>? LocalRotChanged;

        public Vector3 LocalRotation
        {
            get => localRotation;
            set
            {
                localRotation = value;
                localRotMatrix = Matrix4x4.CreateFromYawPitchRoll(value.X, value.Y, value.Z);
                LocalRotChanged?.Invoke(value);
                RecalculateLocalMatrix();
            }
        }



        private Vector3 localScale;
        private Matrix4x4 localScaleMatrix;
        public event Action<Vector3>? LocalScaleChanged;

        public Vector3 LocalScale
        {
            get => localScale;
            set
            {
                localScale = value;
                localScaleMatrix = Matrix4x4.CreateScale(value);
                LocalScaleChanged?.Invoke(value);
                RecalculateLocalMatrix();
            }
        }



        private Vector3 pivot;
        private Matrix4x4 pivotMatrix;
        public event Action<Vector3>? PivotChanged;

        public Vector3 Pivot
        {
            get => pivot;
            set
            {
                pivot = value;
                pivotMatrix = Matrix4x4.CreateTranslation(-value);
                PivotChanged?.Invoke(value);
                RecalculateLocalMatrix();
            }
        }



        private Transform? parent;
        private bool NoParent = false;
        public event Action<Transform?>? ParentChanged;

        public Transform? Parent
        {
            get => parent;
            set
            {
                parent = value;
                NoParent = value is null;
                ParentChanged?.Invoke(value);
            }
        }



        private Matrix4x4 localMatrix;
        public Matrix4x4 LocalMatrix
        {
            get => localMatrix;
        }
        public event Action? TransformChanged;
        private void RecalculateLocalMatrix()
        {
            localMatrix = pivotMatrix * localScaleMatrix * localRotMatrix * localPosMatrix;
            TransformChanged?.Invoke();
        }

        public Matrix4x4 GlobalMatrix
        {
            get => NoParent ? localMatrix : localMatrix * Parent!.GlobalMatrix;
        }

        public Transform(Vector3 pos, Transform? par = null)
        {
            LocalPosition = pos;
            LocalRotation = Vector3.Zero;
            LocalScale = Vector3.One;
            Pivot = Vector3.Zero;
            Parent = par;
        }
    }
}
