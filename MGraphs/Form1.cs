using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Microsoft.Msagl.Drawing;
using Color = System.Drawing.Color;

namespace MGraphs
{
    public partial class Form1 : Form
    {
        private readonly List<Graph> _graphs = new List<Graph>();
        private int _currentIndex;

        double _lastHue;

        public static Color ColorFromHsv(double hue, double saturation, double value)
        {
            int hi = Convert.ToInt32(Math.Floor(hue / 60)) % 6;
            double f = hue / 60 - Math.Floor(hue / 60);

            value = value * 255;
            int v = Convert.ToInt32(value);
            int p = Convert.ToInt32(value * (1 - saturation));
            int q = Convert.ToInt32(value * (1 - f * saturation));
            int t = Convert.ToInt32(value * (1 - (1 - f) * saturation));

            switch (hi)
            {
                case 0:
                    return Color.FromArgb(255, v, t, p);
                case 1:
                    return Color.FromArgb(255, q, v, p);
                case 2:
                    return Color.FromArgb(255, p, v, t);
                case 3:
                    return Color.FromArgb(255, p, q, v);
                case 4:
                    return Color.FromArgb(255, t, p, v);
                default:
                    return Color.FromArgb(255, v, p, q);
            }
        }

        private Microsoft.Msagl.Drawing.Color GetRandomColor()
        {
            _lastHue += 0.618033988749895;
            if (_lastHue > 1) { _lastHue -= 1; }
            var hueAsDegree = _lastHue * 360;
            var color = ColorFromHsv(hueAsDegree, 0.5, 0.95);
            return new Microsoft.Msagl.Drawing.Color(color.R, color.G, color.B);
        }

        public Graph CreateDrawableGraph(bool[,] array, int[] colors)
        {
            var maxColor = colors.Max();

            var graph = new Graph("G " + (maxColor + 1))
            { Directed = false };
            graph.Attr.AddStyle(Style.None);

            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < i; j++)
                {
                    if (!array[i, j])
                        continue;

                    var e = graph.AddEdge(i.ToString(), j.ToString());
                    e.Attr.ArrowheadAtTarget = ArrowStyle.None;
                }
            }

            var colorings = new Microsoft.Msagl.Drawing.Color[maxColor + 1];
            for (int i = 0; i < colorings.Length; i++)
                colorings[i] = GetRandomColor();

            for (int i = 0; i < colors.Length; i++)
                graph.FindNode(i.ToString()).Attr.FillColor = colorings[colors[i]];

            return graph;
        }

        string GraphToString(bool[,] graph)
        {
            char[] chars = new char[graph.Length];
            int index = 0;
            foreach (var b in graph)
                chars[index++] = b ? '1' : '0';

            return new string(chars);
        }

        public Form1()
        {
            InitializeComponent();
            viewerB.MouseClick += ViewerB_MouseClick;

            for (int i = 7; i < 12; i++)
            {
                var list = MGraphFinder.GenerateMGraphs(i);

                foreach (var graph in list)
                {
                    var colors = new GraphColorer(graph).Colors;
                    _graphs.Add(CreateDrawableGraph(graph, colors));
                }
            }
        }

        private void ViewerB_MouseClick(object sender, MouseEventArgs e)
        {
            viewerB.Graph = _graphs[_currentIndex++];
            if (_currentIndex == _graphs.Count)
                _currentIndex = 0;
        }
    }
}
