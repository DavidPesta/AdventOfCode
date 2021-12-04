using AdventOfCode;

namespace Year2021
{
	public class Day04 : DayOption
	{
		public Day04(string day) : base(day, "Giant Squid"){}
		
		public void Part1(string input)
		{
			var numbers = GetNumbers(input);
			var boards = GetBoards(input);
			
			foreach (var number in numbers)
			{
				foreach (var board in boards)
				{
					board.MarkSpace(number);
					if (board.IsBingo())
					{
						var sum = board.SumUnmarkedSpaces();
						var answer = number * sum;
						Console.WriteLine(answer);
						return;
					}
				}
			}
		}
		
		public void Part2(string input)
		{
			var numbers = GetNumbers(input);
			var boards = GetBoards(input);
			
			var numBoards = boards.Count;
			var boardsComplete = new List<int>();
			
			foreach (var number in numbers)
			{
				var boardNumber = 0;
				foreach (var board in boards)
				{
					board.MarkSpace(number);
					if (board.IsBingo())
					{
						if (!boardsComplete.Contains(boardNumber)) boardsComplete.Add(boardNumber);
						
						if (boardsComplete.Count == numBoards)
						{
							var sum = board.SumUnmarkedSpaces();
							var answer = number * sum;
							Console.WriteLine(answer);
							return;
						}
					}
					boardNumber++;
				}
			}
		}
		
		private List<int> GetNumbers(string input)
		{
			var line = input.Split('\n')[0];
			return line.Split(',').Select(n => Convert.ToInt32(n)).ToList();
		}
		
		private List<Board> GetBoards(string input)
		{
			var boards = new List<Board>();
			
			var lines = input.Split('\n');
			
			var spaces = new List<List<int?>>();
			for (var i = 1; i < lines.Length; i++)
			{
				if ((i - 1) % 6 == 0)
				{
					spaces = new List<List<int?>>();
					continue;
				}
				
				spaces.Add(GetLineNumbers(lines[i]));
				
				if ((i - 6) % 6 == 0)
				{
					boards.Add(new Board(spaces));
				}
			}
			
			return boards;
		}
		
		// TODO: I'm certain there is a great way to do this with a simple line of LINQ.
		private List<int?> GetLineNumbers(string line)
		{
			var lineNumbers = new List<int?>();
			
			var items = line.Split(' ');
			foreach (var item in items)
			{
				if (!string.IsNullOrEmpty(item))
				{
					lineNumbers.Add(Convert.ToInt32(item));
				}
			}
			
			return lineNumbers;
		}
	}
}