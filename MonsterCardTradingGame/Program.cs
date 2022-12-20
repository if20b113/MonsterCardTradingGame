// See https://aka.ms/new-console-template for more information
using MonsterCardTradingGame.BL;
using MonsterCardTradingGame.DAL;
using MonsterCardTradingGame.Model;
using MonsterCardTradingGame.Server;
using System.Net;


Console.WriteLine("Simple http-server! http://localhost:10001/");
Console.WriteLine();

var server = new HttpServer(IPAddress.Any, 10001);
server.RegisterEndpoint("/users", new UsersEndpoint());
server.run();






