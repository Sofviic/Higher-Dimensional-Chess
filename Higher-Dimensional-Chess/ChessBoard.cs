using System.Collections.Generic;
using System.Drawing;

namespace _3DChess {
	class ChessBoard {
		public Vector2 size;
		public string[,] pieces;
		public ChessBoard(Vector2 size) {
			pieces = new string[size.x, size.y];
			this.size = size;
		}
		public ChessBoard(string[,] pieces) {
			this.pieces = pieces;
			size = (pieces.GetLength(0), pieces.GetLength(1));
		}
		public ChessBoard(Vector2 size, string[,] pieces) {
			this.pieces = pieces;
			this.size = size;
		}
		public Bitmap DrawWith(Dictionary<string, Bitmap> bpieces, Dictionary<string, Bitmap> bboard, Vector2 bsize) {
			Bitmap res = BitmapFunc.SolidColour(100, 0, 0, bsize.x, bsize.y);
			for(int i = 0; i < size.x; ++i) for(int j = 0; j < size.y; ++j) {
					res = res.Add(bboard[(i + j) % 2 == 1 ? "CB" : "CW"], (i, j) * bsize / size);
					if(!(pieces[i, j] is null) && pieces[i, j] != string.Empty && bpieces.ContainsKey(pieces[i, j])) res = res.Add(bpieces[pieces[i, j]], (i, j) * bsize / size, bboard["CB"]);
				}
			return res;
		}
	}
}
