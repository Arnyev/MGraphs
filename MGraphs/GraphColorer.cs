namespace MGraphs
{
    class GraphColorer
    {
        private readonly bool[,] _graph;

        public GraphColorer(bool[,] graph)
        {
            _graph = graph;
        }

        private int[] _colors;

        public int[] Colors
        {
            get
            {
                if (_colors != null)
                    return _colors;

                return _colors = CreateColors();
            }
        }

        private int[] CreateColors()
        {
            var colors = new int[_graph.GetLength(0)];

            for (int colorCount = 1; colorCount < _graph.GetLength(0); colorCount++)
                if (ColorWithNColors(colorCount, colors))
                    break;

            return colors;
        }

        private bool ColorWithNColors(int n, int[] colors)
        {
            for (int i = 0; i < colors.Length; i++)
                colors[i] = -1;

            colors[0] = 0;

            return ColorWithNColorsR(n, colors, 1);
        }

        private bool ColorWithNColorsR(int n, int[] colors, int vertex)
        {
            if (vertex == colors.Length)
                return true;

            for (int color = 0; color < n; color++)
            {
                bool canBeColored = true;
                for (int otherVertex = 0; otherVertex < _graph.GetLength(1); otherVertex++)
                    if (_graph[vertex, otherVertex] && colors[otherVertex] == color)
                    {
                        canBeColored = false;
                        break;
                    }

                if (!canBeColored)
                    continue;

                colors[vertex] = color;
                if (ColorWithNColorsR(n, colors, vertex + 1))
                    return true;
            }

            return false;
        }
    }
}
