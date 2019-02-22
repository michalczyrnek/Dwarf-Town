using Dwarf_Town;
using Dwarf_Town.Enums;
using Dwarf_Town.Interfaces;
using Dwarf_Town.Locations.Mine;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace Dwarf_TownTests.Locations
{
    public class MineTests
    {
        [Test]
        public void FiveDwarvesBroughtOutOre()
        {
            //given
            Mine mine = MineFactory.CreateStandardMine(new WindowsConsole());
            List<Dwarf> dwarves = new List<Dwarf>();
            for (int i = 0; i < 5; i++)
            {
                dwarves.Add(new Dwarf(DwarfType.FATHER));
            }

            //when
            mine.DwarvesGoWork(dwarves.Select(i => i._work).ToList());

            //then
            foreach (var dwarf in dwarves)
            {
                Assert.IsFalse(dwarf.BackPack.ShowBackpack().Count == 0);
            }
        }

        [Test]
        public void SuicideDestroyShaftAndKillDwarves()
        {
            //given
            Mine mine = MineFactory.CreateStandardMine(new WindowsConsole());
            List<Dwarf> dwarves = new List<Dwarf>();
            for (int i = 0; i < 4; i++)
            {
                dwarves.Add(new Dwarf(DwarfType.FATHER));
            }
            dwarves.Add(new Dwarf(DwarfType.SUICIDE));

            //when
            mine.DwarvesGoWork(dwarves.Select(i => i._work).ToList());

            //then
            foreach (var dwarf in dwarves)
            {
                Assert.IsFalse(dwarf.IsAlive);
            }
            Assert.IsFalse(mine.Shafts[0].EfficientShaft);
            Assert.IsTrue(mine.Shafts[1].EfficientShaft);
        }

        [Test]
        public void DwarvesWorkInSecondShaftWhenFirstWasDestroyed()
        {
            //given
            Mine mine = MineFactory.CreateStandardMine(new WindowsConsole());
            List<Dwarf> dwarves = new List<Dwarf>();
            for (int i = 0; i < 5; i++)
            {
                dwarves.Add(new Dwarf(DwarfType.FATHER));
            }
            mine.Shafts[0].EfficientShaft = false;

            //when
            mine.DwarvesGoWork(dwarves.Select(i => i._work).ToList());

            //then
            foreach (var dwarf in dwarves)
            {
                Assert.IsFalse(dwarf.BackPack.ShowBackpack().Count == 0);
            }
            Assert.IsFalse(mine.Shafts[0].EfficientShaft);
            Assert.IsTrue(mine.Shafts[1].EfficientShaft);
        }

        [Test]
        public void AllSixDwarvesBroughtOutOre()
        {
            //given
            Mine mine = MineFactory.CreateStandardMine(new WindowsConsole());
            List<Dwarf> dwarves = new List<Dwarf>();
            for (int i = 0; i < 6; i++)
            {
                dwarves.Add(new Dwarf(DwarfType.FATHER));
            }

            //when
            mine.DwarvesGoWork(dwarves.Select(i => i._work).ToList());

            //then
            foreach (var dwarf in dwarves)
            {
                Assert.IsFalse(dwarf.BackPack.ShowBackpack().Count == 0);
            }
        }

        [Test]
        public void SuicideKillDwarvesInFirstShaftSoOthersWorkInSecondShaft()
        {
            //given
            Mine mine = MineFactory.CreateStandardMine(new WindowsConsole());
            List<Dwarf> dwarves = new List<Dwarf>();
            dwarves.Add(new Dwarf(DwarfType.SUICIDE));
            for (int i = 0; i < 15; i++)
            {
                dwarves.Add(new Dwarf(DwarfType.FATHER));
            }

            //when
            mine.DwarvesGoWork(dwarves.Select(i => i._work).ToList());

            //then
            for (int i = 0; i < 5; i++)
            {
                Assert.IsTrue(dwarves[i].BackPack.ShowBackpack().Count == 0);
            }
            for (int i = 5; i < 16; i++)
            {
                Assert.IsFalse(dwarves[i].BackPack.ShowBackpack().Count == 0);
            }
            Assert.IsFalse(mine.Shafts[0].EfficientShaft);
            Assert.IsTrue(mine.Shafts[1].EfficientShaft);
        }

        [Test]
        public void MineRegisterWhatDwarvesBroughtOut()
        {
            //given
            Mine mine = MineFactory.CreateStandardMine(new WindowsConsole());
            Mock<IWork> workingDwarfOne = new Mock<IWork>();
            Mock<IWork> workingDwarfTwo = new Mock<IWork>();
            workingDwarfOne.Setup(i => i.ShowWhatYouBroughtOut()).Returns(new List<MineralType>()
            {
                MineralType.DirtyGold,
                MineralType.DirtyGold,
                MineralType.DirtyGold,
            });
            workingDwarfTwo.Setup(i => i.ShowWhatYouBroughtOut()).Returns(new List<MineralType>()
            {
                MineralType.Gold,
                MineralType.Gold,
                MineralType.Gold,
            });

            //when
            mine.DwarvesGoWork(new List<IWork>()
            {
            { workingDwarfOne.Object },
            { workingDwarfTwo.Object }
            }
            );

            //then
            Assert.IsTrue(mine.ShowMineResults()[MineralType.DirtyGold] == 3);
            Assert.IsTrue(mine.ShowMineResults()[MineralType.Gold] == 3);
        }
    }
}