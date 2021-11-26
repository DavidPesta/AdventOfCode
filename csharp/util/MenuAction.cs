/*
* Console Menu - MIT License
* Copyright (c) 2021 David Pesta
* https://github.com/DavidPesta/ConsoleMenu
*/

namespace AdventOfCode
{
	public abstract class MenuAction
	{
		protected ProgramData Data;
		
		public virtual string Command { get; protected set; }
		public virtual string MenuText { get; protected set; }
		
		public abstract MenuSignal Action();
		
		public void SetData(ProgramData data)
		{
			Data = data;
		}
		
		public string MenuDisplay()
		{
			string display = "";
			
			if (Command == null)
			{
				display = MenuText;
			}
			else
			{
				display = $"{Color.Bold}{Color.Green}{Command}{Color.Normal}. {MenuText}";
			}
			
			return display;
		}
	}
}