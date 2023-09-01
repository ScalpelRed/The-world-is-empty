using Game.Main;
using System.Numerics;
using Genbox.VelcroPhysics.Dynamics;

namespace Game.Phys
{
    public class PhysicalWorld : ObjectModule
    {
        public Vector2 ConstantForce
        {
            get => PhysWorld.Gravity.ToNumerics();
            set => PhysWorld.Gravity = value.ToXna();
        }

        public readonly World PhysWorld;

        public readonly List<RigidBody> RigidBodies = new();

        public PhysicalWorld(WorldObject linkedObject, Vector2 constantForce) : base(linkedObject)
        {
            PhysWorld = new(constantForce.ToXna());
        }

        internal void AddRigidBody(RigidBody rb)
        {
            RigidBodies.Add(rb);
        }

        public override void Step()
        {
            PhysWorld.Step(Game.Core.OpenGL.DeltaTime);
        }
    }
}
