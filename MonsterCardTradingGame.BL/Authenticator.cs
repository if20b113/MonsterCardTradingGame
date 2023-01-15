using MonsterCardTradingGame.DAL;
using MonsterCardTradingGame.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonsterCardTradingGame.BL
{
    public class Authenticator
    {

        public string GetToken(Dictionary<string, string> headers)
        {
            string token = string.Empty;

            string[] temp;

            if(headers.TryGetValue("Authorization", out token))
            {
                Console.WriteLine(token);

                temp = token.Split(' ');

                Console.WriteLine(temp[2]);

                token = temp[2];

                return token;
            }
            else
            {

                return token;

            }
           
        }

        public bool CheckToken(string token, AuthTokenSchema DBToken)
        {
            if ((token == DBToken.token) || (token == "admin-mtcgToken"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool CheckAdminToken(string token)
        {
            if (token == "admin-mtcgToken")
            {
                return true;
            }
            else
            {
                return false;
            }
        }



    }
}
