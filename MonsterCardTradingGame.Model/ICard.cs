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

        void DoDamage()
        {
            

        }

    }
}
