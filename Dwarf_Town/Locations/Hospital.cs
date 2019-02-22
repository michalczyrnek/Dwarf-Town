using Dwarf_Town.Enums;
using Dwarf_Town.Interfaces;
using System.Collections.Generic;

namespace Dwarf_Town.Locations
{
    public class Hospital
    {
        private readonly IChance _chance;

        public Hospital(IChance chance)
        {
            _chance = chance;
        }

        public List<Dwarf> GenerateDwarves(int number)
        {
            List<Dwarf> dwarves = new List<Dwarf>();
            for (int i = 0; i < number; i++)
            {
                dwarves.Add(Generate());
            }
            return dwarves;
        }

        public List<Dwarf> DailyGenerate()
        {
            List<Dwarf> dwarves = new List<Dwarf>();
            var chance = _chance.GenerateChance(1, 100);
            if (chance == 1)
            {
                dwarves.Add(Generate());
                return dwarves;
            }
            return dwarves;
        }

        public Dwarf Generate()
        {
            var chance = _chance.GenerateChance(1, 100);
            if (chance == 100)
            {
                return new Dwarf(DwarfType.SUICIDE);
            }
            else if (chance >= 1 && chance <= 33)
            {
                return new Dwarf(DwarfType.FATHER);
            }
            else if (chance > 33 && chance <= 66)
            {
                return new Dwarf(DwarfType.IDLER);
            }
            else
            {
                return new Dwarf(DwarfType.SINGLE);
            }
        }
    }
}