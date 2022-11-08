using MonsterCardTradingGame.Model;

namespace MonsterCardTradingGame.test
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [TestCase(10, 5, 1)]
        [TestCase(5, 6, 2)]
        [TestCase(-1, 2, 2)]
        [TestCase(1, 3, 2)]
        [TestCase(0, 1, 2)]
        [TestCase(1, 0, 1)]
        public void Test1(int damage1, int damage2, int winner)
        {
            MonsterCard Monster = new MonsterCard("Monster", damage1, 2);
            SpellCard Fire = new SpellCard("Fireball", damage2, 1);

            var result = Monster.DoDamage(Monster, Fire);

            Assert.That(result, Is.EqualTo(winner));

            // Assert.Pass();
        }
    }
}