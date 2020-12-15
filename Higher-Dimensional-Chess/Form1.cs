using System;
using System.Drawing;
using System.Windows.Forms;

namespace _3DChess {
	public partial class Form1 : Form {
		Bitmap board;
		ChessEngine eng;
		public Form1() {
			InitializeComponent();
			SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer, true);
			Size = new Size(1217, 1240);
			eng = new ChessEngine((8, 8), @"D:\Users\abdul\source\repos\Higher-Dimensional-Chess\Higher-Dimensional-Chess\Pieces.json", @"D:\Users\abdul\source\repos\Higher-Dimensional-Chess\Higher-Dimensional-Chess\Board.json");
			RedrawBoard();
			Paint += Form1_Paint;
			MouseDown += Form1_MouseDown;
			MouseUp += Form1_MouseUp;
			MouseMove += Form1_MouseMove;
		}

		bool holding = false;
		string heldPiece = "";

		private Bitmap RedrawBoard() {
			return board = eng.DrawCurrentBoard((1200, 1200));
		}

		private void Form1_MouseDown(object sender, MouseEventArgs e) {
			if(eng.IsPiece(eng.GetCell(GetCellFromPos(e.Location)))) {
				holding = true;
				heldPiece = eng.Hold(GetCellFromPos(e.Location));
				RedrawBoard();
				Refresh();
			}
		}

		private void Form1_MouseUp(object sender, MouseEventArgs e) {
			holding = false;
			eng.Unhold();
			RedrawBoard();
			Refresh();
		}

		private void Form1_MouseMove(object sender, MouseEventArgs e) {
			if(holding) {
				board = RedrawBoard().Add(eng.piecesDict[heldPiece], e.Location, true);
				Refresh();
			}
		}

		private void Form1_Paint(object sender, PaintEventArgs e) {
			e.Graphics.DrawImage(board, 0, 0);
		}

		private Vector2 GetCellFromPos(Vector2 pos) => pos * (8, 8) / Size;
	}
}
