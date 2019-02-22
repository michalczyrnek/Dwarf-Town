using Dwarf_Town.Enums;
using System.Collections.Generic;

namespace Dwarf_Town.Interfaces
{
    public interface IWork : IChance
    {
        int Dig();

        void HideToBackpack(MineralType ore);

        List<MineralType> ShowWhatYouBroughtOut();

        void DeathSentence();
    }
}