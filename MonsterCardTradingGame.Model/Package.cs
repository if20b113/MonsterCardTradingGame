using MonsterCardTradingGame.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonsterCardTradingGame.Model
{
    public class Package
    {

        public List<ICard> PackageCards = new List<ICard>();

        public Package()
        {
            PackageCards.Add(new MonsterCard(""));
            PackageCards.Add(new SpellCard(""));

        }




    }
}
