using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonsterCardTradingGame.Model
{
    public class BatteLog
    {
        public IUser Player1 { get; set; }
        public IUser Player2 { get; set; }
        public IUser Winner { get; set; }

        public IUser Loser { get; set; }

        public bool IsDraw { get; set; }

        public List<string>RoundLog = new List<string>();



    }
}
