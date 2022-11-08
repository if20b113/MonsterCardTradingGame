using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonsterCardTradingGame.Model
{
    public interface IUser
    {
        string Username { get; }

        string Password { get; }
        Dictionary<string, object>? BattleResult { get; set; }
        ICard LastCard { get; }
        int CardCount { get; }


        void AddDeck(List<ICard> cards);


        void AddToDeck(ICard CardtoAdd);

        void RemoveCardFromDeck(ICard CardtoRemove);

        ICard GetRandomCard();
    }
}
