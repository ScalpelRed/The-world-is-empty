using Game.Main;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Gameplay.Items.RawMaterials
{
    public class RawMaterial : Item
    {
        public const int PhaseSolid = 0;
        public const int PhaseLiquid = 1;
        public const int PhaseGas = 2;

        public int ColdTransfTemp { get; protected set; }
        public RawMaterial ColdTransfOut { get; protected set; }
        public bool HasColdTransf { get; protected set; }

        public int HotTransfTemp { get; protected set; }
        public RawMaterial HotTransfOut { get; protected set; }
        public bool HasHotTransf { get; protected set; }

        public int Phase { get; protected set; }

        public RawMaterial(string rawName, int phase, GameCore core) : base("raw/" + rawName, core)
        {
            ColdTransfOut = this;
            HotTransfOut = this;
            Phase = phase;
        }

        public bool AddColdTransformation(int temperature, RawMaterial result)
        {
            if (HasHotTransf && temperature >= HotTransfTemp) throw new Exception($"Cold transformation temperature ({temperature}) should be lower than " +
                $"hot transformation temperature ({HotTransfTemp})");

            if (result == this) throw new Exception("Material cannot transform into itself.");

            bool res = HasColdTransf;

            ColdTransfTemp = temperature;
            ColdTransfOut = result;
            HasColdTransf = true;

            return res;
        }

        public bool AddHotTransformation(int temperature, RawMaterial result)
        {
            if (HasColdTransf && temperature >= HotTransfTemp) throw new Exception($"Hot transformation temperature ({temperature}) should be lower than " +
                $"cold transformation temperature ({ColdTransfTemp})");

            if (result == this) throw new Exception("Material cannot transform into itself.");

            bool res = HasHotTransf;

            HotTransfTemp = temperature;
            HotTransfOut = result;
            HasHotTransf = true;

            return res;
        }
    }
}
