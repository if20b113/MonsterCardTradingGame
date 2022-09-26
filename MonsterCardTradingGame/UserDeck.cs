using MonsterCardTradingGame.InterfaceTest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace MonsterCardTradingGame
{
    class UserDeck
    {
        public List<ICard> Deckcards = new List<ICard>();

        public UserDeck()
        {
            Deckcards.Add(new MonsterCard(""));
            Deckcards.Add(new SpellCard(""));

        }




    }
}
