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

        public List<AbstractCard> PackageCards = new List<AbstractCard>();

        public Package(AbstractCard Card1)
        {
            PackageCards.Add(Card1);
        }




    }
}
