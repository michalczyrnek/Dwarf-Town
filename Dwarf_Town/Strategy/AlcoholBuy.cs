using Dwarf_Town.Interfaces;
using Dwarf_Town.Locations;

namespace Dwarf_Town.Strategy
{
    public class AlcoholBuy : IBuy
    {
        public void Buy(Shop shop, Dwarf dwarf)
        {
            dwarf.Wallet.DailyCash /= 2;
            shop.Account += dwarf.Wallet.DailyCash;
            shop.Alcohol += dwarf.Wallet.DailyCash;
        }
    }
}