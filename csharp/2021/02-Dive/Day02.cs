using AdventOfCode;

namespace Year2021
{
	public class Day02 : DayOption
	{
		public Day02(string day) : base(day, "Dive!"){}
		
		public void Part1(string input)
		{
			var lines = input.Split('\n');
			
			var position = 0;
			var depth = 0;
			foreach (var line in lines)
			{
				var stuff = line.Split(' ');
				var command = stuff[0];
				var num = int.Parse(stuff[1]);
				
				if (command == "forward") position += num;
				if (command == "down") depth += num;
				if (command == "up") depth -= num;
			}
			
			Console.WriteLine(position * depth);
		}
		
		public void Part2(string input)
		{
			var lines = input.Split('\n');
			
			var position = 0;
			var depth = 0;
			var aim = 0;
			foreach (var line in lines)
			{
				var stuff = line.Split(' ');
				var command = stuff[0];
				var num = int.Parse(stuff[1]);
				
				if (command == "down") aim += num;
				if (command == "up") aim -= num;
				if (command == "forward")
				{
					depth += aim * num;
					position += num;
				}
			}
			
			Console.WriteLine(position * depth);
		}
	}
}