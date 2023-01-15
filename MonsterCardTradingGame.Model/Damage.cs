using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonsterCardTradingGame.Model
{
   public class Damage: IDamage
    {

        public decimal DamageValue { get; set; }

        public decimal DamageAdd(decimal amount ,decimal Damage)
        {
            DamageValue = Damage + amount;

            return DamageValue;
        }

        public decimal DamageMultiply(decimal amount, decimal Damage)
        {
            DamageValue = Damage * amount;

            return DamageValue;
        }

        public decimal DamageDivide(decimal amount, decimal Damage)
        {
            DamageValue = Damage / amount;

            return DamageValue;

        }


        public decimal HandleDamageTriangle(DamageType Type1, DamageType Type2, decimal DamageCard1)
        {
            var BaseDamage = DamageCard1;

            if(Type1 == Type2)
            {
               return BaseDamage;
            }

            if(Type1 == DamageType.Water)
            {
                if (Type2 == DamageType.Fire)
                {
                    BaseDamage = DamageMultiply(2, BaseDamage);

                }
                else
                {
                    BaseDamage = DamageDivide(2, BaseDamage);
                }
            }
            else if (Type1 == DamageType.Fire)
            {
                if (Type2 == DamageType.Normal)
                {
                    BaseDamage = DamageMultiply(2, BaseDamage);
                }
                else
                {
                    BaseDamage = DamageDivide(2, BaseDamage);
                }
            }
            else
            {
                if (Type2 == DamageType.Water)
                {
                    BaseDamage = DamageMultiply(2, BaseDamage);
                }
                else
                {
                    BaseDamage = DamageDivide(2, BaseDamage);
                }
            }
            return BaseDamage;
        }
    }
}
