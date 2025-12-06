namespace AdventOfCode2025.Tasks
{
    public class Task6 : AdventTask
    {
        public Task6()
        {
            Filename += "6.txt";
        }

        public override void Solve1(string input)
        {
            long result = 0;
            var lines = GetLinesArray(input);
            var operations = lines.Last().Split(" ").Select(s => s.Trim()).Where(s => s != "");
            // Gets numbers left to right and top to bottom, they are then agregated based on the operator
            var numberLines = lines.Take(lines.Length - 1)
                .Select(l => l.Split(" ").Where(s => long.TryParse(s, out var _)).Select(s => long.Parse(s.Trim())).ToArray())
                .ToArray();
            for (int i = 0; i < operations.Count(); i++)
            {
                var op = operations.ElementAt(i);
                Func<long, long, long> calcFunc = op switch
                {
                    "+" => (a, b) => a + b,
                    "*" => (a, b) => a * b,
                    _ => throw new Exception("Unknown operation")
                };
                var currSum = numberLines.Select(nl => nl[i]).Aggregate(calcFunc);
                result += currSum;
            }
            Console.WriteLine(result);
        }

        public override void Solve2(string input)
        {
            long result = 0;
            var lines = GetMatrixArrayNoTrim(input);
            var maxCols = lines.Select(c => c.Length).Max();
            var currentNumbers = new long[maxCols];
            // Iterate from right to left and top to bottom
            for (var j = maxCols - 1; j >= 0; j--)
            {
                for (var i = 0; i < lines.Length; i++)
                {
                    // some lines are not as long as others so we need to make sure we are inside the bounds
                    if (!(i >= 0 && i < lines.Length && j >= 0 && j < lines[i].Length))
                        continue;

                    // Check if we have an operator or a number
                    // if there is an operator we calculate it and reset current numbers
                    // otherwise we build the current number (top to bottom)
                    var ch = lines[i][j];
                    if (ch == '+')
                    {
                        result += currentNumbers.Sum();
                        currentNumbers = new long[maxCols];
                    }
                    else if (ch == '*')
                    {
                        result += currentNumbers.Where(n => n != 0).Aggregate((a, b) => a * b);
                        currentNumbers = new long[maxCols];
                    }
                    else if (char.IsDigit(ch))
                    {
                        var number = long.Parse(ch.ToString());
                        currentNumbers[j] = currentNumbers[j] * 10 + number;
                    }
                }
            }
            Console.WriteLine(result);
        }
    }
}
