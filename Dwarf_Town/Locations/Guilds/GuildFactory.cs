using Dwarf_Town.Enums;
using Dwarf_Town.Interfaces;
using Dwarf_Town.Locations.Guild.OreValue;
using System.Collections.Generic;

namespace Dwarf_Town.Locations.Guild
{
    public static class GuildFactory
    {
        public static Guild CreateStandardGuild(IOutputWriter presenter)

        {
            Guild guild = new Guild(
                new Dictionary<MineralType, IOreValue>
                {
                {MineralType.DirtyGold, new DirtGoldValue()},
                 {MineralType.Gold, new GoldValue()},
                 {MineralType.Silver, new SilverValue()},
                 {MineralType.Mithril, new MithrilValue()}
                },
                new Dictionary<MineralType, decimal>
                {
                {MineralType.DirtyGold,0},
                 {MineralType.Gold, 0},
                 {MineralType.Silver, 0},
                 {MineralType.Mithril, 0}
                },
                presenter
            );
            return guild;
        }
    }
}