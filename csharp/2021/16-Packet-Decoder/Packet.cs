using AdventOfCode;

namespace Year2021
{
	public enum PacketType : ushort
	{
		Sum = 0,
		Product = 1,
		Minimum = 2,
		Maximum = 3,
		Literal = 4,
		GreaterThan = 5,
		LessThan = 6,
		EqualTo = 7
	}
	
	public abstract class Packet
	{
		public string PacketBin { get; protected set; }
		protected short Version;
		protected PacketType Type;
		
		public int BinLength
		{
			get { return PacketBin.Length; }
		}
		
		public abstract long Value { get; protected set; }
		
		public Packet(short version, short typeID)
		{
			Version = version;
			Type = (PacketType)typeID;
		}
		
		public abstract long GetTotalVersion();
		
		public static Packet ParseNextPacket(string bin)
		{
			var version = (short)Transform.Bin2Dec(bin.Substring(0, 3));
			var typeID = (short)Transform.Bin2Dec(bin.Substring(3, 3));
			
			if (typeID == 4) return new LiteralValue(bin, version, typeID);
			else return new Operator(bin, version, typeID);
		}
	}
}