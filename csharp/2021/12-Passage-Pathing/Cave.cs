namespace Year2021
{
	public enum CaveSize
	{
		Big,
		Small,
		Neither
	}
	
	public class Cave
	{
		public string Name;
		public CaveSize Size = CaveSize.Neither;
		public List<Cave> AdjacentCaves = new List<Cave>();
		
		public Cave(string name)
		{
			Name = name;
			if (Name != "start" && Name != "end")
			{
				if (Name.ToLower() == Name) Size = CaveSize.Small;
				else Size = CaveSize.Big;
			}
		}
		
		public void AddAdjacentCave(Cave cave)
		{
			AdjacentCaves.Add(cave);
		}
		
		public void FindPathsToEndPart1(List<string> paths, int curPath)
		{
			if (Name == "end") return;
			
			var path = paths[curPath];
			paths.RemoveAt(curPath);
			
			var prevCaves = path.Split(",").ToList();
			
			foreach (var adjacentCave in AdjacentCaves)
			{
				if (adjacentCave.Name == "start") continue;
				
				if (prevCaves.Contains(adjacentCave.Name) && adjacentCave.Size == CaveSize.Small) continue;
				
				var newPath = $"{path},{adjacentCave.Name}";
				paths.Add(newPath);
				adjacentCave.FindPathsToEndPart1(paths, paths.Count()-1);
			}
		}
		
		public void FindPathsToEndPart2(List<string> paths, int curPath, bool usedExtraSmallCaveAllowance = false)
		{
			if (Name == "end") return;
			
			var path = paths[curPath];
			paths.RemoveAt(curPath);
			
			var prevCaves = path.Split(",").ToList();
			
			foreach (var adjacentCave in AdjacentCaves)
			{
				if (adjacentCave.Name == "start") continue;
				
				var applyExtraSmallCaveAllowance = usedExtraSmallCaveAllowance;
				if (prevCaves.Contains(adjacentCave.Name) && adjacentCave.Size == CaveSize.Small)
				{
					if (usedExtraSmallCaveAllowance == false) applyExtraSmallCaveAllowance = true;
					else continue;
				}
				
				var newPath = $"{path},{adjacentCave.Name}";
				paths.Add(newPath);
				adjacentCave.FindPathsToEndPart2(paths, paths.Count()-1, applyExtraSmallCaveAllowance);
			}
		}
	}
}