using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonsterCardTradingGame.DAL
{
    public class UserSchema
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public int Coins { get; set; }


        public UserSchema(string username, string password, int coins)
        {
            Username = username;
            Password = password;
            Coins = coins;
        }
    }
}
