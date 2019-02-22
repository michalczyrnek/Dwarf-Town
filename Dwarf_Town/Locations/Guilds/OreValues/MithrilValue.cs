using System;

namespace Dwarf_Town.Locations.Guild.OreValue
{
    internal class MithrilValue : IOreValue
    {
        public decimal GenerateOreValue()
        {
            Random rand = new Random();
            int value = rand.Next(15, 25);
            return value;
        }
    }
}