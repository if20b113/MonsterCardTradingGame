namespace MonsterCardTradingGame.Model
{
    public class User : IUser
    {
        public string Username { get; }
        public string Password { get; }

        private readonly List<ICard> UserDeck;
        public int CardCount => UserDeck.Count;
        public ICard LastCard { get; private set; } = null!;
        public Dictionary<string, object>? BattleResult { get; set; }
        private readonly Random randomNumber;


        public User(string username,string password, List<ICard> deck)
        {
            Username = username;
            Password = password;
            this.UserDeck = deck;
            randomNumber = new Random();
        }

        public User(string username, string password)
        {
            Username = username;
            Password = password;
            UserDeck = new List<ICard>();
            randomNumber = new Random();
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
