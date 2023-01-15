using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonsterCardTradingGame.DAL
{
    public class PackageSchema
    {
        public List<CardSchema> Card { get; set; }

        public PackageSchema(List<CardSchema> Card) { 
        
           this.Card = Card;
        }
    }
}
