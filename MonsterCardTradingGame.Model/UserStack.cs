using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonsterCardTradingGame.Model
{
    public class UserStack : ITrading
    {

        public List<ICard> Stackcards = new List<ICard>();

        public UserStack()
        {
            Stackcards.Add(new MonsterCard(""));
            Stackcards.Add(new SpellCard(""));

        }


    }
}
