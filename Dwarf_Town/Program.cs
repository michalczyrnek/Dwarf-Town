using System;

namespace Dwarf_Town
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            SimulationStart simulation = SimulationStartFactory.GenerateStandardStartConditions();
            simulation.Start();
            Console.ReadKey();
        }
    }
}