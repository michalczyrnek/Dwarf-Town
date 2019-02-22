using Dwarf_Town;
using Dwarf_Town.Enums;
using Dwarf_Town.Interfaces;
using Dwarf_Town.Locations;
using Dwarf_Town.Locations.Guild;
using Dwarf_Town.Locations.Guild.OreValue;
using Dwarf_Town.Locations.Mine;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;

namespace Dwarf_TownTests
{
    public class EndToEndTests
    {
        [Test]
        public void OneDwarSingleWorkFor10DaysAndBroughtOutGoldWorth10()
        {
            //given

            //Set base components
            Mock<IChance> chanceForDwarfBorn = new Mock<IChance>();
            chanceForDwarfBorn.Setup(i => i.GenerateChance(It.IsAny<int>(), It.IsAny<int>())).Returns(2);
            Mock<IChanceForBroughtOutOre> chanceForOre = new Mock<IChanceForBroughtOutOre>();
            chanceForOre.Setup(i => i.GetTheOre(It.IsAny<int>())).Returns(MineralType.Gold);
            Mock<IOreValue> oreValue = new Mock<IOreValue>();
            oreValue.Setup(i => i.GenerateOreValue()).Returns(10);
            Mock<IOutputWriter> presenter = new Mock<IOutputWriter>();
            Dwarf dwarf = new Dwarf(DwarfType.FATHER);
            Mock<IWork> workingDwarf = new Mock<IWork>();
            workingDwarf.Setup(i => i.Dig()).Returns(1);
            workingDwarf.Setup(i => i.GenerateChance(It.IsAny<int>(), It.IsAny<int>())).Returns(20);
            workingDwarf.Setup(i => i.HideToBackpack(It.IsAny<MineralType>())).Callback((MineralType ore) =>
            {
                dwarf.BackPack.AddOre(ore);
            });
            workingDwarf.Setup(i => i.ShowWhatYouBroughtOut()).Returns(dwarf.BackPack.ShowBackpack());
            dwarf._work = workingDwarf.Object;

            //Set start condtions
            Guild guild = new Guild(
                new Dictionary<MineralType, IOreValue>() { { MineralType.Gold, oreValue.Object } },
                new Dictionary<MineralType, decimal>() { { MineralType.Gold, 0 } },
                presenter.Object
                );
            List<Dwarf> dwarves = new List<Dwarf>() { dwarf };
            Canteen canteen = new Canteen(200);
            Hospital hospital = new Hospital(chanceForDwarfBorn.Object);
            int maxDay = 10;
            Mine mine = new Mine(1, new Dictionary<MineralType, int>() { { MineralType.Gold, 0 } },
                chanceForOre.Object, presenter.Object);
            Shop shop = new Shop();
            int dwarvesBornFirstDay = 0;
            SimulationStart simulation = new SimulationStart(presenter.Object, guild, mine, canteen,
                shop, hospital, dwarves, dwarvesBornFirstDay, maxDay);


            //when
            simulation.Start();


            //then
            Assert.AreEqual(25, guild.ShowTresure());
            Assert.AreEqual(100, guild.ShowGuildRegister()[0]);
            Assert.AreEqual(37.50m, dwarf.Wallet.OverallCash);
            presenter.Verify(i => i.WriteLine("\nSimulation end because all simulation days passed"));
            presenter.Verify(i => i.WriteLine("*************"));
            presenter.Verify(i => i.WriteLine($"Dwarves died: 0"));
            presenter.Verify(i => i.WriteLine("\nMining results (Quantity/Overall value):"));
            presenter.Verify(i => i.WriteLine($"Gold: 10/100 gp"));
            presenter.Verify(i => i.WriteLine("\nShop results:"));
            presenter.Verify(i => i.WriteLine($"Alcohol: 0"));
            presenter.Verify(i => i.WriteLine($"Food: 37,50"));
            presenter.Verify(i => i.WriteLine($"\nIn Canteen left 190 food rations"));
            presenter.Verify(i => i.WriteLine("\nTresure results:"));
            presenter.Verify(i => i.WriteLine($"Guild gathered 25,00 gp"));
            presenter.Verify(i => i.WriteLine($"Shop gathered 37,50 gp"));
            presenter.Verify(i => i.WriteLine($"Dwarf gathered 37,50 gp"));
            presenter.Verify(i => i.WriteLine("*************"));
        }

        [Test]
        public void SimulationEndAfterSuicideKillAllDwarvesAtFirstDay()
        {
            //given
            Mock<IOutputWriter> presenter = new Mock<IOutputWriter>();
            Mock<IChance> chanceForDwarfBorn = new Mock<IChance>();
            chanceForDwarfBorn.Setup(i => i.GenerateChance(It.IsAny<int>(), It.IsAny<int>())).Returns(2);
            List<Dwarf> dwarves = new List<Dwarf>();
            dwarves.Add(new Dwarf(DwarfType.SUICIDE));
            for (int i = 0; i < 4; i++)
            {
                dwarves.Add(new Dwarf(DwarfType.FATHER));
            }
            Canteen canteen = new Canteen(200);
            Hospital hospital = new Hospital(chanceForDwarfBorn.Object);
            int maxDay = 10;
            Mine mine = MineFactory.CreateStandardMine(presenter.Object);
            int dwarvesBornFirstDay = 0;
            Guild guild = GuildFactory.CreateStandardGuild(presenter.Object);
            Shop shop = new Shop();

            SimulationStart simulation = new SimulationStart(presenter.Object, guild, mine, canteen, shop,
                hospital, dwarves, dwarvesBornFirstDay, maxDay);
            Assert.IsTrue(simulation.Dwarves.Count == 5);


            //when
            simulation.Start();


            //then
            Assert.IsTrue(simulation.Dwarves.Count == 0);
            Assert.IsTrue(Cementary.GraveyardStats == 5);
            Assert.IsTrue(simulation.ActualDay == 1);
            presenter.Verify(i => i.WriteLine("Shaft destroyed."));
            presenter.Verify(i => i.WriteLine("\nCasualties in mine: 5"));
            simulation.Presenter.WriteLine("\nSimulation end because all dwarves died");
        }
    }
}