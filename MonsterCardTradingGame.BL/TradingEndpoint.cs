using MonsterCardTradingGame.DAL;
using MonsterCardTradingGame.Model;
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
    public class TradingEndpoint : IHttpEndpoint
    {
        DatabaseHandler StoreDBHandler = new DatabaseHandler();
        Authenticator TokenA = new Authenticator();
        public void HandleRequest(HttpRequest rq, HttpResponse rs)
        {
            switch (rq.Method)
            {
                case HttpMethod.GET:
                    GetAllTradeDeals(rq, rs);
                    break;
                case HttpMethod.POST:
                    if (rq.SpecialEndpoint == "/tradings/*")
                    {
                        CarryOutTrade(rq, rs);
                    }
                    else
                    {
                        CreateTrade(rq, rs);
                    }

                    break;
                case HttpMethod.DELETE:
                    DeleteTradeDeal(rq, rs);
                    break;
            }
        }

        public void GetAllTradeDeals(HttpRequest rq, HttpResponse rs)
        {
   
            string token = TokenA.GetToken(rq.headers);

            List <TradingSchema> AllTrades = new List <TradingSchema>();

            if (token == null)
            {
                rs.ResponseCode = 401;
                rs.ResponseText = "Access token is missing or invalid";
                return;
            }

            AuthTokenSchema TokenDB = StoreDBHandler.ReadTokenDB(token);

            if (!TokenA.CheckToken(token, TokenDB))
            {
                rs.ResponseCode = 401;
                rs.ResponseText = "Access token is missing or invalid";
                return;

            }
            string username = TokenDB.username;

            AllTrades = StoreDBHandler.GetAllTrades();

            if(AllTrades.Count <= 0)
            {
                rs.ResponseCode = 204;
                rs.ResponseText = "The request was fine, but there are no trading deals available";
                return;

            }

            rs.Content = JsonSerializer.Serialize(AllTrades);
            rs.ContentType = "application/json";
            rs.ResponseCode = 200;
            rs.ResponseText = "There are trading deals available, the response contains these";
            return;

        }

        public void CreateTrade(HttpRequest rq, HttpResponse rs)
        {
            var NewTrade = JsonSerializer.Deserialize<TradingSchema>(rq.Content);

            string token = TokenA.GetToken(rq.headers);

            if (token == null)
            {
                rs.ResponseCode = 401;
                rs.ResponseText = "Access token is missing or invalid";
                return;
            }

            AuthTokenSchema TokenDB = StoreDBHandler.ReadTokenDB(token);

            if (!TokenA.CheckToken(token, TokenDB))
            {
                rs.ResponseCode = 401;
                rs.ResponseText = "Access token is missing or invalid";
                return;

            }
            string username = TokenDB.username;

            if (!StoreDBHandler.CheckTradeexists(NewTrade.Id))
            {
                rs.ResponseCode = 409;
                rs.ResponseText = "A deal with this deal ID already exists.";
                return;
            }

            if(StoreDBHandler.CheckCardAvailability(NewTrade.CardToTrade,username))
            {
                rs.ResponseCode = 403;
                rs.ResponseText = "The deal contains a card that is not owned by the user or locked in the deck.";
                return;
            }

            StoreDBHandler.CreateTradeDeal(NewTrade,username);

            StoreDBHandler.lockcard(NewTrade.CardToTrade);

            rs.ResponseCode = 201;
            rs.ResponseText = "Trading deal successfully created";
            return;
        }

        public void DeleteTradeDeal(HttpRequest rq, HttpResponse rs)
        {
            string token = TokenA.GetToken(rq.headers);

            var temp = rq.Path.Split('/');

            string tradeid = temp[2];

            TradingSchema TradeToDelete = null;

            if (token == null)
            {
                rs.ResponseCode = 401;
                rs.ResponseText = "Access token is missing or invalid";
                return;
            }

            AuthTokenSchema TokenDB = StoreDBHandler.ReadTokenDB(token);

            if (!TokenA.CheckToken(token, TokenDB))
            {
                rs.ResponseCode = 401;
                rs.ResponseText = "Access token is missing or invalid";
                return;

            }
            string username = TokenDB.username;

            if (StoreDBHandler.CheckTradeexists(tradeid))
            {
                rs.ResponseCode = 404;
                rs.ResponseText = "The provided deal ID was not found.";
                return;
            }

            TradeToDelete = StoreDBHandler.GetTrade(tradeid);

            if (TradeToDelete.Seller != username)
            {
                rs.ResponseCode = 410;
                rs.ResponseText = "Trade can only be deleted by owner";
                return;
            }

            StoreDBHandler.deleteTrade(tradeid);

            StoreDBHandler.unlockcard(TradeToDelete.CardToTrade);

            rs.ResponseCode = 200;
            rs.ResponseText = "Trading deal successfully deleted";
            return;

        }

        public void CarryOutTrade(HttpRequest rq, HttpResponse rs)
        {
           
            var NewTradecard = JsonSerializer.Deserialize<string>(rq.Content);

            string token = TokenA.GetToken(rq.headers);

            TradingSchema Tradetoexec = null;

            var temp = rq.Path.Split('/');

            string tradeid = temp[2];

            if (token == null)
            {
                rs.ResponseCode = 401;
                rs.Content = "Access token is missing or invalid";
                return;
            }

            AuthTokenSchema TokenDB = StoreDBHandler.ReadTokenDB(token);

            if (!TokenA.CheckToken(token, TokenDB))
            {
                rs.ResponseCode = 401;
                rs.Content = "Access token is missing or invalid";
                return;

            }
            string username = TokenDB.username;

            if (StoreDBHandler.CheckTradeexists(tradeid))
            {
                rs.ResponseCode = 404;
                rs.ResponseText = "The provided deal ID was not found.";
                return;
            }

            Tradetoexec = StoreDBHandler.GetTrade(tradeid);

            if(Tradetoexec.Seller == username)
            {
                rs.ResponseCode = 402;
                rs.ResponseText = "Cannot trade with self";
                return;

            }

            if (StoreDBHandler.CheckCardAvailability(NewTradecard, username))
            {
                rs.ResponseCode = 403;
                rs.ResponseText = "The offered card is not owned by the user, or the requirements are not met (Type, MinimumDamage), or the offered card is locked in the deck.";
                return;
            }

           CardSchema CardToTrade =  StoreDBHandler.GetCard(NewTradecard);

           CardPrinter Printer = new CardPrinter();

           ICard CardTrade = Printer.PrintCard(CardToTrade.Name,CardToTrade.Damage);

           if(CardToTrade.Damage < Tradetoexec.MinimumDamage)
            {
                rs.ResponseCode = 403;
                rs.ResponseText = "The offered card is not owned by the user, or the requirements are not met (Type, MinimumDamage), or the offered card is locked in the deck.";
                return;
            } 
            
           if(Tradetoexec.Type.Contains("monster") && CardTrade is not IMonster)
            {
                rs.ResponseCode = 403;
                rs.ResponseText = "The offered card is not owned by the user, or the requirements are not met (Type, MinimumDamage), or the offered card is locked in the deck.";
                return;
            }

            if (Tradetoexec.Type.Contains("spell") && CardTrade is not ISpell)
            {
                rs.ResponseCode = 403;
                rs.ResponseText = "The offered card is not owned by the user, or the requirements are not met (Type, MinimumDamage), or the offered card is locked in the deck.";
                return;
            }

            StoreDBHandler.updateCardOwner(Tradetoexec.CardToTrade, username);

            StoreDBHandler.updateCardOwner(CardToTrade.Id, Tradetoexec.Seller);

            StoreDBHandler.unlockcard(CardToTrade.Id);

            StoreDBHandler.unlockcard(Tradetoexec.CardToTrade);

            StoreDBHandler.deleteTrade(Tradetoexec.Id);

            rs.ResponseCode = 200;
            rs.ResponseText = "Trading deal successfully executed.";
            return;
        }
    }
}
