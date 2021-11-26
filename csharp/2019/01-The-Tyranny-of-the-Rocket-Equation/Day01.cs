using AdventOfCode;

namespace Year2019
{
	public class Day01 : DayOption
	{
		public Day01(string day) : base(day, "The Tyranny of the Rocket Equation"){}
		
		public void Part1(string input)
		{
			var componentMasses = input.Split('\n').Select(n => int.Parse(n));
			
			var totalFuel = 0;
			foreach (var componentMass in componentMasses)
			{
				totalFuel += CalcFuelToLaunchMass(componentMass);
			}
			
			Console.WriteLine(totalFuel);
		}
		
		public void Part2(string input)
		{
			var componentMasses = input.Split('\n').Select(n => int.Parse(n));
			
			var totalFuel = 0;
			foreach (var componentMass in componentMasses)
			{
				var mass = componentMass;
				
				do {
					mass = CalcFuelToLaunchMass(mass);
					totalFuel += mass;
				} while(mass > 0);
			}
			
			Console.WriteLine(totalFuel);
		}
		
		private int CalcFuelToLaunchMass(int mass)
		{
			var fuel = (mass / 3) - 2;
			if (fuel < 0) fuel = 0;
			return fuel;
		}
	}
}