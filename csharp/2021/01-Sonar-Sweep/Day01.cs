using AdventOfCode;

namespace Year2021
{
	public class Day01 : DayOption
	{
		public Day01(string day) : base(day, "Sonar Sweep"){}
		
		public void Part1(string input)
		{
			var depths = input.Split('\n').Select(d => Convert.ToInt32(d)).ToList();
			
			var increases = 0;
			for (var i = 1; i < depths.Count(); i++)
			{
				if (depths[i] > depths[i-1]) increases++;
			}
			
			Console.WriteLine(increases);
		}
		
		public void Part2(string input)
		{
			var depths = input.Split('\n').Select(d => Convert.ToInt32(d)).ToList();
			
			var increases = 0;
			for (var i = 3; i < depths.Count(); i++)
			{
				if (depths[i-2] + depths[i-1] + depths[i] > depths[i-3] + depths[i-2] + depths[i-1]) increases++;
			}
			
			Console.WriteLine(increases);
		}
	}
}