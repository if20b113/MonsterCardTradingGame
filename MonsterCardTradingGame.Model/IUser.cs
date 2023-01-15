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
        ICard LastCard { get; }
        int CardCount { get; }

        BatteLog PlayerBattleLog { get; set; }

        const int coins = 20;


        void AddDeck(List<ICard> cards);


        void AddToDeck(ICard CardtoAdd);

        void RemoveCardFromDeck(ICard CardtoRemove);

        ICard GetRandomCard();
    }
}
