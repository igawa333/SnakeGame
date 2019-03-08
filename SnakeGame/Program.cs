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
        public int X { get; private set; }
        public int Y { get; private set; }

        public IntPair(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }


        public void Set(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        public IntPair Sum(int x, int y)
        {
            this.X += x;
            this.Y += y;
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

                switch (status)
                {
                    case Status.Playing:
                        break;
                    case Status.GameOver:
                        Console.ForegroundColor = System.ConsoleColor.Red;
                        Console.WriteLine("  GAME OVER");
                        view = null;
                        coord = null;
                        SystemMessage();
                        break;
                    case Status.Clear:
                        Console.Clear();
                        view.Draw((maxSize * speedUp - curSpeed) / speedUp + 2, maxSize);
                        Console.ForegroundColor = System.ConsoleColor.Blue;
                        Console.WriteLine("  GAME CLEAR");
                        view = null;
                        coord = null;
                        SystemMessage();
                        break;
                    default:
                        Console.WriteLine("えっ");
                        Console.WriteLine(status);
                        Thread.Sleep(5000);
                        break;
                }
            }
        }

        public static void SpeedUp()
        {
            curSpeed -= speedUp;
        }
        
        public static void SystemMessage()
        {
            Console.ForegroundColor = System.ConsoleColor.White;
            Console.WriteLine("Press ENTER to restart.");
            Console.WriteLine("Press ESC to quit.");

        Select:
            ConsoleKeyInfo input = Console.ReadKey();
            switch (input.Key)
            {
                case ConsoleKey.Enter:
                    Console.Clear();
                    Main();
                    return;
                case ConsoleKey.Escape:
                    return;
                default:
                    goto Select;
            }
        }
    }
}
