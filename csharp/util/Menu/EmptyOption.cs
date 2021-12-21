/*
* Console Menu - MIT License
* Copyright (c) 2021 David Pesta
* https://github.com/DavidPesta/ConsoleMenu
*/

namespace AdventOfCode
{
	public class EmptyOption : MenuAction
	{
		public EmptyOption(string menuText)
		{
			MenuText = menuText;
		}
		
		public override MenuSignal Action()
		{
			return MenuSignal.Continue;
		}
	}
}