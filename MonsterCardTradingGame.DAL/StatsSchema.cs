using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonsterCardTradingGame.DAL
{
    public class StatsSchema
    {
        public string Name { get; set; }

        public float Elo { get; set; }

        public int Wins { get; set; }

        public int Losses { get; set; }

        public int GamesPlayed { get; set; }


        public StatsSchema(string Name,float Elo, int Wins, int Losses, int GamesPlayed) { 
        
            this.Name = Name;
            this.Elo = Elo;
            this.Wins = Wins;
            this.Losses = Losses;
            this.GamesPlayed = GamesPlayed;
        }
    }
}
