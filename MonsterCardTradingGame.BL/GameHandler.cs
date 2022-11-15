using System.Collections.Concurrent;
using System.Collections.Specialized;
using MonsterCardTradingGame.DAL;
using MonsterCardTradingGame.Model;

namespace MonsterCardTradingGame.BL
{
    public class GameHandler
    {
        private bool listening;
        private readonly Dictionary<string, Task> tasks;
        private Thread autoStart;
        private readonly ConcurrentQueue<IUser> playerQueue;

        public GameHandler()
        {
            listening = true;
            tasks = new Dictionary<string, Task>();
            autoStart = new Thread(RunMatchmaking);
            autoStart.Start();

        }

        private void RunMatchmaking()
        {

            while (listening)
            {
                if(playerQueue.Count >= 2)
                {
                    IUser? Player1 = null;
                    IUser? Player2 = null;

                    if(Player1.Username == Player2.Username)
                    {




                    }
                    else
                    {



                    }

                }




            }




        }

    }
}
