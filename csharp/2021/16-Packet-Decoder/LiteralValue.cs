using AdventOfCode;

namespace Year2021
{
	public class LiteralValue : Packet
	{
		protected string LiteralBin = "";
		public override long Value { get; protected set; }
		
		public LiteralValue(string bin, short version, short typeID) : base(version, typeID)
		{
			PacketBin = bin.Substring(0, 6);
			var remainingBin = bin.Substring(6);
			
			string valueBin = "";
			int lastChunkIndex;
			var chunkIndex = 0;
			do
			{
				LiteralBin += remainingBin.Substring(chunkIndex, 5);
				valueBin += remainingBin.Substring(chunkIndex + 1, 4);
				lastChunkIndex = chunkIndex;
				chunkIndex += 5;
			} while (remainingBin[lastChunkIndex] != '0');
			
			PacketBin += LiteralBin;
			
			Value = Transform.Bin2Dec(valueBin);
		}
		
		public override long GetTotalVersion()
		{
			return Version;
		}
	}
}