using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MonsterCardTradingGame.Server
{
    public class HttpProcessor
    {
        private TcpClient clientSocket;
        private HttpServer httpServer;
        
        public HttpProcessor(HttpServer httpServer, TcpClient clientSocket)
        {
            this.httpServer = httpServer;
            this.clientSocket = clientSocket;
        }

        public void run()
        {
            var reader = new StreamReader(clientSocket.GetStream());
            var request = new HttpRequest(reader);
            request.Parse();
            var writer = new StreamWriter(clientSocket.GetStream()) { AutoFlush = true };
            var response = new HttpResponse(writer);

            IHttpEndpoint endpoint;
            endpoint = GetEndpoint(request);

            if (endpoint != null)
            {
                endpoint.HandleRequest(request, response);
            }
            else
            {
                //Thread.Sleep(10000);
                response.ResponseCode = 404;
                response.ResponseText = "Not Found";
                response.Content = "<html><body>Not found!</body></html>";
                response.Headers.Add("Content-Type", "text/html"); // application/json
            }
            response.Process();
        }

        public IHttpEndpoint GetEndpoint(HttpRequest request)
        {
            IHttpEndpoint endpoint;
            httpServer.Endpoints.TryGetValue(request.Path, out endpoint!);


            if (endpoint == null)
            {
               var splitPath = request.Path.Split('/');

               // Console.WriteLine(splitPath);
               // Console.WriteLine(splitPath[1]);

                if (splitPath[1].Contains("users"))
                {
                    httpServer.Endpoints.TryGetValue("/users/*", out endpoint!);
                    request.SpecialEndpoint = "/users/*";
                }
                if (splitPath[1].Contains("tradings"))
                {
                    httpServer.Endpoints.TryGetValue("/tradings/*", out endpoint!);
                    request.SpecialEndpoint = "/tradings/*";
                }

                //Console.WriteLine(splitPath[2]);
            }

            return endpoint;
        }
    }
}
