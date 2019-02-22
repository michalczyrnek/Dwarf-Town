using Dwarf_Town.Enums;
using Dwarf_Town.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Dwarf_Town.Locations.Mine
{
    public class Mine
    {
        public List<Shaft> Shafts;
        private Dictionary<MineralType, int> _oreRegister;
        private Dictionary<MineralType, int> _dailyOreRegister;
        public IOutputWriter Presenter;

        public Mine(int numberOfShafts, Dictionary<MineralType, int> register, IChanceForBroughtOutOre randomizer, IOutputWriter presenter)
        {
            Shafts = new List<Shaft>();
            for (int i = 0; i < numberOfShafts; i++)
            {
                Shafts.Add(new Shaft(randomizer));
            }
            _oreRegister = register;
            _dailyOreRegister = new Dictionary<MineralType, int>(_oreRegister);
            Presenter = presenter;
        }

        public void DwarvesGoWork(List<IWork> dwarvesVisitMine)
        {
            do
            {
                foreach (var shaft in Shafts)
                {
                    if (shaft.EfficientShaft == true)
                    {
                        if (dwarvesVisitMine.Count >= 5)
                        {
                            shaft.PerformWork(dwarvesVisitMine.GetRange(0, 5));
                            RegistBroughtOutOre(dwarvesVisitMine.SelectMany(i => i.ShowWhatYouBroughtOut()).ToList());
                            dwarvesVisitMine.RemoveRange(0, 5);
                        }
                        else
                        {
                            shaft.PerformWork(dwarvesVisitMine.GetRange(0, dwarvesVisitMine.Count));
                            RegistBroughtOutOre(dwarvesVisitMine.SelectMany(i => i.ShowWhatYouBroughtOut()).ToList());
                            dwarvesVisitMine.RemoveRange(0, dwarvesVisitMine.Count);
                        }

                        if (shaft.EfficientShaft == false)
                        {
                            Presenter.WriteLine("Shaft destroyed.");
                        }
                    }
                }
            } while (dwarvesVisitMine.Count > 0);
            UpdateMineRegister();
        }

        public Dictionary<MineralType, int> ShowMineResults()
        {
            return _oreRegister;
        }

        private void RegistBroughtOutOre(List<MineralType> ores)
        {
            foreach (var ore in ores)
            {
                _dailyOreRegister[ore]++;
            }
        }

        private void UpdateMineRegister()
        {
            Presenter.WriteLine("\nMine brought out:");
            foreach (var ore in _dailyOreRegister)
            {
                Presenter.WriteLine($"{ore.Key}: {ore.Value}");
                _oreRegister[ore.Key] += ore.Value;
            }
            foreach (var key in _dailyOreRegister.Keys.ToList())
            {
                _dailyOreRegister[key] = 0;
            }
        }
    }
}