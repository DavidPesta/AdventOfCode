using AdventOfCode;

namespace Year2021
{
	public class Day25 : DayOption
	{
		public Day25(string day) : base(day, "Sea Cucumber"){}
		
		public void Part1(string input)
		{
			var map = Map2D<char>.LoadInputToCharValues(input);
			
			var step = 0;
			long moves = 0;
			do {
				step++;
				moves = Step(map);
			} while (moves > 0);
			
			Console.WriteLine(step);
		}
		
		public void Part2(string input)
		{
			Console.WriteLine("Automatically solved by completing all previous 2021 challenges.");
		}
		
		private long Step(Map2D<char> map)
		{
			long totalMoves = 0;
			
			totalMoves += EastStep(map);
			totalMoves += SouthStep(map);
			
			return totalMoves;
		}
		
		private long EastStep(Map2D<char> map)
		{
			var moves = new List<(long, long, long)>();
			
			for (long x = (long)map.Xmin; x <= (long)map.Xmax; x++)
			{
				for (long y = (long)map.Ymin; y <= (long)map.Ymax; y++)
				{
					long nextX = x+1;
					if (nextX > (long)map.Xmax) nextX = 0;
					if (map[x, y] == '>' && map[nextX, y] == '.')
					{
						moves.Add((x, y, nextX));
					}
				}
			}
			
			foreach (var (x, y, nextX) in moves)
			{
				map[x, y] = '.';
				map[nextX, y] = '>';
			}
			
			return moves.Count();
		}
		
		private long SouthStep(Map2D<char> map)
		{
			var moves = new List<(long, long, long)>();
			
			for (long x = (long)map.Xmin; x <= (long)map.Xmax; x++)
			{
				for (long y = (long)map.Ymin; y <= (long)map.Ymax; y++)
				{
					long nextY = y+1;
					if (nextY > (long)map.Ymax) nextY = 0;
					if (map[x, y] == 'v' && map[x, nextY] == '.')
					{
						moves.Add((x, y, nextY));
					}
				}
			}
			
			foreach (var (x, y, nextY) in moves)
			{
				map[x, y] = '.';
				map[x, nextY] = 'v';
			}
			
			return moves.Count();
		}
	}
}