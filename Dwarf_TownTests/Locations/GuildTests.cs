using Dwarf_Town;
using Dwarf_Town.Enums;
using Dwarf_Town.Interfaces;
using Dwarf_Town.Locations.Guild;
using NUnit.Framework;
using System.Collections.Generic;

namespace Dwarf_TownTests.Locations
{
    [TestFixture]
    public class GuildTests
    {
        [Test]
        public void ReturnPaymentForOneDwarWithEmptyBackpack()
        {
            //given
            Guild guild = GuildFactory.CreateStandardGuild(new WindowsConsole());
            Dwarf dwarf = new Dwarf(DwarfType.FATHER);
            List<ISell> miners = new List<ISell>() { dwarf._sell };

            //when
            guild.PaymentForDwarves(miners);

            //then
            Assert.AreEqual(0, guild.ShowTresure());
            Assert.AreEqual(0, dwarf.Wallet.DailyCash);
            Assert.IsTrue(dwarf.BackPack.ShowBackpack().Count == 0);
        }

        [Test]
        public void ReturnPaymentForOneDwarfWithOneDirtyGold()
        {
            //given
            Guild guild = GuildFactory.CreateStandardGuild(new WindowsConsole());
            Dwarf dwarf = new Dwarf(DwarfType.FATHER);
            dwarf.BackPack.AddOre(MineralType.DirtyGold);
            List<ISell> miners = new List<ISell>() { dwarf._sell };

            //when
            guild.PaymentForDwarves(miners);

            //then
            Assert.AreEqual(0.5, guild.ShowTresure());
            Assert.AreEqual(1.5, dwarf.Wallet.DailyCash);
            Assert.IsTrue(dwarf.BackPack.ShowBackpack().Count == 0);
        }

        [Test]
        public void ReturnPaymentForOneDwarfWithOTwoDirtyGold()
        {
            //given
            Guild guild = GuildFactory.CreateStandardGuild(new WindowsConsole());
            Dwarf dwarf = new Dwarf(DwarfType.FATHER);
            dwarf.BackPack.AddOre(MineralType.DirtyGold);
            dwarf.BackPack.AddOre(MineralType.DirtyGold);

            List<ISell> miners = new List<ISell>() { dwarf._sell };

            //when
            guild.PaymentForDwarves(miners);

            //then
            Assert.AreEqual(1, guild.ShowTresure());
            Assert.AreEqual(3, dwarf.Wallet.DailyCash);
            Assert.IsTrue(dwarf.BackPack.ShowBackpack().Count == 0);
            Assert.IsTrue(guild.ShowGuildRegister()[MineralType.DirtyGold] == 4);
        }

        [Test]
        public void ReturnPaymentForTwoDwarvesWithOTwoDirtyGold()
        {
            //given
            Guild guild = GuildFactory.CreateStandardGuild(new WindowsConsole());
            Dwarf dwarfOne = new Dwarf(DwarfType.FATHER);
            Dwarf dwarfTwo = new Dwarf(DwarfType.FATHER);
            dwarfOne.BackPack.AddOre(MineralType.DirtyGold);
            dwarfOne.BackPack.AddOre(MineralType.DirtyGold);
            dwarfTwo.BackPack.AddOre(MineralType.DirtyGold);
            dwarfTwo.BackPack.AddOre(MineralType.DirtyGold);

            List<ISell> miners = new List<ISell>()
            {
                 dwarfOne._sell,
                 dwarfTwo._sell
            };

            //when
            guild.PaymentForDwarves(miners);

            //then
            Assert.AreEqual(2, guild.ShowTresure());
            Assert.AreEqual(3, dwarfOne.Wallet.DailyCash);
            Assert.AreEqual(3, dwarfTwo.Wallet.DailyCash);
            Assert.IsTrue(dwarfOne.BackPack.ShowBackpack().Count == 0);
            Assert.IsTrue(dwarfTwo.BackPack.ShowBackpack().Count == 0);
            Assert.IsTrue(guild.ShowGuildRegister()[MineralType.DirtyGold] == 8);
        }
    }
}