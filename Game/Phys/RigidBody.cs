using Game.Main;
using System.Numerics;
using Genbox.VelcroPhysics.Dynamics;
using Genbox.VelcroPhysics.Factories;

namespace Game.Phys
{
    public class RigidBody : ObjectModule
    {
        public readonly PhysicalWorld PhysWorld;

        public readonly Body Body;

        public float Mass
        {
            get => Body.Mass;
            set => Body.Mass = value;
        }

        public float Inertia
        {
            get => Body.Inertia;
            set => Body.Inertia = value;
        }

        public BodyType BodyType
        {
            get => Body.BodyType;
            set => Body.BodyType = value;
        }

        private bool allowTransformUpdating = true;

        public RigidBody(WorldObject linkedObject, float mass, PhysicalWorld world, PhysicalMesh2D? mesh = null) : base(linkedObject)
        {
            if (mesh is null)
            {
                Body = BodyFactory.CreateBody(world.PhysWorld, linkedObject.Transform.GlobalPosition2.ToXna());
            }
            else
            {
                Body = BodyFactory.CreateCompoundPolygon(world.PhysWorld, mesh.Verts, 1, linkedObject.Transform.GlobalPosition2.ToXna(), 0, BodyType.Dynamic);
            }

            Body.Mass = mass;
            PhysWorld = world;
            world.AddRigidBody(this);

            Transform.LocalPosChanged += (Vector3 pos) =>
            {
                if (allowTransformUpdating) Body.Position = pos.XY().ToXna();
            };
            Transform.LocalRotChanged += (Vector3 rot) =>
            {
                if (allowTransformUpdating) Body.Rotation = rot.Z;
            };
        }

        public override void Step()
        {
            allowTransformUpdating = false;
            Transform.LocalPosition = new Vector3(Body.Position.ToNumerics(), 0);
            Transform.LocalRotation = new Vector3(Transform.LocalRotation.XY(), Body.Rotation);
            allowTransformUpdating = true;
        }

        public void AddForce(Vector2 force)
        {
            Body.ApplyForce(force.ToXna());
        }

        public void AddTorque(float torque)
        {
            Body.ApplyTorque(torque);
        }
    }
}
