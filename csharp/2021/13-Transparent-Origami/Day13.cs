using AdventOfCode;

namespace Year2021
{
	public class Day13 : DayOption
	{
		public Day13(string day) : base(day, "Transparent Origami"){}
		
		(int, int) PaperDimensions;
		Dictionary<int, Dictionary<int, int>> Dots = new Dictionary<int, Dictionary<int, int>>();
		List<(char, int)> Folds = new List<(char, int)>();
		
		public void Part1(string input)
		{
			ParseInput(input);
			
			(var dimensions, var dots) = ApplyFold(PaperDimensions, Dots, Folds[0]);
			
			var numDots = 0;
			foreach (var kvp in dots)
			{
				numDots += kvp.Value.Count();
			}
			
			Console.WriteLine(numDots);
		}
		
		public void Part2(string input)
		{
			ParseInput(input);
			
			var dimensions = PaperDimensions;
			var dots = Dots;
			
			foreach (var fold in Folds)
			{
				(dimensions, dots) = ApplyFold(dimensions, dots, fold);
			}
			
			PrintPaper(dots, dimensions);
		}
		
		private void PrintPaper(Dictionary<int, Dictionary<int, int>> dots, (int, int) dimensions)
		{
			for (var y = 0; y <= dimensions.Item2; y++)
			{
				var output = Color.Normal;
				for (var x = 0; x <= dimensions.Item1; x++)
				{
					if (dots.ContainsKey(x) && dots[x].ContainsKey(y)) output += "#";
					else output += ".";
				}
				Console.WriteLine(output);
			}
			Console.WriteLine();
		}
		
		private ((int, int), Dictionary<int, Dictionary<int, int>>) ApplyFold ((int, int) dimensions, Dictionary<int, Dictionary<int, int>> dots, (char, int) fold)
		{
			(int, int) newDimensions = dimensions;
			if (fold.Item1 == 'x') newDimensions.Item1 = fold.Item2-1;
			if (fold.Item1 == 'y') newDimensions.Item2 = fold.Item2-1;
			
			var newDots = new Dictionary<int, Dictionary<int, int>>();
			
			foreach (var kvp in dots)
			{
				var x = kvp.Key;
				foreach (var kvp2 in kvp.Value)
				{
					var y = kvp2.Key;
					
					if (fold.Item1 == 'x')
					{
						if (x < fold.Item2)
						{
							if (!newDots.ContainsKey(x)) newDots[x] = new Dictionary<int, int>();
							newDots[x][y] = 1;
						}
						else
						{
							var newx = 2*fold.Item2 - x;
							if (!newDots.ContainsKey(newx)) newDots[newx] = new Dictionary<int, int>();
							newDots[newx][y] = 1;
						}
					}
					
					if (fold.Item1 == 'y')
					{
						if (y < fold.Item2)
						{
							if (!newDots.ContainsKey(x)) newDots[x] = new Dictionary<int, int>();
							newDots[x][y] = 1;
						}
						else
						{
							var newy = 2*fold.Item2 - y;
							if (!newDots.ContainsKey(x)) newDots[x] = new Dictionary<int, int>();
							newDots[x][newy] = 1;
						}
					}
				}
			}
			
			return (newDimensions, newDots);
		}
		
		private void ParseInput(string input)
		{
			var lines = input.Split("\n");
			
			for (var i = 0; i < lines.Count(); i++)
			{
				var line = lines[i];
				
				if (line == "") 
				{
					for (var j = i+1; j < lines.Count(); j++)
					{
						line = lines[j];
						var fold = line.Split(" ")[2].Split("=");
						Folds.Add((char.Parse(fold[0]), int.Parse(fold[1])));
					}
					return;
				}
				
				var dot = line.Split(",");
				var x = int.Parse(dot[0]);
				var y = int.Parse(dot[1]);
				if (PaperDimensions.Item1 < x) PaperDimensions.Item1 = x;
				if (PaperDimensions.Item2 < y) PaperDimensions.Item2 = y;
				if (!Dots.ContainsKey(x)) Dots[x] = new Dictionary<int, int>();
				Dots[x][y] = 1;
			}
		}
	}
}