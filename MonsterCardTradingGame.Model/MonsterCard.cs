using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using static MonsterCardTradingGame.Model.ICard;

namespace MonsterCardTradingGame.Model
{



    public class MonsterCard : AbstractCard
    {

        public MonsterCard(string name, int damage, int type) : base(name, damage,type)
        {



        }

    }
}
