using System;
using System.Drawing;
using System.Windows.Forms;

namespace _3DChess {
	public partial class Form1 : Form {
		Image board;
		ChessEngine eng;
		public Form1() {
			InitializeComponent();
			SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer, true);
			Size = new Size(1217, 1240);
			eng = new ChessEngine((8, 8), @"D:\Users\abdul\source\repos\Higher-Dimensional-Chess\Higher-Dimensional-Chess\Pieces.json", @"D:\Users\abdul\source\repos\Higher-Dimensional-Chess\Higher-Dimensional-Chess\Board.json");
			RedrawBoard();
			Paint += Form1_Paint;
			MouseDown += Form1_MouseDown;
			MouseClick += Form1_MouseClick;
		}

		private void Form1_MouseClick(object sender, MouseEventArgs e) {
			eng.SetCell(GetCellFromPos(e.Location), "");
			RedrawBoard();
			Refresh();
		}

		private void RedrawBoard() {
			board = eng.DrawCurrentBoard((1200, 1200));
		}

		private void Form1_MouseDown(object sender, MouseEventArgs e) {
		}

		private void Form1_Paint(object sender, PaintEventArgs e) {
			e.Graphics.DrawImage(board, 0, 0);
		}

		private Vector2 GetCellFromPos(Vector2 pos) => pos * (8, 8) / Size;
	}
}
