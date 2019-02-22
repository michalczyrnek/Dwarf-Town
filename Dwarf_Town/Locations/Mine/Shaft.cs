using Dwarf_Town.Interfaces;
using System.Collections.Generic;

namespace Dwarf_Town.Locations.Mine
{
    public class Shaft
    {
        public bool EfficientShaft;
        private IChanceForBroughtOutOre ChanceForSpecificOre;

        public Shaft(IChanceForBroughtOutOre randomizer)
        {
            EfficientShaft = true;
            ChanceForSpecificOre = randomizer;
        }

        public void PerformWork(List<IWork> dwarvesWorkingInShaft)
        {
            foreach (var dwarf in dwarvesWorkingInShaft)

            {
                int HowManyTimesDwarfHit = dwarf.Dig();

                if (HowManyTimesDwarfHit == -1)
                {
                    //Suicide attack
                    EfficientShaft = false;

                    break;
                }
                else
                {
                    //Normal work
                    for (int i = 0; i < HowManyTimesDwarfHit; i++)
                    {
                        int chanceForOre = dwarf.GenerateChance(1, 101);
                        dwarf.HideToBackpack(ChanceForSpecificOre.GetTheOre(chanceForOre));
                    }
                }
            }

            if (EfficientShaft == false)
            {
                foreach (var dwarf in dwarvesWorkingInShaft)
                {
                    dwarf.DeathSentence();
                }
            }
        }
    }
}