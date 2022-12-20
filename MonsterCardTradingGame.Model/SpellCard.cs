namespace MonsterCardTradingGame.Model
{
    public class SpellCard : ICard, ISpell
    {

        public DamageType DamageType { get; set; }

        public decimal damage { get; set; }


        public SpellCard(DamageType damageType, decimal damage)
        {
            DamageType = damageType;
            this.damage = damage;
        }

        public string DamageToString()
        {
            return $"{DamageType.ToString()} Spell";
        }



    }
}
