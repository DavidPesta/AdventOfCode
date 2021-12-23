using AdventOfCode;
using System.Text.RegularExpressions;

namespace Year2021
{
	public class Day22 : DayOption
	{
		public Day22(string day) : base(day, "Reactor Reboot"){}
		
		public void Part1(string input)
		{
			var lines = input.Split("\n");
			
			var map = new Map3D<string>();
			
			var filter = ParseDimensions("x=-50..50,y=-50..50,z=-50..50");
			
			foreach (var line in lines)
			{
				var parts = line.Split(" ");
				var command = parts[0];
				var dimensions = ParseDimensions(parts[1]);
				
				var (tx1, tx2, ty1, ty2, tz1, tz2) = TrimDimensionsWithFilter(dimensions, filter);
				
				// If any of the dimensions has no overlap with the filter, there are no cubes selected.
				if (tx2 < tx1 || ty2 < ty1 || tz2 < tz1) continue;
				
				for (var x = tx1; x <= tx2; x++)
				{
					for (var y = ty1; y <= ty2; y++)
					{
						for (var z = tz1; z <= tz2; z++)
						{
							map[x, y, z] = command;
						}
					}
				}
			}
			
			Console.WriteLine(map.GetValueCount("on"));
		}
		
		// Each Cuboid object contains an x1, x2, y1, y2, z1, z2 and will keep track of its own volume. A new Cuboid will check for
		// overlap with all current Cuboids. It will smash all overlapping Cuboids into pieces by first removing the overlapping cuboids
		// and then adding their pieces, if any are remaining (sometimes a cuboid is subsumed by the new one). Finally, if the new Cuboid
		// is an "on", then it will add this whole Cuboid to the collection. If the new Cuboid is "off", then it won't. At the end, sum
		// up all of the volumes of the existing cuboids.
		public void Part2(string input)
		{
			var lines = input.Split("\n");
			
			var cuboids = new Queue<Cuboid>();
			
			foreach (var line in lines)
			{
				var parts = line.Split(" ");
				var command = parts[0];
				var newCuboid = CreateCuboidFromString(parts[1]);
				
				var newCuboids = new Queue<Cuboid>();
				
				while (cuboids.Count > 0)
				{
					var testCuboid = cuboids.Dequeue();
					if (testCuboid.DetectCollision(newCuboid))
					{
						var pieces = testCuboid.ClobberWithCuboid(newCuboid);
						foreach (var piece in pieces) newCuboids.Enqueue(piece);
					}
					else
					{
						newCuboids.Enqueue(testCuboid);
					}
				}
				
				if (command == "on") newCuboids.Enqueue(newCuboid);
				
				cuboids = newCuboids;
			}
			
			long totalVolume = 0;
			foreach (var cuboid in cuboids)
			{
				totalVolume += cuboid.Volume;
			}
			
			Console.WriteLine(totalVolume);
		}
		
		private (long, long, long, long, long, long) TrimDimensionsWithFilter((long, long, long, long, long, long) dimensions, (long, long, long, long, long, long) filter)
		{
			var (dx1, dx2, dy1, dy2, dz1, dz2) = dimensions;
			var (fx1, fx2, fy1, fy2, fz1, fz2) = filter;
			
			// Create new line segments to define overlap. For a given dimension, if d1 < d2 below, there is overlap in that dimension.
			return (Math.Max(dx1, fx1), Math.Min(dx2, fx2), Math.Max(dy1, fy1), Math.Min(dy2, fy2), Math.Max(dz1, fz1), Math.Min(dz2, fz2));
		}
		
		private (long, long, long, long, long, long) ParseDimensions(string d)
		{
			var regex = new Regex(@"x=(?<x1>-?\d+)..(?<x2>-?\d+),y=(?<y1>-?\d+)..(?<y2>-?\d+),z=(?<z1>-?\d+)..(?<z2>-?\d+)");
			var m = regex.Match(d);
			var x1 = long.Parse(m.Groups["x1"].Value);
			var x2 = long.Parse(m.Groups["x2"].Value);
			var y1 = long.Parse(m.Groups["y1"].Value);
			var y2 = long.Parse(m.Groups["y2"].Value);
			var z1 = long.Parse(m.Groups["z1"].Value);
			var z2 = long.Parse(m.Groups["z2"].Value);
			return (x1, x2, y1, y2, z1, z2);
		}
		
		private Cuboid CreateCuboidFromString(string d)
		{
			var (x1, x2, y1, y2, z1, z2) = ParseDimensions(d);
			return new Cuboid(x1, x2, y1, y2, z1, z2);
		}
	}
}