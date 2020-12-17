using System;
using System.Drawing;
using System.Windows.Forms;

namespace _3DChess {
	public partial class Form1 : Form {
		ChessEngine eng;
		Vector2 size = (1200, 1200);
		public Form1() {
			InitializeComponent();
			SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer, true);
			Size = size + (17, 40);
			eng = new ChessEngine((8, 8), @"D:\Users\abdul\source\repos\Higher-Dimensional-Chess\Higher-Dimensional-Chess\Pieces.json", @"D:\Users\abdul\source\repos\Higher-Dimensional-Chess\Higher-Dimensional-Chess\Board.json");
			Refresh();
			Paint += Form1_Paint;
			MouseDown += Form1_MouseDown;
			MouseUp += Form1_MouseUp;
			MouseMove += Form1_MouseMove;
		}

		bool holding = false;
		string heldPiece = "";
		
		private void Form1_MouseDown(object sender, MouseEventArgs e) {
			if(!holding && eng.IsPiece(eng.GetCell(GetCellFromPos(e.Location)))) {
				holding = true;
				heldPiece = eng.Hold(GetCellFromPos(e.Location));
				Refresh();
			}
		}

		private void Form1_MouseUp(object sender, MouseEventArgs e) {
			holding = false;
			eng.MakeMoveFromHold(GetCellFromPos(e.Location));
			eng.Unhold();
			Refresh();
		}

		Vector2 mouseLoc;
		private void Form1_MouseMove(object sender, MouseEventArgs e) {
			mouseLoc = e.Location;
			if(holding) Refresh();
		}

		private void Form1_Paint(object sender, PaintEventArgs e) {
			eng.DrawCurrentBoard(size, e.Graphics);
			if(holding) e.Graphics.DrawImage(eng.piecesDict[heldPiece].Item1, Centre(mouseLoc, eng.piecesDict[heldPiece].Item1));
		}

		private Vector2 GetCellFromPos(Vector2 pos) => pos * (8, 8) / Size;
		private bool In(Vector2 pos, Rectangle rect) => rect.Right > pos.x && rect.Left < pos.x && rect.Top < pos.y && rect.Bottom > pos.y;
		private Vector2 Centre(Vector2 pos, Bitmap b) => pos - b.Centre();
	}
}
