namespace MonsterCardTradingGame.Model
{
    public class SpellCard : ICard, ISpell
    {

        public DamageType DamageType { get; set; }

        public decimal damage { get; set; }

        public string Cardname { get; set; }

        public Specialties SpecialityCheck { get; set; }


        public SpellCard(DamageType damageType, decimal damage)
        {
            DamageType = damageType;
            this.damage = damage;

            if (damageType == DamageType.Normal)
            {
                Cardname =  "RegularSpell";
            }
            else
            {
               Cardname = damageType.ToString() + "Spell";
            }
           
            SpecialityCheck = new();
        }
    }
}
