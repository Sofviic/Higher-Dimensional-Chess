using System;
using System.Drawing;
using System.Windows.Forms;

namespace _3DChess {
	public partial class Form1 : Form {
		Image board;
		public Form1() {
			InitializeComponent();
			SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer, true);
			Size = new Size(1217, 1240);
			ChessEngine e = new ChessEngine((8, 8), @"D:\Users\abdul\source\repos\Higher-Dimensional-Chess\Higher-Dimensional-Chess\Pieces.json", @"D:\Users\abdul\source\repos\Higher-Dimensional-Chess\Higher-Dimensional-Chess\Board.json");
			board = e.DrawCurrentBoard((1200, 1200));
			Paint += Form1_Paint;
		}

		private void Form1_Paint(object sender, PaintEventArgs e) {
			e.Graphics.DrawImage(board, 0, 0);
		}
	}
}
