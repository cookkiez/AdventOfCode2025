using System.Text.RegularExpressions;

namespace AdventOfCode2025.Tasks
{
    public class Task1 : AdventTask
    {
        public Task1()
        {
            Filename += "1.txt";
        }

        public override void Solve1(string input)
            => Console.WriteLine($"Result : {SolveBoth(input, false)}");

        private int SolveBoth(string input, bool partTwo)
        {
            var splitted = GetLinesList(input);
            var zeroes = 0;
            var currentPoint = 50;
            var regex = new Regex("^([LR])(\\d+)$");
            foreach (var line in splitted)
            {
                var match = regex.Match(line);
                var direction = match.Groups[1].Value;
                var steps = int.Parse(match.Groups[2].Value);
                if (partTwo)
                    zeroes += steps / 100;
                steps = steps % 100;
                if (direction == "L")
                {
                    if (steps > currentPoint)
                    {
                        if (currentPoint != 0 && partTwo)
                            zeroes++;
                        currentPoint = 100 - Math.Abs(currentPoint - steps);
                    }
                    else
                        currentPoint = currentPoint - steps;
                }
                else
                {
                    var currentSum = currentPoint + steps;
                    if (currentSum > 100 && partTwo)
                        zeroes++;
                    currentPoint = currentSum % 100;
                }
                currentPoint = Math.Abs(currentPoint);
                if (currentPoint == 0)
                    zeroes++;
                //Console.WriteLine($"{currentPoint}, {line}, {zeroes}");
            }
            return zeroes;
        }

        public override void Solve2(string input)
            => Console.WriteLine($"Result : {SolveBoth(input, true)}");
    }
}
