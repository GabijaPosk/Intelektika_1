using System.Collections.Generic;

namespace Intel_1
{
    public class BFS
    {
        public static List<Point> FindPath(Maze maze)
        {
            var path = new List<Point>();
            var visited = new HashSet<Point>();
            var queue = new Queue<Point>();
            var predecessors = new Dictionary<Point, Point>();
            queue.Enqueue(maze.Start);

            while (queue.Count > 0)
            {
                var current = queue.Dequeue();
                if (visited.Contains(current))
                    continue;

                visited.Add(current);

                if (current.Equals(maze.End))
                {
                    var temp = current;
                    while (!temp.Equals(maze.Start))
                    {
                        path.Add(temp);
                        temp = predecessors[temp];
                    }
                    path.Add(maze.Start);
                    path.Reverse();
                    return path;
                }

                var neighbors = GetNeighbors(current, maze);
                foreach (var neighbor in neighbors)
                {
                    if (!visited.Contains(neighbor) && !queue.Contains(neighbor))
                    {
                        predecessors[neighbor] = current;
                        queue.Enqueue(neighbor);
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