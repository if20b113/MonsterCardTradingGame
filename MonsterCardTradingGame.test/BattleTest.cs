using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities.Serialization;
using MonsterCardTradingGame.BL;
using MonsterCardTradingGame.Model;
using System.Reflection;

namespace MonsterCardTradingGame.test
{
    public class BattleTests
    {

        const int BaseDamage = 10;

        IUser Player1 = new User("Player1", "dfadsa");

        IUser Player2 = new User("Player2", "dfadsa");

        

        List<ICard> Deck1 = new List<ICard>();

        List<ICard> Deck2 = new List<ICard>();

        [SetUp]
        public void Setup()
        {
          

        }

        [Test]
        public void WaterSpellEffectiveVSFireSpell()
        {
            ICard WaterSpell = new SpellCard(DamageType.Water, BaseDamage);

            ICard FireSpell = new SpellCard(DamageType.Fire, BaseDamage);

            Assert.That(WaterSpell.CalculateDamage(FireSpell), Is.EqualTo(BaseDamage*2));
        }

        [Test]
        public void FireSpellEffectiveVSNormalSpell()
        {
            ICard FireSpell = new SpellCard(DamageType.Fire, BaseDamage);

            ICard NormalSpell = new SpellCard(DamageType.Normal, BaseDamage);

            Assert.That(NormalSpell.CalculateDamage(FireSpell), Is.EqualTo(BaseDamage/2));
        }

        [Test]
        public void NormalSpellEffectiveVSWaterSpell()
        {
            ICard Spell1 = new SpellCard(DamageType.Normal, BaseDamage);

            ICard Spell2 = new SpellCard(DamageType.Water, BaseDamage);

            Assert.That(Spell1.CalculateDamage(Spell2), Is.EqualTo(BaseDamage*2));
        }

        [Test]
        public void FireSpellEffectiveVsNormalSpell()
        {
            ICard Spell1 = new SpellCard(DamageType.Fire, BaseDamage);

            ICard Spell2 = new SpellCard(DamageType.Normal, BaseDamage);

            Assert.That(Spell1.CalculateDamage(Spell2), Is.EqualTo(BaseDamage * 2));
        }

        [Test]
        public void NormalSpellEffectiveVsWaterSpell()
        { 
            ICard Spell1 = new SpellCard(DamageType.Normal, BaseDamage);

            ICard Spell2 = new SpellCard(DamageType.Water, BaseDamage);

            Assert.That(Spell1.CalculateDamage(Spell2), Is.EqualTo(BaseDamage * 2));
        }

        [Test]
        public void FireSpellWeakVsWaterSpell()
        {
            ICard Spell1 = new SpellCard(DamageType.Fire, BaseDamage);

            ICard Spell2 = new SpellCard(DamageType.Water, BaseDamage);

            Assert.That(Spell1.CalculateDamage(Spell2), Is.EqualTo(BaseDamage/2));
        }

        [Test]
        public void WaterSpellWeakVsNormalSpell()
        {
            ICard Spell1 = new SpellCard(DamageType.Water, BaseDamage);

            ICard Spell2 = new SpellCard(DamageType.Normal, BaseDamage);

            Assert.That(Spell1.CalculateDamage(Spell2), Is.EqualTo(BaseDamage / 2));
        }

        [Test]
        public void NormalSpellWeakVsFireSpell()
        {
            ICard Spell1 = new SpellCard(DamageType.Normal, BaseDamage);

            ICard Spell2 = new SpellCard(DamageType.Fire, BaseDamage);

            Assert.That(Spell1.CalculateDamage(Spell2), Is.EqualTo(BaseDamage / 2));
        }


        [Test]
        public void MonsterVSMonsterNoElements()
        {
            ICard Monster1 = new MonsterCard(DamageType.Water,MonsterType.Goblin, BaseDamage, "WaterGoblin");

            ICard Monster2 = new MonsterCard(DamageType.Fire, MonsterType.Goblin, BaseDamage, "FireGoblin");

            Assert.That(Monster1.CalculateDamage(Monster2), Is.EqualTo(BaseDamage));

            Assert.That(Monster2.CalculateDamage(Monster1), Is.EqualTo(BaseDamage));
        }

        [Test]
        public void SpellVSMonsterObservesElements()
        {
            ICard Spell1 = new SpellCard(DamageType.Water, BaseDamage);

            ICard Monster2 = new MonsterCard(DamageType.Fire, MonsterType.Goblin, BaseDamage, "FireGoblin");

            Assert.That(Spell1.CalculateDamage(Monster2), Is.EqualTo(BaseDamage*2));

            Assert.That(Monster2.CalculateDamage(Spell1), Is.EqualTo(BaseDamage/2));
        }


        [Test]
        public void FireElvesVSDragon()
        {
            ICard Monster1 = new MonsterCard(DamageType.Fire, MonsterType.Elf, BaseDamage, "Fireelf");

            ICard Monster2 = new MonsterCard(DamageType.Fire, MonsterType.Dragon, BaseDamage, "Dragon");

            Assert.That(Monster2.CalculateDamage(Monster1), Is.EqualTo(0));

        }

        [Test]
        public void KrakenImmunetoSpells()
        {
            ICard Spell1 = new SpellCard(DamageType.Water, BaseDamage);

            ICard Monster2 = new MonsterCard(DamageType.Water, MonsterType.Kraken, BaseDamage, "Kraken");

            Assert.That(Spell1.CalculateDamage(Monster2), Is.EqualTo(0));

        }

        [Test]
        public void WaterVSKnightSpecial()
        {
            ICard Spell1 = new SpellCard(DamageType.Water, BaseDamage);

            ICard Monster2 = new MonsterCard(DamageType.Normal, MonsterType.Knight, BaseDamage, "Knight");

            Assert.That(Spell1.CalculateDamage(Monster2), Is.GreaterThan(Monster2.damage));

        }

        [Test]
        public void TestFight()
        {
            Deck1.Add(new MonsterCard(DamageType.Water, MonsterType.Goblin, 50,"WaterGoblin"));
            Deck1.Add(new MonsterCard(DamageType.Fire, MonsterType.Goblin, 45, "FireGoblin"));
            Deck1.Add(new MonsterCard(DamageType.Fire, MonsterType.Dragon, 55, "Dragon"));
            Deck1.Add(new MonsterCard(DamageType.Normal, MonsterType.Knight, 60, "Knight"));


            Player1.AddDeck(Deck1);
           

            Deck2.Add(new MonsterCard(DamageType.Water, MonsterType.Goblin, 10, "WaterGoblin"));
            Deck2.Add(new MonsterCard(DamageType.Fire, MonsterType.Goblin, 15, "FireGoblin"));
            Deck2.Add(new MonsterCard(DamageType.Fire, MonsterType.Dragon, 26, "Dragon"));
            Deck2.Add(new MonsterCard(DamageType.Normal, MonsterType.Knight, 35, "Knight"));

            Player2.AddDeck(Deck2);

            BattleHandler Battle = new BattleHandler(Player1, Player2);

            var result = Battle.StartBattle();    

            Assert.AreEqual(result.Winner.Username,Player1.Username);
        }


        [Test]
        public void TestFightDraw()
        {
            Deck1.Add(new MonsterCard(DamageType.Water, MonsterType.Goblin, 50, "WaterGoblin"));
            Deck1.Add(new MonsterCard(DamageType.Water, MonsterType.Goblin, 50, "WaterGoblin"));
            Deck1.Add(new MonsterCard(DamageType.Water, MonsterType.Goblin, 50, "WaterGoblin"));
            Deck1.Add(new MonsterCard(DamageType.Water, MonsterType.Goblin, 50, "WaterGoblin"));


            Player1.AddDeck(Deck1);

            Player2.AddDeck(Deck1);

            BattleHandler Battle = new BattleHandler(Player1, Player2);

            var result = Battle.StartBattle();

            Assert.That(result.IsDraw, Is.EqualTo(false));
        }

    }
}