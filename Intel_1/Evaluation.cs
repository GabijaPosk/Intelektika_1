using System;
using System.Diagnostics;

namespace Intel_1
{
    public class EvaluationResult
    {
        public long BfsTime { get; set; }
        public int BfsPathLength { get; set; }
        public long DfsTime { get; set; }
        public int DfsPathLength { get; set; }
    }

    class Evaluation
    {
        public static (TimeSpan BfsDuration, TimeSpan DfsDuration, int BfsPathLength, int DfsPathLength) CompareAlgorithms(Maze maze)
        {
            var stopwatch = new Stopwatch();

            stopwatch.Start();
            var bfsPath = BFS.FindPath(maze);
            stopwatch.Stop();
            TimeSpan bfsDuration = stopwatch.Elapsed;
            int bfsPathLength = bfsPath.Count;

            stopwatch.Restart();
            var dfsPath = DFS.FindPath(maze);
            stopwatch.Stop();
            TimeSpan dfsDuration = stopwatch.Elapsed;
            int dfsPathLength = dfsPath.Count;

            return (bfsDuration, dfsDuration, bfsPathLength, dfsPathLength);
        }
    }
}
