using Dwarf_Town;
using Dwarf_Town.Enums;
using Dwarf_Town.Locations;
using NUnit.Framework;
using System.Collections.Generic;

namespace Dwarf_TownTests.Locations
{
    [TestFixture]
    public class CanteenTests
    {
        [Test]
        public void ShouldSpendFoodRationsWhenDwarvesListGiven()
        {
            //given
            Canteen canteen = new Canteen(100);
            var dwarves = new List<Dwarf>()
            {
                new Dwarf(DwarfType.FATHER),
                new Dwarf(DwarfType.SINGLE)
            };
            //when
            canteen.SpendFoodRations(dwarves);
            //then
            Assert.AreEqual(98, canteen.FoodRations);
        }

        [Test]
        public void ShouldOrder30FoodsWhenHasLessThen10()
        {
            //given
            Canteen canteen = new Canteen(10);
            var dwarves = new List<Dwarf>()
            {
                new Dwarf(DwarfType.FATHER)
            };
            //when
            canteen.SpendFoodRations(dwarves);
            //then
            Assert.AreEqual(39, canteen.FoodRations);
        }
    }
}