using AdventOfCode;

namespace Year2019
{
	public class Day02 : DayOption
	{
		public Day02(string day) : base(day, "1202 Program Alarm"){}
		
		public void Part1(string input)
		{
			var program = input.Split(',').Select(n => int.Parse(n)).ToList();
			
			program[1] = 12;
			program[2] = 2;
			
			Computer(program);
			
			Console.WriteLine(program[0]);
		}
		
		public void Part2(string input)
		{
			var originalProgram = input.Split(',').Select(n => int.Parse(n)).ToList();
			
			int? solution = null;
			for (var noun = 0; noun < 100; noun++)
			{
				for (var verb = 0; verb < 100; verb++)
				{
					var program = new List<int>(originalProgram);
					
					program[1] = noun;
					program[2] = verb;
					
					Computer(program);
					
					if (program[0] == 19690720)
					{
						solution = 100 * noun + verb;
						break;
					}
				}
				if (solution != null) break;
			}
			
			Console.WriteLine(solution);
		}
		
		public void Computer(List<int> program)
		{
			var numParams = 0;
			var pointer = 0;
			
			while(pointer < program.Count) // early abort if end of program reached
			{
				if (program[pointer] == 1)
				{
					numParams = 3;
					if (pointer + numParams >= program.Count) break; // early abort if end of program reached
					
					program[program[pointer+3]] = program[program[pointer+1]] + program[program[pointer+2]];
				}
				
				if (program[pointer] == 2)
				{
					numParams = 3;
					if (pointer + numParams >= program.Count) break; // early abort if end of program reached
					
					program[program[pointer+3]] = program[program[pointer+1]] * program[program[pointer+2]];
				}
				
				if (program[pointer] == 99)
				{
					numParams = 0;
					if (pointer + numParams >= program.Count) break; // early abort if end of program reached
					
					break; // program end by signal 99
				}
				
				pointer += 1 + numParams;
			}
		}
		
		/* Simpler and works, but less correct and robust:
		public void Computer(List<int> program)
		{
			var cursor = 0;
			
			while(program[cursor] != 99)
			{
				if (program[cursor] == 1)
				{
					program[program[cursor+3]] = program[program[cursor+1]] + program[program[cursor+2]];
				}
				
				if (program[cursor] == 2)
				{
					program[program[cursor+3]] = program[program[cursor+1]] * program[program[cursor+2]];
				}
				
				cursor += 4;
			}
		}
		*/
	}
}