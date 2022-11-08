using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonsterCardTradingGame.Model
{
    public interface IDamage
    {
        public decimal DamageAdd(decimal amount, decimal Damage);

        public decimal DamageMultiply(decimal amount, decimal Damage);

        public decimal DamageDivide(decimal amount, decimal Damage);

        public decimal HandleDamageTriangle(DamageType Card1, DamageType Card2, decimal DamageCard1);
    }
        
}
