using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using static MonsterCardTradingGame.Model.ICard;

namespace MonsterCardTradingGame.Model
{
    public abstract class AbstractCard : ICard
    {
        public string name { get; }
        public int damage { get; }
        public ElementType EType { get; }



        public AbstractCard(string name, int damage, int type)
        {
            this.EType = (ElementType)type;
            this.name = name;
            this.damage = damage;
        }


        public int DoDamage(AbstractCard Card1,AbstractCard Card2)
        {

            return (Card1.damage > Card2.damage) ? 1 : 2; 

        }



    }
}
