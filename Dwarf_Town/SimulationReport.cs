using Dwarf_Town.Locations;
using System;

namespace Dwarf_Town
{
    public static class SimulationReport
    {
        public static void GenerateRaport(SimulationStart simulation)
        {
            simulation.Presenter.WriteLine("\n\nEnd Raport");
            if (simulation.ActualDay - 1 == simulation.MaxDay)
            {
                simulation.Presenter.WriteLine("\nSimulation end because all simulation days passed");
            }
            else
            {
                simulation.Presenter.WriteLine("\nSimulation end because all dwarves died");
            }
            simulation.Presenter.WriteLine("*************");
            simulation.Presenter.WriteLine($"Dwarves died: {Cementary.GraveyardStats}");
            simulation.Presenter.WriteLine("\nMining results (Quantity/Overall value):");
            foreach (var orequantity in simulation.Mine.ShowMineResults())
            {
                foreach (var oreworth in simulation.Guild.ShowGuildRegister())
                {
                    if (orequantity.Key == oreworth.Key)
                    {
                        simulation.Presenter.WriteLine($"{orequantity.Key}: {orequantity.Value}/{oreworth.Value} gp");
                        break;
                    }
                }
            }
            simulation.Presenter.WriteLine("\nShop results:");
            simulation.Presenter.WriteLine($"Alcohol: {simulation.Shop.Alcohol}");
            simulation.Presenter.WriteLine($"Food: {simulation.Shop.Food}");
            simulation.Presenter.WriteLine($"\nIn Canteen left {simulation.Canteen.FoodRations} food rations");
            simulation.Presenter.WriteLine("\nTresure results:");
            simulation.Presenter.WriteLine($"Guild gathered {simulation.Guild.ShowTresure()} gp");
            simulation.Presenter.WriteLine($"Shop gathered {Math.Round(simulation.Shop.Account, 2)} gp");
            foreach (var dwarf in simulation.Dwarves)
            {
                simulation.Presenter.WriteLine($"Dwarf gathered {dwarf.Wallet.OverallCash} gp");
            }
            simulation.Presenter.WriteLine("*************");
        }
    }
}