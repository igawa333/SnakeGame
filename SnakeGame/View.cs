using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace SnakeGame
{
    class View
    {
        static public readonly int x = 15;
        static public readonly int y = 15;
        int[,] grid = new int[y, x];
        public int a = 0;

        //画面描画
        public void Draw(int curLevel, int maxLevel)
        {
            Console.WriteLine("■■■■■■■■■■■■■■■■■");
            for (int y = 0; y < grid.GetLength(0); y++)
            {
                Console.Write("■");
                for (int x = 0; x < grid.GetLength(1); x++)
                {
                    switch (grid[y, x])
                    {
                        case 0: Console.Write("　"); break;
                        case 1: Console.Write("□"); break;
                        case 2: Console.Write("・"); break;
                        case 3: Console.Write("◆"); break;
                    }
                }
                Console.Write("■\n");
            }
            Console.WriteLine("■■■■■■■■■■■■■■■■■");
            Console.WriteLine("");
            Console.Write(curLevel + " / " + maxLevel);
        }
        
        //座標をもらって表示データ更新&当たり判定
        public Program.Status UpdateGrid(List<IntPair> snake, IntPair feed)
        {
            try
            {
                grid.SetValue(1, snake[0].Y, snake[0].X);
                grid.SetValue(0, snake[snake.Count() - 1].Y, snake[snake.Count() - 1].X);
                if(snake.Count() == Program.maxSize)
                {
                    grid.SetValue(3, feed.Y, feed.X);
                }
                else if(snake.Count() > Program.maxSize)
                {
                    return Program.Status.Clear;
                }
                else
                {
                    grid.SetValue(2, feed.Y, feed.X);
                }
            }
            catch (IndexOutOfRangeException)
            {
                return Program.Status.GameOver;
            }

            if (snake.LastIndexOf(snake[0]) != 0)
            {
                return Program.Status.GameOver;
            }

            return Program.Status.Playing;
        }

        public void Title()
        {
            Console.WriteLine("■■■■■■■■■■■■■■■■■");
            for (int y = 0; y < grid.GetLength(0); y++)
            {
                Console.Write("■");
                for (int x = 0; x < grid.GetLength(1); x++)
                {
                    if (y == 7)
                    {
                        Console.Write("　　Push any key to start.　　");
                        break;
                    }
                    else
                    {
                        Console.Write("　");
                    }
                }
                Console.Write("■\n");
            }
            Console.WriteLine("■■■■■■■■■■■■■■■■■");
        }

        public void ToStart(List<IntPair> snake, IntPair feed)
        {
            grid.SetValue(1, snake[0].Y, snake[0].X);
            grid.SetValue(1, snake[1].Y, snake[1].X);
            grid.SetValue(2, feed.Y, feed.X);
        }
    }
}
