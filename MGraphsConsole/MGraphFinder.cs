using System.Collections.Generic;

namespace MGraphsConsole
{
    struct Point
    {
        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }
        public int Y { get; set; }
        public int X { get; set; }
    }
    class MGraphFinder
    {
        public static List<bool[,]> GenerateMGraphs(int n)
        {
            if (n <= 5)
                return new List<bool[,]> { GenerateMGraph(n) };

            var startList = new List<Point>();
            var graphs = new List<bool[,]>();
            startList.Add(new Point(n - 4, n - 3));
            startList.Add(new Point(n - 5, n - 3));
            GenerateMGraphsR(startList, graphs);

            return graphs;
        }

        public static bool[,] GenerateMGraph(int n)
        {
            if (n <= 0)
                return new bool[0, 0];
            if (n <= 3)
                return GetBasicMGraph(n);

            var graph = GetBasicMGraph(n);
            graph[0, n - 1] = graph[n - 1, 0] = true;
            for (int i = 1; i < n - 1; i++)
                graph[0, i] = graph[i, 0] = graph[i, n - 1] = graph[n - 1, i] = true;

            return graph;
        }

        private static void GenerateMGraphsR(List<Point> currentEdges, List<bool[,]> graphs)
        {
            var lastPoint = currentEdges[currentEdges.Count - 1];
            if (lastPoint.X == 0)
            {
                var graph = GetBasicMGraph(currentEdges[0].Y + 3);

                foreach (var point in currentEdges)
                    graph[point.X, point.Y + 2] = graph[point.Y + 2, point.X] = true;

                for (int i = lastPoint.Y - 1; i >= 1; i--)
                    graph[0, i + 2] = graph[i + 2, 0] = true;

                graphs.Add(graph);
                return;
            }

            var currentCount = currentEdges.Count;

            currentEdges.Add(new Point(lastPoint.X - 1, lastPoint.Y));
            GenerateMGraphsR(currentEdges, graphs);
            currentEdges.RemoveAt(currentCount);

            if (lastPoint.Y <= lastPoint.X + 1)
                return;

            currentEdges.Add(new Point(lastPoint.X, lastPoint.Y - 1));
            GenerateMGraphsR(currentEdges, graphs);
            currentEdges.RemoveAt(currentCount);
        }

        private static bool[,] GetBasicMGraph(int n)
        {
            var graph = new bool[n, n];
            for (int i = 0; i < n - 2; i++)
                graph[i, i + 1] = graph[i + 1, i] = graph[i, i + 2] = graph[i + 2, i] = true;

            if (n >= 2)
                graph[n - 1, n - 2] = graph[n - 2, n - 1] = true;
            return graph;
        }
    }
}
