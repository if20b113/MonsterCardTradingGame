using MonsterCardTradingGame.Model;
using Npgsql;
using System.Data;

namespace MonsterCardTradingGame.DAL
{
    public class DatabaseConnector
    {
        public string host { get; set; }

        public string username { get; set; }

        public string password { get; set; }

        public string dbname { get; set; }

        public DatabaseConnector(string host, string username, string password, string dbname)
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
