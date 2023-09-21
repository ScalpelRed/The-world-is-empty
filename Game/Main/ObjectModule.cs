using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Main
{
    public abstract class ObjectModule
    {
        public readonly WorldObject LinkedObject;
        public bool StepFromObject = true;

        public GameController Game
        {
            get => LinkedObject.Game;
        }

        public Transform Transform
        {
            get => LinkedObject.Transform;
        }

        public ObjectModule(WorldObject linkedObject)
        {
            LinkedObject = linkedObject;
            linkedObject.AddModule(this);
        }

        public abstract void Step();
    }
}
