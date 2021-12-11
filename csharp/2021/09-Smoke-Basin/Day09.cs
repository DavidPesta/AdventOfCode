using AdventOfCode;

namespace Year2021
{
	public class Day09 : DayOption
	{
		public Day09(string day) : base(day, "Smoke Basin"){}
		
		public void Part1(string input)
		{
			var heightMap = BuildHeightMap(input);
			
			var output = Color.Normal;
			var lowPoints = new List<int>();
			for (var i = 0; i < heightMap.Count(); i++)
			{
				for (var j = 0; j < heightMap[i].Count(); j++)
				{
					var isLowPoint = true;
					if (i > 0 && heightMap[i - 1][j] <= heightMap[i][j]) isLowPoint = false;
					if (j > 0 && heightMap[i][j - 1] <= heightMap[i][j]) isLowPoint = false;
					if (i < heightMap.Count()-1 && heightMap[i + 1][j] <= heightMap[i][j]) isLowPoint = false;
					if (j < heightMap[i].Count()-1 && heightMap[i][j + 1] <= heightMap[i][j]) isLowPoint = false;
					if (isLowPoint == true)
					{
						output += Color.Red + heightMap[i][j].ToString() + Color.Normal;
						lowPoints.Add(heightMap[i][j]);
					}
					else
					{
						output += heightMap[i][j].ToString();
					}
				}
				output += "\n";
			}
			
			//Console.WriteLine(output);
			
			var riskScore = 0;
			foreach (var lowPoint in lowPoints)
			{
				riskScore += lowPoint + 1;
			}
			
			Console.WriteLine(riskScore);
		}
		
		private Dictionary<int, Dictionary<int, int>> HeightMap;
		private Dictionary<int, List<string>> BasinLocations;
		private Dictionary<string, int> LocationBasin;
		
		public void Part2(string input)
		{
			Way1(input);
			Way2(input);
		}
		
		private void Way1(string input)
		{
			HeightMap = BuildHeightMap(input);
			
			BasinLocations = new Dictionary<int, List<string>>();
			LocationBasin = new Dictionary<string, int>();
			var nextBasin = 0;
			
			var output = Color.Normal;
			for (var i = 0; i < HeightMap.Count(); i++)
			{
				for (var j = 0; j < HeightMap[i].Count(); j++)
				{
					if (HeightMap[i][j] < 9)
					{
						var adjacentBasin = FindAdjacentBasin(i, j);
						
						if (adjacentBasin == null) adjacentBasin = nextBasin;
						LocationBasin[$"{i},{j}"] = (int)adjacentBasin;
						if (!BasinLocations.ContainsKey((int)adjacentBasin)) BasinLocations[(int)adjacentBasin] = new List<string>();
						BasinLocations[(int)adjacentBasin].Add($"{i},{j}");
						
						output += $"{Color.Red}*{Color.Normal}";
					}
					else
					{
						nextBasin++;
						output += "*";
					}
				}
				nextBasin++;
				output += "\n";
			}
			
			//Console.WriteLine(output);
			
			//DisplayMap();
			
			Console.WriteLine($"Way 1: {CalculateFinalAnswer()}");
		}
		
		private void Way2(string input)
		{
			HeightMap = BuildHeightMap(input);
			
			BasinLocations = new Dictionary<int, List<string>>();
			LocationBasin = new Dictionary<string, int>();
			var nextBasin = 0;
			
			for (var i = 0; i < HeightMap.Count(); i++)
			{
				for (var j = 0; j < HeightMap[i].Count(); j++)
				{
					if (HeightMap[i][j] < 9)
					{
						if (!LocationBasin.ContainsKey($"{i},{j}"))
						{
							FloodFill(i, j, nextBasin);
							nextBasin++;
						}
					}
				}
			}
			
			//DisplayMap();
			
			Console.WriteLine($"Way 2: {CalculateFinalAnswer()}");
		}
		
		private void FloodFill(int i, int j, int basin)
		{
			AddLocationToBasin(i, j, basin);
			
			var propagators = new Queue<(int, int)>();
			propagators.Enqueue((i, j));
			
			while (propagators.Count > 0)
			{
				var propagator = propagators.Dequeue();
				
				var pi = propagator.Item1;
				var pj = propagator.Item2;
				
				if (pi > 0 && HeightMap[pi-1][pj] < 9 && !LocationBasin.ContainsKey($"{pi-1},{pj}"))
				{
					AddLocationToBasin(pi-1, pj, basin);
					propagators.Enqueue((pi-1, pj));
				}
				
				if (pj > 0 && HeightMap[pi][pj-1] < 9 && !LocationBasin.ContainsKey($"{pi},{pj-1}"))
				{
					AddLocationToBasin(pi, pj-1, basin);
					propagators.Enqueue((pi, pj-1));
				}
				
				if (pi < HeightMap.Count()-1 && HeightMap[pi+1][pj] < 9 && !LocationBasin.ContainsKey($"{pi+1},{pj}"))
				{
					AddLocationToBasin(pi+1, pj, basin);
					propagators.Enqueue((pi+1, pj));
				}
				
				if (pj < HeightMap[pi].Count()-1 && HeightMap[pi][pj+1] < 9 && !LocationBasin.ContainsKey($"{pi},{pj+1}"))
				{
					AddLocationToBasin(pi, pj+1, basin);
					propagators.Enqueue((pi, pj+1));
				}
			}
		}
		
		private void AddLocationToBasin(int i, int j, int basin)
		{
			LocationBasin[$"{i},{j}"] = basin;
			if (!BasinLocations.ContainsKey(basin)) BasinLocations[basin] = new List<string>();
			BasinLocations[basin].Add($"{i},{j}");
		}
		
		private void DisplayMap()
		{
			var numberedOutput = Color.Normal;
			for (var i = 0; i < HeightMap.Count(); i++)
			{
				for (var j = 0; j < HeightMap[i].Count(); j++)
				{
					if (HeightMap[i][j] < 9)
					{
						// Valid ASCII display range: 33-126
						var c = (char)(LocationBasin[$"{i},{j}"] % 93 + 33);
						numberedOutput += $"{Color.Red}{c}{Color.Normal}";
					}
					else
					{
						numberedOutput += "*";
					}
				}
				numberedOutput += "\n";
			}
			Console.WriteLine(numberedOutput);
		}
		
		private int? FindAdjacentBasin(int i, int j)
		{
			int? adjacentBasin = null;
			
			if (j > 0 && LocationBasin.ContainsKey($"{i},{j-1}"))
			{
				adjacentBasin = LocationBasin[$"{i},{j-1}"];
			}
			
			if (i > 0 && LocationBasin.ContainsKey($"{i-1},{j}"))
			{
				if (adjacentBasin == null)
				{
					adjacentBasin = LocationBasin[$"{i-1},{j}"];
				}
				else if ((int)adjacentBasin != LocationBasin[$"{i-1},{j}"])
				{
					ConvertBasinLocationsToDifferentBasin(LocationBasin[$"{i-1},{j}"], (int)adjacentBasin);
				}
			}
			
			return adjacentBasin;
		}
		
		private void ConvertBasinLocationsToDifferentBasin(int from, int to)
		{
			foreach (var location in BasinLocations[from])
			{
				LocationBasin[location] = to;
				BasinLocations[to].Add(location);
			}
			BasinLocations.Remove(from);
		}
		
		private Dictionary<int, Dictionary<int, int>> BuildHeightMap(string input)
		{
			var heightMap = new Dictionary<int, Dictionary<int, int>>();
			
			var rows = input.Split("\n");
			
			for (var i = 0; i < rows.Count(); i++)
			{
				for (var j = 0; j < rows[i].Length; j++)
				{
					if (!heightMap.ContainsKey(i)) heightMap[i] = new Dictionary<int, int>();
					heightMap[i][j] = Convert.ToInt32(rows[i][j].ToString());
				}
			}
			
			return heightMap;
		}
		
		private int CalculateFinalAnswer()
		{
			var basinSizes = new List<int>();
			foreach (var kvp in BasinLocations)
			{
				var BasinLocation = kvp.Value;
				basinSizes.Add(BasinLocation.Count());
				//Console.WriteLine($"{(char)(kvp.Key % 93 + 33)} - {BasinLocation.Count()}");
			}
			
			var orderedSizes = basinSizes.OrderBy(x => x).ToList();
			//foreach (var orderedSize in orderedSizes) Console.WriteLine(orderedSize);
			
			return orderedSizes[orderedSizes.Count()-1] * orderedSizes[orderedSizes.Count()-2] * orderedSizes[orderedSizes.Count()-3];
		}
	}
}