﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MonsterCardTradingGame.DAL
{
    public class HttpServer
    {
        private readonly int port = 8000;
        private readonly IPAddress ipAddress;

        private TcpListener httpListener;

        public HttpServer(IPAddress adr, int port)
        {
            this.ipAddress = adr;
            this.port = port;

            httpListener = new TcpListener(ipAddress, port);
        }

        public void run()
        {
            httpListener.Start();
            while (true)
            {
                Console.WriteLine("Waiting for new client request...");
                var clientSocket = httpListener.AcceptTcpClient();
                var httpProcessor = new HttpProcessor(clientSocket);
                Task.Factory.StartNew(() =>
                {
                    httpProcessor.run();
                });
            }
        }
    }
}
