using Dwarf_Town.Enums;
using Dwarf_Town.Interfaces;
using System.Collections.Generic;

namespace Dwarf_Town.Strategy
{
    public class NormalSell : ISell
    {
        private Dwarf _dwarf;

        public NormalSell(Dwarf dwarf)
        {
            _dwarf = dwarf;
        }

        public void ReceivedMoney(decimal payment)
        {
            _dwarf.Wallet.DailyCash += payment;
        }

        public List<MineralType> ShowBackpack()
        {
            return _dwarf.BackPack.ShowBackpack();
        }
    }
}