using System.Text.RegularExpressions;

namespace AdventOfCode2025.Tasks
{
    public class Task3 : AdventTask
    {
        public Task3()
        {
            Filename += "3.txt";
        }

        private Dictionary<int, long> BestSums = new Dictionary<int, long>();

        public override void Solve1(string input)
        {
            var lines = GetLinesList(input);
            long result = 0;

            foreach(var line in lines)
            {
                var nums = line.ToCharArray().Select(char.GetNumericValue);
                var largestSum = 0;
                for(int i = 0; i < nums.Count() - 1; i++)
                {
                    for (int j = i + 1; j < nums.Count(); j++)
                    {
                        var sum = long.Parse(string.Join("", new List<double> { nums.ElementAt(i), nums.ElementAt(j) }));
                        if (sum > largestSum)
                            largestSum = (int)sum;
                    }
                }
                result += largestSum;
            }
            Console.WriteLine(result);
        }

        private long GetLargestSum(long currentSum, int depth, IEnumerable<double> nums, int index,
            Dictionary<int, long> bestSums)
        {
            if (depth == 0)
                return currentSum;
            long largestSum = bestSums.ContainsKey(depth) ? bestSums[depth] : 0;
            var previousNum = 0;
            for (int i = index; i < nums.Count(); i++)
            {
                var currentNum = nums.ElementAt(i);
                var newSum = long.Parse(string.Join("", currentSum.ToString(), currentNum.ToString()));
                if (previousNum > currentNum || (bestSums.ContainsKey(depth) && bestSums[depth] > newSum))
                    continue;
                var candidateSum = GetLargestSum(newSum, depth - 1, nums, i + 1, bestSums);
                if (candidateSum > largestSum)
                {
                    largestSum = candidateSum;
                    bestSums[depth] = newSum;
                }
                previousNum = (int)currentNum;
            }
            //bestSums[depth] = largestSum;
            return largestSum;
        }

        public override void Solve2(string input)
        {
            var lines = GetLinesList(input);
            long result = 0;
            var l = 0;
            foreach (var line in lines)
            {
                var nums = line.ToCharArray().Select(char.GetNumericValue);
                var currentSum = GetLargestSum(0, 12, nums, 0, new Dictionary<int, long>());
                result += currentSum;
                Console.WriteLine(l);
                l++;
            }
            Console.WriteLine(result);
        }
    }
}
