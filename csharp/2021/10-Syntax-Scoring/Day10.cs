using AdventOfCode;

namespace Year2021
{
	public class Day10 : DayOption
	{
		public Day10(string day) : base(day, "Syntax Scoring"){}
		
		public Dictionary<char, int> IllegalCharPoints = new Dictionary<char, int>()
		{
			{ ')', 3 },
			{ ']', 57 },
			{ '}', 1197 },
			{ '>', 25137 }
		};
		
		public Dictionary<char, char> CloseMatches = new Dictionary<char, char>()
		{
			{')', '('},
			{']', '['},
			{'}', '{'},
			{'>', '<'}
		};
		
		public Dictionary<char, char> OpenMatches = new Dictionary<char, char>()
		{
			{'(', ')'},
			{'[', ']'},
			{'{', '}'},
			{'<', '>'}
		};
		
		public Dictionary<char, int> ClosePoints = new Dictionary<char, int>()
		{
			{ ')', 1 },
			{ ']', 2 },
			{ '}', 3 },
			{ '>', 4 }
		};
		
		public void Part1(string input)
		{
			var lines = input.Split("\n");
			
			var score = 0;
			for (var i = 0; i < lines.Count(); i++)
			{
				var line = lines[i];
				
				var stack = "";
				foreach (var c in line)
				{
					if (c == '(' || c == '[' || c == '{' || c == '<')
					{
						stack += c;
					}
					
					if (c == ')' || c == ']' || c == '}' || c == '>')
					{
						if (CloseMatches[c] != stack[stack.Length-1])
						{
							score += IllegalCharPoints[c];
							break;
						}
						else
						{
							stack = stack.Remove(stack.Length - 1, 1);
						}
					}
				}
			}
			
			Console.WriteLine(score);
		}
		
		public void Part2(string input)
		{
			var lines = input.Split("\n");
			
			var uncorruptedLines = new List<string>();
			for (var i = 0; i < lines.Count(); i++)
			{
				var line = lines[i];
				
				var corruptLine = false;
				var stack = "";
				foreach (var c in line)
				{
					if (c == '(' || c == '[' || c == '{' || c == '<')
					{
						stack += c;
					}
					
					if (c == ')' || c == ']' || c == '}' || c == '>')
					{
						if (CloseMatches[c] != stack[stack.Length-1])
						{
							corruptLine = true;
							break;
						}
						else
						{
							stack = stack.Remove(stack.Length - 1, 1);
						}
					}
				}
				if (!corruptLine)
				{
					uncorruptedLines.Add(line);
				}
			}
			
			var scores = new List<long>();
			for (var i = 0; i < uncorruptedLines.Count; i++)
			{
				var line = uncorruptedLines[i];
				
				var stack = "";
				foreach (var c in line)
				{
					if (c == '(' || c == '[' || c == '{' || c == '<')
					{
						stack += c;
					}
					
					if (c == ')' || c == ']' || c == '}' || c == '>')
					{
						stack = stack.Remove(stack.Length - 1, 1);
					}
				}
				
				long lineScore = 0;
				for (int j = stack.Count() - 1; j >= 0; j--)
				{
					lineScore *= 5;
					lineScore += ClosePoints[OpenMatches[stack[j]]];
				}
				scores.Add(lineScore);
			}
			
			var sortedScores = scores.OrderBy(s => s).ToList();
			
			for (var i = 0; i < sortedScores.Count(); i++)
			{
				if (i == (sortedScores.Count() - 1) / 2)
				{
					Console.WriteLine(sortedScores[i]);
				}
			}
		}
	}
}