using MonsterCardTradingGame.DAL;
using MonsterCardTradingGame.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using HttpMethod = MonsterCardTradingGame.Server.HttpMethod;

namespace MonsterCardTradingGame.BL
{
    public class GameEndpoint : IHttpEndpoint
    {
        DatabaseHandler GameDBHandler = new DatabaseHandler();
        Authenticator TokenA = new Authenticator();

        private readonly GameHandler game = null!;
        public void HandleRequest(HttpRequest rq, HttpResponse rs)
        {
            switch (rq.Method)
            {
                case HttpMethod.POST:
                    QueueForBAttle(rq, rs);
                    break;
                case HttpMethod.GET:
                    if(rq.Path == "/stats")
                    {
                        GetUserStats(rq,rs);
                    }
                    if(rq.Path == "/scoreboard")
                    {
                        GetScoreboard(rq,rs);
                    }
                    break;
            }
        }

        private void GetUserStats(HttpRequest rq, HttpResponse rs)
        {
            
            string token = TokenA.GetToken(rq.headers);

            StatsSchema PlayerStats = null;

            if (token == null)
            {
                rs.ResponseCode = 401;
                rs.Content = "Access token is missing or invalid";
                return;
            }

            AuthTokenSchema TokenDB = GameDBHandler.ReadTokenDB(token);

            if (!TokenA.CheckToken(token, TokenDB))
            {
                rs.ResponseCode = 401;
                rs.Content = "Access token is missing or invalid";
                return;

            }
            string username = TokenDB.username;

            PlayerStats = GameDBHandler.GetPlayerStats(username);

            rs.Content = JsonSerializer.Serialize(PlayerStats);
            rs.ContentType = "application/json";
            rs.ResponseCode = 200;
            rs.ResponseText = "The stats could be retrieved successfully.";
        }

        private void GetScoreboard(HttpRequest rq, HttpResponse rs)
        {
            string token = TokenA.GetToken(rq.headers);

            List<StatsSchema> PlayerStats = null;

            if (token == null)
            {
                rs.ResponseCode = 401;
                rs.Content = "Access token is missing or invalid";
                return;
            }

            AuthTokenSchema TokenDB = GameDBHandler.ReadTokenDB(token);

            if (!TokenA.CheckToken(token, TokenDB))
            {
                rs.ResponseCode = 401;
                rs.Content = "Access token is missing or invalid";
                return;

            }

            PlayerStats = GameDBHandler.GetScoreboard();

            rs.Content = JsonSerializer.Serialize(PlayerStats);
            rs.ContentType = "application/json";
            rs.ResponseCode = 200;
            rs.ResponseText = "The scoreboard could be retrived successfully";
        }

        private void QueueForBAttle(HttpRequest rq, HttpResponse rs)
        {
           
            string token = TokenA.GetToken(rq.headers);


            if (token == null)
            {
                rs.ResponseCode = 401;
                rs.Content = "Access token is missing or invalid";
                return;
            }

            AuthTokenSchema TokenDB = GameDBHandler.ReadTokenDB(token);

            if (!TokenA.CheckToken(token, TokenDB))
            {
                rs.ResponseCode = 401;
                rs.Content = "Access token is missing or invalid";
                return;

            }


            //var result = game.Play();
        }
    }
}
