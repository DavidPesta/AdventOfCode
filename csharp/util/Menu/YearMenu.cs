namespace AdventOfCode
{
	public class YearMenu : Menu
	{
		public int Year { get; set; }
		
		public YearMenu(string command, string year)
		{
			Year = int.Parse(year);
			
			Command = command;
			MenuText = $"{Year}";
		}
		
		public override void Compose()
		{
			Title = $"{Color.Red}Advent of Code ({Year}){Color.Normal}";
			Description = $"Run the programs that calculate solutions to Advent Day problems in {Year}.";
			
			foreach (var dayType in Data.YearDayTypes[Year])
			{
				//Add<dayType.Type>(dayType.Day.ToString()); // This doesn't work. Alternative below:
				// https://stackoverflow.com/a/32935359/508558
				typeof(YearMenu).
					GetMethod("Add").
					MakeGenericMethod(dayType.Type).
					Invoke(this, new object[]{ new string[]{ dayType.Day.ToString() } });
			}
			
			Add<EmptyOption>("");
			Add<ExitOption>();
		}
	}
}