using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonsterCardTradingGame.Model
{
    public enum MonsterType
    {
        Goblin,
        Troll,
        Dragon,
        Wizard,
        Ork,
        Knight,
        Kraken,
        Elf,
    }

    public static class MonsterTypeMethods
    {
       public static MonsterType? GetType(string type)
        {
            if (Enum.TryParse(type, out MonsterType enumType)) return enumType;
            return null;
        }
    }
}
