namespace MonsterCardTradingGame.Model
{
    public interface ICard
    {

        public DamageType DamageType { get; set; }
        decimal damage { get; set; }
        public Specialties SpecialityCheck { get; set; }
        public string Cardname { get; set; }

        decimal CalculateDamage(ICard OtherCard)
        {

            var BaseDamage = damage;
            var Damage = new Damage();
   
            if (this is ISpell || OtherCard is ISpell)
            {
                BaseDamage = Damage.HandleDamageTriangle(DamageType, OtherCard.DamageType, BaseDamage);
               
            }
            BaseDamage = SpecialityCheck.calculateSpecialty(this, OtherCard, BaseDamage);

            return BaseDamage;
        }
    }
}
