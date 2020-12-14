using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace _3DChess {
	class ChessEngine {
		Vector2 boardSize;
		string[,] pieces;
		dynamic pieceRules;
		dynamic textures;

		public Dictionary<string, Bitmap> piecesDict { get; private set; }
		public Dictionary<string, Bitmap> texturDict { get; private set; }

		//======================================================================================================================================================================INIT
		public ChessBoard current { get; private set; }
		public ChessEngine(Vector2 boardSize, string pieceJSON, string textureJSON) {
			this.boardSize = boardSize;
			pieces = new string[boardSize.x, boardSize.y];
			pieceRules = JsonConvert.DeserializeObject(File.ReadAllText(pieceJSON));
			textures = JsonConvert.DeserializeObject(File.ReadAllText(textureJSON));
			piecesDict = PiecesDictionary();
			texturDict = BoardDictionary();
			current = SetupBoard();
		}
		public Dictionary<string, Bitmap> PiecesDictionary() {
			Dictionary<string, Bitmap> d = new Dictionary<string, Bitmap>();
			string path = Path.Combine(Application.StartupPath, @"..\..\");
			foreach(dynamic piece in pieceRules) {
				d.Add(piece["ID"].ToString() + "W", (Bitmap)Image.FromFile(path + piece["Image"]["W"].ToString()));
				d.Add(piece["ID"].ToString() + "B", (Bitmap)Image.FromFile(path + piece["Image"]["B"].ToString()));
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
				foreach(Vector2 v in ParseVector2Array(piece["Start"]["W"])) c.pieces[v.x, v.y] = pieces[v.x, v.y] = piece["ID"].ToString() + "W";
				foreach(Vector2 v in ParseVector2Array(piece["Start"]["B"])) c.pieces[v.x, v.y] = pieces[v.x, v.y] = piece["ID"].ToString() + "B";
			}
			return c;
		}
		public ChessBoard MakeMove(ChessBoard board, Vector2 from, Vector2 to) {
			throw new Exception("TODO");
		}

		public Bitmap DrawCurrentBoard(Vector2 size) {
			return current.DrawWith(piecesDict, texturDict, size);
		}

		public Vector2[] ParseVector2Array(dynamic x){
			List<Vector2> res = new List<Vector2>();
			foreach(dynamic y in x) res.Add((y[0], y[1]));
			return res.ToArray();
		}
	}
}
