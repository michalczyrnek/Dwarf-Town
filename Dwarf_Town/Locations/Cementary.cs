using Dwarf_Town.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Dwarf_Town.Locations
{
    public static class Cementary
    {
        public static int GraveyardStats;

        public static void BuryTheDwarves(List<Dwarf> dwarves, IOutputWriter presenter)
        {
            int numberOfDead = dwarves.Where(i => i.IsAlive == false).ToList().Count;
            GraveyardStats += numberOfDead;
            dwarves.RemoveAll(x => x.IsAlive == false);
            presenter.WriteLine($"\nCasualties in mine: {numberOfDead}");
        }
    }
}