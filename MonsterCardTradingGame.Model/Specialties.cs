using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonsterCardTradingGame.Model
{
    public class Specialties
    {

        public string specialitylog { get; set; }

        public decimal calculateSpecialty(ICard Card1, ICard CardToDamage, decimal BaseDamage)
        {
            if(Card1 is ISpell && CardToDamage is IMonster) 
            { 
                SpellCard TmpSpellCard = (SpellCard)Card1;
                MonsterCard TmpMonsterCard = (MonsterCard)CardToDamage;

                if(TmpMonsterCard.MonsterType == MonsterType.Kraken)
                {
                    specialitylog = "Kraken is immune against spells"; 
                    return 0;
                }
                if(TmpSpellCard.DamageType == DamageType.Water && TmpMonsterCard.MonsterType == MonsterType.Knight)
                {
                    specialitylog = "Knight drowned due to wearing heavy armor";
                    return BaseDamage + 999;
                }

            }
            if(Card1 is IMonster && CardToDamage is IMonster)
            {
                MonsterCard TmpMonsterCard1 = (MonsterCard)Card1;
                MonsterCard TmpMonsterCard2 = (MonsterCard)CardToDamage;

                if(TmpMonsterCard1.MonsterType == MonsterType.Goblin && TmpMonsterCard2.MonsterType == MonsterType.Dragon)
                {
                    specialitylog = "Goblins are to afraid of Dragons to attack";
                    return 0;
                }
                if (TmpMonsterCard1.MonsterType == MonsterType.Ork && TmpMonsterCard2.MonsterType == MonsterType.Wizard)
                {
                    specialitylog = "Wizzard can control Orks so they are not able to damage them";
                    return 0;
                }
                if (TmpMonsterCard1.MonsterType == MonsterType.Dragon && TmpMonsterCard2.MonsterType == MonsterType.Elf && TmpMonsterCard2.DamageType == DamageType.Fire)
                {
                    specialitylog = "Fire elves evade Dragon attacks";
                    return 0;
                }

            }
            return BaseDamage;
        }
    }
}
