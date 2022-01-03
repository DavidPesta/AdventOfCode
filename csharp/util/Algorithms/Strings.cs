using System.Text;

namespace AdventOfCode
{
	public static class Strings
	{
		public static IEnumerable<string> SplitBy(this string str, int splitNum)
		{
			var result = new List<string>();
			
			var nextString = new StringBuilder();
			foreach (var c in str)
			{
				if (nextString.Length == splitNum)
				{
					result.Add(nextString.ToString());
					nextString = new StringBuilder();
				}
				
				nextString.Append(c);
			}
			
			if (nextString.Length > 0) result.Add(nextString.ToString());
			
			return result;
		}
	}
}