using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace _3DChess {
	class ChessEngine {
		public readonly Vector2 boardSize;
		bool[,] changes;
		dynamic pieceRules;
		dynamic textures;

		public Dictionary<string, (Bitmap, Vector2[], int)> piecesDict { get; private set; }
		public Dictionary<string, Bitmap> texturDict { get; private set; }

		//======================================================================================================================================================================INIT
		public ChessBoard current { get; private set; }
		public ChessEngine(Vector2 boardSize, string pieceJSON, string textureJSON) {
			this.boardSize = boardSize;
			changes = new bool[boardSize.x, boardSize.y];
			pieceRules = JsonConvert.DeserializeObject(File.ReadAllText(pieceJSON));
			textures = JsonConvert.DeserializeObject(File.ReadAllText(textureJSON));
			piecesDict = PiecesDictionary();
			texturDict = BoardDictionary();
			current = SetupBoard();
		}
		public Dictionary<string, (Bitmap, Vector2[], int)> PiecesDictionary() {
			Dictionary<string, (Bitmap, Vector2[], int)> d = new Dictionary<string, (Bitmap, Vector2[], int)>();
			string path = Path.Combine(Application.StartupPath, @"..\..\");
			foreach(dynamic piece in pieceRules) {
				d.Add(piece["ID"].ToString() + "W", ((Bitmap)Image.FromFile(path + piece["Image"]["W"].ToString()), MathFunc.Neg((Vector2[])ParseVector2Array(piece["Movement"]["Move"])), (int)piece["Movement"]["Hops"]));
				d.Add(piece["ID"].ToString() + "B", ((Bitmap)Image.FromFile(path + piece["Image"]["B"].ToString()), (Vector2[])ParseVector2Array(piece["Movement"]["Move"]), (int)piece["Movement"]["Hops"]));
			}
			return d;
		}
		public Dictionary<string, Bitmap> BoardDictionary() {
			Dictionary<string, Bitmap> d = new Dictionary<string, Bitmap>();
			string path = Path.Combine(Application.StartupPath, @"..\..\");
			d.Add("CW", (Bitmap)Image.FromFile(path + textures["Cells"]["W"].ToString()));
			d.Add("CB", (Bitmap)Image.FromFile(path + textures["Cells"]["B"].ToString()));
			return d;
		}

		//======================================================================================================================================================================OTHER
		public ChessBoard SetupBoard() {
			ChessBoard c = new ChessBoard(boardSize);
			foreach(dynamic piece in pieceRules) {
				foreach(Vector2 v in ParseVector2Array(piece["Start"]["W"])) c.pieces[v.x, v.y] = piece["ID"].ToString() + "W";
				foreach(Vector2 v in ParseVector2Array(piece["Start"]["B"])) c.pieces[v.x, v.y] = piece["ID"].ToString() + "B";
			}
			return c;
		}
		public ChessBoard MakeMove(ChessBoard board, Vector2 from, Vector2 to) {
			string piece = GetCell(from);
			ChessBoard res = board;
			if(IsPiece(piece)) {
				foreach(Vector2 move in piecesDict[piece].Item2) {
					if(from + move == to) {
						res = res.SetCell(from, null);
						res = res.SetCell(to, piece);
						Changed(from);
						Changed(to);
						return res;
					}
				}
			}
			return res;
		}
		public ChessBoard MakeMoveFromHold(ChessBoard board, Vector2 to) {
			return MakeMove(board, held, to);
		}
		public ChessBoard SetCell(ChessBoard board, Vector2 cell, string piece) {
			return board.SetCell(cell, piece);
		}

		public void MakeMove(Vector2 from, Vector2 to) {
			current = MakeMove(current, from, to);
			Changed(from);
			Changed(to);
		}
		public void MakeMoveFromHold(Vector2 to) {
			current = MakeMoveFromHold(current, to);
		}
		public void SetCell(Vector2 cell, string piece) {
			current = SetCell(current, cell, piece);
			Changed(cell);
		}
		public string GetCell(Vector2 cell) => current.pieces[cell.x, cell.y];

		bool holding = false;
		Vector2 held;
		public string Hold(Vector2 cell) {
			holding = true;
			held = cell;
			return current.pieces[cell.x, cell.y];
		}
		public void Unhold() => holding = false;

		public bool IsPiece(string id) => !IsNotPiece(id);
		public bool IsNotPiece(string id) => id is null || id == string.Empty;

		bool drawn = false;
		Bitmap old;

		public void DumpDrawCache() => drawn = false;
		public Bitmap DrawBoard(ChessBoard board, Vector2 size) {
			if(!drawn) {
				drawn = true;
				return old = board.DrawWith(piecesDict.StripDictionary(), texturDict, size);
			}
			return holding ? board.DrawWith(piecesDict.StripDictionary(), texturDict, size, old, changes, held) : board.DrawWith(piecesDict.StripDictionary(), texturDict, size, old, changes);
		}
		public Bitmap DrawCurrentBoard(Vector2 size) {
			return DrawBoard(current, size);
		}
		private void Changed(Vector2 cell) => changes[cell.x, cell.y] = true;

		public Vector2[] ParseVector2Array(dynamic x) {
			List<Vector2> res = new List<Vector2>();
			foreach(dynamic y in x) res.Add((y[0], y[1]));
			return res.ToArray();
		}
		public Vector2 GetRandomPiece() {
			Vector2 cell = null;
			while(cell is null || IsNotPiece(GetCell(cell))) cell = Vector2.Random(Vector2.Zero, boardSize);
			return cell;
		}
	}
}
