using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonsterCardTradingGame.DAL
{
    public class AuthTokenSchema
    {
        public string username { get; set; }   
        public string token { get; set; }
        public DateTime timestamp { get; set; }


        public AuthTokenSchema(string username, string token, DateTime timestamp)
        {
            this.username = username;
            this.token = token;
            this.timestamp = timestamp;
        }



    }
}
