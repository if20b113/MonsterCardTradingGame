using MonsterCardTradingGame.DAL;
using MonsterCardTradingGame.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonsterCardTradingGame.BL
{
    internal class UserHandler
    {
        public void registerUser(User newUser)
        {
            Server GameServer = new Server();
            GameServer.registerUserDB(newUser);
        }
    }
}
