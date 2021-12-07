using AdventOfCode;

namespace Year2021
{
	public class Day07 : DayOption
	{
		public Day07(string day) : base(day, "The Treachery of Whales"){}
		
		public void Part1(string input)
		{
			var positions = input.Split(',').Select(n => Convert.ToInt32(n)).ToList();
			
			var start = positions.Min();
			var end = positions.Max();
			
			long leastFuel = 99999999999999;
			for (var target = start; target <= end; target++)
			{
				var fuel = 0;
				foreach (var position in positions)
				{
					fuel += Math.Abs(target - position);
				}
				if (fuel < leastFuel) leastFuel = fuel;
			}
			
			Console.WriteLine(leastFuel);
		}
		
		public void Part2(string input)
		{
			var positions = input.Split(',').Select(n => Convert.ToInt32(n)).ToList();
			
			var start = positions.Min();
			var end = positions.Max();
			
			long leastFuel = 99999999999999;
			for (var target = start; target <= end; target++)
			{
				var fuel = 0;
				foreach (var position in positions)
				{
					var distance = Math.Abs(target - position);
					for (var f = distance; f > 0; f--)
					{
						fuel += f;
					}
				}
				if (fuel < leastFuel) leastFuel = fuel;
			}
			
			Console.WriteLine(leastFuel);
		}
	}
}