using System.Text.RegularExpressions;

namespace AdventOfCode
{
	public abstract class DayOption : MenuAction
	{
		private string Title { get; set; }
		private string Day { get; set; }
		
		public DayOption(string day, string title)
		{
			Day = day;
			Title = title;
		}
		
		public override string Command
		{
			get {
				return Day;
			}
		}
		
		public override string MenuText
		{
			get {
				return $"Day {Day}: {Title}";
			}
		}
		
		public override MenuSignal Action()
		{
			var type = this.GetType();
			var regex = new Regex(@"Year([0-9]+).Day([0-9]+)");
			var match = regex.Match(type.FullName);
			var year = int.Parse(match.Groups[1].Value);
			var day = int.Parse(Day); // or int.Parse(match.Groups[2].Value);
			
			var part1 = type.GetMethod("Part1");
			if (part1 != null)
			{
				Console.WriteLine(Christmasize($"Running Year {year} Day {day} Part 1") + $"{Color.Yellow}");
				part1.Invoke(this, new object[] { Data.YearDayInputs[year][day] });
				Console.WriteLine($"{Color.Normal}");
			}
			
			var part2 = type.GetMethod("Part2");
			if (part2 != null)
			{
				Console.WriteLine(Christmasize($"Running Year {year} Day {day} Part 2") + $"{Color.Yellow}");
				part2.Invoke(this, new object[] { Data.YearDayInputs[year][day] });
				Console.WriteLine($"{Color.Normal}");
			}
			
			Console.ReadLine();
			
			return MenuSignal.Continue;
		}
		
		private string Christmasize(string str)
		{
			var newStr = $"{Color.Bold}{Color.Red}";
			
			bool red = true;
			for (var i = 0; i < str.Length; i++)
			{
				if (str[i] == ' ')
				{
					red = !red;
					if (red) newStr += $"{Color.Red}";
					else newStr += $"{Color.Green}";
				}
				
				newStr += str[i];
			}
			
			newStr += $"{Color.Normal}";
			
			return newStr;
		}
	}
}