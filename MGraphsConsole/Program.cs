using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MGraphsConsole
{
    class Program
    {
        static string _helpText =
            "MGraphs\n" +
            "Valid options:\n" +
            "\t--generate-graph=filename\n" +
            "\t--generate-directory=directory\n" +
            "\t--generate-vertices=vertices count\n" +
            "\t--input-graph=filename\n" +
            "\t--input-directory=directory\n" +
            "\t--help";
        static HashSet<string> _knownOpts = new HashSet<string>(new string[]{
            "generate-graph",
            "generate-directory",
            "generate-vertices",
            "input-graph",
            "input-directory",
            "help",
            });
        static void Main(string[] args)
        {
            try
            {
                var opt = CommandLineParser.Parse(args);
                if (opt.Keys.Any(k => !_knownOpts.Contains(k)))
                    throw new Exception("Unknown option.");
                if (opt.ContainsKey("help") || opt.Count == 0)
                {
                    Console.WriteLine(_helpText);
                    return;
                }
                bool generate_file = opt.TryGetValue("generate-graph", out string filename);
                bool generate_dir = opt.TryGetValue("generate-directory", out string directory);
                if (generate_file && generate_dir)
                    throw new Exception("Choose one generate-graph or generate-directory.");
                if (generate_file || generate_dir)
                {
                    if (!opt.TryGetValue("generate-vertices", out string verticesStr))
                        throw new Exception("Please provide number of vertices");
                    int vertices = int.Parse(verticesStr);
                    if (generate_file)
                        GraphAdapter.SaveGraphToFile(filename, ',', MGraphFinder.GenerateMGraph(vertices));
                    else
                        GraphAdapter.SaveGraphsToDirectory(directory, ',', MGraphFinder.GenerateMGraphs(vertices).ToArray());
                }
                bool input_file = opt.TryGetValue("input-graph", out filename);
                bool input_dir = opt.TryGetValue("input-directory", out directory);
                if (input_file && input_dir)
                    throw new Exception("Choose one input-graph or input-directory.");
                if (input_file || input_dir)
                {
                    if (input_file)
                    {
                        var graph = GraphAdapter.DeserializeGraph(filename, ',');
                        var color = new GraphColorer(graph);
                        color.SaveColoringToFile("coloring_" + filename);
                    }
                    else
                    {
                        var graphs = GraphAdapter.LoadGraphsFromDirectory(directory, ',', out string errors, out string[] filenames);
                        if (!string.IsNullOrEmpty(errors))
                        {
                            Console.WriteLine("Following errors occured while reading files:");
                            Console.WriteLine(errors);
                        }
                        directory = "coloring_" + directory;
                        if (!Directory.Exists(directory))
                            Directory.CreateDirectory(directory);
                        var sb = new StringBuilder();
                        for (int i = 0; i < graphs.Length; ++i)
                        {
                            try
                            {
                                var color = new GraphColorer(graphs[i]);
                                color.SaveColoringToFile("coloring_" + filenames[i]);
                            }
                            catch (Exception e)
                            {
                                sb.AppendLine(e.Message);
                            }
                        }
                        errors = sb.ToString();
                        if (!string.IsNullOrEmpty(errors))
                        {
                            Console.WriteLine("Following errors occured while coloring:");
                            Console.WriteLine(errors);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(_helpText);
            }
        }
    }
}
