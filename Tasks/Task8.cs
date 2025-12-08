namespace AdventOfCode2025.Tasks
{
    public class Task8 : AdventTask
    {
        public Task8()
        {
            Filename += "8.txt";
        }

        public override void Solve1(string input)
        {
            var lines = GetLinesList(input);
            var distances = GetDistances(lines);
            distances = distances.Take(1000).ToList();

            var hashes = new List<HashSet<(long, long, long)>>();
            var lastPair = CalculateConnectedBoxes(distances, hashes, lines.Count, false);
            Console.WriteLine(hashes.Select(h => h.Count).OrderDescending().Take(3).Aggregate((a, b) => a * b));
        }

        public override void Solve2(string input)
        {
            var lines = GetLinesList(input);
            var distances = GetDistances(lines);

            var lastPair = CalculateConnectedBoxes(distances, new List<HashSet<(long, long, long)>>(), lines.Count, true);
            Console.WriteLine(lastPair.Item1.Item1 * lastPair.Item2.Item1);
        }

        private long Distance((long, long, long) a, (long, long, long) b)
            => (long)Math.Sqrt(Math.Pow(a.Item1 - b.Item1, 2) + Math.Pow(a.Item2 - b.Item2, 2) + Math.Pow(a.Item3 - b.Item3, 2));

        private IEnumerable<((long, long, long), (long, long, long), long)> GetDistances(List<string> lines)
        {
            var distances = new List<((long, long, long), (long, long, long), long)>();
            for (int i = 0; i < lines.Count; i++)
            {
                var line = lines[i].Split(",").Select(long.Parse).ToArray();
                var first = (line[0], line[1], line[2]);
                for (int j = i + 1; j < lines.Count; j++)
                {
                    if (j == i)
                        continue;
                    var line2 = lines[j].Split(",").Select(long.Parse).ToArray();
                    var second = (line2[0], line2[1], line2[2]);
                    var dist = Distance(first, second);
                    distances.Add((first, second, dist));
                }
            }
            return distances.DistinctBy(d => (d.Item1, d.Item2)).OrderBy(d => d.Item3);
        }

        private ((long, long, long), (long, long, long)) CalculateConnectedBoxes(
            IEnumerable<((long, long, long), (long, long, long), long)> distances, List<HashSet<(long, long, long)>> hashes, 
            int numberOfBoxes, bool breakIfOneCircuit)
        {
            var lastPair = ((0L, 0L, 0L), (0L, 0L, 0L));
            foreach (var dist in distances)
            {
                var first = hashes.FirstOrDefault(h => h.Contains(dist.Item1));
                var second = hashes.FirstOrDefault(h => h.Contains(dist.Item2));
                if (first == null && second == null)
                {
                    var hash = new HashSet<(long, long, long)> { dist.Item1, dist.Item2 };
                    hashes.Add(hash);
                }
                else if (first != null && second == null)
                {
                    first.Add(dist.Item2);
                }
                else if (first == null && second != null)
                {
                    second.Add(dist.Item1);
                }
                else if (first != null && second != null && first != second)
                {
                    first.UnionWith(second);
                    hashes.Remove(second);
                }
                lastPair = (dist.Item1, dist.Item2);
                if (breakIfOneCircuit && hashes.Count == 1 && hashes.First().Count == numberOfBoxes)
                    break;
            }
            return lastPair;
        }
    }
}
