using System.Text.RegularExpressions;

namespace Year2021
{
	public class Target
	{
		public long X1;
		public long X2;
		public long Y1;
		public long Y2;
		
		public Target(string input)
		{
			ParseInput(input);
		}
		
		public void ParseInput(string input)
		{
			var regex = new Regex(@"target area: x=(?<x1>-?\d+)..(?<x2>-?\d+), y=(?<y1>-?\d+)..(?<y2>-?\d+)");
			
			var m = regex.Match(input);
			X1 = long.Parse(m.Groups["x1"].Value);
			X2 = long.Parse(m.Groups["x2"].Value);
			Y1 = long.Parse(m.Groups["y1"].Value);
			Y2 = long.Parse(m.Groups["y2"].Value);
		}
		
		public bool VelocityHitsTarget(long xvel, long yvel)
		{
			long curX = 0;
			long curY = 0;
			
			// As soon as the probe has 1. a downward velocity and 2. is positioned below the target,
			// there is no way for it to hit the target, so the loop is done and return false.
			while (yvel >= 0 || curY >= Y1)
			{
				if (curX >= X1 && curX <= X2 && curY >= Y1 && curY <= Y2)
				{
					return true;
				}
				
				curX += xvel;
				curY += yvel;
				if (xvel > 0) xvel--;
				if (xvel < 0) xvel++;
				yvel--;
			}
			
			return false;
		}
	}
}