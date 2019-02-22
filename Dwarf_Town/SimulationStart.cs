using Dwarf_Town.Interfaces;
using Dwarf_Town.Locations;
using Dwarf_Town.Locations.Guild;
using Dwarf_Town.Locations.Mine;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Dwarf_Town
{
    public class SimulationStart
    {
        public Guild Guild;
        public Mine Mine;
        public Canteen Canteen;
        public Shop Shop;
        public Hospital Hospital;
        public IOutputWriter Presenter;
        public List<Dwarf> Dwarves;
        public int MaxDay;
        public int DwarvesBornFirstDay;
        public int ActualDay;

        public SimulationStart(IOutputWriter presenter, Guild guild, Mine mine, Canteen canteen,  
           Shop shop,Hospital hospital,   List<Dwarf> dwarves,  int dwarvesBornFirstDay,    int maxDay)
        {
            Presenter = presenter;
            Guild = guild;
            Mine =  mine;
            Canteen = canteen;
            Shop = shop;
            Hospital =hospital;
            Dwarves = dwarves;
            MaxDay = maxDay;
            DwarvesBornFirstDay = dwarvesBornFirstDay;
            ActualDay = 1;
        }

        public void Start()
        {
            while (ActualDay <= MaxDay)
            {
                Presenter.WriteLine($"\n\n######\nToday is day number {ActualDay}\n");
                if (ActualDay == 1)
                {
                    Dwarves.AddRange(Hospital.GenerateDwarves(DwarvesBornFirstDay));
                }
                else
                {
                    Dwarves.AddRange(Hospital.DailyGenerate());
                }
                Mine.DwarvesGoWork(Dwarves.Select(i => i._work).ToList());
                Cementary.BuryTheDwarves(Dwarves, Presenter);
                if (Dwarves.Count == 0)
                {
                    break;
                }

                Guild.PaymentForDwarves(Dwarves.Select(i => i._sell).ToList());
                Canteen.SpendFoodRations(Dwarves);
                Shop.BuyGoods(Dwarves);
                UpdateSimulationState();
                ActualDay++;
            }

            SimulationReport.GenerateRaport(this);
        }

        private void UpdateSimulationState()
        {
            foreach (var dwarf in Dwarves)
            {
                dwarf.Wallet.OverallCash += Math.Round(dwarf.Wallet.DailyCash, 2);
                dwarf.Wallet.DailyCash = 0;
            }
        }
    }
}