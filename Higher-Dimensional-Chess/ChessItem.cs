using System;
using System.Collections.Generic;
using System.Drawing;

namespace _3DChess {
	class ChessItem {
		public Bitmap img;
		public ChessBoard.P p;
		public ChessBoard.PColour pc;
		public bool hasMoved;
		public ChessBoard board;
		public bool royal;
		public ChessItem(ChessBoard parent, Bitmap image, ChessBoard.P piece, bool royal = false) : this(parent, image, piece, piece.ToString()[0] == 'W' ? ChessBoard.PColour.W : ChessBoard.PColour.B, royal) { }
		public ChessItem(ChessBoard parent, Bitmap image, ChessBoard.P piece, ChessBoard.PColour pieceColour, bool royal = false) {
			board = parent;
			img = image;
			p = piece;
			pc = pieceColour;
			hasMoved = false;
			this.royal = royal;
		}

		//======================================================================================================================================================================MOVMENT
		public (int, int)[] Movement(int x, int y) {
			return
				p == ChessBoard.P.WP || p == ChessBoard.P.BP ? PawnMovement(x, y) :
				p == ChessBoard.P.WN || p == ChessBoard.P.BN ? KnightMovement(x, y) :
				p == ChessBoard.P.WK || p == ChessBoard.P.BK ? KingMovement(x, y) :
				p == ChessBoard.P.WB || p == ChessBoard.P.BB ? BishopMovement(x, y) :
				p == ChessBoard.P.WR || p == ChessBoard.P.BR ? RookMovement(x, y) :
				p == ChessBoard.P.WQ || p == ChessBoard.P.BQ ? QueenMovement(x, y) :
				new (int, int)[0];
		}

		private (int, int)[] PawnMovement(int x, int y) {
			List<(int, int)> res = new List<(int, int)>();
			for(int j = 0; j < board.r; ++j) for(int i = 0; i < board.c; ++i)
					if(hasMoved && PawnOnceMovable(x, y, i, j, pc == ChessBoard.PColour.B) ||
					 !hasMoved && PawnTwiceMovable(x, y, i, j, pc == ChessBoard.PColour.B)
						)
						res.Add((i, j));
			return res.ToArray();
		}

		private (int, int)[] KnightMovement(int x, int y) {
			List<(int, int)> res = new List<(int, int)>();
			for(int j = 0; j < board.r; ++j) for(int i = 0; i < board.c; ++i)
					if(KnightMovable(x, y, i, j))
						res.Add((i, j));
			return res.ToArray();
		}

		private (int, int)[] KingMovement(int x, int y) {
			List<(int, int)> res = new List<(int, int)>();
			for(int j = 0; j < board.r; ++j) for(int i = 0; i < board.c; ++i)
					if(KingMovable(x, y, i, j))
						res.Add((i, j));
			return res.ToArray();
		}

		private (int, int)[] BishopMovement(int x, int y) {
			List<(int, int)> res = new List<(int, int)>();
			for(int j = 0; j < board.r; ++j) for(int i = 0; i < board.c; ++i)
					if(BishopMovable(x, y, i, j))
						res.Add((i, j));
			return res.ToArray();
		}

		private (int, int)[] RookMovement(int x, int y) {
			List<(int, int)> res = new List<(int, int)>();
			for(int j = 0; j < board.r; ++j) for(int i = 0; i < board.c; ++i)
					if(RookMovable(x, y, i, j))
						res.Add((i, j));
			return res.ToArray();
		}

		private (int, int)[] QueenMovement(int x, int y) {
			List<(int, int)> res = new List<(int, int)>();
			for(int j = 0; j < board.r; ++j) for(int i = 0; i < board.c; ++i)
					if(QueenMovable(x, y, i, j))
						res.Add((i, j));
			return res.ToArray();
		}

		//======================================================================================================================================================================MOVMENT-CHECKS
		private static bool PawnOnceMovable(int sx, int sy, int dx, int dy, bool posY) => dx == sx && dy == sy + (posY ? 1 : -1);
		private static bool PawnTwiceMovable(int sx, int sy, int dx, int dy, bool posY) => dx == sx && (dy == sy + (posY ? 1 : -1) || dy == sy + (posY ? 2 : -2));

		private static bool _21Movable(int sx, int sy, int dx, int dy) => (dx == sx + 2 || dx == sx - 2) && (dy == sy + 1 || dy == sy - 1);
		private static bool _12Movable(int sx, int sy, int dx, int dy) => (dx == sx + 1 || dx == sx - 1) && (dy == sy + 2 || dy == sy - 2);
		private static bool KnightMovable(int sx, int sy, int dx, int dy) => _21Movable(sx, sy, dx, dy) || _12Movable(sx, sy, dx, dy);

		private static bool _1DiagMovable(int sx, int sy, int dx, int dy) => (dx == sx + 1 || dx == sx - 1) && (dy == sy + 1 || dy == sy - 1);
		private static bool _1OrthMovable(int sx, int sy, int dx, int dy) => (dx == sx && (dy == sy + 1 || dy == sy - 1)) || ((dx == sx + 1 || dx == sx - 1) && dy == sy);
		private static bool KingMovable(int sx, int sy, int dx, int dy) => _1DiagMovable(sx, sy, dx, dy) || _1OrthMovable(sx, sy, dx, dy);

		private static bool BishopMovable(int sx, int sy, int dx, int dy) => dx != sx && Math.Abs(dx - sx) == Math.Abs(dy - sy);

		private static bool RookMovable(int sx, int sy, int dx, int dy) => dx == sx ^ dy == sy;

		private static bool QueenMovable(int sx, int sy, int dx, int dy) => BishopMovable(sx, sy, dx, dy) || RookMovable(sx, sy, dx, dy);

		//======================================================================================================================================================================BOARD-CHECKS
		public bool InBoard(int x, int y) => x >= 0 && y >= 0 && x < board.c && y < board.r;
	}
}
