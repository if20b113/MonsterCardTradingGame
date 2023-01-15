using System.Collections.Concurrent;
using System.Collections.Specialized;
using System.Numerics;
using MonsterCardTradingGame.DAL;
using MonsterCardTradingGame.Model;

namespace MonsterCardTradingGame.BL
{
    public class GameHandler
    {
        private bool listening;
        private readonly Dictionary<string, Task> tasks;
        private Thread autoStart;
        private readonly ConcurrentQueue<IUser>? playerQueue;

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
                try
                {
                    if (playerQueue?.Count >= 2)
                    {
                        IUser? Player1 = null;
                        IUser? Player2 = null;

                        while (Player1 == null) playerQueue.TryDequeue(out Player1);
                        while (Player2 == null) playerQueue.TryDequeue(out Player2);

                        if (Player1?.Username == Player2?.Username)
                        {
                            BatteLog BattleLogError = new BatteLog();

                            BattleLogError.Winner = null;

                            BattleLogError.Loser = null;

                            Player1.PlayerBattleLog = BattleLogError;

                            Player2.PlayerBattleLog = BattleLogError;
                        }
                        else
                        {
                         
                            var id = Guid.NewGuid().ToString();
                            var task = Task.Run(() => Process(Player1, Player2));
                            tasks[id] = task;
                          
                            task.ContinueWith(t =>
                            {
                                tasks.Remove(id, out t!);
                            });
                        }
                    }
                    else Thread.Sleep(15);
                }
                catch
                {



                }
            }

        }

        private void Process(IUser? Player1, IUser? Player2)
        {

          var Battle = new BattleHandler(Player1,Player2);

          var result = Battle.StartBattle();

          Player1.PlayerBattleLog = result;

          Player2.PlayerBattleLog = result;
        }

        public BatteLog Play(IUser Player)
        {

            playerQueue?.Enqueue(Player);
            while (Player.PlayerBattleLog is null)
            {
                Thread.Sleep(30);
            }
            return Player.PlayerBattleLog;

        }
    }
}
