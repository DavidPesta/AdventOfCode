namespace Year2021
{
	public class Board
	{
		List<List<int?>> Spaces;
		List<List<int?>> RemainingSpaces;
		
		public Board(List<List<int?>> spaces)
		{
			Spaces = spaces;
			RemainingSpaces = new List<List<int?>>(Spaces);
		}
		
		public void MarkSpace(int number)
		{
			for (var i = 0; i < 5; i++)
			{
				for (var j = 0; j < 5; j++)
				{
					if (RemainingSpaces[i][j] == number)
					{
						RemainingSpaces[i][j] = null;
					}
				}
			}
		}
		
		public bool IsBingo()
		{
			// Check Rows
			for (var i = 0; i < 5; i++)
			{
				var numMarked = 0;
				for (var j = 0; j < 5; j++)
				{
					if (RemainingSpaces[i][j] == null) numMarked++;
				}
				if (numMarked == 5) return true;
			}
			
			// Check Columns
			for (var j = 0; j < 5; j++)
			{
				var numMarked = 0;
				for (var i = 0; i < 5; i++)
				{
					if (RemainingSpaces[i][j] == null) numMarked++;
				}
				if (numMarked == 5) return true;
			}
			
			return false;
		}
		
		public int SumUnmarkedSpaces()
		{
			var sum = 0;
			
			for (var i = 0; i < 5; i++)
			{
				for (var j = 0; j < 5; j++)
				{
					if (RemainingSpaces[i][j] != null)
					{
						sum += (int)RemainingSpaces[i][j];
					}
				}
			}
			
			return sum;
		}
	}
}