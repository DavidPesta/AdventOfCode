using AdventOfCode;

namespace Year2021
{
	public enum LengthTypeMode : ushort
	{
		Length = 0,
		Number = 1
	}
	
	public class Operator : Packet
	{
		protected LengthTypeMode LengthTypeMode;
		protected string LengthBin;
		protected ushort LengthNumber;
		protected List<Packet> SubPackets = new List<Packet>();
		
		public delegate long Operation();
		public Dictionary<PacketType, Operation> Operations = new Dictionary<PacketType, Operation>();
		
		private long? ValueCache = null;
		public override long Value
		{
			get {
				if (ValueCache == null) ValueCache = Operations[Type]();
				return (long)ValueCache;
			}
			protected set { throw new Exception("Cannot set the Value on an operator."); }
		}
		
		public Operator(string bin, short version, short typeID) : base(version, typeID)
		{
			Operations[PacketType.Sum] = Sum;
			Operations[PacketType.Product] = Product;
			Operations[PacketType.Minimum] = Minimum;
			Operations[PacketType.Maximum] = Maximum;
			Operations[PacketType.GreaterThan] = GreaterThan;
			Operations[PacketType.LessThan] = LessThan;
			Operations[PacketType.EqualTo] = EqualTo;
			
			LengthTypeMode = (LengthTypeMode)ushort.Parse(bin[6].ToString());
			
			if (LengthTypeMode == LengthTypeMode.Length)
			{
				LengthBin = bin.Substring(7, 15);
				LengthNumber = (ushort)Transform.Bin2Dec(LengthBin);
				PacketBin = bin.Substring(0, 7+15);
				var remainingBin = bin.Substring(7+15);
				BuildSubPacketsWithLengthMode(remainingBin);
			}
			
			if (LengthTypeMode == LengthTypeMode.Number)
			{
				LengthBin = bin.Substring(7, 11);
				LengthNumber = (ushort)Transform.Bin2Dec(LengthBin);
				PacketBin = bin.Substring(0, 7+11);
				var remainingBin = bin.Substring(7+11);
				BuildSubPacketsWithNumberMode(remainingBin);
			}
		}
		
		private void BuildSubPacketsWithLengthMode(string remainingBin)
		{
			var remainingLength = LengthNumber;
			
			do
			{
				var packet = Packet.ParseNextPacket(remainingBin);
				SubPackets.Add(packet);
				PacketBin += packet.PacketBin;
				remainingBin = remainingBin.Substring((int)packet.BinLength);
				remainingLength -= (ushort)packet.BinLength;
			} while (remainingLength > 0);
		}
		
		private void BuildSubPacketsWithNumberMode(string remainingBin)
		{
			for (var i = 0; i < LengthNumber; i++)
			{
				var packet = Packet.ParseNextPacket(remainingBin);
				SubPackets.Add(packet);
				PacketBin += packet.PacketBin;
				remainingBin = remainingBin.Substring((int)packet.BinLength);
			}
		}
		
		public override long GetTotalVersion()
		{
			long version = Version;
			
			foreach (var subPacket in SubPackets)
			{
				version += subPacket.GetTotalVersion();
			}
			
			return version;
		}
		
		public long Sum()
		{
			long value = 0;
			
			foreach (var subPacket in SubPackets)
			{
				value += subPacket.Value;
			}
			
			return value;
		}
		
		public long Product()
		{
			long value = 1;
			
			foreach (var subPacket in SubPackets)
			{
				value *= subPacket.Value;
			}
			
			return value;
		}
		
		public long Minimum()
		{
			var value = long.MaxValue;
			
			foreach (var subPacket in SubPackets)
			{
				if (subPacket.Value < value) value = subPacket.Value;
			}
			
			return value;
		}
		
		public long Maximum()
		{
			var value = long.MinValue;
			
			foreach (var subPacket in SubPackets)
			{
				if (subPacket.Value > value) value = subPacket.Value;
			}
			
			return value;
		}
		
		public long GreaterThan()
		{
			if (SubPackets[0].Value > SubPackets[1].Value) return 1;
			else return 0;
		}
		
		public long LessThan()
		{
			if (SubPackets[0].Value < SubPackets[1].Value) return 1;
			else return 0;
		}
		
		public long EqualTo()
		{
			if (SubPackets[0].Value == SubPackets[1].Value) return 1;
			else return 0;
		}
	}
}