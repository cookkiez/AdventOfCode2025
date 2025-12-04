using System.Text.RegularExpressions;

namespace AdventOfCode2025.Tasks
{
    public class Task2 : AdventTask
    {
        public Task2()
        {
            Filename += "2.txt";
        }
        public override void Solve1(string input)
        {
            var ranges = input.Split(",");
            long sum = 0;
            foreach(var range in ranges)
            {
                var bounds = range.Split("-").Select(long.Parse).ToArray();
                for(long num = bounds[0]; num <= bounds[1]; num++)
                {
                    var stringNum = num.ToString();
                    //Console.WriteLine(num);
                    if (stringNum.Length % 2 != 0)
                        continue;
                    var first = stringNum.Substring(0, stringNum.Length / 2);
                    var second = stringNum.Substring(stringNum.Length / 2);
                    if (first == second)
                        sum += num;
                }
            }
            Console.WriteLine($"REsult: {sum}");
        }

        public override void Solve2(string input)
        {
            var ranges = input.Split(",");
            long sum = 0;
            var regex = new Regex("^(\\d+)\\1+$");
            foreach (var range in ranges)
            {
                var bounds = range.Split("-").Select(long.Parse).ToArray();
                for (long num = bounds[0]; num <= bounds[1]; num++)
                {
                    var stringNum = num.ToString();
                    if (regex.IsMatch(stringNum))
                        sum += num;
                }
            }
            Console.WriteLine($"REsult: {sum}");
        }
    }
}
