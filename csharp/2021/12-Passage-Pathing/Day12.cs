using AdventOfCode;

namespace Year2021
{
	public class Day12 : DayOption
	{
		public Day12(string day) : base(day, "Passage Pathing"){}
		
		Dictionary<string, Cave> Caves;
		
		public void Part1(string input)
		{
			Caves = LoadCaves(input);
			
			var paths = new List<string>() { "start" };
			Caves["start"].FindPathsToEndPart1(paths, 0);
			
			Console.WriteLine(paths.Count());
		}
		
		public void Part2(string input)
		{
			Caves = LoadCaves(input);
			
			var paths = new List<string>() { "start" };
			Caves["start"].FindPathsToEndPart2(paths, 0);
			
			Console.WriteLine(paths.Count());
		}
		
		private Dictionary<string, Cave> LoadCaves(string input)
		{
			var caves = new Dictionary<string, Cave>();
			
			var lines = input.Split("\n");
			
			foreach (var caveConnection in lines)
			{
				var caveNames = caveConnection.Split("-");
				
				Cave cave0;
				Cave cave1;
				
				if (caves.ContainsKey(caveNames[0])) cave0 = caves[caveNames[0]];
				else
				{
					cave0 = new Cave(caveNames[0]);
					caves[caveNames[0]] = cave0;
				}
				
				if (caves.ContainsKey(caveNames[1])) cave1 = caves[caveNames[1]];
				else
				{
					cave1 = new Cave(caveNames[1]);
					caves[caveNames[1]] = cave1;
				}
				
				cave0.AddAdjacentCave(cave1);
				cave1.AddAdjacentCave(cave0);
			}
			
			return caves;
		}
	}
}