using MonsterCardTradingGame.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonsterCardTradingGame.BL
{
    public class BattleHandler
    {
        AbstractCard Card1;
        AbstractCard Card2;

        public BattleHandler()
        {

            

        }


        public int Battle(AbstractCard CardPlayer1, AbstractCard CardPlayer2)
        {

            Card1 = CardPlayer1;
            Card2 = CardPlayer2;


            return 0;


        }

        public void BattleRules(AbstractCard CardPlayer1, AbstractCard CardPlayer2)
        {

            switch(CardPlayer1.EType){


            }



        }




    }
}
