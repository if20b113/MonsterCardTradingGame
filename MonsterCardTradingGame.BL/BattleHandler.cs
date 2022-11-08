using System.Security.Cryptography.X509Certificates;
using MonsterCardTradingGame.Model;

namespace MonsterCardTradingGame.BL
{
    public class BattleHandler
    {

        IUser Player1;
        IUser Player2;
        int roundtimer = 100;
        IUser? winner;
        IUser? loser;

        public BattleHandler(IUser Player1, IUser Player2)
        {
            this.Player1 = Player1;
            this.Player2 = Player2;

        }


        public void StartBattle()
        {
            for(int i = 0; i < roundtimer; i++)
            {
                var cardPlayer1 = Player1.GetRandomCard();
                var cardPlayer2 = Player2.GetRandomCard();  
                var DamageCard1 = cardPlayer1.CalculateDamage(cardPlayer2);
                var DamageCard2 = cardPlayer2.CalculateDamage(cardPlayer1);

              if(DamageCard1 > DamageCard2)
                {
                    Player1.AddToDeck(cardPlayer2);
                    Player2.RemoveCardFromDeck(cardPlayer2);
                }

              else if(DamageCard2 > DamageCard1)
               {
                    Player2.AddToDeck(cardPlayer1);
                    Player1.RemoveCardFromDeck(cardPlayer1);
                }

              if(Player1.CardCount == 0)
                {
                    winner = Player2;
                    loser = Player1;
                }

                if (Player2.CardCount == 0)
                {
                    winner = Player1;
                    loser = Player2;
                }
            }
        }




    }
}
