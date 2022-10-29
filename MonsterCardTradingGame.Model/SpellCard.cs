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
    public class SpellCard : AbstractCard
    {
        public SpellCard(string name, int damage ,int type) : base(name, damage, type)
        {

        }
    }
}
