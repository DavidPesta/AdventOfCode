using AdventOfCode;

namespace Year2021
{
	public class Day17 : DayOption
	{
		public Day17(string day) : base(day, "Trick Shot"){}
		
		public void Part1(string input)
		{
			var target = new Target(input);
			var solver = new Day17Solver(target);
			
			var highestProbePosition = solver.FindHighestProbePositionWhenLaunchedWithStyle();
			
			Console.WriteLine(highestProbePosition);
		}
		
		public void Part2(string input)
		{
			var target = new Target(input);
			var solver = new Day17Solver(target);
			
			var initialVelocities = solver.FindInitialVelocitiesThatHitTarget();
			
			Console.WriteLine(initialVelocities.Count());
		}
	}
}