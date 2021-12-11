using AdventOfCode;

namespace Year2021
{
	public class Day11 : DayOption
	{
		public Day11(string day) : base(day, "Dumbo Octopus"){}
		
		Dictionary<int, Dictionary<int, int>> Grid = new Dictionary<int, Dictionary<int, int>>();
		int FlashCounter = 0;
		
		public void Part1(string input)
		{
			CreateGrid(input);
			
			for (var step = 0; step < 100; step++)
			{
				AdvanceStep();
			}
			
			Console.WriteLine(FlashCounter);
		}
		
		public void Part2(string input)
		{
			CreateGrid(input);
			
			var step = 0;
			while (!AllAreFlashing())
			{
				step++;
				AdvanceStep();
			}
			
			Console.WriteLine(step);
		}
		
		private bool AllAreFlashing()
		{
			for (var y = 0; y < Grid.Count(); y++)
			{
				for (var x = 0; x < Grid[y].Count(); x++)
				{
					if (Grid[y][x] != 0) return false;
				}
			}
			
			return true;
		}
		
		private void AdvanceStep()
		{
			for (var y = 0; y < Grid.Count(); y++)
			{
				for (var x = 0; x < Grid[y].Count(); x++)
				{
					Grid[y][x]++;
				}
			}
			
			for (var y = 0; y < Grid.Count(); y++)
			{
				for (var x = 0; x < Grid[y].Count(); x++)
				{
					CheckForFlash(y, x);
				}
			}
			
			for (var y = 0; y < Grid.Count(); y++)
			{
				for (var x = 0; x < Grid[y].Count(); x++)
				{
					if (Grid[y][x] == -1) Grid[y][x] = 0;
				}
			}
		}
		
		private void CheckForFlash(int y, int x)
		{
			if (Grid[y][x] < 10) return;
			
			FlashCounter++;
			Grid[y][x] = -1;
			
			for (int yi = y - 1; yi <= y + 1; yi++)
			{
				if (yi < 0) continue;
				if (yi >= Grid.Count()) continue;
				for (int xj = x - 1; xj <= x + 1; xj++)
				{
					if (xj < 0) continue;
					if (xj >= Grid[y].Count()) continue;
					if (Grid[yi][xj] == -1) continue;
					Grid[yi][xj]++;
				}
			}
			
			for (int yi = y - 1; yi <= y + 1; yi++)
			{
				if (yi < 0) continue;
				if (yi >= Grid.Count()) continue;
				for (int xj = x - 1; xj <= x + 1; xj++)
				{
					if (xj < 0) continue;
					if (xj >= Grid[y].Count()) continue;
					if (Grid[yi][xj] == -1) continue;
					CheckForFlash(yi, xj);
				}
			}
		}
		
		private void DisplayGrid()
		{
			var output = Color.Normal;
			for (var y = 0; y < Grid.Count(); y++)
			{
				for (var x = 0; x < Grid[y].Count(); x++)
				{
					if (Grid[y][x] == 0 || Grid[y][x] > 9 || Grid[y][x] == -1) output += Color.Red;
					output += Grid[y][x].ToString().PadRight(3);
					if (Grid[y][x] == 0 || Grid[y][x] > 9 || Grid[y][x] == -1) output += Color.Normal;
				}
				output += "\n";
			}
			Console.WriteLine(output);
		}
		
		private void CreateGrid(string input)
		{
			var rows = input.Split("\n");
			for (var y = 0; y < rows.Count(); y++)
			{
				for (var x = 0; x < rows[y].Length; x++)
				{
					if (!Grid.ContainsKey(y)) Grid[y] = new Dictionary<int, int>();
					Grid[y][x] = Convert.ToInt32(rows[y][x].ToString());
				}
			}
		}
	}
}