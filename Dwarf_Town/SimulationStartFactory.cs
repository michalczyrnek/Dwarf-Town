using Dwarf_Town.Interfaces;
using Dwarf_Town.Locations;
using Dwarf_Town.Locations.Guild;
using Dwarf_Town.Locations.Mine;
using System.Collections.Generic;

namespace Dwarf_Town
{
    public static class SimulationStartFactory
    {
        public static SimulationStart GenerateStandardStartConditions()
        {
            IOutputWriter presenter = new WindowsConsole();

            SimulationStart start = new SimulationStart(presenter, GuildFactory.CreateStandardGuild(presenter),
                MineFactory.CreateStandardMine(presenter), new Canteen(200), new Shop(), new Hospital(new Chance()),
                new List<Dwarf>(), 10, 30);

            return start;
        }
    }
}