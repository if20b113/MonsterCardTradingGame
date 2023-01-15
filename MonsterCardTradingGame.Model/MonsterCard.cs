namespace MonsterCardTradingGame.Model
{

    public class MonsterCard : ICard, IMonster
    {

        public DamageType DamageType { get; set; }

        public MonsterType MonsterType { get; set; }

        public Specialties SpecialityCheck { get; set; }

        public string Cardname { get; set; }   
        public decimal damage { get; set; }

       public MonsterCard(DamageType damageType, MonsterType monsterType, decimal Damage , string Cardname)
        {
            DamageType = damageType;
            MonsterType = monsterType;
            damage = Damage;
            this.Cardname = Cardname;

            SpecialityCheck = new();
        }
    }
}
