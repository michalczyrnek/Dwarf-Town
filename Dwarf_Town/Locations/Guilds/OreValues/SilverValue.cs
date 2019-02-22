using System;

namespace Dwarf_Town.Locations.Guild.OreValue
{
    public class SilverValue : IOreValue
    {
        public decimal GenerateOreValue()
        {
            Random rand = new Random();
            int value = rand.Next(5, 15);
            return value;
        }
    }
}