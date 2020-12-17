using System.Collections.Generic;
using System.Drawing;

namespace _3DChess {
	class ChessBoard {
		public const bool STUPIDITY = false;
		public Vector2 size { get; private set; }
		public string[,] pieces { get; private set; }
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
		public void DrawWith(Dictionary<string, Bitmap> bpieces, Dictionary<string, Bitmap> bboard, Vector2 bsize, Graphics g) {
			for(int i = 0; i < size.x; ++i) for(int j = 0; j < size.y; ++j) {
					try {
						g.DrawImage(bboard[(i + j) % 2 == 1 ? "CB" : "CW"], (i, j) * bsize / size);
					} catch { }
					if(IsPiece(pieces[i, j]) && bpieces.ContainsKey(pieces[i, j])) g.DrawImage(bpieces[pieces[i, j]], (i, j) * bsize / size - bpieces[pieces[i, j]].Centre(bboard["CB"]));
				}
#pragma warning disable CS0162 // Unreachable code detected
			if(STUPIDITY) g.DrawImage(BitmapFunc.SolidColour(0, 0, 0, bsize.x, bsize.y), Vector2.Zero);
#pragma warning restore CS0162 // Unreachable code detected
		}
		public void DrawWith(Dictionary<string, Bitmap> bpieces, Dictionary<string, Bitmap> bboard, Vector2 bsize, Graphics g, bool[,] cells) {
			for(int i = 0; i < size.x; ++i) for(int j = 0; j < size.y; ++j) if(cells[i, j]) {
						try {
							g.DrawImage(bboard[(i + j) % 2 == 1 ? "CB" : "CW"], (i, j) * bsize / size);
						} catch { }
						if(IsPiece(pieces[i, j]) && bpieces.ContainsKey(pieces[i, j])) g.DrawImage(bpieces[pieces[i, j]], (i, j) * bsize / size - bpieces[pieces[i, j]].Centre(bboard["CB"]));
					}
		}
		public void DrawWith(Dictionary<string, Bitmap> bpieces, Dictionary<string, Bitmap> bboard, Vector2 bsize, Graphics g, bool[,] cells, Vector2 undraw) {
			for(int i = 0; i < size.x; ++i) for(int j = 0; j < size.y; ++j) if(cells[i, j] || (i, j) == undraw) {
						try {
							g.DrawImage(bboard[(i + j) % 2 == 1 ? "CB" : "CW"], (i, j) * bsize / size);
						} catch { }
						if((i, j) != undraw && IsPiece(pieces[i, j]) && bpieces.ContainsKey(pieces[i, j])) g.DrawImage(bpieces[pieces[i, j]], (i, j) * bsize / size - bpieces[pieces[i, j]].Centre(bboard["CB"]));
					}
		}
		public ChessBoard Copy() => new ChessBoard(size, pieces);
		public ChessBoard SetCell(Vector2 cell, string piece) {
			string[,] np = pieces;
			np[cell.x, cell.y] = piece;
			ChessBoard c = new ChessBoard(size, np);
			return c;
		}
		public bool IsPiece(string id) => !IsNotPiece(id);
		public bool IsNotPiece(string id) => id is null || id == string.Empty;
	}
}
