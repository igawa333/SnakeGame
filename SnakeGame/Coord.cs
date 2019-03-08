using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace SnakeGame
{
    class Coord
    {
        Random rand = new Random();
        IntPair head = new IntPair(0, 0);
        IntPair feed = new IntPair(0, 0);
        IntPair direction = new IntPair(1, 0);
        List<IntPair> snake = new List<IntPair> { new IntPair(0, 0), new IntPair(0, 0), new IntPair(0, 0) };
                
        //入力を待ち受けてdirectionを書き換える
        public void ChangeDirection()
        {
            ConsoleKeyInfo input = Console.ReadKey(true);
            
            if (head.X - snake[1].X == 0)   //今の方向によって受け付ける入力を切り替える
            {
                switch (input.Key)
                {
                    case ConsoleKey.RightArrow:
                        direction.Set(1, 0);
                        break;
                    case ConsoleKey.LeftArrow:
                        direction.Set(-1, 0);
                        break;
                    default: break;
                }
            }
            else if(head.Y - snake[1].Y == 0)
            {
                switch (input.Key)
                {
                    case ConsoleKey.UpArrow:
                        direction.Set(0, -1);
                        break;
                    case ConsoleKey.DownArrow:
                        direction.Set(0, 1);
                        break;
                    default: break;
                }
            }
        }

        public void SetFeed()
        {
            feed.Set(rand.Next(1, View.x), rand.Next(1, View.y));

            if (snake.Contains(feed))
            {
                SetFeed();
            }
        }

        public IntPair ReturnFeed()
        {
            return feed;
        }

        public List<IntPair> MoveSnake()
        {
            head.Sum(direction.X, direction.Y);

            if (head.Equals(feed))
            {
                SetFeed();
                Program.SpeedUp();
                AddBody(snake[snake.Count() - 1].X, snake[snake.Count() - 1].Y);
            }

            for (int i = snake.Count() - 1; i > 0; i--)
            {
                snake[i] = snake[i - 1];
            }
            snake[0] = head;
            return snake;
        }
        
        public void AddBody(int x, int y)
        {
            snake.Add(new IntPair(x, y));
        }
    }
}
