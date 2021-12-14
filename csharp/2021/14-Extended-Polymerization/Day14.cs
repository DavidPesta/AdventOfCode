using AdventOfCode;

namespace Year2021
{
	public class Day14 : DayOption
	{
		public Day14(string day) : base(day, "Extended Polymerization"){}
		
		public void Part1(string input)
		{
			var template = LoadTemplate(input);
			var rules = LoadRules(input);
			
			for (var i = 0; i < 10; i++)
			{
				template = RunStep(template, rules);
			}
			
			Dictionary<char, int> elementCounts = new Dictionary<char, int>();
			foreach (var element in template)
			{
				if (!elementCounts.ContainsKey(element))
				{
					elementCounts[element] = 0;
				}
				elementCounts[element]++;
			}
			
			var largestCount = 0;
			var smallestCount = 999999999;
			foreach (var kvp in elementCounts)
			{
				var count = kvp.Value;
				if (count > largestCount) largestCount = count;
				if (count < smallestCount) smallestCount = count;
			}
			
			Console.WriteLine(largestCount - smallestCount);
		}
		
		List<(string, char)> Rules = new List<(string, char)>();
		Dictionary<string, char> RulesLookup = new Dictionary<string, char>();
		Dictionary<char, long> ElementCounts = new Dictionary<char, long>();
		Dictionary<string, long> RuleCounts = new Dictionary<string, long>();
		
		public void Part2(string input)
		{
			var template = LoadTemplate(input);
			Rules = LoadRules(input);
			
			foreach (var rule in Rules)
			{
				(var search, var replace) = rule;
				RulesLookup[search] = replace;
				RuleCounts[search] = 0;
				
				if (!ElementCounts.ContainsKey(replace))
				{
					ElementCounts[replace] = 0;
				}
			}
			
			foreach (var element in template)
			{
				ElementCounts[element]++;
			}
			
			foreach (var rule in Rules)
			{
				(var search, var replace) = rule;
				
				var startIndex = 0;
				int searchIndexFound;
				do
				{
					searchIndexFound = template.IndexOf(search, startIndex);
					if (searchIndexFound != -1)
					{
						RuleCounts[search]++;
						startIndex = searchIndexFound + 1;
					}
				}
				while (searchIndexFound != -1);
			}
			
			for (var i = 0; i < 40; i++)
			{
				RunStepPart2();
			}
			
			long? largestCount = null;
			long? smallestCount = null;
			foreach (var kvp in ElementCounts)
			{
				var count = kvp.Value;
				if (largestCount == null || count > largestCount) largestCount = count;
				if (smallestCount == null || count < smallestCount) smallestCount = count;
			}
			
			Console.WriteLine(largestCount - smallestCount);
		}
		
		private void RunStepPart2()
		{
			var newRuleCounts = new Dictionary<string, long>();
			foreach (var rule in Rules)
			{
				(var search, var replace) = rule;
				newRuleCounts[search] = 0;
			}
			
			foreach (var ruleCount in RuleCounts)
			{
				var rule = ruleCount.Key;
				var count = ruleCount.Value;
				var replace = RulesLookup[rule];
				
				ElementCounts[replace] += count;
				
				var ruleParts = rule.ToCharArray();
				var maybeRule1 = ruleParts[0].ToString() + replace.ToString();
				var maybeRule2 = replace.ToString() + ruleParts[1].ToString();
				
				if (RulesLookup.ContainsKey(maybeRule1)) 
				{
					newRuleCounts[maybeRule1] += count;
				}
				if (RulesLookup.ContainsKey(maybeRule2))
				{
					newRuleCounts[maybeRule2] += count;
				}
			}
			
			RuleCounts = newRuleCounts;
		}
		
		private string RunStep(string template, List<(string, char)> rules)
		{
			List<(int, char)> insertions = new List<(int, char)>();
			
			foreach (var rule in rules)
			{
				(var search, var replace) = rule;
				
				var startIndex = 0;
				int searchIndexFound;
				do
				{
					searchIndexFound = template.IndexOf(search, startIndex);
					if (searchIndexFound != -1)
					{
						insertions.Add((searchIndexFound +1, replace));
						startIndex = searchIndexFound + 1;
					}
				}
				while (searchIndexFound != -1);
			}
			
			var orderedInsertions = insertions.OrderByDescending(i => i.Item1).ToList();
			
			foreach (var insertion in orderedInsertions)
			{
				(var index, var insertChar) = insertion;
				template = template.Insert(index, insertChar.ToString());
			}
			
			return template;
		}
		
		private string LoadTemplate(string input)
		{
			return input.Split("\n")[0];
		}
		
		private List<(string, char)> LoadRules(string input)
		{
			var rules = new List<(string, char)>();
			
			var lines = input.Split("\n");
			
			for (var i = 2; i < lines.Count(); i++)
			{
				var line = lines[i];
				var rule = line.Split(" -> ");
				rules.Add((rule[0], char.Parse(rule[1])));
			}
			
			return rules;
		}
	}
}