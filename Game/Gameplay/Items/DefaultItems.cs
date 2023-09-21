using Game.Gameplay.Items.RawMaterials;
using Game.Main;

namespace Game.Gameplay.Items
{
    public partial class ItemList
    {
        public static void AddDefaultItems(ItemList list, GameCore core)
        {
            {
                RawMaterial wasteRock = new("waste_rock", RawMaterial.PhaseSolid, core);
                RawMaterial wasteLiquid = new("waste_liquid", RawMaterial.PhaseLiquid, core);
                RawMaterial wasteGas = new("waste_gas", RawMaterial.PhaseGas, core);

                list.Add(wasteRock, wasteLiquid, wasteGas);
            }

            {
                RawMaterial steam = new("steam", RawMaterial.PhaseGas, core);
                RawMaterial water = new("water", RawMaterial.PhaseLiquid, core);
                RawMaterial ice = new("ice", RawMaterial.PhaseSolid, core);

                steam.AddColdTransformation(99, water);
                water.AddHotTransformation(100, steam);
                water.AddColdTransformation(-1, ice);
                ice.AddHotTransformation(0, water);

                list.Add(ice, water, steam);
            }

            {
                RawMaterial neorha = new("neorha", RawMaterial.PhaseGas, core);
                RawMaterial liquidNeorha = new("liquid_neorha", RawMaterial.PhaseLiquid, core);

                neorha.AddColdTransformation(27, liquidNeorha);
                liquidNeorha.AddHotTransformation(26, neorha);

                list.Add(neorha, liquidNeorha);
            }
        }
    }
}
