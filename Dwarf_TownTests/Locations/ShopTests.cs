using Dwarf_Town;
using Dwarf_Town.Enums;
using Dwarf_Town.Locations;
using NUnit.Framework;
using System.Collections.Generic;

namespace Dwarf_TownTests.Locations
{
    [TestFixture]
    public class ShopTests
    {
        [Test]
        public void ShouldTakeHalfDailyMoneyFromFatherAndGiveEquivalenceToShopAccountAndUpdateFoodCountWhenDwarfGoToShop()
        {
            //given
            Shop shop = new Shop();
            shop.Account = 200;
            shop.Food = 50;
            List<Dwarf> dwarves = new List<Dwarf>();
            Dwarf dwarf = new Dwarf(DwarfType.FATHER);
            dwarf.Wallet.DailyCash = 100M;
            dwarves.Add(dwarf);
            //when
            shop.BuyGoods(dwarves);
            //then
            Assert.AreEqual(50, dwarf.Wallet.DailyCash);
            Assert.AreEqual(250, shop.Account);
            Assert.AreEqual(100, shop.Food);
        }

        [Test]
        public void ShouldTakeHalfDailyMoneyFromSingleAndGiveEquivalenceToShopAccountAndUpdateAlcoholCountWhenDwarfGoToShop()
        {
            //given
            Shop shop = new Shop();
            shop.Account = 200;
            shop.Alcohol = 50;
            List<Dwarf> dwarves = new List<Dwarf>();
            Dwarf dwarf = new Dwarf(DwarfType.SINGLE);
            dwarf.Wallet.DailyCash = 100M;
            dwarves.Add(dwarf);
            //when
            shop.BuyGoods(dwarves);
            //then
            Assert.AreEqual(50, dwarf.Wallet.DailyCash);
            Assert.AreEqual(250, shop.Account);
            Assert.AreEqual(100, shop.Alcohol);
        }
    }
}