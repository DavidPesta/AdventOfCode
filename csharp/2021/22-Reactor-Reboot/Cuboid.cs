namespace Year2021
{
	public enum Dim
	{
		X = 1,
		Y = 2,
		Z = 3
	}
	
	public class Cuboid
	{
		public Dictionary<Dim, (long, long)> Dimensions = new Dictionary<Dim, (long, long)>();
		
		public long X1
		{
			get => Dimensions[Dim.X].Item1;
			set => Dimensions[Dim.X] = (value, Dimensions[Dim.X].Item2);
		}
		
		public long X2
		{
			get => Dimensions[Dim.X].Item2;
			set => Dimensions[Dim.X] = (Dimensions[Dim.X].Item1, value);
		}
		
		public long Y1
		{
			get => Dimensions[Dim.Y].Item1;
			set => Dimensions[Dim.Y] = (value, Dimensions[Dim.Y].Item2);
		}
		
		public long Y2
		{
			get => Dimensions[Dim.Y].Item2;
			set => Dimensions[Dim.Y] = (Dimensions[Dim.Y].Item1, value);
		}
		
		public long Z1
		{
			get => Dimensions[Dim.Z].Item1;
			set => Dimensions[Dim.Z] = (value, Dimensions[Dim.Z].Item2);
		}
		
		public long Z2
		{
			get => Dimensions[Dim.Z].Item2;
			set => Dimensions[Dim.Z] = (Dimensions[Dim.Z].Item1, value);
		}
		
		public long Volume { get => (X2-X1+1)*(Y2-Y1+1)*(Z2-Z1+1); }
		
		public Cuboid(long x1, long x2, long y1, long y2, long z1, long z2)
		{
			Dimensions[Dim.X] = (x1, x2);
			Dimensions[Dim.Y] = (y1, y2);
			Dimensions[Dim.Z] = (z1, z2);
		}
		
		public bool DetectCollision(Cuboid c)
		{
			if (c.X1 > X2) return false;
			if (c.X2 < X1) return false;
			if (c.Y1 > Y2) return false;
			if (c.Y2 < Y1) return false;
			if (c.Z1 > Z2) return false;
			if (c.Z2 < Z1) return false;
			return true;
		}
		
		public List<Cuboid> ClobberWithCuboid(Cuboid c)
		{
			var pieces = new List<Cuboid>();
			//pieces.Add((Cuboid)MemberwiseClone());
			pieces.Add(new Cuboid(X1, X2, Y1, Y2, Z1, Z2));
			
			if (c.X1 > X1 && c.X1 <= X2) pieces = SlicePiecesLeftOfPosition(pieces, Dim.X, c.X1);
			if (c.X2 >= X1 && c.X2 < X2) pieces = SlicePiecesLeftOfPosition(pieces, Dim.X, c.X2+1);
			
			if (c.Y1 > Y1 && c.Y1 <= Y2) pieces = SlicePiecesLeftOfPosition(pieces, Dim.Y, c.Y1);
			if (c.Y2 >= Y1 && c.Y2 < Y2) pieces = SlicePiecesLeftOfPosition(pieces, Dim.Y, c.Y2+1);
			
			if (c.Z1 > Z1 && c.Z1 <= Z2) pieces = SlicePiecesLeftOfPosition(pieces, Dim.Z, c.Z1);
			if (c.Z2 >= Z1 && c.Z2 < Z2) pieces = SlicePiecesLeftOfPosition(pieces, Dim.Z, c.Z2+1);
			
			return RemovePiecesFromClobberSpace(pieces, c);
		}
		
		private List<Cuboid> SlicePiecesLeftOfPosition(List<Cuboid> pieces, Dim dim, long position)
		{
			var newPieces = new List<Cuboid>();
			
			foreach (var piece in pieces)
			{
				var (p1, p2) = piece.Dimensions[dim];
				if (position > p1 && position <= p2)
				{
					// Why is leftPiece the same exact object as piece after a clone???
					//var leftPiece = (Cuboid)piece.MemberwiseClone();
					var leftPiece = new Cuboid(piece.X1, piece.X2, piece.Y1, piece.Y2, piece.Z1, piece.Z2);
					leftPiece.Dimensions[dim] = (p1, position-1);
					newPieces.Add(leftPiece);
					
					//var rightPiece = (Cuboid)piece.MemberwiseClone();
					var rightPiece = new Cuboid(piece.X1, piece.X2, piece.Y1, piece.Y2, piece.Z1, piece.Z2);
					rightPiece.Dimensions[dim] = (position, p2);
					newPieces.Add(rightPiece);
				}
				else
				{
					newPieces.Add(piece);
				}
			}
			
			return newPieces;
		}
		
		private List<Cuboid> RemovePiecesFromClobberSpace(List<Cuboid> pieces, Cuboid clobberingCuboid)
		{
			var remainingPieces = new List<Cuboid>();
			
			foreach (var piece in pieces)
			{
				if (!piece.DetectCollision(clobberingCuboid)) remainingPieces.Add(piece);
			}
			
			return remainingPieces;
		}
	}
}