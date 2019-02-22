using Dwarf_Town.Enums;
using Dwarf_Town.Interfaces;
using System.Collections.Generic;

namespace Dwarf_Town.Strategy
{
    public class ExplodeWork : IWork
    {
        private Dwarf _dwarf;

        public ExplodeWork(Dwarf dwarf)
        {
            _dwarf = dwarf;
        }

        public void DeathSentence()
        {
            _dwarf.IsAlive = false;
        }

        public int Dig()
        {
            // Value -1 destroy shaft
            return -1;
        }

        public int GenerateChance(int lowerBound, int upperBound)
        {
            return 0;
        }

        public void HideToBackpack(MineralType ore)
        {
        }

        public List<MineralType> ShowWhatYouBroughtOut()
        {
            return new List<MineralType>();
        }
    }
}