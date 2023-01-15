using MonsterCardTradingGame.DAL;
using MonsterCardTradingGame.Model;
using MonsterCardTradingGame.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml;
using HttpMethod = MonsterCardTradingGame.Server.HttpMethod;

namespace MonsterCardTradingGame.BL
{
    public class CardsEndpoint : IHttpEndpoint
    {
        public void HandleRequest(HttpRequest rq, HttpResponse rs)
        {
            switch (rq.Method)
            {
                case HttpMethod.GET:
                    if (rq.Path == "/cards")
                    {
                        GetAllUserCards(rq, rs);
                    }
                    else
                    {
                        GetCurrentActiveDeck(rq, rs);
                    }
                    break;
                case HttpMethod.PUT:
                    ConfigureDeck(rq, rs);
                    break;
            }
        }

        public void GetAllUserCards(HttpRequest rq, HttpResponse rs)
        {

            DatabaseHandler CardsDBHandler = new DatabaseHandler();

            Authenticator TokenA = new Authenticator();

            string token = TokenA.GetToken(rq.headers);

            List<CardSchema> UserCard = new();

            if (token == null)
            {

                rs.ResponseCode = 401;
                rs.Content = "Access token is missing or invalid";
                return;

            }

            AuthTokenSchema TokenDB = CardsDBHandler.ReadTokenDB(token);

            if (!TokenA.CheckToken(token, TokenDB))
            {
                rs.ResponseCode = 401;
                rs.Content = "Access token is missing or invalid";
                return;

            }

            string username = TokenDB.username;


            UserCard = CardsDBHandler.GetAllUserCards(username);

            if(UserCard.Count <= 0) {

                rs.ResponseCode = 204;
                rs.Content = "The request was fine, but the user doesn't have any cards";
                return;

            }

            rs.Content = JsonSerializer.Serialize(UserCard);
            rs.ContentType = "application/json";
            rs.ResponseCode = 200;
            rs.ResponseText = "The user has cards, the response contains these";

        }

        private void ConfigureDeck(HttpRequest rq, HttpResponse rs)
        {
            DatabaseHandler CardsDBHandler = new DatabaseHandler();

            Authenticator TokenA = new Authenticator();

            var DeckCardIds = JsonSerializer.Deserialize<List<string>>(rq.Content);

            string token = TokenA.GetToken(rq.headers);

            if (token == null)
            {
                rs.ResponseCode = 401;
                rs.Content = "Access token is missing or invalid";
                return;
            }

            AuthTokenSchema TokenDB = CardsDBHandler.ReadTokenDB(token);

            if (!TokenA.CheckToken(token, TokenDB))
            {
                rs.ResponseCode = 401;
                rs.Content = "Access token is missing or invalid";
                return;

            }

            if(DeckCardIds.Count < 4)
            {
                rs.ResponseCode = 400;
                rs.Content = "The provided deck did not include the required amount of cards";
                return;
            }

            string username = TokenDB.username;

            foreach(string id in DeckCardIds)
            {
               if( CardsDBHandler.CheckCardAvailability(id, username))
                {
                    rs.ResponseCode = 403;
                    rs.Content = "At least one of the provided cards does not belong to the user or is not available.";
                    return;

                }
            }

            CardsDBHandler.SetDeckinactive(username);

            CardsDBHandler.WriteDeckDB(DeckCardIds, username);

            foreach (string id in DeckCardIds)
            {
                CardsDBHandler.lockcard(id);
            }

            rs.ResponseCode = 200;
            rs.Content = "The deck has been successfully configured";

        }

        private void GetCurrentActiveDeck(HttpRequest rq, HttpResponse rs)
        {
            DatabaseHandler CardsDBHandler = new DatabaseHandler();

            Authenticator TokenA = new Authenticator();

            string token = TokenA.GetToken(rq.headers);

            List<CardSchema> CardSchemas = new List<CardSchema>();

            List <string> DeckCardIDs = new List <string>();

            if (token == null)
            {
                rs.ResponseCode = 401;
                rs.Content = "Access token is missing or invalid";
                return;
            }

            AuthTokenSchema TokenDB = CardsDBHandler.ReadTokenDB(token);

            if (!TokenA.CheckToken(token, TokenDB))
            {
                rs.ResponseCode = 401;
                rs.Content = "Access token is missing or invalid";
                return;
            }

            string username = TokenDB.username;

            DeckCardIDs = CardsDBHandler.GetDeckCardIds(username);

            if(DeckCardIDs == null) {

                rs.ResponseCode = 204;
                rs.Content = "The request was fine, but the deck doesn't have any cards";
                return;

            }

            foreach(string id in DeckCardIDs)
            {
                CardSchemas.Add(CardsDBHandler.GetCard(id));
            }

            rs.Content = JsonSerializer.Serialize(CardSchemas);
            rs.ContentType = "application/json";
            rs.ResponseCode = 204;
            rs.ResponseText = "The deck has cards, the response contains these";
            return;


        }
    }
}
