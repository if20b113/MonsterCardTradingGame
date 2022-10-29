using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace MonsterCardTradingGame.Model
{

    public interface ICard
    {

        enum ElementType
        {
            fire,water,normal
        }


        ElementType EType { get; }
        string name { get; }
        int damage { get; }

       public int DoDamage()
        {
            return 0;
        }

    }
}
