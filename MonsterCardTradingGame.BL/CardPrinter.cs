using MonsterCardTradingGame.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonsterCardTradingGame.BL
{
    public class CardPrinter
    {
        DamageType newDamage;
        MonsterType newMonster;
        public ICard? PrintCard(string fullCardName, float damage)
       {
            ICard PrintedCard = null;
      

            if (fullCardName.EndsWith("Spell"))
            {
                var temp = fullCardName.Split("Spell");

                string Element = temp[0];

                Enum.TryParse(Element, out newDamage);

                PrintedCard = new SpellCard(newDamage, (decimal)damage);
            }
            else
            {


                PrintedCard = PrintMonsterCard(fullCardName, damage);

            }

            return PrintedCard;
       }

        public ICard PrintMonsterCard(string fullCardName, float damage)
        {
            ICard PrintedMonsterCard = null;
          
            var element = GetElement(fullCardName);
            //PrintedCard = new MonsterCard(newDamage, newMonster, (decimal)damage);

            if(element != null)
            {
                var MonsterType = SplitCardName(fullCardName);

                if(MonsterType == null)
                {
                    return null;

                }

                Enum.TryParse(MonsterType, out newMonster);

                Enum.TryParse(element, out newDamage);

                PrintedMonsterCard = new MonsterCard(newDamage, newMonster, (decimal)damage , fullCardName);

                return PrintedMonsterCard;
            }
            else
            {
                Enum.TryParse(fullCardName, out newMonster);

                switch (newMonster)
                {
                    case MonsterType.Dragon:
                        newDamage = DamageType.Fire;
                        break;
                    case MonsterType.Wizard:
                        newDamage = DamageType.Normal;
                        break;
                    case MonsterType.Ork:
                        newDamage = DamageType.Normal;
                        break;
                    case MonsterType.Knight:
                        newDamage = DamageType.Normal;
                        break;
                    case MonsterType.Kraken:
                        newDamage = DamageType.Water;
                        break;
                }

                PrintedMonsterCard = new MonsterCard(newDamage, newMonster, (decimal)damage, fullCardName);

                return PrintedMonsterCard;
            }
        }


        public string GetElement(string fullCardName)
        {
            if (fullCardName.StartsWith("Fire"))
            {

                return "Fire";

            }
            else if (fullCardName.StartsWith("Water"))
            {

                return "Water";

            }
            else if (fullCardName.StartsWith("Regular"))
            {

                return "Normal";

            }

            return null;
          
        }

        public string SplitCardName(string fullCardName)
        {
            if (fullCardName.StartsWith("Fire"))
            {
                var temp = fullCardName.Split("Fire");

                string MonsterType = temp[1];

                return MonsterType;

            }
            else if (fullCardName.StartsWith("Water"))
            {

                var temp = fullCardName.Split("Water");

                string MonsterType = temp[1];

                return MonsterType;

            }
            else if (fullCardName.StartsWith("Regular"))
            {

                var temp = fullCardName.Split("Regular");

                string MonsterType = temp[1];

                return MonsterType;

            }

            return null;
        }

    }
}
