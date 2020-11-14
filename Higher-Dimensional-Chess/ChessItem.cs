using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

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
		public ChessItem(ChessItem other) {
			img = other.img;
			p = other.p;
			pc = other.pc;
			hasMoved = other.hasMoved;
			board = other.board;
			royal = other.royal;
		}
		public ChessItem Clone() => new ChessItem(this); 
		
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
					if(Movable(pc == ChessBoard.PColour.B ? p1Move : p1Move.Neg(), (x, y), (i, j)) ||
					 !hasMoved && Movable(pc == ChessBoard.PColour.B ? p2Move : p2Move.Neg(), (x, y), (i, j))
						)
						res.Add((i, j));
			return res.ToArray();
		}

		private (int, int)[] KnightMovement(int x, int y) {
			List<(int, int)> res = new List<(int, int)>();
			for(int j = 0; j < board.r; ++j) for(int i = 0; i < board.c; ++i)
					if(Movable(NMove, (x, y), (i, j)))
						res.Add((i, j));
			return res.ToArray();
		}

		private (int, int)[] KingMovement(int x, int y) {
			List<(int, int)> res = new List<(int, int)>();
			for(int j = 0; j < board.r; ++j) for(int i = 0; i < board.c; ++i)
					if(Movable(RMove.Concat(BMove), (x, y), (i, j)))
						res.Add((i, j));
			return res.ToArray();
		}

		private (int, int)[] BishopMovement(int x, int y) {
			List<(int, int)> res = new List<(int, int)>();
			for(int j = 0; j < board.r; ++j) for(int i = 0; i < board.c; ++i)
					if(MovableRepeat(board, board.radius, BMove, (x, y), (i, j)))
						res.Add((i, j));
			return res.ToArray();
		}

		private (int, int)[] RookMovement(int x, int y) {
			List<(int, int)> res = new List<(int, int)>();
			for(int j = 0; j < board.r; ++j) for(int i = 0; i < board.c; ++i)
					if(MovableRepeat(board, board.radius, RMove, (x, y), (i, j)))
						res.Add((i, j));
			return res.ToArray();
		}

		private (int, int)[] QueenMovement(int x, int y) {
			List<(int, int)> res = new List<(int, int)>();
			for(int j = 0; j < board.r; ++j) for(int i = 0; i < board.c; ++i)
					if(MovableRepeat(board, board.radius, RMove.Concat(BMove), (x, y), (i, j)))
						res.Add((i, j));
			return res.ToArray();
		}

		//======================================================================================================================================================================MOVMENT-CHECKS
		private static bool Movable((int, int) m, (int, int) p, (int, int) t) => t == p.Add(m);
		private static bool Movable(IEnumerable<(int, int)> m, (int, int) p, (int, int) t) => m.Select(x => x.Add(p)).Any(x => x == t);
		private static bool MovableRepeat(ChessBoard board, int n, (int, int) m, (int, int) p, (int, int) t) => n < 1 ? false : Movable(m, p, t) || MovableRepeat(board, n - 1, m, p, t.Sub(m));
		private static bool MovableRepeat(ChessBoard board, int n, IEnumerable<(int, int)> m, (int, int) p, (int, int) t) => n < 1 ? false : Movable(m, p, t) || m.Select(x => MovableRepeat(board, n - 1, x, p, t.Sub(x))).Any(z => z);

		(int, int)[] p1Move = new (int, int)[] { (0, 1) };
		(int, int)[] p2Move = new (int, int)[] { (0, 2) };
		(int, int)[] pTake = new (int, int)[] { (-1, 1), (1, 1) };
		(int, int)[] RMove = new (int, int)[] { (0, -1), (-1, 0), (1, 0), (0, 1) };
		(int, int)[] BMove = new (int, int)[] { (-1, -1), (1, -1), (-1, 1), (1, 1) };
		(int, int)[] NMove = new (int, int)[] { (-1, -2), (1, -2), (-2, -1), (2, -1), (-2, 1), (2, 1), (-1, 2), (1, 2) };

		//======================================================================================================================================================================BOARD-CHECKS
		public bool InBoard(int x, int y) => x >= 0 && y >= 0 && x < board.c && y < board.r;
		public bool InBoard((int, int) p) => p.Item1 >= 0 && p.Item2 >= 0 && p.Item1 < board.c && p.Item2 < board.r;
		public static bool InBoard(ChessBoard board, int x, int y) => x >= 0 && y >= 0 && x < board.c && y < board.r;
		public static bool InBoard(ChessBoard board, (int, int) p) => p.Item1 >= 0 && p.Item2 >= 0 && p.Item1 < board.c && p.Item2 < board.r;
	}
}
