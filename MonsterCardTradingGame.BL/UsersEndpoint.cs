using MonsterCardTradingGame.DAL;
using MonsterCardTradingGame.Model;
using MonsterCardTradingGame.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using HttpMethod = MonsterCardTradingGame.Server.HttpMethod;

namespace MonsterCardTradingGame.BL
{
    public class UsersEndpoint : IHttpEndpoint
    {
        DatabaseHandler UserDBHandler = new DatabaseHandler();
        Authenticator TokenA = new Authenticator();
        public void HandleRequest(HttpRequest rq, HttpResponse rs)
        {
            switch (rq.Method)
            {
                case HttpMethod.POST:
                    if(rq.Path == "/sessions")
                    {
                        LoginUser(rq, rs);
                    }
                    else
                    {
                        CreateUser(rq, rs);
                    }
                    break;
                case HttpMethod.GET:
                    GetUserProfile(rq, rs);
                    break;
                case HttpMethod.PUT:
                    UpdateUserProfile(rq, rs);
                    break;
            }
        }

        private void CreateUser(HttpRequest rq, HttpResponse rs)
        {
            try
            {
               var Player = JsonSerializer.Deserialize<User>(rq.Content);

               // call BL

              bool checkUser =  UserDBHandler.Checkifuserexists(Player.Username);

              if (checkUser)
                {
                    rs.ResponseCode = 409;
                    rs.Content = "User with same username already registered";
                    return;

                }
                else
                {
                    UserSchema newPlayer = new(Player.Username, Player.Password, 20);
                    ProfileShema PlayerProfile = new (Player.Username, "Your Bio here", ":)");
                    StatsSchema PlayerStats = new(Player.Username, 100, 0, 0, 0);


                    UserDBHandler.Regsiteruser(newPlayer);

                    UserDBHandler.CreateUserProfile(PlayerProfile,Player.Username);

                    UserDBHandler.CreateUserStats(Player.Username, PlayerStats);

                    rs.ResponseCode = 201;
                    rs.Content = "User successfully created";
                }  
            }
            catch (Exception)
            {
                rs.ResponseCode = 400;
                rs.Content = "Failed to create User! ";
            }
        }


        private void LoginUser(HttpRequest rq, HttpResponse rs)
        {
            try
            {
                var Player = JsonSerializer.Deserialize<User>(rq.Content);

               // DatabaseHandler UserDBHandler = new DatabaseHandler();

                bool checkUser = UserDBHandler.Checkifuserexists(Player.Username);

                if (!checkUser)
                {
                    rs.ResponseCode = 401;
                    rs.Content = "Invalid username/password provided";
                    return;
                }

                var loginuser = UserDBHandler.Getuser(Player.Username);

               // Console.WriteLine(loginuser.Username);
               // Console.WriteLine(loginuser.Password);

                if(loginuser.Password == Player.Password )
                {

                    UserDBHandler.WriteTokenDB(loginuser.Username,loginuser.Username + "-mtcgToken");

                    rs.ResponseCode = 201;
                    rs.ResponseText = "User login successful";
                    rs.Content = loginuser.Username + "-mtcgToken";
                    rs.ContentType = "application/json";

                }
                else
                {
                    rs.ResponseCode = 401;
                    rs.Content = "Invalid username/password provided";
                }
            }
            catch (Exception)
            {
                rs.ResponseCode = 401;
                rs.Content = "Invalid username/password provided";
            }
        }



        private void GetUserProfile(HttpRequest rq, HttpResponse rs)
        {
            try
            {
                ProfileShema Profile;

                var temp = rq.Path.Split('/');

                string username = temp[2];

                bool checkUser = UserDBHandler.Checkifuserexists(username);

                if (!checkUser)
                {
                    rs.ResponseCode = 404;
                    rs.Content = "User not found.";
                    return;
                }

               string token = TokenA.GetToken(rq.headers);

               if(token == null)
                {
                    rs.ResponseCode = 401;
                    rs.Content = "Access token is missing or invalid";
                    return;

                }

               AuthTokenSchema TokenDB = UserDBHandler.ReadTokenDB(token);

                if (!TokenA.CheckToken(token,TokenDB))
                {
                    rs.ResponseCode = 401;
                    rs.Content = "Access token is missing or invalid";
                    return;

                }


                Profile = UserDBHandler.GetProfile(username);

                if (Profile == null)
                {
                    rs.ResponseCode = 404;
                    rs.ResponseText = "User not found.";
                    return;
                }

                rs.Content = JsonSerializer.Serialize(Profile);
                rs.ContentType = "application/json";
                rs.ResponseCode = 200;
                rs.ResponseText = "Data successfully retrieved";

            }
            catch (Exception)
            {
                rs.ResponseCode = 404;
                rs.Content = "User not found.";
            }
        }


        private void UpdateUserProfile(HttpRequest rq, HttpResponse rs)
        {
            try
            {
                var Profile = JsonSerializer.Deserialize<ProfileShema>(rq.Content);

                var temp = rq.Path.Split('/');

                string username = temp[2];

                bool checkUser = UserDBHandler.Checkifuserexists(username);

                if (!checkUser)
                {
                    rs.ResponseCode = 404;
                    rs.Content = "User not found.";
                    return;
                }

                Authenticator TokenA = new Authenticator();

                string token = TokenA.GetToken(rq.headers);

                if (token == null)
                {
                    rs.ResponseCode = 401;
                    rs.Content = "Access token is missing or invalid";
                    return;

                }

                AuthTokenSchema TokenDB = UserDBHandler.ReadTokenDB(token);

                if (!TokenA.CheckToken(token, TokenDB))
                {
                    rs.ResponseCode = 401;
                    rs.Content = "Access token is missing or invalid";
                    return;

                }

                UserDBHandler.updateProfile(username, Profile);
                UserDBHandler.UpdateStatsName(username,Profile.Name);

                rs.ResponseCode = 200;
                rs.Content = "User sucessfully updated.";

            }
            catch (Exception)
            {
                rs.ResponseCode = 404;
                rs.Content = "User not found.";
            }
        }
    }
}
