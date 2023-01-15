using MonsterCardTradingGame.Model;
using Npgsql;
using NpgsqlTypes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace MonsterCardTradingGame.DAL
{
    public class DatabaseHandler
    {

        DatabaseConnector DBConnector = new("localhost", "swe1user", "swe1pw", "mctg");


        public bool Regsiteruser(UserSchema newUser)
        {

            using var connection_ = DBConnector.Connect();

            connection_.Open();
            try
            {
                IDbCommand command = connection_.CreateCommand();

                command.CommandText = @"
insert into players 
    (username, password, coins) 
values
    (@username, @password, @coins)
";

                NpgsqlCommand c = command as NpgsqlCommand;

                c.Parameters.AddWithValue("username", NpgsqlDbType.Varchar, newUser.Username);
                c.Parameters.AddWithValue("password", NpgsqlDbType.Varchar, newUser.Password);
                c.Parameters.AddWithValue("coins", NpgsqlDbType.Integer, newUser.Coins);

                c.Prepare();

                command.ExecuteNonQuery();

                connection_.Close();

                return true;
            }
            catch (Exception)
            {

                connection_.Close();
                return false;

            }


        }

        public void CreateUserProfile(ProfileShema PlayerProfile, string username)
        {
            using var connection_ = DBConnector.Connect();

            connection_.Open();
            try
            {
                IDbCommand command = connection_.CreateCommand();

                command.CommandText = @"
insert into profiles 
    (name, bio, image, username) 
values
    (@name, @bio, @image, @username)
";

                NpgsqlCommand c = command as NpgsqlCommand;

                c.Parameters.AddWithValue("username", NpgsqlDbType.Varchar, username);
                c.Parameters.AddWithValue("name", NpgsqlDbType.Varchar, PlayerProfile.Name);
                c.Parameters.AddWithValue("Bio", NpgsqlDbType.Varchar, PlayerProfile.Bio);
                c.Parameters.AddWithValue("image", NpgsqlDbType.Varchar, PlayerProfile.Image);

                c.Prepare();

                command.ExecuteNonQuery();

                connection_.Close();
            }
            catch (Exception)
            {

                connection_.Close();

            }

        }


        public ProfileShema GetProfile(string username)
        {

            using var connection_ = DBConnector.Connect();

            ProfileShema Profile = null;

            connection_.Open();
            try
            {
                IDbCommand command = connection_.CreateCommand();

                command.CommandText = @"
SELECT *
FROM profiles
WHERE username = @username
";

                NpgsqlCommand c = command as NpgsqlCommand;

                c.Parameters.AddWithValue("username", NpgsqlDbType.Varchar, username);

                c.Prepare();

                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Profile = new ProfileShema(
                       reader.GetString(0),
                       reader.GetString(1),
                       reader.GetString(2)
                       );

                }
                reader.Close();


                connection_.Close();

                return Profile;

            }
            catch (Exception)
            {

                connection_.Close();
                return null;

            }

        }


        public bool updateProfile(string username, ProfileShema profile)
        {
            using var connection_ = DBConnector.Connect();

            connection_.Open();
            try
            {
                IDbCommand command = connection_.CreateCommand();

                command.CommandText = @"
UPDATE profiles
	SET name = @name, bio = @bio, image = @image
	WHERE username = @username;";

                NpgsqlCommand c = command as NpgsqlCommand;

                c.Parameters.AddWithValue("username", NpgsqlDbType.Varchar, username);
                c.Parameters.AddWithValue("name", NpgsqlDbType.Varchar, profile.Name);
                c.Parameters.AddWithValue("bio", NpgsqlDbType.Varchar, profile.Bio);
                c.Parameters.AddWithValue("image", NpgsqlDbType.Varchar, profile.Image);

                c.Prepare();

                command.ExecuteNonQuery();

                connection_.Close();

                return true;

            }

            catch (Exception)
            {
                connection_.Close();
                return false;
            }

        }


        public bool Checkifuserexists(string username)
        {

            using var connection_ = DBConnector.Connect();

            connection_.Open();
            try
            {
                IDbCommand command = connection_.CreateCommand();

                command.CommandText = @"
SELECT *
FROM players 
WHERE username = @username
";

                NpgsqlCommand c = command as NpgsqlCommand;

                c.Parameters.AddWithValue("username", NpgsqlDbType.Varchar, username);

                c.Prepare();

                string str = Convert.ToString(command.ExecuteScalar());

                //Console.WriteLine(str);

                if (String.IsNullOrEmpty(str))
                {
                    connection_.Close();
                    return false;

                }
                else
                {
                    connection_.Close();
                    return true;
                }
            }
            catch (Exception)
            {

                connection_.Close();
                return false;

            }





        }

        public UserSchema? Getuser(string username)
        {

            using var connection_ = DBConnector.Connect();

            UserSchema User = null;

            connection_.Open();

            try
            {
                IDbCommand command = connection_.CreateCommand();

                command.CommandText = @"
SELECT *
FROM players 
WHERE username = @username
";

                NpgsqlCommand? c = command as NpgsqlCommand;

                c.Parameters.AddWithValue("username", NpgsqlDbType.Varchar, username);

                c.Prepare();

                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    User = new UserSchema(
                       reader.GetString(0),
                       reader.GetString(1),
                       reader.GetInt32(2)
                       );

                }
                reader.Close();

                return User;
            }
            catch (Exception)
            {

                connection_.Close();
                return null;

            }

        }

        public void WriteTokenDB(string username, string token)
        {

            using var connection_ = DBConnector.Connect();

            DateTime times = DateTime.Now;

            string timestamp = times.ToString("dd / MM / yyyy HH: mm:ss");

            connection_.Open();
            try
            {
                IDbCommand command = connection_.CreateCommand();

                command.CommandText = @"
insert into authtokens 
    (username, token, timestamp) 
values
    (@username, @token, @timestamp)
";

                NpgsqlCommand c = command as NpgsqlCommand;

                c.Parameters.AddWithValue("username", NpgsqlDbType.Varchar, username);
                c.Parameters.AddWithValue("token", NpgsqlDbType.Varchar, token);
                c.Parameters.AddWithValue("timestamp", NpgsqlDbType.Varchar, timestamp);

                c.Prepare();

                command.ExecuteNonQuery();

                connection_.Close();
            }
            catch (Exception)
            {

                connection_.Close();

            }

        }

        public AuthTokenSchema ReadTokenDB(string token)
        {
            using var connection_ = DBConnector.Connect();

            AuthTokenSchema UserToken = null;

            connection_.Open();

            try
            {
                IDbCommand command = connection_.CreateCommand();

                command.CommandText = @"
SELECT *
FROM authtokens 
WHERE token = @token
";

                NpgsqlCommand c = command as NpgsqlCommand;

                c.Parameters.AddWithValue("token", NpgsqlDbType.Varchar, token);

                c.Prepare();

                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    UserToken = new AuthTokenSchema(
                       reader.GetString(0),
                       reader.GetString(1),
                       DateTime.Parse(reader.GetString(2))
                       );

                }
                reader.Close();

                return UserToken;
            }
            catch (Exception)
            {

                connection_.Close();
                return null;

            }


        }


        public void CreatePackage(List<CardSchema> cards)
        {

            using var connection_ = DBConnector.Connect();

            List<string> cardids = new();

            foreach (CardSchema card in cards)
            {
                cardids.Add(card.Id);
            }

            connection_.Open();
            try
            {
                IDbCommand command = connection_.CreateCommand();

                command.CommandText = @"
insert into packages 
    (p_cards) 
values
    (@p_cards)
";

                NpgsqlCommand c = command as NpgsqlCommand;

                c.Parameters.Add("@p_cards", NpgsqlDbType.Array | NpgsqlDbType.Varchar).Value = cardids;


                c.Prepare();

                command.ExecuteNonQuery();

                connection_.Close();
            }
            catch (Exception)
            {

                connection_.Close();

            }

        }

        public bool CheckifCardsexists(CardSchema card)
        {

            using var connection_ = DBConnector.Connect();

            int result = 0;

            connection_.Open();
            try
            {
                IDbCommand command = connection_.CreateCommand();

                command.CommandText = @"
SELECT COUNT(*) 
FROM cards
WHERE card_id = @card_id
";

                NpgsqlCommand c = command as NpgsqlCommand;

                c.Parameters.AddWithValue("card_id", NpgsqlDbType.Varchar, card.Id);

                c.Prepare();

                result = Convert.ToInt32(command.ExecuteScalar());

                Console.WriteLine(result);

                if (result == 1)
                {

                    connection_.Close();

                    return true;
                }

                connection_.Close();

                return false;
            }
            catch (Exception)
            {
                connection_.Close();

                return true;
            }
        }

        public void CreateCard(CardSchema card)
        {

            using var connection_ = DBConnector.Connect();

            connection_.Open();
            try
            {
                IDbCommand command = connection_.CreateCommand();

                command.CommandText = @"
insert into cards 
    (card_id, cardname, carddamage) 
values
    (@card_id, @cardname, @carddamage)
";

                NpgsqlCommand c = command as NpgsqlCommand;

                c.Parameters.AddWithValue("card_id", NpgsqlDbType.Varchar, card.Id);
                c.Parameters.AddWithValue("cardname", NpgsqlDbType.Varchar, card.Name);
                c.Parameters.AddWithValue("carddamage", NpgsqlDbType.Numeric, card.Damage);

                c.Prepare();

                command.ExecuteNonQuery();

                connection_.Close();
            }
            catch (Exception)
            {

                connection_.Close();

            }

        }

        public void updateCardOwner(string card, string username)
        {
            using var connection_ = DBConnector.Connect();

            connection_.Open();
            try
            {
                IDbCommand command = connection_.CreateCommand();

                command.CommandText = @"
UPDATE cards
	SET cardowner = @cardowner
	WHERE card_id = @card_id;";

                NpgsqlCommand c = command as NpgsqlCommand;

                c.Parameters.AddWithValue("cardowner", NpgsqlDbType.Varchar, username);
                c.Parameters.AddWithValue("card_id", NpgsqlDbType.Varchar, card);

                c.Prepare();

                command.ExecuteNonQuery();

                connection_.Close();
            }

            catch (Exception)
            {
                connection_.Close();

            }
        }


        public List<int> GetPackagesIDs()
        {
            using var connection_ = DBConnector.Connect();

            List<int> packageids = new List<int>();

            connection_.Open();
            try
            {
                IDbCommand command = connection_.CreateCommand();

                command.CommandText = @"
SELECT p_id
FROM packages";

                NpgsqlCommand c = command as NpgsqlCommand;

                c.Prepare();

                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    packageids.Add(reader.GetInt32(0));
                }
                reader.Close();

                connection_.Close();

                return packageids;

            }
            catch (Exception)
            {
                connection_.Close();

                return null;
            }
        }

        public List<string> GetPackageCards(int packageid)
        {
            using var connection_ = DBConnector.Connect();



            string[] packagecardss = null;

            connection_.Open();

            try
            {
                IDbCommand command = connection_.CreateCommand();

                command.CommandText = @"
SELECT p_cards
FROM packages WHERE p_id = @p_id";

                NpgsqlCommand c = command as NpgsqlCommand;

                c.Parameters.AddWithValue("p_id", NpgsqlDbType.Integer, packageid);

                c.Prepare();

                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    packagecardss = (string[])reader.GetValue(0);
                }
                reader.Close();

                List<string> packagecards = new List<string>(packagecardss);

                connection_.Close();

                return packagecards;


            }
            catch (Exception)
            {
                connection_.Close();

                return null;
            }
        }

        public CardSchema GetCard(string cardid)
        {
            using var connection_ = DBConnector.Connect();

            CardSchema packagecard = null;

            connection_.Open();

            try
            {
                IDbCommand command = connection_.CreateCommand();

                command.CommandText = @"
SELECT card_id ,cardname ,carddamage
FROM cards WHERE card_id = @card_id";

                NpgsqlCommand c = command as NpgsqlCommand;

                c.Parameters.AddWithValue("card_id", NpgsqlDbType.Varchar, cardid);

                c.Prepare();

                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    packagecard = new CardSchema(
                       reader.GetString(0),
                       reader.GetString(1),
                       reader.GetFloat(2)
                       );
                }
                reader.Close();

                connection_.Close();

                return packagecard;
            }
            catch (Exception)
            {
                connection_.Close();

                return null;
            }
        }

        public void UpdateUserCoins(string username, int newcoins)
        {
            using var connection_ = DBConnector.Connect();

            connection_.Open();

            try
            {
                IDbCommand command = connection_.CreateCommand();

                command.CommandText = @"
UPDATE players
	SET coins = @coins
	WHERE username = @username;";

                NpgsqlCommand c = command as NpgsqlCommand;

                c.Parameters.AddWithValue("username", NpgsqlDbType.Varchar, username);
                c.Parameters.AddWithValue("coins", NpgsqlDbType.Integer, newcoins);

                c.Prepare();

                command.ExecuteNonQuery();

                connection_.Close();
            }
            catch (Exception)
            {
                connection_.Close();
            }
        }

        public List<CardSchema> GetAllUserCards(string username)
        {
            using var connection_ = DBConnector.Connect();

            List<CardSchema> Usercards = new();

            connection_.Open();

            try
            {
                IDbCommand command = connection_.CreateCommand();

                command.CommandText = @"
SELECT card_id ,cardname ,carddamage
FROM cards WHERE cardowner = @cardowner";

                NpgsqlCommand c = command as NpgsqlCommand;

                c.Parameters.AddWithValue("cardowner", NpgsqlDbType.Varchar, username);

                c.Prepare();

                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Usercards.Add(new CardSchema(
                       reader.GetString(0),
                       reader.GetString(1),
                       reader.GetFloat(2)
                       ));
                }
                reader.Close();

                connection_.Close();

                return Usercards;


            }
            catch (Exception)
            {
                connection_.Close();

                return null;
            }
        }


        public void lockcard(string cardid)
        {
            using var connection_ = DBConnector.Connect();

            connection_.Open();

            try
            {
                IDbCommand command = connection_.CreateCommand();

                command.CommandText = @"
UPDATE cards
	SET locked = true
	WHERE card_id = @card_id;";

                NpgsqlCommand c = command as NpgsqlCommand;

                c.Parameters.AddWithValue("card_id", NpgsqlDbType.Varchar, cardid);

                c.Prepare();

                command.ExecuteNonQuery();

                connection_.Close();
            }
            catch (Exception)
            {
                connection_.Close();
            }
        }

        public void unlockcard(string cardid)
        {
            using var connection_ = DBConnector.Connect();

            connection_.Open();

            try
            {
                IDbCommand command = connection_.CreateCommand();

                command.CommandText = @"
UPDATE cards
	SET locked = false
	WHERE card_id = @card_id;";

                NpgsqlCommand c = command as NpgsqlCommand;

                c.Parameters.AddWithValue("card_id", NpgsqlDbType.Varchar, cardid);

                c.Prepare();

                command.ExecuteNonQuery();

                connection_.Close();
            }
            catch (Exception)
            {
                connection_.Close();
            }
        }

        public bool retrievelockedcard(string cardid)
        {
            using var connection_ = DBConnector.Connect();

            bool result = false;

            connection_.Open();

            try
            {
                IDbCommand command = connection_.CreateCommand();

                command.CommandText = @"
SELECT locked
FROM cards WHERE card_id = @card_id;";

                NpgsqlCommand c = command as NpgsqlCommand;

                c.Parameters.AddWithValue("card_id", NpgsqlDbType.Varchar, cardid);

                c.Prepare();

                result = Convert.ToBoolean(command.ExecuteScalar());

                connection_.Close();

                return result;
            }
            catch (Exception)
            {
                connection_.Close();

                return true;
            }
        }

        public bool CheckCardAvailability(string cardid, string username)
        {

            using var connection_ = DBConnector.Connect();

            int result = 0;

            connection_.Open();
            try
            {
                IDbCommand command = connection_.CreateCommand();

                command.CommandText = @"
SELECT COUNT(*) 
FROM cards
WHERE card_id = @card_id AND cardowner = @cardowner AND locked = false
";
                NpgsqlCommand c = command as NpgsqlCommand;

                c.Parameters.AddWithValue("card_id", NpgsqlDbType.Varchar, cardid);

                c.Parameters.AddWithValue("cardowner", NpgsqlDbType.Varchar, username);

                c.Prepare();

                result = Convert.ToInt32(command.ExecuteScalar());

                if (result == 1)
                {
                    connection_.Close();

                    return false;
                }

                connection_.Close();

                return true;
            }
            catch (Exception)
            {
                connection_.Close();

                return true;
            }
        }

        public void WriteDeckDB(List<string> cardids, string username)
        {

            using var connection_ = DBConnector.Connect();

            connection_.Open();
            try
            {
                IDbCommand command = connection_.CreateCommand();

                command.CommandText = @"
insert into decks 
    (deck_cards , deck_owner , active) 
values
    (@deck_cards,@deck_owner, true)
";

                NpgsqlCommand c = command as NpgsqlCommand;

                c.Parameters.Add("deck_cards", NpgsqlDbType.Array | NpgsqlDbType.Varchar).Value = cardids;

                c.Parameters.AddWithValue("deck_owner", NpgsqlDbType.Varchar, username);

                c.Prepare();

                command.ExecuteNonQuery();

                connection_.Close();
            }
            catch (Exception)
            {
                connection_.Close();
            }
        }

        public void SetDeckinactive(string username)
        {
            using var connection_ = DBConnector.Connect();

            connection_.Open();

            try
            {
                IDbCommand command = connection_.CreateCommand();

                command.CommandText = @"
UPDATE decks
	SET active = false
	WHERE username = @username;";

                NpgsqlCommand c = command as NpgsqlCommand;

                c.Parameters.AddWithValue("username", NpgsqlDbType.Varchar, username);

                c.Prepare();

                command.ExecuteNonQuery();

                connection_.Close();
            }
            catch (Exception)
            {
                connection_.Close();
            }
        }

        public List<string> GetDeckCardIds(string username)
        {
            using var connection_ = DBConnector.Connect();

            List<string> ids = new List<string>();

            string[] DBids = null;

            connection_.Open();

            try
            {
                IDbCommand command = connection_.CreateCommand();

                command.CommandText = @"
SELECT deck_cards
FROM decks WHERE deck_owner = @deck_owner";

                NpgsqlCommand c = command as NpgsqlCommand;

                c.Parameters.AddWithValue("deck_owner", NpgsqlDbType.Varchar, username);

                c.Prepare();

                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    DBids = (string[])reader.GetValue(0);
                }
                reader.Close();

                ids = new List<string>(DBids);

                connection_.Close();

                return ids;

            }
            catch (Exception)
            {
                connection_.Close();

                return null;
            }
        }
        public void CreateUserStats(string username, StatsSchema UserStats)
        {
            using var connection_ = DBConnector.Connect();

            connection_.Open();
            try
            {
                IDbCommand command = connection_.CreateCommand();

                command.CommandText = @"
insert into stats 
    (username, name, elo, wins, losses, gamesplayed) 
values
    (@username, @name, @elo, @wins, @losses, @gamesplayed)
";

                NpgsqlCommand c = command as NpgsqlCommand;

                c.Parameters.AddWithValue("username", NpgsqlDbType.Varchar, username);
                c.Parameters.AddWithValue("name", NpgsqlDbType.Varchar, UserStats.Name);
                c.Parameters.AddWithValue("elo", NpgsqlDbType.Numeric, UserStats.Elo);
                c.Parameters.AddWithValue("wins", NpgsqlDbType.Numeric, UserStats.Wins);
                c.Parameters.AddWithValue("losses", NpgsqlDbType.Numeric, UserStats.Losses);
                c.Parameters.AddWithValue("gamesplayed", NpgsqlDbType.Numeric, UserStats.GamesPlayed);

                c.Prepare();

                command.ExecuteNonQuery();

                connection_.Close();
            }
            catch (Exception)
            {
                connection_.Close();
            }
        }


        public void UpdateStatsName(string username, string newname)
        {
            using var connection_ = DBConnector.Connect();

            connection_.Open();
            try
            {
                IDbCommand command = connection_.CreateCommand();

                command.CommandText = @"
UPDATE stats
	SET name = @name
	WHERE username = @username;";

                NpgsqlCommand c = command as NpgsqlCommand;

                c.Parameters.AddWithValue("username", NpgsqlDbType.Varchar, username);
                c.Parameters.AddWithValue("name", NpgsqlDbType.Varchar, newname);

                c.Prepare();

                command.ExecuteNonQuery();

                connection_.Close();
            }
            catch (Exception)
            {
                connection_.Close();
            }
        }




        public void UpdateStats(string username, StatsSchema UserStats)
        {
            using var connection_ = DBConnector.Connect();

            connection_.Open();
            try
            {
                IDbCommand command = connection_.CreateCommand();

                command.CommandText = @"
UPDATE stats
	SET elo = @elo , wins = @wins , losses = @losses , gamesplayed = @gamesplayed
	WHERE username = @username;";

                NpgsqlCommand c = command as NpgsqlCommand;

                c.Parameters.AddWithValue("username", NpgsqlDbType.Varchar, username);
                c.Parameters.AddWithValue("elo", NpgsqlDbType.Numeric, UserStats.Elo);
                c.Parameters.AddWithValue("wins", NpgsqlDbType.Numeric, UserStats.Elo);
                c.Parameters.AddWithValue("losses", NpgsqlDbType.Numeric, UserStats.Elo);
                c.Parameters.AddWithValue("gamesplayed", NpgsqlDbType.Numeric, UserStats.Elo);

                c.Prepare();

                command.ExecuteNonQuery();

                connection_.Close();
            }
            catch (Exception)
            {
                connection_.Close();
            }
        }

        public StatsSchema GetPlayerStats(string username)
        {
            using var connection_ = DBConnector.Connect();

            StatsSchema PlayerStats = null;

            connection_.Open();
            try
            {
                IDbCommand command = connection_.CreateCommand();

                command.CommandText = @"
SELECT *
FROM stats WHERE username = @username;";

                NpgsqlCommand c = command as NpgsqlCommand;

                c.Parameters.AddWithValue("username", NpgsqlDbType.Varchar, username);

                c.Prepare();

                var reader = command.ExecuteReader();


                while (reader.Read())
                {
                    PlayerStats = new(reader.GetString(1), reader.GetFloat(2), reader.GetInt32(3), reader.GetInt32(4), reader.GetInt32(5));
                }
                reader.Close();

                connection_.Close();

                return PlayerStats;
            }
            catch (Exception)
            {
                connection_.Close();
                return PlayerStats;
            }
        }

        public List<StatsSchema> GetScoreboard()
        {
            using var connection_ = DBConnector.Connect();

            List<StatsSchema> PlayerStats = new List<StatsSchema>();

            connection_.Open();
            try
            {
                IDbCommand command = connection_.CreateCommand();

                command.CommandText = @"
SELECT *
FROM stats order by elo desc;";

                NpgsqlCommand c = command as NpgsqlCommand;

                c.Prepare();

                var reader = command.ExecuteReader();


                while (reader.Read())
                {
                    PlayerStats.Add(new StatsSchema(reader.GetString(1),reader.GetFloat(2), reader.GetInt32(3), reader.GetInt32(4), reader.GetInt32(5)));
                }
                reader.Close();

                connection_.Close();

                return PlayerStats;
            }
            catch (Exception)
            {
                connection_.Close();
                return PlayerStats;
            }
        }

        public List<TradingSchema> GetAllTrades()
        {

            using var connection_ = DBConnector.Connect();

            List<TradingSchema> Trades = new List<TradingSchema>();

            connection_.Open();
            try
            {
                IDbCommand command = connection_.CreateCommand();

                command.CommandText = @"
SELECT *
FROM tradingdeals;";

                NpgsqlCommand c = command as NpgsqlCommand;

                c.Prepare();

                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Trades.Add(new TradingSchema(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetFloat(4), reader.GetString(3)));
                }
                reader.Close();

                connection_.Close();

                return Trades;
            }
            catch (Exception)
            {
                connection_.Close();
                return Trades;
            }
        }

        public bool CheckTradeexists(string tradeid)
        {

            using var connection_ = DBConnector.Connect();

            int result = 0;

            connection_.Open();
            try
            {
                IDbCommand command = connection_.CreateCommand();

                command.CommandText = @"
SELECT COUNT(*) 
FROM tradingdeals
WHERE t_id = @t_id
";
                NpgsqlCommand c = command as NpgsqlCommand;

                c.Parameters.AddWithValue("t_id", NpgsqlDbType.Varchar,tradeid);

                c.Prepare();

                result = Convert.ToInt32(command.ExecuteScalar());

                if (result == 1)
                {
                    connection_.Close();

                    return false;
                }

                connection_.Close();

                return true;
            }
            catch (Exception)
            {
                connection_.Close();

                return true;
            }

        }

        public void CreateTradeDeal(TradingSchema newTrade, string username)
        {
            using var connection_ = DBConnector.Connect();

            connection_.Open();
            try
            {
                IDbCommand command = connection_.CreateCommand();

                command.CommandText = @"
insert into tradingdeals
    (t_id, t_owner, cardtotrade, type, mindamage) 
values
    (@t_id, @t_owner, @cardtotrade, @type, @mindamage)
";

                NpgsqlCommand c = command as NpgsqlCommand;

                c.Parameters.AddWithValue("t_owner", NpgsqlDbType.Varchar, username);
                c.Parameters.AddWithValue("t_id", NpgsqlDbType.Varchar, newTrade.Id);
                c.Parameters.AddWithValue("cardtotrade", NpgsqlDbType.Varchar, newTrade.CardToTrade);
                c.Parameters.AddWithValue("type", NpgsqlDbType.Varchar, newTrade.Type);
                c.Parameters.AddWithValue("mindamage", NpgsqlDbType.Numeric, newTrade.MinimumDamage);
              
                c.Prepare();

                command.ExecuteNonQuery();

                connection_.Close();
            }
            catch (Exception)
            {
                connection_.Close();
            }
        }

        public void deleteTrade(string tradeid)
        {
            using var connection_ = DBConnector.Connect();

            connection_.Open();
            try
            {
                IDbCommand command = connection_.CreateCommand();

                command.CommandText = @"DELETE FROM tradingdeals WHERE t_id = @t_id";

                NpgsqlCommand c = command as NpgsqlCommand;

                c.Parameters.AddWithValue("t_id", NpgsqlDbType.Varchar, tradeid);
              
                c.Prepare();

                command.ExecuteNonQuery();

                connection_.Close();
            }
            catch (Exception)
            {
                connection_.Close();
            }
        }

        public TradingSchema GetTrade(string tradeid)
        {
            using var connection_ = DBConnector.Connect();

           TradingSchema CurrentTrade = null;

            connection_.Open();
            try
            {
                IDbCommand command = connection_.CreateCommand();

                command.CommandText = @"
SELECT *
FROM tradingdeals WHERE t_id=@t_id;";

                NpgsqlCommand c = command as NpgsqlCommand;

                c.Parameters.AddWithValue("t_id", NpgsqlDbType.Varchar, tradeid);

                c.Prepare();

                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    CurrentTrade = new TradingSchema(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetFloat(4), reader.GetString(3));
                }
                reader.Close();

                connection_.Close();

                return CurrentTrade;
            }
            catch (Exception)
            {
                connection_.Close();
                return CurrentTrade;
            }
        }

    }
}
