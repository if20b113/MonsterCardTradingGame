using MonsterCardTradingGame.DAL;
using MonsterCardTradingGame.Model;
using MonsterCardTradingGame.Server;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using HttpMethod = MonsterCardTradingGame.Server.HttpMethod;

namespace MonsterCardTradingGame.BL
{
    public class StoreEndpoint : IHttpEndpoint
    {

        DatabaseHandler StoreDBHandler = new DatabaseHandler();

        Authenticator TokenA = new Authenticator();

        public void HandleRequest(HttpRequest rq, HttpResponse rs)
        {
            switch (rq.Method)
            {
                case HttpMethod.POST:
                    
                    if(rq.Path == "/transactions/packages")
                    {
                        BuyPackage(rq,rs);
                    }
                    else
                    {
                        CreatePackage(rq, rs);
                    }
                    break;
            }
        }

        private void CreatePackage(HttpRequest rq, HttpResponse rs)
        {
            try
            {

                var Package = JsonSerializer.Deserialize<List<CardSchema>>(rq.Content);

                Authenticator TokenA = new Authenticator();

                string token = TokenA.GetToken(rq.headers);

                if(token == null) {

                    rs.ResponseCode = 401;
                    rs.Content = "Access token is missing or invalid";
                    return;

                }

                if (!TokenA.CheckAdminToken(token))
                {
                    rs.ResponseCode = 403;
                    rs.Content = "Provided user is not admin";
                    return;
                }

                foreach(CardSchema card in Package) {

                    if (StoreDBHandler.CheckifCardsexists(card))
                    {
                        rs.ResponseCode = 409;
                        rs.Content = "At least one card in the packages already exists";
                        return;
                    }

                }

                StoreDBHandler.CreatePackage(Package);


                foreach (CardSchema card in Package)
                {

                    StoreDBHandler.CreateCard(card);
             
                }

                rs.ResponseCode = 201;
                rs.Content = "Package and cards successfully created";
            }
            catch (Exception)
            {
                rs.ResponseCode = 400;
                rs.Content = "Access token is missing or invalid";
            }

        }

        private void BuyPackage(HttpRequest rq, HttpResponse rs)
        {

            string token = TokenA.GetToken(rq.headers);

            List<int> packageids = new List<int>();

            List<string> packagecards = new List<string>();

            List<CardSchema> acquiredcards = new List<CardSchema>();

            UserSchema User;

            string username;

            int selectdpackage = 0;

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
            username = TokenDB.username;

            packageids = StoreDBHandler.GetPackagesIDs();

            if(packageids.Count <= 0)
            {

                rs.ResponseCode = 404;
                rs.Content = "No card package available for buying";
                return;

            }

            User = StoreDBHandler.Getuser(username);

            if(User.Coins < 5)
            {
                rs.ResponseCode = 403;
                rs.Content = "Not enough money for buying a card package";
            }

            selectdpackage = SelectRandomPackage(packageids);

            packagecards = StoreDBHandler.GetPackageCards(selectdpackage);

            foreach(string card in packagecards)
            {
              
                var temp = username;
               
                StoreDBHandler.updateCardOwner(card, temp);
                acquiredcards.Add(StoreDBHandler.GetCard(card));
            }

            StoreDBHandler.UpdateUserCoins(username, User.Coins - 5);

            rs.Content = JsonSerializer.Serialize(acquiredcards);
            rs.ContentType = "application/json";
            rs.ResponseCode = 200;
            rs.ResponseText = "A package has been successfully bought";
        }

        private int SelectRandomPackage(List<int> packageids)
        {
            Random R = new Random();

            int somerandomnumber = R.Next(0, packageids.Count);

            return packageids.ElementAt(somerandomnumber);
        }

    }
}


