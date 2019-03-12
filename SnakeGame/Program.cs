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
            var isRunning = true;
            var view = new View();
            var coord = new Coord();
            Task.Run(() => { while (isRunning) { coord.CatchInput(); } });
            
            while (isRunning)
            {
                switch (status)
                {
                    case Status.Playing:
                        Console.Clear();
                        view.Draw((maxSize * speedUp - curSpeed) / speedUp + 2, maxSize);
                        Thread.Sleep(curSpeed);
                        status = view.UpdateGrid(coord.MoveSnake(), coord.ReturnFeed());
                        break;

                    case Status.Title:
                        view.Title();
                        if (coord.Input.Key == ConsoleKey.Enter)
                        {
                            status = Status.Playing;
                            curSpeed = defaultSpeed;
                            coord.SetFeed();
                            view.ToStart(coord.MoveSnake(), coord.ReturnFeed());
                        }
                        Thread.Sleep(defaultSpeed);
                        break;

                    case Status.GameOver:
                    case Status.Clear:
                        Console.Clear();
                        view.Draw((maxSize * speedUp - curSpeed) / speedUp + 2, maxSize);
                        switch (status)
                        {
                            case Status.GameOver:
                                Console.ForegroundColor = System.ConsoleColor.Red;
                                Console.WriteLine("  GAME OVER");
                                break;
                            case Status.Clear:
                                Console.ForegroundColor = System.ConsoleColor.Blue;
                                Console.WriteLine("  GAME CLEAR");
                                break;
                        }
                        Console.ForegroundColor = System.ConsoleColor.White;
                        Console.WriteLine("Press ENTER to restart.");
                        Console.WriteLine("Press ESC to quit.");
                        switch (coord.Input.Key)
                        {
                            case ConsoleKey.Enter:
                                status = Status.Title;
                                coord.Reset();
                                break;
                            case ConsoleKey.Escape:
                                isRunning = false;
                                return;
                        }
                        Thread.Sleep(defaultSpeed);
                        break;
                }
            }
        }

        public static void SpeedUp()
        {
            curSpeed -= speedUp;
        }
    }
}
