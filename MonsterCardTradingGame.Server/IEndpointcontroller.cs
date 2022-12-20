using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonsterCardTradingGame.Server
{
    public interface IEndpointcontroller
    {
         void HandleRequest(HttpRequest req, HttpResponse rep);
    }
}
