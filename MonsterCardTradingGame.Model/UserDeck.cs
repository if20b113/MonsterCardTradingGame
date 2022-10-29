using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;


namespace MonsterCardTradingGame.Model
{
    public class UserDeck
    {
        public List<AbstractCard> Deckcards = new List<AbstractCard>();

        public UserDeck(AbstractCard Card1)
        {
            Deckcards.Add(Card1);
           // Deckcards.Add(new SpellCard(""));

        }




    }
}
