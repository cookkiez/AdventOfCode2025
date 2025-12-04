namespace AdventOfCode2025.Tasks
{
    public class Task4 : AdventTask
    {
        public Task4()
        {
            Filename += "4.txt";
        }
        public override void Solve1(string input)
        {
            var map = GetMatrixArray(input);
            long result = 0;
            for (int i = 0; i < map.Length; i++)
            {
                for(int j = 0; j < map[0].Length; j++)
                {
                    result += CanBeMoved(map, i, j);
                }
            }
            
            Console.WriteLine(result);
        }

        private int CanBeMoved(char[][] map, int i, int j)
        {
            var currentChar = map[i][j];
            if (currentChar == '@')
            {
                var papers = IsPaper(map, i, j + 1) +
                             IsPaper(map, i + 1, j) +
                             IsPaper(map, i - 1, j) +
                             IsPaper(map, i, j - 1) +
                             IsPaper(map, i + 1, j + 1) +
                             IsPaper(map, i - 1, j + 1) +
                             IsPaper(map, i + 1, j - 1) +
                             IsPaper(map, i - 1, j - 1);
                if (papers < 4)
                    return 1;
            }
            return 0;
        }

        private int IsPaper(char[][] map, int i, int j)
        {
            if (i < 0 || i >= map.Length || j < 0 || j >= map[0].Length)
                return 0;
            var c = map[i][j];
            return c == '@' ? 1 : 0;
        }

        public override void Solve2(string input)
        {
            var map = GetMatrixArray(input);
            long result = 0;
            var toBeMoved = new HashSet<(int, int)>();
            do
            {
                toBeMoved.Clear();
                for (int i = 0; i < map.Length; i++)
                {
                    for (int j = 0; j < map[0].Length; j++)
                    {
                        var canBeMoved = CanBeMoved(map, i, j);
                        result += canBeMoved;
                        if (canBeMoved == 1)
                            toBeMoved.Add((i, j));
                    }
                }
                foreach (var (i, j) in toBeMoved)
                {
                    map[i][j] = '.';
                }
            } while (toBeMoved.Count > 0);

            Console.WriteLine(result);
        }
    }
}
