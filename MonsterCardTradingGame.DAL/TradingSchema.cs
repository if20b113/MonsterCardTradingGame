using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonsterCardTradingGame.DAL
{
    public class TradingSchema
    {
        public string Id { get; set; }
        public string Seller { get; set; }
        public string CardToTrade { get; set; }
        public float MinimumDamage { get; set; }
        public string Type { get; set; }
        public TradingSchema(string Id,string Seller, string CardToTrade, float MinimumDamage, string Type) { 
        
            this.Id = Id;
            this.Seller= Seller;
            this.CardToTrade = CardToTrade;
            this.MinimumDamage = MinimumDamage;
            this.Type = Type;
        
        }
    }
}
