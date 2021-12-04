using AdventOfCode;

namespace Year2021
{
	public class Day03 : DayOption
	{
		public Day03(string day) : base(day, "Binary Diagnostic"){}
		
		public void Part1(string input)
		{
			var lines = input.Split('\n');
			
			var numChars = 12;
			
			var gamma = "";
			var epsilon = "";
			for (var i = 0; i < numChars; i++)
			{
				var numZeroes = 0;
				var numOnes = 0;
				foreach (var line in lines)
				{
					if (line[i] == '0')
					{
						numZeroes++;
					}
					if (line[i] == '1')
					{
						numOnes++;
					}
				}
				
				if (numZeroes > numOnes)
				{
					gamma += "0";
					epsilon += "1";
				}
				else
				{
					gamma += "1";
					epsilon += "0";
				}
			}
			
			Console.WriteLine(gamma);
			Console.WriteLine(epsilon);
			
			//var gammaDec = 0;
			//var epsilonDec = 0;
			
			for (var i = 0; i < numChars; i++)
			{
				
			}
			
		}
		
		public void Part2(string input)
		{
			var list = input.Split('\n').ToList();
			var i = 0;
			while (list.Count > 1)
			{
				var zeroList = new List<string>();
				var oneList = new List<string>();
				
				var numZeroes = 0;
				var numOnes = 0;
				foreach (var line in list)
				{
					if (line[i] == '0')
					{
						numZeroes++;
						zeroList.Add(line);
					}
					if (line[i] == '1')
					{
						numOnes++;
						oneList.Add(line);
					}
				}
				
				if (numZeroes > numOnes) list = zeroList;
				else list = oneList;
				
				i++;
			}
			
			Console.WriteLine(list[0]);
			
			list = input.Split('\n').ToList();
			i = 0;
			while (list.Count > 1)
			{
				var zeroList = new List<string>();
				var oneList = new List<string>();
				
				var numZeroes = 0;
				var numOnes = 0;
				foreach (var line in list)
				{
					if (line[i] == '0')
					{
						numZeroes++;
						zeroList.Add(line);
					}
					if (line[i] == '1')
					{
						numOnes++;
						oneList.Add(line);
					}
				}
				
				if (numZeroes <= numOnes) list = zeroList;
				else list = oneList;
				
				i++;
			}
			
			Console.WriteLine(list[0]);
		}
	}
}