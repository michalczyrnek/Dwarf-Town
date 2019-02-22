using Dwarf_Town.Enums;
using Dwarf_Town.Interfaces;
using Dwarf_Town.Models;
using Dwarf_Town.Strategy;

namespace Dwarf_Town
{
    public class Dwarf
    {
        public IWork _work = null;
        public ISell _sell = null;
        public IBuy Buy { get; set; }

        public Dwarf(DwarfType dwarfType)
        {
            if (dwarfType == DwarfType.FATHER)
            {
                _work = new DiggWork(this);
                _sell = new NormalSell(this);
                Buy = new FoodBuy();
            }
            else if (dwarfType == DwarfType.IDLER)
            {
                _work = new DiggWork(this);
                _sell = new NormalSell(this);
                Buy = new NormalBuy();
            }
            else if (dwarfType == DwarfType.SINGLE)
            {
                _work = new DiggWork(this);
                _sell = new NormalSell(this);
                Buy = new AlcoholBuy();
            }
            else if (dwarfType == DwarfType.SUICIDE)
            {
                _work = new ExplodeWork(this);
                _sell = new DefaultSell();
                Buy = new DefaultBuy();
            }

            IsAlive = true;
            BackPack = new Backpack();
            Wallet = new Wallet();
            DwarfType = dwarfType;
        }

        public bool IsAlive { get; set; }
        public Backpack BackPack { get; set; }
        public Wallet Wallet { get; set; }
        public DwarfType DwarfType { get; set; }
    }
}