/*
* Console Menu - MIT License
* Copyright (c) 2021 David Pesta
* https://github.com/DavidPesta/ConsoleMenu
*/

namespace AdventOfCode
{
	public class ExitOption : MenuAction
	{
		public ExitOption()
		{
			Command = "x";
			MenuText = "Exit the menu.";
		}
		
		public override MenuSignal Action()
		{
			return MenuSignal.Exit;
		}
	}
}