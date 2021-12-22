namespace AdventOfCode
{
	public class Map2D<T>
	{
		Dictionary<(long, long), T> Map = new Dictionary<(long, long), T>();
		
		public long? Xmin { get; private set; }
		public long? Xmax { get; private set; }
		public long? Ymin { get; private set; }
		public long? Ymax { get; private set; }
		
		public T this[long x, long y]
		{
			get {
				if (!Map.ContainsKey((x, y))) return default(T);
				return Map[(x, y)];
			}
			set {
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
		
		public override string ToString()
		{
			var output = "";
			
			for (var y = (long)Ymin; y <= Ymax; y++)
			{
				for (var x = (long)Xmin; x <= Xmax; x++)
				{
					output += this[x, y].ToString();
				}
				output += "\n\n";
			}
			
			return output;
		}
	}
}