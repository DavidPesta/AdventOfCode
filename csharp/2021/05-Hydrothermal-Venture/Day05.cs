using AdventOfCode;

namespace Year2021
{
	public class Day05 : DayOption
	{
		public Day05(string day) : base(day, "Hydrothermal Venture"){}
		
		public void Part1(string input)
		{
			var map = new Dictionary<int, Dictionary<int, int>>();
			
			var lines = input.Split("\n");
			
			foreach (var line in lines)
			{
				var pairs = line.Split(" -> ");
				var startPair = pairs[0].Split(",").Select(n => Convert.ToInt32(n)).ToList();
				var endPair = pairs[1].Split(",").Select(n => Convert.ToInt32(n)).ToList();
				
				if (startPair[0] == endPair[0])
				{
					int x = startPair[0];
					
					int startY;
					int endY;
					if (startPair[1] < endPair[1])
					{
						startY = startPair[1];
						endY = endPair[1];
					}
					else
					{
						startY = endPair[1];
						endY = startPair[1];
					}
					
					for (var y = startY; y <= endY; y++)
					{
						if (!map.ContainsKey(x)) map[x] = new Dictionary<int, int>();
						if (!map[x].ContainsKey(y)) map[x][y] = 0;
						map[x][y]++;
					}
				}
				
				if (startPair[1] == endPair[1])
				{
					int y = startPair[1];
					
					int startX;
					int endX;
					if (startPair[0] < endPair[0])
					{
						startX = startPair[0];
						endX = endPair[0];
					}
					else
					{
						startX = endPair[0];
						endX = startPair[0];
					}
					
					for (var x = startX; x <= endX; x++)
					{
						if (!map.ContainsKey(x)) map[x] = new Dictionary<int, int>();
						if (!map[x].ContainsKey(y)) map[x][y] = 0;
						map[x][y]++;
					}
				}
			}
			
			var numGreaterThanOne = 0;
			foreach (KeyValuePair<int, Dictionary<int, int>> kvp in map)
			{
				var x = kvp.Key;
				var ydict = kvp.Value;
				foreach (KeyValuePair<int, int> kvp2 in ydict)
				{
					var y = kvp2.Key;
					var numTimes = kvp2.Value;
					if (numTimes > 1) numGreaterThanOne++;
				}
			}
			
			Console.WriteLine(numGreaterThanOne);
		}
		
		public void Part2(string input)
		{
			var map = new Dictionary<int, Dictionary<int, int>>();
			
			var lines = input.Split("\n");
			
			foreach (var line in lines)
			{
				var pairs = line.Split(" -> ");
				var startPair = pairs[0].Split(",").Select(n => Convert.ToInt32(n)).ToList();
				var endPair = pairs[1].Split(",").Select(n => Convert.ToInt32(n)).ToList();
				
				if (startPair[0] == endPair[0])
				{
					int x = startPair[0];
					
					int startY;
					int endY;
					if (startPair[1] < endPair[1])
					{
						startY = startPair[1];
						endY = endPair[1];
					}
					else
					{
						startY = endPair[1];
						endY = startPair[1];
					}
					
					for (var y = startY; y <= endY; y++)
					{
						if (!map.ContainsKey(x)) map[x] = new Dictionary<int, int>();
						if (!map[x].ContainsKey(y)) map[x][y] = 0;
						map[x][y]++;
					}
				}
				
				if (startPair[1] == endPair[1])
				{
					int y = startPair[1];
					
					int startX;
					int endX;
					if (startPair[0] < endPair[0])
					{
						startX = startPair[0];
						endX = endPair[0];
					}
					else
					{
						startX = endPair[0];
						endX = startPair[0];
					}
					
					for (var x = startX; x <= endX; x++)
					{
						if (!map.ContainsKey(x)) map[x] = new Dictionary<int, int>();
						if (!map[x].ContainsKey(y)) map[x][y] = 0;
						map[x][y]++;
					}
				}
				
				if (startPair[0] != endPair[0] && startPair[1] != endPair[1])
				{
					var x1 = startPair[0];
					var y1 = startPair[1];
					var x2 = endPair[0];
					var y2 = endPair[1];
					
					var curX = x1;
					var curY = y1;
					while (curX != x2 && curY != y2)
					{
						if (!map.ContainsKey(curX)) map[curX] = new Dictionary<int, int>();
						if (!map[curX].ContainsKey(curY)) map[curX][curY] = 0;
						map[curX][curY]++;
						
						if (x2 > curX) curX++;
						else curX--;
						if (y2 > curY) curY++;
						else curY--;
					}
					if (!map.ContainsKey(curX)) map[curX] = new Dictionary<int, int>();
					if (!map[curX].ContainsKey(curY)) map[curX][curY] = 0;
					map[curX][curY]++;
				}
			}
			
			var numGreaterThanOne = 0;
			foreach (KeyValuePair<int, Dictionary<int, int>> kvp in map)
			{
				var x = kvp.Key;
				var ydict = kvp.Value;
				foreach (KeyValuePair<int, int> kvp2 in ydict)
				{
					var y = kvp2.Key;
					var numTimes = kvp2.Value;
					if (numTimes > 1) numGreaterThanOne++;
				}
			}
			
			Console.WriteLine(numGreaterThanOne);
		}
	}
}