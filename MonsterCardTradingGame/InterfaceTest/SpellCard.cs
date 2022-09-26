using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace MonsterCardTradingGame.InterfaceTest
{
    class SpellCard : ICard
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
