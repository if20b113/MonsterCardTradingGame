using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonsterCardTradingGame.DAL
{
    public class CardSchema
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public float Damage { get; set; }


        public CardSchema(string Id, string Name , float Damage) { 
        
            this.Id = Id;
            this.Name = Name;
            this.Damage = Damage;
        }
    }
}
