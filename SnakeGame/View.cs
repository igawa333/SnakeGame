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
        public int UpdateGrid(List<IntPair> snake, IntPair feed)
        {
            try
            {
                grid.SetValue(1, snake[0].y, snake[0].x);
                grid.SetValue(0, snake[snake.Count() - 1].y, snake[snake.Count() - 1].x);
                if(snake.Count() == Program.maxSize)
                {
                    grid.SetValue(3, feed.y, feed.x);
                }
                else if(snake.Count() > Program.maxSize)
                {
                    return 3;
                }
                else
                {
                    grid.SetValue(2, feed.y, feed.x);
                }
            }
            catch (IndexOutOfRangeException)
            {
                return 2;
            }

            if (snake.LastIndexOf(snake[0]) != 0)
            {
                return 2;
            }

            return 1;
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
            grid.SetValue(1, snake[0].y, snake[0].x);
            grid.SetValue(1, snake[1].y, snake[1].x);
            grid.SetValue(2, feed.y, feed.x);
        }
    }
}
