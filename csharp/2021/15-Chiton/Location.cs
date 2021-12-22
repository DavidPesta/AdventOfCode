using AdventOfCode;

namespace Year2021
{
	public class Location
	{
		public long X;
		public long Y;
		public long LocationRisk;
		public long? PathRisk;
		//public List<Location> Path;
		//public Dictionary<(short, short), Location> AdjacentLocations = new Dictionary<(short, short), Location>();
		
		public Location(long x, long y, long locationRisk)
		{
			X = x;
			Y = y;
			LocationRisk = locationRisk;
		}
		
		public List<Location> FetchAdjacentLocations(Map2D<Location> Map)
		{
			var locations = new List<Location>();
			
			if (X > 0) locations.Add(Map[X-1, Y]);
			if (X < Map.Xmax) locations.Add(Map[X+1, Y]);
			if (Y > 0) locations.Add(Map[X, Y-1]);
			if (Y < Map.Ymax) locations.Add(Map[X, Y+1]);
			
			return locations;
		}
		
		public override string ToString()
		{
			return $"({LocationRisk},{PathRisk}) ";
		}
	}
}