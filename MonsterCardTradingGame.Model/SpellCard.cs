using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using static MonsterCardTradingGame.Model.ICard;

namespace MonsterCardTradingGame.Model
{
    public class SpellCard : ICard
    {
        
        string name = "";

        ICard.ElementType Etype;

        public SpellCard(string cardname)
        {
            const int damage = 0;

            name = cardname;
        }
    }
}
