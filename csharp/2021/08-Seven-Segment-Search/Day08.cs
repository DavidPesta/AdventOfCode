using AdventOfCode;

namespace Year2021
{
	public class Day08 : DayOption
	{
		public Day08(string day) : base(day, "Seven Segment Search"){}
		
		// Number of segments in digits:
		// 2: 1
		// 3: 7
		// 4: 4
		// 5: 2, 3, 5
		// 6: 0, 6, 9
		// 7: 8
		
		public void Part1(string input)
		{
			var lineOutputs = input.Split("\n").Select(l => l.Split("|")[1]);
			
			var numSingles = 0;
			foreach (var lineOutput in lineOutputs)
			{
				var segments = lineOutput.Split(" ");
				foreach (var segment in segments)
				{
					var l = segment.Length;
					if (l == 2 || l == 3 || l == 4 || l == 7) numSingles++;
				}
			}
			
			Console.WriteLine(numSingles);
		}
		
		public void Part2(string input)
		{
			var lineInputs = input.Split("\n").Select(l => l.Split("|")[0].Trim()).ToList();
			var lineOutputs = input.Split("\n").Select(l => l.Split("|")[1].Trim()).ToList();
			
			var segmentDigitsToDigits = new Dictionary<string, char>()
			{
				{"abcefg", '0'},
				{"cf", '1'},
				{"acdeg", '2'},
				{"acdfg", '3'},
				{"bcdf", '4'},
				{"abdfg", '5'},
				{"abdefg", '6'},
				{"acf", '7'},
				{"abcdefg", '8'},
				{"abcdfg", '9'}
			};
			
			var sumLineNumbers = 0;
			
			for (var i = 0; i < lineInputs.Count; i++)
			{
				var signalsToSegments = MapSignalsToSegments(lineInputs[i]);
				var signalDigits = lineOutputs[i].Split(" ");
				
				var lineDigits = "";
				foreach (var signalDigit in signalDigits)
				{
					var digitSegments = new List<char>();
					foreach (var signal in signalDigit)
					{
						digitSegments.Add(signalsToSegments[signal]);
					}
					var orderedDigitSegments = digitSegments.OrderBy(ds => ds).ToList();
					
					var segmentDigit = string.Join("", orderedDigitSegments);
					lineDigits += segmentDigitsToDigits[segmentDigit];
				}
				
				sumLineNumbers += int.Parse(lineDigits);
			}
			
			Console.WriteLine(sumLineNumbers);
		}
		
		// Number of segments across all 10 digits.
		// a: 8
		// b: 6
		// c: 8
		// d: 7
		// e: 4
		// f: 9
		// g: 7
		
		private Dictionary<char, char> MapSignalsToSegments(string lineInput)
		{
			// Find segment a's signal by removing the 2 signal input from the 3 signal input. (Segments for 1 and 7 are known, and 7 extends 1 by one segment.)
			// Find segment b's signal by finding the signal that appears 6 times across all digits.
			// Find segment c's signal by finding the signal that is NOT segment a's signal that appears 8 times across all digits.
			// Find segment e's signal by finding the signal that appears 4 times across all digits.
			// Find segment f's signal by finding the signal that appears 9 times across all digits.
			// Find segment d's signal by finding the signal that appears 7 times across all digits that also appears in the known segments for digit 4.
			// Find segment g's signal by finding the signal that appears 7 times across all digits that doesn't appear in the known segments for digit 4.
			
			var signalsToSegments = new Dictionary<char, char>();
			
			var signalDigits = lineInput.Split(" ");
			
			// Segment a's signal:
			var twoSignals = signalDigits.Where(s => s.Length == 2).First();
			var threeSignals = signalDigits.Where(s => s.Length == 3).First();
			var aSignal = threeSignals.ToCharArray().ToList();
			foreach (var twoSignal in twoSignals)
			{
				aSignal.Remove(twoSignal);
			}
			signalsToSegments[aSignal[0]] = 'a';
			
			var signalCounts = new Dictionary<char, int>()
			{
				{'a', 0},
				{'b', 0},
				{'c', 0},
				{'d', 0},
				{'e', 0},
				{'f', 0},
				{'g', 0}
			};
			foreach (var signalDigit in signalDigits)
			{
				foreach (var signal in signalDigit)
				{
					signalCounts[signal]++;
				}
			}
			
			var digitFourSignals = signalDigits.Where(s => s.Length == 4).First().ToCharArray().ToList();
			
			foreach (KeyValuePair<char, int> kvp in signalCounts)
			{
				var signal = kvp.Key;
				var count = kvp.Value;
				
				if (count == 6) signalsToSegments[signal] = 'b';
				if (count == 8 && signal != aSignal[0]) signalsToSegments[signal] = 'c';
				if (count == 4) signalsToSegments[signal] = 'e';
				if (count == 9) signalsToSegments[signal] = 'f';
				if (count == 7 && digitFourSignals.Contains(signal)) signalsToSegments[signal] = 'd';
				if (count == 7 && !digitFourSignals.Contains(signal)) signalsToSegments[signal] = 'g';
			}
			
			return signalsToSegments;
		}
	}
}