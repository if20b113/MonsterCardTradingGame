﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonsterCardTradingGame.Server
{
    public interface IHttpEndpoint
    {
        void HandleRequest(HttpRequest rq, HttpResponse rs);
    }
}
