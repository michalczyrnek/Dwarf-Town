using Dwarf_Town;
using Dwarf_Town.Enums;
using Dwarf_Town.Interfaces;
using Dwarf_Town.Locations.Guild;
using Dwarf_Town.Locations.Mine;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;

namespace Dwarf_TownTests
{
    public class WindowsConsoleTests
    {
        [Test]
        public void ShouldShowThatDwarfSellDirytGold()
        {
            //given
            Mock<IOutputWriter> presenter = new Mock<IOutputWriter>();
            Guild guild = GuildFactory.CreateStandardGuild(presenter.Object);
            Dwarf dwarf = new Dwarf(DwarfType.FATHER);
            dwarf.BackPack.AddOre(MineralType.DirtyGold);
            List<ISell> miners = new List<ISell>() { dwarf._sell };

            //when
            guild.PaymentForDwarves(miners);

            //then
            presenter.Verify(i => i.WriteLine("\nGuild received ore worth:"));
            presenter.Verify(i => i.WriteLine("DirtyGold: 2 gp"));
            presenter.Verify(i => i.WriteLine("Gold: 0 gp"));
            presenter.Verify(i => i.WriteLine("Mithril: 0 gp"));
            presenter.Verify(i => i.WriteLine("Silver: 0 gp"));
        }

        [Test]
        public void ShouldShowThatSuicideDestroyShaft()
        {
            //given
            Mock<IOutputWriter> presenter = new Mock<IOutputWriter>();
            Mine mine = MineFactory.CreateStandardMine(presenter.Object);
            Dwarf dwarf = new Dwarf(DwarfType.SUICIDE);

            //when
            mine.DwarvesGoWork(new List<IWork>() { dwarf._work });

            //given
            presenter.Verify(i => i.WriteLine("Shaft destroyed."));
        }

        [Test]
        public void ShouldShowThatDwarfBroughtOutGold()
        {
            //given
            Mock<IOutputWriter> presenter = new Mock<IOutputWriter>();
            Mine mine = MineFactory.CreateStandardMine(presenter.Object);
            Mock<IWork> workingDwarf = new Mock<IWork>();
            workingDwarf.Setup(i => i.ShowWhatYouBroughtOut()).Returns(new List<MineralType>() { MineralType.Gold });

            //when
            mine.DwarvesGoWork(new List<IWork>() { workingDwarf.Object });

            //then
            presenter.Verify(i => i.WriteLine("\nMine brought out:"));
            presenter.Verify(i => i.WriteLine("DirtyGold: 0"));
            presenter.Verify(i => i.WriteLine("Gold: 1"));
            presenter.Verify(i => i.WriteLine("Mithril: 0"));
            presenter.Verify(i => i.WriteLine("Silver: 0"));
        }
    }
}