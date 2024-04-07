using System;

namespace Intel_1
{
    public class Maze
    {
        public int[,] Grid { get; private set; }
        public Point Start { get; set; }
        public Point End { get; set; }

        public Maze(int[,] grid, Point start, Point end)
        {
            Grid = grid;
            Start = start;
            End = end;
        }

        public void Display()
        {
            for (int y = 0; y < Grid.GetLength(0); y++)
            {
                for (int x = 0; x < Grid.GetLength(1); x++)
                {
                    if (Start.X == x && Start.Y == y)
                    {
                        Console.Write("S ");
                    }
                    else if (End.X == x && End.Y == y)
                    {
                        Console.Write("E "); 
                    }
                    else
                    {
                        Console.Write(Grid[y, x] == 1 ? "# " : ". ");
                    }
                }
                Console.WriteLine();
            }
        }
    }
}