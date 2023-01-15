using System.Runtime.Intrinsics.X86;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using MonsterCardTradingGame.Model;

namespace MonsterCardTradingGame.BL
{
    public class BattleHandler
    {

        IUser Player1;
        IUser Player2;
        int roundtimer = 100;
       
        public BattleHandler(IUser Player1, IUser Player2)
        {
            this.Player1 = Player1;
            this.Player2 = Player2;

        }


        public BatteLog StartBattle()
        {
            BatteLog CurBattelog = new();

            for (int i = 1; i < roundtimer; i++)
            {
                var cardPlayer1 = Player1.GetRandomCard();
                var cardPlayer2 = Player2.GetRandomCard();
                var DamageCard1 = cardPlayer1.CalculateDamage(cardPlayer2);
                var DamageCard2 = cardPlayer2.CalculateDamage(cardPlayer1);


                var cards = $"Round {i}: {Player1.Username} choose {cardPlayer1.Cardname} and {Player2.Username} choose {cardPlayer2.Cardname}";
                CurBattelog.RoundLog.Add(cards);

                if (DamageCard1 > DamageCard2)
                {
                    Player1.AddToDeck(cardPlayer2);
                    Player2.RemoveCardFromDeck(cardPlayer2);

                    if (cardPlayer2.SpecialityCheck.specialitylog != null)
                    {

                        var res = $"BATTLE between {Player1.Username} and {Player2.Username} Round: {i}, {Player1.Username} won with speciality rule: {cardPlayer2.SpecialityCheck.specialitylog}";
                        CurBattelog.RoundLog.Add(res);
                    }
                    else if (cardPlayer1.SpecialityCheck.specialitylog != null)
                    {

                        var res = $"BATTLE between {Player1.Username} and {Player2.Username} Round: {i}, {Player1.Username} won with speciality rule: {cardPlayer1.SpecialityCheck.specialitylog}";
                        CurBattelog.RoundLog.Add(res);
                    }
                    else
                    {
                        var res = $"BATTLE between {Player1.Username} and {Player2.Username} Round: {i}, {Player1.Username} won with {DamageCard1} Damage against {DamageCard2}";
                        CurBattelog.RoundLog.Add(res);

                    }
                }

                else if (DamageCard2 > DamageCard1)
                {
                    Player2.AddToDeck(cardPlayer1);
                    Player1.RemoveCardFromDeck(cardPlayer1);

                    if (cardPlayer1.SpecialityCheck.specialitylog != null)
                    {
                        var res = $"BATTLE between {Player1.Username} and {Player2.Username} Round: {i}, {Player2.Username} won with speciality rule: {cardPlayer1.SpecialityCheck.specialitylog}";
                        CurBattelog.RoundLog.Add(res);
                    }
                    else if (cardPlayer2.SpecialityCheck.specialitylog != null)
                    {
                        var res = $"BATTLE between {Player1.Username} and {Player2.Username} Round: {i}, {Player2.Username} won with speciality rule: {cardPlayer2.SpecialityCheck.specialitylog}";
                        CurBattelog.RoundLog.Add(res);
                    }
                    else
                    {
                        var res = $"BATTLE between {Player1.Username} and {Player2.Username} Round: {i}, {Player2.Username} won with {DamageCard2} Damage against {DamageCard1}";
                        CurBattelog.RoundLog.Add(res);

                    }

                }
                else if (DamageCard2 == DamageCard1)
                {
                    var res = $"BATTLE between {Player1.Username} and {Player2.Username} Round: {i}, DRAW";
                    CurBattelog.RoundLog.Add(res);
                }

                if (Player1.CardCount == 0)
                {
                    CurBattelog.Winner = Player2;
                    CurBattelog.Loser = Player1;
                    var res = $"{Player2.Username} won the Battle in {i} Rounds";
                    CurBattelog.RoundLog.Add(res);
                    return CurBattelog;
                }
                if (Player2.CardCount == 0)
                {
                    CurBattelog.Winner = Player1;
                    CurBattelog.Loser = Player2;
                    var res = $"{Player1.Username} won the Game in {i} Rounds";
                    CurBattelog.RoundLog.Add(res);
                    return CurBattelog;
                }
            }

            var draw = $"GAME ended in a Draw Stats not affected";
            CurBattelog.RoundLog.Add(draw);
            return CurBattelog;

        }
    }
}
