namespace AdventOfCode
{
	public class Map3D<T>
	{
		Dictionary<(long, long, long), T> Map = new Dictionary<(long, long, long), T>();
		
		public long? Xmin { get; private set; }
		public long? Xmax { get; private set; }
		public long? Ymin { get; private set; }
		public long? Ymax { get; private set; }
		public long? Zmin { get; private set; }
		public long? Zmax { get; private set; }
		
		private bool ApplyValueCounts;
		private Dictionary<T, long> ValueCounts;
		
		public Map3D(bool applyValueCounts = true)
		{
			ApplyValueCounts = applyValueCounts;
			if (ApplyValueCounts)
			{
				ValueCounts = new Dictionary<T, long>();
			}
		}
		
		public T this[long x, long y, long z]
		{
			get {
				if (!Map.ContainsKey((x, y, z))) return default(T);
				return Map[(x, y, z)];
			}
			set {
				if (ApplyValueCounts)
				{
					if (Map.ContainsKey((x, y, z))) ValueCounts[Map[(x, y, z)]]--;
					if (!ValueCounts.ContainsKey(value)) ValueCounts[value] = 0;
					ValueCounts[value]++;
				}
				
				Map[(x, y, z)] = value;
				
				if (Xmin == null || Xmin > x) Xmin = x;
				if (Xmax == null || Xmax < x) Xmax = x;
				
				if (Ymin == null || Ymin > y) Ymin = y;
				if (Ymax == null || Ymax < y) Ymax = y;
				
				if (Zmin == null || Zmin > z) Zmin = z;
				if (Zmax == null || Zmax < z) Zmax = z;
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
		
		public long Zsize()
		{
			if (Zmin == null || Zmax == null) return 0;
			return (long)Zmax - (long)Zmin + 1;
		}
		
		public long GetValueCount(T key)
		{
			return ValueCounts[key];
		}
		
		public override string ToString()
		{
			var output = "";
			
			for (var z = (long)Zmin; z <= Zmax; z++)
			{
				for (var y = (long)Ymin; y <= Ymax; y++)
				{
					for (var x = (long)Xmin; x <= Xmax; x++)
					{
						output += this[x, y, z].ToString();
					}
					output += "\n";
				}
				output += "\n\n";
			}
			
			return output;
		}
	}
}