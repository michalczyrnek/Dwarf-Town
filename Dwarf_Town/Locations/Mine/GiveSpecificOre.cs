using Dwarf_Town.Enums;

namespace Dwarf_Town.Locations.Mine
{
    public class GiveSpecificOre : IChanceForBroughtOutOre
    {
        public MineralType GetTheOre(int chanceForMineral)
        {
            if (chanceForMineral <= 5)
            {
                return MineralType.Mithril;
            }
            else if (chanceForMineral <= 20)
            {
                return MineralType.Gold;
            }
            else if (chanceForMineral <= 55)
            {
                return MineralType.Silver;
            }
            else
            {
                return MineralType.DirtyGold;
            }
        }
    }
}