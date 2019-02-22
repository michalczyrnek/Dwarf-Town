using System.Collections.Generic;

namespace Dwarf_Town.Locations
{
    public class Canteen
    {
        public Canteen(int foodRations)
        {
            FoodRations = foodRations;
        }

        public int FoodRations { get; set; }

        public void SpendFoodRations(List<Dwarf> dwarves)
        {
            FoodRations -= dwarves.Count;
            if (FoodRations < 10)
            {
                FoodRations += 30;
            }
        }
    }
}