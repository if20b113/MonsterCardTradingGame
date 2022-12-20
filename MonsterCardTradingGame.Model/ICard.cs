namespace MonsterCardTradingGame.Model
{
    public interface ICard
    {

        public DamageType DamageType { get; set; }
        decimal damage { get; set; }
        decimal CalculateDamage(ICard OtherCard)
        {

            var BaseDamage = damage;
            var Damage = new Damage();

            if (this is ISpell || OtherCard is ISpell)
            {
                BaseDamage = Damage.HandleDamageTriangle(DamageType, OtherCard.DamageType, BaseDamage);
            }

            return BaseDamage;
        }
    }
}
