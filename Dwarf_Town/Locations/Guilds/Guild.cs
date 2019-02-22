using Dwarf_Town.Enums;
using Dwarf_Town.Interfaces;
using Dwarf_Town.Locations.Guild.OreValue;
using System;
using System.Collections.Generic;

namespace Dwarf_Town.Locations.Guild
{
    public class Guild
    {
        private decimal _account;
        private Dictionary<MineralType, IOreValue> _oresOnMarket;
        private Dictionary<MineralType, decimal> _oreValueRegister;
        private Dictionary<MineralType, decimal> _dailyOreValueRegister;
        public IOutputWriter Presenter;

        public Guild(Dictionary<MineralType, IOreValue> oresOnMarket, Dictionary<MineralType, decimal> oreValueRegister, IOutputWriter presenter)
        {
            _account = 0;
            _oresOnMarket = oresOnMarket;
            _oreValueRegister = oreValueRegister;
            _dailyOreValueRegister = new Dictionary<MineralType, decimal>(_oreValueRegister);
            Presenter = presenter;
        }

        public decimal ReturnOreValue(MineralType mineraltype)
        {
            return _oresOnMarket[mineraltype].GenerateOreValue();
        }

        public void PaymentForDwarves(List<ISell> dwarvesVisitGuild)
        {
            foreach (var dwarf in dwarvesVisitGuild)
            {
                foreach (var ore in dwarf.ShowBackpack())
                {
                    decimal value = ReturnOreValue(ore);
                    _dailyOreValueRegister[ore] += value;
                    decimal provision = Math.Round((value * 0.25m), 2);
                    _account += provision;
                    decimal payment = Math.Round((value - provision), 2);
                    dwarf.ReceivedMoney(payment);
                }
                dwarf.ShowBackpack().Clear();
            }
            UpdateMineRegister();
        }

        public decimal ShowTresure()
        {
            return _account;
        }

        public Dictionary<MineralType, decimal> ShowGuildRegister()
        {
            return _oreValueRegister;
        }

        private void UpdateMineRegister()
        {
            Presenter.WriteLine("\nGuild received ore worth:");
            foreach (var ore in _dailyOreValueRegister)
            {
                Presenter.WriteLine($"{ore.Key}: {ore.Value} gp");
                _oreValueRegister[ore.Key] += ore.Value;
            }
            foreach (var key in _oreValueRegister.Keys)
            {
                _dailyOreValueRegister[key] = 0;
            }
        }
    }
}