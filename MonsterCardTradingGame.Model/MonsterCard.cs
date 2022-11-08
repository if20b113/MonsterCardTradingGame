namespace MonsterCardTradingGame.Model
{

    public class MonsterCard : ICard, IMonster
    {

        public DamageType DamageType { get; set; }

        public MonsterType MonsterType { get; set; }

        public decimal damage { get; set; }

       public MonsterCard(DamageType damageType, MonsterType monsterType, decimal Damage)
        {
            DamageType = damageType;
            MonsterType = monsterType;
            damage = Damage;
        }





    }
}
