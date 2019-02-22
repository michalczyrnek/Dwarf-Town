using Dwarf_Town;
using Dwarf_Town.Enums;
using Dwarf_Town.Interfaces;
using Dwarf_Town.Locations.Mine;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;

namespace Dwarf_TownTests.Locations
{
    [TestFixture]
    public class ShaftTests
    {
        [Test]
        public void DropDiffrentOres()
        {
            //when
            var mineralOne = new GiveSpecificOre().GetTheOre(5);
            var mineralTwo = new GiveSpecificOre().GetTheOre(20);
            var mineralThree = new GiveSpecificOre().GetTheOre(55);
            var mineralFour = new GiveSpecificOre().GetTheOre(100);

            //then
            Assert.IsTrue(mineralOne == MineralType.Mithril);
            Assert.IsTrue(mineralTwo == MineralType.Gold);
            Assert.IsTrue(mineralThree == MineralType.Silver);
            Assert.IsTrue(mineralFour == MineralType.DirtyGold);
        }

        [Test]
        public void DwarfBroughtOutThreeGold()
        {
            //given
            Shaft shaft = new Shaft(new GiveSpecificOre());
            Dwarf dwarf = new Dwarf(DwarfType.FATHER);
            Mock<IWork> workingDwarf = new Mock<IWork>();
            workingDwarf.Setup(i => i.Dig()).Returns(3);
            workingDwarf.Setup(i => i.GenerateChance(It.IsAny<int>(), It.IsAny<int>())).Returns(20);
            workingDwarf.Setup(i => i.HideToBackpack(It.IsAny<MineralType>())).Callback((MineralType ore) =>
            {
                dwarf.BackPack.AddOre(ore);
            });

            //when
            shaft.PerformWork(new List<IWork>() { workingDwarf.Object });

            //then
            Assert.IsTrue(dwarf.BackPack.ShowBackpack().Count == 3);
            foreach (var ore in dwarf.BackPack.ShowBackpack())
            {
                Assert.IsTrue(ore == MineralType.Gold);
            }
        }

        [Test]
        public void SuicideDestroyShaft()
        {
            //given
            Shaft shaft = new Shaft(new GiveSpecificOre());
            Dwarf dwarf = new Dwarf(DwarfType.SUICIDE);

            //when
            shaft.PerformWork(new List<IWork>() { dwarf._work });

            //then
            Assert.IsFalse(shaft.EfficientShaft);
            Assert.IsFalse(dwarf.IsAlive);
        }
    }
}