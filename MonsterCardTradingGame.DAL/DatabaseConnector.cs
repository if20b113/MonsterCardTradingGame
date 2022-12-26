using MonsterCardTradingGame.Model;
using Npgsql;
using System.Data;

namespace MonsterCardTradingGame.DAL
{
    public class DatabaseConnector
    {
        string host { get; set; }

        string username { get; set; }

        string password { get; set; }

        string dbname { get; set; }

        DatabaseConnector(string host, string username, string password, string dbname)
        {
            this.host = host;
            this.username = username;
            this.password = password;
            this.dbname = dbname;
        }

        public NpgsqlConnection Connect()
        {
            var connstring = $"Server={host};Port=5432;User Id={username};Password={password};Database={dbname};";

            var connection = new NpgsqlConnection(connstring);

            return connection;
        }
    }
}
