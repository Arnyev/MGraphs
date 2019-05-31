using System;
using System.Collections.Generic;
using System.Linq;

namespace MGraphs
{
    class GraphColorer
    {
        private readonly bool[,] _graph;
        private readonly ulong[] _inverseVertexNeighbours;

        public GraphColorer(bool[,] graph)
        {
            _graph = graph;
            _inverseVertexNeighbours = new ulong[graph.GetLength(0)];

            var mask = ~0ul >> (64 - graph.GetLength(0));
            for (int i = 0; i < graph.GetLength(0); i++)
            {
                var neighbours = 0ul;
                for (int j = 0; j < graph.GetLength(1); j++)
                    if (_graph[i, j] || i == j)
                        neighbours |= 1ul << j;

                _inverseVertexNeighbours[i] = mask & ~neighbours;
            }
        }

        private int[] _colors;

        public int[] Colors
        {
            get
            {
                if (_colors != null)
                    return _colors;

                _colors = CreateColors();

                return _colors;
            }
        }

        public bool CheckColors(int[] colors)
        {
            var colorGroups = colors
                .Select((x, i) => Tuple.Create(x, i))
                .GroupBy(t => t.Item1)
                .Select(g => g.Select(t => t.Item2).ToArray())
                .ToArray();

            foreach (var colorGroup in colorGroups)
                foreach (var v in colorGroup)
                    foreach (var u in colorGroup)
                        if (_graph[v, u])
                            return false;

            return true;
        }

        private int[] CreateColors()
        {
            var independentSets = IndependentSets();

            var arraysize = 1ul << _graph.GetLength(0);
            var neededColors = new int[arraysize];
            var remainingSets = new ulong[arraysize];

            for (ulong i = 1; i < arraysize; i++)
                neededColors[i] = int.MaxValue;

            foreach (var set in independentSets)
                neededColors[set] = 1;

            for (ulong set = 1ul; set < arraysize; set++)
            {
                if (neededColors[set] != int.MaxValue)
                    continue;

                int leastColors = int.MaxValue;
                ulong leastColorsSet = 0;
                foreach (var independentSet in independentSets)
                {
                    var inverse = ~independentSet;
                    var rest = set & inverse;
                    if (neededColors[rest] < leastColors)
                    {
                        leastColors = neededColors[rest];
                        leastColorsSet = rest;
                    }
                }

                neededColors[set] = leastColors + 1;
                remainingSets[set] = leastColorsSet;
            }

            var colorSets = GetColorSets(remainingSets);
            var colors = GetColors(colorSets);

            return colors;
        }

        private List<ulong> IndependentSets()
        {
            var available = 0ul;
            for (int i = 0; i < _graph.GetLength(0); i++)
                available |= 1ul << i;

            var cliques = new List<ulong>();
            BronKerbosh(0ul, available, 0ul, cliques);
            return cliques;
        }

        private int[] GetColors(List<ulong> colorSets)
        {
            var colors = new int[_graph.GetLength(0)];

            int currentColor = 0;
            foreach (var colorSet in colorSets)
            {
                for (int i = 0; i < _graph.GetLength(0); i++)
                    if ((colorSet & (1ul << i)) != 0)
                        colors[i] = currentColor;

                currentColor++;
            }

            return colors;
        }

        private static List<ulong> GetColorSets(ulong[] remainingSets)
        {
            var colorSets = new List<ulong>();

            var currentSet = (ulong)remainingSets.LongLength - 1;
            while (currentSet != 0)
            {
                var rest = remainingSets[currentSet];
                var colorSet = currentSet & ~rest;
                colorSets.Add(colorSet);
                currentSet = rest;
            }

            return colorSets;
        }

        void BronKerbosh(ulong current, ulong available, ulong skipped, List<ulong> maximalCliques)
        {
            if (available == 0ul && skipped == 0ul)
                maximalCliques.Add(current);

            if (available == 0ul)
                return;

            for (int vertex = 0; vertex < _inverseVertexNeighbours.Length; vertex++)
            {
                var vertexFlag = 1ul << vertex;
                if ((vertexFlag & available) == 0ul)
                    continue;

                var neighbours = _inverseVertexNeighbours[vertex];
                BronKerbosh(current | vertexFlag, available & neighbours, skipped & neighbours, maximalCliques);
                available = available ^ vertexFlag;
                skipped = skipped | vertexFlag;
            }
        }
    }
}
