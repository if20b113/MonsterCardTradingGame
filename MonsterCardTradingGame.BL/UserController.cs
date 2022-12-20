using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonsterCardTradingGame.Model;
using MonsterCardTradingGame.Server;
using Newtonsoft.Json;
using HttpMethod = MonsterCardTradingGame.Server.HttpMethod;


namespace MonsterCardTradingGame.BL
{

    [HTTPEndpoint("/users", HttpMethod.POST)]
    public class RegsiterUser: IEndpointcontroller
    {
        
        public void HandleRequest(HttpRequest req, HttpResponse res)
        {
            if(req.Content == null)
            {

                res.ResponseCode = 400;
                res.ResponseText = "Bad Request";
                return;

            }

           var newUser = JsonConvert.DeserializeObject<User>(req.Content);

            //Database.CreateUser

            Console.WriteLine(newUser?.Username);


            res.Content = "User created";

            res.ResponseCode = 201;

        }
       
    }

    [HTTPEndpoint("/session", HttpMethod.GET)]
    public class LoginUser : IEndpointcontroller
    {

        public void HandleRequest(HttpRequest req, HttpResponse res)
        {
            if (req.Content == null)
            {

                res.ResponseCode = 400;
                res.ResponseText = "Bad Request";
                return;

            }

            var newUser = JsonConvert.DeserializeObject<User>(req.Content);

            //Database.LoginUser

            Console.WriteLine(newUser?.Username);


            res.Content = "Sucessfully Login";

            res.ResponseCode = 201;

        }

    }









}
