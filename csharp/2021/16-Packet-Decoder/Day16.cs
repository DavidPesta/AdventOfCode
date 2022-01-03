using AdventOfCode;

namespace Year2021
{
	public class Day16 : DayOption
	{
		public Day16(string day) : base(day, "Packet Decoder"){}
		
		public void Part1(string input)
		{
			var bin = Transform.Hex2Bin(input);
			var packet = Packet.ParseNextPacket(bin);
			Console.WriteLine(packet.GetTotalVersion());
		}
		
		public void Part2(string input)
		{
			var bin = Transform.Hex2Bin(input);
			var packet = Packet.ParseNextPacket(bin);
			Console.WriteLine(packet.Value);
		}
	}
}