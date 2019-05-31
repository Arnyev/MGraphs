using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MGraphs
{
    class GraphAdapter
    {
        public static bool[,] DeserializeGraph(string csvPath, char separator)
        {
            var file = File.ReadAllLines(csvPath);

            var nodesNumber = file.Length;
            var matrix = new bool[nodesNumber, nodesNumber];

            for (int i = 0; i < nodesNumber; i++)
            {
                var row = file[i].Split(separator);
                if (row.Length != nodesNumber)
                    throw new ArgumentException("Provided adjacency matrix is not a square matrix!");

                for (int j = 0; j < nodesNumber; j++)
                    matrix[i, j] = row[j].Trim() == "1";
            }

            return matrix;
        }

        public static void SaveGraphToFile(string filePath, char separator, bool[,] graph)
        {
            var lines = new string[graph.GetLength(0)];
            var sb = new StringBuilder();
            for (int i = 0; i < graph.GetLength(0); i++)
            {
                sb.Append(graph[i, 0] ? '1' : '0');

                for (int j = 1; j < graph.GetLength(1); j++)
                {
                    sb.Append(separator);
                    sb.Append(graph[i, j] ? '1' : '0');
                }

                lines[i] = sb.ToString();
                sb.Clear();
            }

            File.WriteAllLines(filePath, lines);
        }

        public static void SaveGraphsToDirectory(string directoryPath, char separator, bool[][,] graphs)
        {
            if (!Directory.Exists(directoryPath))
                Directory.CreateDirectory(directoryPath);

            for (var i = 0; i < graphs.Length; i++)
            {
                var graph = graphs[i];
                var path = directoryPath + '\\' + i.ToString("D4") + ".csv";
                SaveGraphToFile(path, separator, graph);
            }
        }

        public static bool[][,] LoadGraphsFromDirectory(string directoryPath, char separator, out string errors)
        {
            List<bool[,]> graphs = new List<bool[,]>();
            var sb = new StringBuilder();

            foreach (var file in Directory.EnumerateFiles(directoryPath))
            {
                try
                {
                    var graph = DeserializeGraph(file, separator);
                    graphs.Add(graph);
                }
                catch (Exception e)
                {
                    sb.AppendLine(e.Message);
                }
            }

            errors = sb.ToString();
            return graphs.ToArray();
        }
    }
}
