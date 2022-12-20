using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonsterCardTradingGame.Server
{
    public class HTTPEndpointAttribute : System.Attribute
    {
        public string Path { get; set; }

        public HttpMethod Method { get; set; }

        public HTTPEndpointAttribute(string path, HttpMethod method)
        {
            Path = path;
            Method = method;
        }
    }
}
