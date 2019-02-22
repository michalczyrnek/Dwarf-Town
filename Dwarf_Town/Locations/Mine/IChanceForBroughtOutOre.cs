using Dwarf_Town.Enums;

namespace Dwarf_Town.Locations.Mine
{
    public interface IChanceForBroughtOutOre
    {
        MineralType GetTheOre(int chanceForMineral);
    }
}