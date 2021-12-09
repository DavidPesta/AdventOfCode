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
		private int NextBasin;
		
		public void Part2(string input)
		{
			HeightMap = BuildHeightMap(input);
			
			BasinLocations = new Dictionary<int, List<string>>();
			LocationBasin = new Dictionary<string, int>();
			NextBasin = 0;
			
			var output = Color.Normal;
			for (var i = 0; i < HeightMap.Count(); i++)
			{
				for (var j = 0; j < HeightMap[i].Count(); j++)
				{
					if (HeightMap[i][j] < 9)
					{
						var adjacentBasin = FindAdjacentBasin(i, j);
						
						if (adjacentBasin == null) adjacentBasin = NextBasin;
						LocationBasin[$"{i},{j}"] = (int)adjacentBasin;
						if (!BasinLocations.ContainsKey((int)adjacentBasin)) BasinLocations[(int)adjacentBasin] = new List<string>();
						BasinLocations[(int)adjacentBasin].Add($"{i},{j}");
						
						output += $"{Color.Red}*{Color.Normal}";
					}
					else
					{
						NextBasin++;
						output += "*";
					}
				}
				NextBasin++;
				output += "\n";
			}
			
			//Console.WriteLine(output);
			
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
			
			var basinSizes = new List<int>();
			foreach (var kvp in BasinLocations)
			{
				var BasinLocation = kvp.Value;
				basinSizes.Add(BasinLocation.Count());
				Console.WriteLine($"{(char)(kvp.Key % 93 + 33)} - {BasinLocation.Count()}");
			}
			
			var orderedSizes = basinSizes.OrderBy(x => x).ToList();
			//foreach (var orderedSize in orderedSizes) Console.WriteLine(orderedSize);
			
			var answer = orderedSizes[orderedSizes.Count()-1] * orderedSizes[orderedSizes.Count()-2] * orderedSizes[orderedSizes.Count()-3];
			
			Console.WriteLine(answer);
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
	}
}