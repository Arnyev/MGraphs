using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace MGraphs
{
    class MGraphFinder
    {
        public static List<bool[,]> GenerateMGraphs(int n)
        {
            var startList = new List<Point>();
            var finalEdges = new List<List<Point>>();
            startList.Add(new Point(n - 2, n - 1));
            startList.Add(new Point(n - 3, n - 1));
            GenerateMGraphsR(startList, finalEdges);

            return finalEdges.Select(EdgesToGraph).ToList();
        }

        private static bool[,] EdgesToGraph(List<Point> edges)
        {
            var graph = new bool[edges[0].Y + 1, edges[0].Y + 1];

            foreach (var point in edges)
                graph[point.X, point.Y] = graph[point.Y, point.X] = true;

            return graph;
        }

        private static void GenerateMGraphsR(List<Point> currentEdges, List<List<Point>> finalPaths)
        {
            var lastPoint = currentEdges[currentEdges.Count - 1];
            if (lastPoint.X == 0)
            {
                for (int i = lastPoint.Y - 1; i >= 1; i--)
                    currentEdges.Add(new Point(0, i));
                finalPaths.Add(currentEdges);
                return;
            }

            var currentCount = currentEdges.Count;

            currentEdges.Add(new Point(lastPoint.X - 1, lastPoint.Y));
            GenerateMGraphsR(currentEdges, finalPaths);

            if (lastPoint.Y <= lastPoint.X + 1)
                return;

            var newList1 = new List<Point>(currentCount + 1);
            for (int i = 0; i < currentCount; i++)
                newList1.Add(currentEdges[i]);

            newList1.Add(new Point(lastPoint.X, lastPoint.Y - 1));
            GenerateMGraphsR(newList1, finalPaths);
        }

        private static bool[,] CopyGraph(bool[,] graph)
        {
            var newGraph = new bool[graph.GetLength(0), graph.GetLength(0)];
            for (int i = 0; i < graph.GetLength(0); i++)
                for (int j = 0; j < graph.GetLength(1); j++)
                    newGraph[i, j] = graph[i, j];

            return newGraph;
        }

        private static bool[,] GetBasicGraph(int n)
        {
            var graph = new bool[n, n];
            return graph;
            for (int i = 0; i < n - 2; i++)
                graph[i, i + 1] = graph[i + 1, i] = graph[i, i + 2] = graph[i + 2, i] = true;

            if (n >= 2)
                graph[n - 1, n - 2] = graph[n - 2, n - 1] = true;
            return graph;
        }
    }
}
