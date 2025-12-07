using System.ComponentModel;
using System.Linq.Expressions;

namespace AdventOfCode2025.Tasks
{
    public class Task7 : AdventTask
    {
        public Task7()
        {
            Filename += "7.txt";
        }

        private void TestPrint(char[][] map, HashSet<(int, int)> visited)
        {
            for (var i = 0; i < map.Length; i++)
            {
                for (var j = 0; j < map[0].Length; j++)
                {
                    var c = map[i][j];
                    if (visited.Contains((i, j)))
                        Console.Write("|");
                    else
                        Console.Write(c);
                }
                Console.WriteLine();
            }
        }
        public override void Solve1(string input)
        {
            long result = 0;
            var map = GetMatrixArray(input);
            var queue = new Queue<(int row, int col)>();
            for (var i = 0; i < map.Length; i++)
            {
                for (var j = 0; j < map[0].Length; j++)
                {
                    var c = map[i][j];
                    if (c == 'S')
                    {
                        queue.Enqueue((i, j));
                        break;
                    }
                }
                if (queue.Count > 0)
                    break;
            }
            var visited = new HashSet<(int row, int col)>();
            while (queue.Count > 0)
            {
                var (row, col) = queue.Dequeue();
                if (visited.Contains((row, col)) || CheckIfIndexOutsideMatrix(map, row, col))
                    continue;
                visited.Add((row, col));
                if (!CheckIfIndexOutsideMatrix(map, row + 1, col))
                {
                    var next = map[row + 1][col];
                    if (next == '^')
                    {
                        queue.Enqueue((row + 1, col + 1));
                        queue.Enqueue((row + 1, col - 1));
                        result++;
                    }
                    else if (next == '.')
                        queue.Enqueue((row + 1, col));
                }
            }

            //TestPrint(map, visited);
            Console.WriteLine(result);
        }
        public override void Solve2(string input)
        {
            var map = GetMatrixArray(input);
            var stack = new Stack<(int row, int col)>();
            for (var i = 0; i < map.Length; i++)
            {
                for (var j = 0; j < map[0].Length; j++)
                {
                    var c = map[i][j];
                    if (c == 'S')
                    {
                        Console.WriteLine(Explore(map, i, j, new Dictionary<(int, int), long>()));
                        return;
                    }
                }
                if (stack.Count > 0)
                    break;
            }
        }

        private long Explore(char[][] map, int row, int col, Dictionary<(int, int), long> visited)
        {
            if (CheckIfIndexOutsideMatrix(map, row, col) || CheckIfIndexOutsideMatrix(map, row + 1, col))
                return 1;
            if (visited.ContainsKey((row, col)))
                return visited[(row, col)];
            
            var next = map[row + 1][col];
            long paths = 0;
            if (next == '^')
            {
                paths += Explore(map, row + 1, col + 1, visited);
                paths += Explore(map, row + 1, col - 1, visited);
            }
            else if (next == '.')
                paths += Explore(map, row + 1, col, visited);
            visited[(row, col)] = paths;
            return paths;
        }
    }
}
