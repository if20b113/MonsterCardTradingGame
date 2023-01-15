using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonsterCardTradingGame.DAL
{
    public class ProfileShema
    {
        public string Name { get; set; }

        public string Bio { get; set; }

        public string Image { get; set; }

        public ProfileShema(string Name, string Bio,string Image) { 
        
            this.Name = Name;
            this.Bio = Bio; 
            this.Image = Image;
      
        }   
    }
}
