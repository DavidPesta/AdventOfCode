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
		
		public delegate long OperationMethod(Operator o);
		public OperationMethod Operation;
		public static readonly Dictionary<PacketType, OperationMethod> Operations = new Dictionary<PacketType, OperationMethod>()
		{
			{PacketType.Sum, Sum},
			{PacketType.Product, Product},
			{PacketType.Minimum, Minimum},
			{PacketType.Maximum, Maximum},
			{PacketType.GreaterThan, GreaterThan},
			{PacketType.LessThan, LessThan},
			{PacketType.EqualTo, EqualTo}
		};
		
		private long? ValueCache = null;
		public override long Value
		{
			get
			{
				if (ValueCache == null) ValueCache = Operation(this); // Note: This cache isn't really needed here because a full traversal only calculates this once.
				return (long)ValueCache;
			}
			protected set { throw new Exception("Cannot set the Value on an operator."); }
		}
		
		public Operator(string bin, short version, short typeID) : base(version, typeID)
		{
			Operation = Operations[Type];
			
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
		
		public static long Sum(Operator o)
		{
			long value = 0;
			
			foreach (var subPacket in o.SubPackets)
			{
				value += subPacket.Value;
			}
			
			return value;
		}
		
		public static long Product(Operator o)
		{
			long value = 1;
			
			foreach (var subPacket in o.SubPackets)
			{
				value *= subPacket.Value;
			}
			
			return value;
		}
		
		public static long Minimum(Operator o)
		{
			var value = long.MaxValue;
			
			foreach (var subPacket in o.SubPackets)
			{
				if (subPacket.Value < value) value = subPacket.Value;
			}
			
			return value;
		}
		
		public static long Maximum(Operator o)
		{
			var value = long.MinValue;
			
			foreach (var subPacket in o.SubPackets)
			{
				if (subPacket.Value > value) value = subPacket.Value;
			}
			
			return value;
		}
		
		public static long GreaterThan(Operator o)
		{
			if (o.SubPackets[0].Value > o.SubPackets[1].Value) return 1;
			else return 0;
		}
		
		public static long LessThan(Operator o)
		{
			if (o.SubPackets[0].Value < o.SubPackets[1].Value) return 1;
			else return 0;
		}
		
		public static long EqualTo(Operator o)
		{
			if (o.SubPackets[0].Value == o.SubPackets[1].Value) return 1;
			else return 0;
		}
	}
}