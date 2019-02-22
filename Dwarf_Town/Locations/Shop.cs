using System;
using System.Collections.Generic;

namespace Dwarf_Town.Locations
{
    public class Shop
    {
        public Decimal Account { get; set; }
        public Decimal Alcohol { get; set; }
        public Decimal Food { get; set; }

        public void BuyGoods(List<Dwarf> dwarves)
        {
            foreach (var dwarf in dwarves)
            {
                dwarf.Buy.Buy(this, dwarf);
            }
        }
    }
}