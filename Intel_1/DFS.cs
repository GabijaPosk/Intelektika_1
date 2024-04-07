using System;
using System.Collections.Generic;

namespace Intel_1
{
    public class DFS
    {
        public static List<Point> FindPath(Maze maze)
        {
            var path = new List<Point>();
            var visited = new HashSet<Point>();
            var stack = new Stack<Point>();
            var predecessors = new Dictionary<Point, Point>();

            stack.Push(maze.Start);
            visited.Add(maze.Start);

            while (stack.Count > 0)
            {
                var current = stack.Pop();

                if (current.Equals(maze.End))
                {
                    Stack<Point> reversePath = new Stack<Point>();
                    while (predecessors.ContainsKey(current))
                    {
                        reversePath.Push(current);
                        current = predecessors[current];
                    }
                    reversePath.Push(maze.Start); 

                    while (reversePath.Count > 0)
                    {
                        path.Add(reversePath.Pop());
                    }
                    return path;
                }

                var neighbors = GetNeighbors(current, maze);
                foreach (var neighbor in neighbors)
                {
                    if (!visited.Contains(neighbor))
                    {
                        stack.Push(neighbor);
                        visited.Add(neighbor);
                        predecessors[neighbor] = current; 
                    }
                }
            }
            return path; 
        }

        private static IEnumerable<Point> GetNeighbors(Point current, Maze maze)
        {
            var directions = new List<Point>
            {
                new Point(0, -1), 
                new Point(1, 0), 
                new Point(0, 1),  
                new Point(-1, 0) 
            };

            foreach (var direction in directions)
            {
                var next = new Point(current.X + direction.X, current.Y + direction.Y);
                if (next.Y >= 0 && next.Y < maze.Grid.GetLength(0) && next.X >= 0 && next.X < maze.Grid.GetLength(1) && maze.Grid[next.Y, next.X] == 0)
                {
                    yield return next;
                }
            }
        }
    }
}