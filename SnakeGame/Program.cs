using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SnakeGame
{
    public struct IntPair
    {
        public int x { get; private set; }
        public int y { get; private set; }

        public IntPair(int x, int y)
        {
            this.x = x;
            this.y = y;
        }


        public void Set(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public IntPair Sum(int x, int y)
        {
            this.x += x;
            this.y += y;
            return this;
        }
    }

    class Program
    {
        static readonly public int maxSize = 30;
        static readonly public int defaultSpeed = 240;
        static readonly public int speedUp = defaultSpeed / maxSize;
        static int curSpeed;
        public enum Status
        {
            Title, Playing, Clear, GameOver
        }

        static void Main()
        {
            var status = Status.Title;
            var view = new View();
            var coord = new Coord();
            
            //タイトル画面
            view.Title();
            Console.ReadKey(true);

            //開始処理
            status = Status.Playing;
            curSpeed = defaultSpeed;
            coord.SetFeed();
            view.ToStart(coord.MoveSnake(), coord.ReturnFeed());

            //メイン処理
            Task.Run(() => { while (status == Status.Playing) { coord.ChangeDirection(); } });

            while (status == Status.Playing)
            {
                Console.Clear();
                view.Draw((maxSize * speedUp - curSpeed) / speedUp + 2, maxSize);
                Thread.Sleep(curSpeed);
                status = view.UpdateGrid(coord.MoveSnake(), coord.ReturnFeed());
            }

            //ゲームクリアまたはゲームオーバー処理
            switch (status)
            {
                case Status.GameOver:
                    GameOver();
                    break;
                case Status.Clear:
                    Console.Clear();
                    view.Draw((maxSize * speedUp - curSpeed) / speedUp + 2, maxSize);
                    GameClear();
                    break;
                default:
                    Console.WriteLine("えっ");
                    Console.WriteLine(status);
                    Thread.Sleep(5000);
                    break;
            }

            Console.Clear();
            view = null;
            coord = null;
            Main();
        }

        public static void SpeedUp()
        {
            curSpeed -= speedUp;
        }

    public static void GameOver()
        {
            var message = "Return to title in ";

            Console.ForegroundColor = System.ConsoleColor.Red;
            Console.WriteLine("  GAMEOVER");

            Console.ForegroundColor = System.ConsoleColor.White;
            Console.Write(message);

            for (int time = 10;time > 0 ; time--)
            {
                Console.CursorLeft = message.Length;
                Console.Write("{0}secs", time.ToString("D2"));
                Thread.Sleep(1000);
            }
            //この待ち時間中に2回キー入力があるとメインに戻った時にそのままスタートしてしまう
            //1回目はメインでタスクとして実行したCoord.ChangeDirection()のReadKey()が生きているからそれに食われる
            //2回目がメインの最初の段のReadKey()にそのまま投げられてしまう（バッファをクリアとかできないんですか）
    
            //本当はこうしたかったけど上述の理由で1回目の入力が受け付けられなくて非常にモヤモヤしたので1回やめた
            //
            //    status = false;
            //    Console.WriteLine("");
            //    Console.WriteLine("GAME OVER");
            //    Console.WriteLine("Press ENTER to restart.");
            //    Console.WriteLine("Press ESC to quit.");

            //Select:
            //    ConsoleKeyInfo input = Console.ReadKey();
            //    switch (input.Key)
            //    {
            //        case ConsoleKey.Enter:
            //            Console.Clear();
            //            Main();
            //            return;
            //        case ConsoleKey.Escape:
            //            return;
            //        default:
            //            goto Select;
            //    }
        }

        public static void GameClear()
        {
            var message = "Return to title in ";

            Console.ForegroundColor = System.ConsoleColor.Blue;
            Console.WriteLine("  GAMECLEAR");

            Console.ForegroundColor = System.ConsoleColor.White;
            Console.Write(message);

            for (int time = 10; time > 0; time--)
            {
                Console.CursorLeft = message.Length;
                Console.Write("{0}secs", time.ToString("D2"));
                Thread.Sleep(1000);
            }
        }
    }
}
