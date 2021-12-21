namespace AdventOfCode
{
	public class MainMenu : Menu
	{
		public override void Compose()
		{
			Title = $"{Color.Red}Advent of Code{Color.Normal}";
			Description = "Choose an AoC Event year.";
			
			for (var i = 1; i <= Data.Years.Count; i++)
			{
				Add<YearMenu>(i.ToString(), Data.Years[i-1].ToString());
			}
			
			Add<EmptyOption>("");
			Add<ExitOption>();
		}
	}
}