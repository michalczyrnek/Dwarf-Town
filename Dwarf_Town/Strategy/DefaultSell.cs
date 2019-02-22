using Dwarf_Town.Enums;
using Dwarf_Town.Interfaces;
using System.Collections.Generic;

namespace Dwarf_Town.Strategy
{
    public class DefaultSell : ISell
    {
        public void ReceivedMoney(decimal payment)
        {
        }

        public void Sell()
        {
        }

        public List<MineralType> ShowBackpack()
        {
            return new List<MineralType>();
        }
    }
}