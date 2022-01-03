using AdventOfCode;
using NUnit.Framework;

namespace Year2021
{
	[TestFixture]
	public class Day16_Tests
	{
		[Test]
		public void Part1Example1VersionSum()
		{
			var bin = Transform.Hex2Bin("8A004A801A8002F478");
			var packet = Packet.ParseNextPacket(bin);
			Assert.AreEqual(16, packet.GetTotalVersion());
		}
		
		[Test]
		public void Part1Example2VersionSum()
		{
			var bin = Transform.Hex2Bin("620080001611562C8802118E34");
			var packet = Packet.ParseNextPacket(bin);
			Assert.AreEqual(12, packet.GetTotalVersion());
		}
		
		[Test]
		public void Part1Example3VersionSum()
		{
			var bin = Transform.Hex2Bin("C0015000016115A2E0802F182340");
			var packet = Packet.ParseNextPacket(bin);
			Assert.AreEqual(23, packet.GetTotalVersion());
		}
		
		[Test]
		public void Part1Example4VersionSum()
		{
			var bin = Transform.Hex2Bin("A0016C880162017C3686B18A3D4780");
			var packet = Packet.ParseNextPacket(bin);
			Assert.AreEqual(31, packet.GetTotalVersion());
		}
		
		[Test]
		public void Part2SumExample()
		{
			var bin = Transform.Hex2Bin("C200B40A82");
			var packet = Packet.ParseNextPacket(bin);
			Assert.AreEqual(3, packet.Value);
		}
		
		[Test]
		public void Part2ProductExample()
		{
			var bin = Transform.Hex2Bin("04005AC33890");
			var packet = Packet.ParseNextPacket(bin);
			Assert.AreEqual(54, packet.Value);
		}
		
		[Test]
		public void Part2MinimumExample()
		{
			var bin = Transform.Hex2Bin("880086C3E88112");
			var packet = Packet.ParseNextPacket(bin);
			Assert.AreEqual(7, packet.Value);
		}
		
		[Test]
		public void Part2MaximumExample()
		{
			var bin = Transform.Hex2Bin("CE00C43D881120");
			var packet = Packet.ParseNextPacket(bin);
			Assert.AreEqual(9, packet.Value);
		}
		
		[Test]
		public void Part2LessThanExample()
		{
			var bin = Transform.Hex2Bin("D8005AC2A8F0");
			var packet = Packet.ParseNextPacket(bin);
			Assert.AreEqual(1, packet.Value);
		}
		
		[Test]
		public void Part2GreaterThanExample()
		{
			var bin = Transform.Hex2Bin("F600BC2D8F");
			var packet = Packet.ParseNextPacket(bin);
			Assert.AreEqual(0, packet.Value);
		}
		
		[Test]
		public void Part2EqualToExample()
		{
			var bin = Transform.Hex2Bin("9C005AC2F8F0");
			var packet = Packet.ParseNextPacket(bin);
			Assert.AreEqual(0, packet.Value);
		}
		
		[Test]
		public void Part2NestedExample()
		{
			var bin = Transform.Hex2Bin("9C0141080250320F1802104A08");
			var packet = Packet.ParseNextPacket(bin);
			Assert.AreEqual(1, packet.Value);
		}
	}
}