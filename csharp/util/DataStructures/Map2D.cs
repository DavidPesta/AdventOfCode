namespace AdventOfCode
{
	public class Map2D<T>
	{
		Dictionary<(long, long), T> Map = new Dictionary<(long, long), T>();
		
		public long? Xmin { get; private set; }
		public long? Xmax { get; private set; }
		public long? Ymin { get; private set; }
		public long? Ymax { get; private set; }
		
		private bool ApplyValueCounts;
		private Dictionary<T, long> ValueCounts;
		
		public Map2D(bool applyValueCounts = true)
		{
			ApplyValueCounts = applyValueCounts;
			if (ApplyValueCounts)
			{
				ValueCounts = new Dictionary<T, long>();
			}
		}
		
		public T this[long x, long y]
		{
			get {
				if (!Map.ContainsKey((x, y))) return default(T);
				return Map[(x, y)];
			}
			set {
				if (ApplyValueCounts)
				{
					if (Map.ContainsKey((x, y))) ValueCounts[Map[(x, y)]]--;
					if (!ValueCounts.ContainsKey(value)) ValueCounts[value] = 0;
					ValueCounts[value]++;
				}
				
				Map[(x, y)] = value;
				
				if (Xmin == null || Xmin > x) Xmin = x;
				if (Xmax == null || Xmax < x) Xmax = x;
				
				if (Ymin == null || Ymin > y) Ymin = y;
				if (Ymax == null || Ymax < y) Ymax = y;
			}
		}
		
		public long Xsize()
		{
			if (Xmin == null || Xmax == null) return 0;
			return (long)Xmax - (long)Xmin + 1;
		}
		
		public long Ysize()
		{
			if (Ymin == null || Ymax == null) return 0;
			return (long)Ymax - (long)Ymin + 1;
		}
		
		public long GetValueCount(T key)
		{
			return ValueCounts[key];
		}
		
		public static Map2D<char> LoadInputToCharValues(string input)
		{
			var map = new Map2D<char>();
			
			var lines = input.Split("\n");
			for (long y = 0; y < lines.Count(); y++)
			{
				for (long x = 0; x < lines[y].Length; x++)
				{
					map[x, y] = lines[y][(int)x];
				}
			}
			
			return map;
		}
		
		public override string ToString()
		{
			var output = "";
			
			for (var y = (long)Ymin; y <= Ymax; y++)
			{
				for (var x = (long)Xmin; x <= Xmax; x++)
				{
					output += this[x, y].ToString();
				}
				if (y != Ymax) output += "\n";
			}
			
			return output;
		}
	}
}