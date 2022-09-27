using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using static MonsterCardTradingGame.Model.ICard;

namespace MonsterCardTradingGame.Model
{



    public class MonsterCard : ICard
    {
       
        
        string name = "";

       ICard.ElementType Etype;

        public MonsterCard(string cardname)
        {
            const int damage = 0;

            name = cardname;
        }
    }
}
