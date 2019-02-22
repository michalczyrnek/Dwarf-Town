using Dwarf_Town.Enums;
using Dwarf_Town.Interfaces;
using System.Collections.Generic;

namespace Dwarf_Town.Locations.Mine
{
    public static class MineFactory
    {
        public static Mine CreateStandardMine(IOutputWriter presenter)
        {
            Mine mine = new Mine(2, new Dictionary<MineralType, int>()
            {
                {MineralType.DirtyGold,0 },
                {MineralType.Gold,0 },
                {MineralType.Silver,0 },
                {MineralType.Mithril,0 } },
                new GiveSpecificOre(), presenter);
            return mine;
        }
    }
}