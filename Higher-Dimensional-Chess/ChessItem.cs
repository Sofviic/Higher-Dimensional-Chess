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
		public bool CanMoveTo(Vector2 from, Vector2 to) {//TODO: Check Blocks
			return Movement(from.x, from.y).Contains(to);
		}

		public Vector2[] Movement(int x, int y) {
			return
				p == ChessBoard.P.WP || p == ChessBoard.P.BP ? PawnMovement(x, y) :
				p == ChessBoard.P.WN || p == ChessBoard.P.BN ? KnightMovement(x, y) :
				p == ChessBoard.P.WK || p == ChessBoard.P.BK ? KingMovement(x, y) :
				p == ChessBoard.P.WB || p == ChessBoard.P.BB ? BishopMovement(x, y) :
				p == ChessBoard.P.WR || p == ChessBoard.P.BR ? RookMovement(x, y) :
				p == ChessBoard.P.WQ || p == ChessBoard.P.BQ ? QueenMovement(x, y) :
				new Vector2[0];
		}

		private Vector2[] PawnMovement(int x, int y) {
			List<Vector2> res = new List<Vector2>();
			for(int j = 0; j < board.r; ++j) for(int i = 0; i < board.c; ++i)
					if(Movable(pc == ChessBoard.PColour.B ? p1Move : p1Move.Neg(), (x, y), (i, j)) ||
					 !hasMoved && Movable(pc == ChessBoard.PColour.B ? p2Move : p2Move.Neg(), (x, y), (i, j))
						)
						res.Add((i, j));
			return res.ToArray();
		}
		
		private Vector2[] KnightMovement(int x, int y) {
			List<Vector2> res = new List<Vector2>();
			for(int j = 0; j < board.r; ++j) for(int i = 0; i < board.c; ++i)
					if(Movable(NMove, (x, y), (i, j)))
						res.Add((i, j));
			return res.ToArray();
		}

		private Vector2[] KingMovement(int x, int y) {
			List<Vector2> res = new List<Vector2>();
			for(int j = 0; j < board.r; ++j) for(int i = 0; i < board.c; ++i)
					if(Movable(RMove.Concat(BMove), (x, y), (i, j)))
						res.Add((i, j));
			return res.ToArray();
		}

		private Vector2[] BishopMovement(int x, int y) {
			List<Vector2> res = new List<Vector2>();
			for(int j = 0; j < board.r; ++j) for(int i = 0; i < board.c; ++i)
					if(MovableRepeat(board, board.radius, BMove, (x, y), (i, j)))
						res.Add((i, j));
			return res.ToArray();
		}

		private Vector2[] RookMovement(int x, int y) {
			List<Vector2> res = new List<Vector2>();
			for(int j = 0; j < board.r; ++j) for(int i = 0; i < board.c; ++i)
					if(MovableRepeat(board, board.radius, RMove, (x, y), (i, j)))
						res.Add((i, j));
			return res.ToArray();
		}

		private Vector2[] QueenMovement(int x, int y) {
			List<Vector2> res = new List<Vector2>();
			for(int j = 0; j < board.r; ++j) for(int i = 0; i < board.c; ++i)
					if(MovableRepeat(board, board.radius, RMove.Concat(BMove), (x, y), (i, j)))
						res.Add((i, j));
			return res.ToArray();
		}

		//======================================================================================================================================================================MOVMENT-CHECKS
		private static bool Movable(Vector2 m, Vector2 p, Vector2 t) => t == p.Add(m);
		private static bool Movable(IEnumerable<Vector2> m, Vector2 p, Vector2 t) => m.Select(x => x.Add(p)).Any(x => x == t);
		private static bool MovableRepeat(ChessBoard board, int n, Vector2 m, Vector2 p, Vector2 t) => n < 1 ? false : Movable(m, p, t) || MovableRepeat(board, n - 1, m, p, t.Sub(m));
		private static bool MovableRepeat(ChessBoard board, int n, IEnumerable<Vector2> m, Vector2 p, Vector2 t) => n < 1 ? false : Movable(m, p, t) || m.Select(x => MovableRepeat(board, n - 1, x, p, t.Sub(x))).Any(z => z);

		Vector2[] p1Move = new Vector2[] { (0, 1) };
		Vector2[] p2Move = new Vector2[] { (0, 2) };
		Vector2[] pTake = new Vector2[] { (-1, 1), (1, 1) };
		Vector2[] RMove = new Vector2[] { (0, -1), (-1, 0), (1, 0), (0, 1) };
		Vector2[] BMove = new Vector2[] { (-1, -1), (1, -1), (-1, 1), (1, 1) };
		Vector2[] NMove = new Vector2[] { (-1, -2), (1, -2), (-2, -1), (2, -1), (-2, 1), (2, 1), (-1, 2), (1, 2) };

		//======================================================================================================================================================================BOARD-CHECKS
		public bool InBoard(int x, int y) => x >= 0 && y >= 0 && x < board.c && y < board.r;
		public bool InBoard(Vector2 p) => p.x >= 0 && p.y >= 0 && p.x < board.c && p.y < board.r;
		public static bool InBoard(ChessBoard board, int x, int y) => x >= 0 && y >= 0 && x < board.c && y < board.r;
		public static bool InBoard(ChessBoard board, Vector2 p) => p.x >= 0 && p.y >= 0 && p.x < board.c && p.y < board.r;
	}
}
