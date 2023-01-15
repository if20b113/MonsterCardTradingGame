// See https://aka.ms/new-console-template for more information
using MonsterCardTradingGame.BL;
using MonsterCardTradingGame.DAL;
using MonsterCardTradingGame.Model;
using MonsterCardTradingGame.Server;
using System.Net;


Console.WriteLine("MCTG-server! http://localhost:10001/");
Console.WriteLine();

var server = new HttpServer(IPAddress.Any, 10001);
server.RegisterEndpoint("/users", new UsersEndpoint());
server.RegisterEndpoint("/sessions", new UsersEndpoint());
server.RegisterEndpoint("/users/*", new UsersEndpoint());
server.RegisterEndpoint("/packages", new StoreEndpoint());
server.RegisterEndpoint("/transactions/packages", new StoreEndpoint());
server.RegisterEndpoint("/cards", new CardsEndpoint());
server.RegisterEndpoint("/deck", new CardsEndpoint());
server.RegisterEndpoint("/tradings", new TradingEndpoint());
server.RegisterEndpoint("/tradings/*", new TradingEndpoint());
server.RegisterEndpoint("/stats", new GameEndpoint());
server.RegisterEndpoint("/score", new GameEndpoint());
server.RegisterEndpoint("/battles", new GameEndpoint());
server.run();



