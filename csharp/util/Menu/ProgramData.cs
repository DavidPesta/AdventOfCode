using System.Reflection;
using System.Text.RegularExpressions;

namespace AdventOfCode
{
	public class ProgramData
	{
		public List<int> Years { get; set; } = new List<int>();
		public Dictionary<int, List<DayType>> YearDayTypes { get; set; } = new Dictionary<int, List<DayType>>();
		public Dictionary<int, Dictionary<int, string>> YearDayInputs = new Dictionary<int, Dictionary<int, string>>();
		
		public ProgramData()
		{
			var namespaces = PrepareYears();
			PrepareYearDays(namespaces);
			PrepareYearDayInputs();
		}
		
		private IEnumerable<string> PrepareYears()
		{
			var namespaces = Assembly.GetExecutingAssembly().GetTypes().
				Select(t => t.Namespace).
				Distinct().
				Where(n => !string.IsNullOrEmpty(n) && n.StartsWith("Year"));
			
			Years = namespaces.
				Select(ns => int.Parse(ns.Replace("Year", ""))).
				OrderBy(y => y).
				ToList();
			
			return namespaces;
		}
		
		private void PrepareYearDays(IEnumerable<string> namespaces)
		{
			// GOTCHA:
			// If you use an anonymous function (with LINQ) in your solution, C# creates a class inside the same namespace with a similar name.
			// For example:
			//   Normal Class: Year2019.Day01
			//   Auto-Created Class: Year2019.Day01+<>c
			// That auto-created class needs to be filtered out and ignored.
			// This is done by ensuring the '$' at the end of the Regex string and applying it with the Regex.IsMatch inside of the Where.
			
			var regex = new Regex(@"^Year([0-9]+).Day([0-9]+)$");
			foreach (var @namespace in namespaces)
			{
				var classes = AppDomain.CurrentDomain.GetAssemblies().
					SelectMany(t => t.GetTypes()).
					Where(t => t.IsClass && t.Namespace == @namespace && Regex.IsMatch(t.FullName, regex.ToString()));
				
				var year = int.Parse(@namespace.Replace("Year", ""));
				YearDayTypes[year] = new List<DayType>();
				foreach (var @class in classes)
				{
					var match = regex.Match(@class.FullName);
					//var year = int.Parse(match.Groups[1].Value);
					var day = int.Parse(match.Groups[2].Value);
					YearDayTypes[year].Add(new DayType(){
						Day = day,
						Type = @class
					});
				}
				YearDayTypes[year] = YearDayTypes[year].OrderBy(d => d.Day).ToList();
			}
		}
		
		private void PrepareYearDayInputs()
		{
			foreach (var kv in YearDayTypes)
			{
				var year = kv.Key;
				
				if (!YearDayInputs.ContainsKey(year))
				{
					YearDayInputs[year] = new Dictionary<int, string>();
				}
				
				var dayFolders = Directory.GetDirectories($"{year}");
				
				var dayTypes = kv.Value;
				foreach (var dayType in dayTypes)
				{
					var day = dayType.Day;
					var folderPrefix = $"{year}/{day.ToString().PadLeft(2, '0')}";
					var inputFilePath = dayFolders.Where(f => f.StartsWith(folderPrefix)).Single() + "/input.txt";
					YearDayInputs[year][day] = File.ReadAllText(inputFilePath);
				}
			}
		}
	}
}