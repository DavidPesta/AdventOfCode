using System.Text;

namespace AdventOfCode
{
	public static class Transform
	{
		// "5E" --> "1011110"
		public static string Hex2Bin(string hex)
		{
			var bin = new StringBuilder();
			
			foreach (var c in hex)
			{
				var binWord = "";
				var n = Convert.ToInt16(c.ToString(), 16);
				for (var i = 0; i < 4; i++)
				{
					binWord = (n%2).ToString() + binWord;
					n /= 2;
				}
				bin.Append(binWord);
			}
			
			return bin.ToString();
		}
		
		// "1011110" --> 94
		public static long Bin2Dec(string bin)
		{
			long dec = 0;
			
			for (var i = 0; i < bin.Length; i++)
			{
				var binDigit = Convert.ToInt16(bin[i].ToString());
				dec += binDigit * (long)Math.Pow(2, bin.Length - i - 1);
			}
			
			return dec;
		}
	}
}