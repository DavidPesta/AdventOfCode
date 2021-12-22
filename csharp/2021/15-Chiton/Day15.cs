using AdventOfCode;

namespace Year2021
{
	public class Day15 : DayOption
	{
		public Day15(string day) : base(day, "Chiton"){}
		
		public void Part1(string input)
		{
			var map = BuildMap(input);
			
			var smallestRisk = FindSmallestRisk(map, 0, 0, (long)map.Xmax, (long)map.Ymax);
			
			Console.WriteLine(smallestRisk);
		}
		
		public void Part2(string input)
		{
			var map = BuildMap(input);
			var fullMap = BuildFullMap(map);
			
			var smallestRisk = FindSmallestRisk(fullMap, 0, 0, (long)fullMap.Xmax, (long)fullMap.Ymax);
			
			Console.WriteLine(smallestRisk);
		}
		
		private long FindSmallestRisk(Map2D<Location> map, long x1, long y1, long x2, long y2)
		{
			var orderedPathRiskLocationsToPropagate = new PriorityQueue<Location, long>();
			
			map[x1, y1].PathRisk = 0;
			orderedPathRiskLocationsToPropagate.Enqueue(map[x1, y1], (long)map[x1, y1].PathRisk);
			
			while (orderedPathRiskLocationsToPropagate.Count > 0)
			{
				var location = orderedPathRiskLocationsToPropagate.Dequeue();
				
				var adjacentLocations = location.FetchAdjacentLocations(map);
				foreach (var adjacentLocation in adjacentLocations)
				{
					if (adjacentLocation.PathRisk == null || location.PathRisk + adjacentLocation.LocationRisk < adjacentLocation.PathRisk)
					{
						adjacentLocation.PathRisk = location.PathRisk + adjacentLocation.LocationRisk;
						orderedPathRiskLocationsToPropagate.Enqueue(adjacentLocation, (long)adjacentLocation.PathRisk);
					}
				}
				
				//Console.WriteLine(map.ToString());
				//Console.ReadLine();
			}
			
			return (long)map[x2, y2].PathRisk;
		}
		
		private Map2D<Location> BuildMap(string input)
		{
			var map = new Map2D<Location>();
			
			var lines = input.Split("\n");
			
			for (var y = 0; y < lines.Count(); y++)
			{
				for (var x = 0; x < lines[y].Length; x++)
				{
					map[x, y] = new Location(x, y, int.Parse(lines[y][x].ToString()));
				}
			}
			
			return map;
		}
		
		private Map2D<Location> BuildFullMap(Map2D<Location> map)
		{
			var fullMap = new Map2D<Location>();
			
			for (var y = 0; y <= map.Ymax; y++)
			{
				for (var x = 0; x <= map.Xmax; x++)
				{
					for (var i = 0; i <= 4; i++)
					{
						for (var j = 0; j <= 4; j++)
						{
							var newX = x + i*map.Xsize();
							var newY = y + j*map.Ysize();
							var newLocationRisk = map[x, y].LocationRisk + i + j;
							if (newLocationRisk > 9) newLocationRisk -= 9;
							fullMap[newX, newY] = new Location(newX, newY, newLocationRisk);
						}
					}
				}
			}
			
			return fullMap;
		}
	}
}