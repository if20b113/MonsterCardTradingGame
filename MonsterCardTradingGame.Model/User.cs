namespace MonsterCardTradingGame.Model
{
    public class User : IUser
    {
        public string Username { get; set; }
        public string Password { get; set; }

        private readonly List<ICard> UserDeck;
        public int CardCount => UserDeck.Count;
        public ICard LastCard { get; private set; } = null!;
        public int Coins { get; set; }

        public BatteLog PlayerBattleLog { get; set; }

        private readonly Random randomNumber;


        public User(string username,string password)
        {
            Username = username;
            Password = password;
            randomNumber = new Random();
            UserDeck = new List<ICard>();
            PlayerBattleLog = new BatteLog();
        }

        public void AddDeck(List<ICard> cards)
        {
            UserDeck.AddRange(cards);
        }

        public void AddToDeck(ICard CardtoAdd)
        {
            UserDeck.Add(CardtoAdd);
        }

        public void RemoveCardFromDeck(ICard CardtoRemove)
        {
            UserDeck.Remove(CardtoRemove);
        }


        public ICard GetRandomCard()
        {
            var index = randomNumber.Next(UserDeck.Count);
            LastCard = UserDeck[index];
            return LastCard;
        }


    }
}
