using System.Collections.Generic;

namespace AdventOfCode2025.Tasks
{
    public class Task5 : AdventTask
    {
        public Task5()
        {
            Filename += "5.txt";
        }

        public override void Solve1(string input)
        {
            var result = 0;
            var lines = GetLinesList(input);
            
            var foodRanges = new List<(long, long)>();
            var lineIndex = GetFoodRanges(foodRanges, 0, lines);
            lineIndex++;
            for(var i = lineIndex; i < lines.Count; i++)
            {
                var food = long.Parse(lines[i]);
                foreach(var foodRange in foodRanges)
                {
                    if (food >= foodRange.Item1 && food <= foodRange.Item2)
                    { 
                        result++; 
                        break; 
                    }
                }
            }
            Console.WriteLine(result);
        }

        private int GetFoodRanges(List<(long, long)> foodRanges, int lineIndex, List<string> lines)
        {
            while (lines[lineIndex] != "")
            {
                var range = lines[lineIndex].Split("-").Select(long.Parse);
                foodRanges.Add((range.First(), range.Last()));
                lineIndex++;
            }
            return lineIndex;
        }

        public override void Solve2(string input)
        {
            long result = -1;
            var lines = GetLinesList(input);
            var foodRanges = new List<(long, long)>();
            var linesIndex = GetFoodRanges(foodRanges, 0, lines);

            foodRanges = foodRanges.OrderBy(f => f.Item1).ToList();
            long currentFirst = foodRanges.First().Item1;
            long currentSecond = foodRanges.First().Item2;
            foreach (var foodRange in foodRanges.Skip(1))
            {
                if (foodRange.Item1 >= currentFirst && foodRange.Item1 <= currentSecond)
                    currentSecond = Math.Max(currentSecond, foodRange.Item2);
                else
                {
                    result += currentSecond - currentFirst + 1;
                    currentSecond = foodRange.Item2;
                    currentFirst = foodRange.Item1;
                }
                    
            }
            result += currentSecond - currentFirst + 1;
            Console.WriteLine(result + 1); // we lose a +1 at some point
        }
    }
}
