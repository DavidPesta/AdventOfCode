using AdventOfCode;

namespace Year2021
{
	public class Day06 : DayOption
	{
		public Day06(string day) : base(day, "Lanternfish"){}
		
		public void Part1(string input)
		{
			var fishDays = input.Split(',').Select(n => Convert.ToInt32(n)).ToList();
			
			var numDays = 80;
			
			for (var day = 0; day < numDays; day++)
			{
				var numNewFish = 0;
				for (var fishIdx = 0; fishIdx < fishDays.Count; fishIdx++)
				{
					if (fishDays[fishIdx] == 0)
					{
						numNewFish++;
						fishDays[fishIdx] = 6;
					}
					else
					{
						fishDays[fishIdx]--;
					}
				}
				for (var i = 0; i < numNewFish; i++)
				{
					fishDays.Add(8);
				}
			}
			
			Console.WriteLine(fishDays.Count);
		}
		
		public void Part2(string input)
		{
			var fishDays = input.Split(',').Select(n => Convert.ToInt32(n)).ToList();
			
			var fishDaysArr = new List<long>() { 0, 0, 0, 0, 0, 0, 0, 0, 0 };
			foreach (var fishDay in fishDays)
			{
				fishDaysArr[fishDay]++;
			}
			
			var numDays = 256;
			
			for (var day = 0; day < numDays; day++)
			{
				long numNewFish = 0;
				
				for (var i = 0; i < fishDaysArr.Count-1; i++)
				{
					if (i == 0) numNewFish = fishDaysArr[i];
					fishDaysArr[i] = fishDaysArr[i+1];
				}
				fishDaysArr[6] += numNewFish;
				
				fishDaysArr[8] = numNewFish;
			}
			
			long count = 0;
			for (var i = 0; i < fishDaysArr.Count; i++)
			{
				count += fishDaysArr[i];
			}
			
			Console.WriteLine(count);
		}
	}
}