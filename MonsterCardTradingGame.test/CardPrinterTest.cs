using MonsterCardTradingGame.BL;
using MonsterCardTradingGame.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonsterCardTradingGame.test
{
    public class CardPrinterTest
    {
     
        MonsterCard MonsterCardtoPrint;

        SpellCard SpellCardToPrint;

        const int BaseDamage = 10;

        CardPrinter CardPrinter;

        [SetUp]
        public void Setup()
        {
            CardPrinter = new CardPrinter();
        }


        [Test]
        public void PrintDragon()
        {
            MonsterCardtoPrint = (MonsterCard)CardPrinter.PrintCard("Dragon", BaseDamage);

            Assert.IsNotNull(MonsterCardtoPrint);

            Assert.That(MonsterCardtoPrint.Cardname, Is.EqualTo("Dragon"));

            Assert.That(MonsterCardtoPrint.DamageType, Is.EqualTo(DamageType.Fire));

            Assert.That(MonsterCardtoPrint.MonsterType, Is.EqualTo(MonsterType.Dragon));
        }


        [Test]
        public void PrintKraken()
        {
            MonsterCardtoPrint = (MonsterCard)CardPrinter.PrintCard("Kraken", BaseDamage);

            Assert.IsNotNull(MonsterCardtoPrint);

            Assert.That(MonsterCardtoPrint.Cardname, Is.EqualTo("Kraken"));

            Assert.That(MonsterCardtoPrint.DamageType, Is.EqualTo(DamageType.Water));

            Assert.That(MonsterCardtoPrint.MonsterType, Is.EqualTo(MonsterType.Kraken));
        }


        public void PrintKnight()
        {
            MonsterCardtoPrint = (MonsterCard)CardPrinter.PrintCard("Knight", BaseDamage);

            Assert.IsNotNull(MonsterCardtoPrint);

            Assert.That(MonsterCardtoPrint.Cardname, Is.EqualTo("Knight"));

            Assert.That(MonsterCardtoPrint.DamageType, Is.EqualTo(DamageType.Normal));

            Assert.That(MonsterCardtoPrint.MonsterType, Is.EqualTo(MonsterType.Knight));
        }

        [Test]
        public void PrintWaterSpell()
        {
            SpellCardToPrint = (SpellCard)CardPrinter.PrintCard("WaterSpell", BaseDamage);

            Assert.IsNotNull(SpellCardToPrint);

            Assert.That(SpellCardToPrint.Cardname, Is.EqualTo("WaterSpell"));

            Assert.That(SpellCardToPrint.DamageType, Is.EqualTo(DamageType.Water));
        }

        [Test]
        public void PrintRegularSpell()
        {
            SpellCardToPrint = (SpellCard)CardPrinter.PrintCard("RegularSpell", BaseDamage);

            Assert.IsNotNull(SpellCardToPrint);

            Assert.That(SpellCardToPrint.Cardname, Is.EqualTo("RegularSpell"));

            Assert.That(SpellCardToPrint.DamageType, Is.EqualTo(DamageType.Normal));
        }

        [Test]
        public void PrintFireSpell()
        {
            SpellCardToPrint = (SpellCard)CardPrinter.PrintCard("FireSpell", BaseDamage);

            Assert.IsNotNull(SpellCardToPrint);

            Assert.That(SpellCardToPrint.Cardname, Is.EqualTo("FireSpell"));

            Assert.That(SpellCardToPrint.DamageType, Is.EqualTo(DamageType.Fire));
        }

    }
}
