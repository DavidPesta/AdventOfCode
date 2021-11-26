using AdventOfCode;

namespace Year2015
{
	public class Day01 : DayOption
	{
		public Day01(string day) : base(day, "Not Quite Lisp"){}
		
		public void Part1(string input)
		{
			var floor = 0;
			
			foreach (var instruction in input)
			{
				if (instruction == '(') floor++;
				else floor--;
			}
			
			Console.WriteLine(floor);
		}
		
		public void Part2(string input)
		{
			var floor = 0;
			
			var step = 0;
			foreach (var instruction in input)
			{
				step++;
				if (instruction == '(') floor++;
				else floor--;
				if (floor == -1) break;
			}
			
			Console.WriteLine(step);
		}
	}
}