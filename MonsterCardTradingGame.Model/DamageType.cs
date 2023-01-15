using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonsterCardTradingGame.Model
{
    public enum DamageType
    {
        Normal,
        Fire,
        Water
    }


    public  class DamageTypeHandler
    {
      
        public static string DTtoString(DamageType dt)
        {
            var name = dt.ToString();

            return name;    
        }

        public static DamageType? GetDamageType(string type)
        {
            if (Enum.TryParse(type, out DamageType enumType)) return enumType;
            return null;
        }


    }

}

