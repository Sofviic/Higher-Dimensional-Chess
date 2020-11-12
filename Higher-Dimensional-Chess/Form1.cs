using System;
using System.Drawing;
using System.Windows.Forms;

namespace _3DChess {
	public partial class Form1 : Form {
		ChessBoard chess;
		public Form1() {
			InitializeComponent();
			SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer, true);
			textBox1.GotFocus += UnsetFocus;
			chess = new ChessBoard(8, 8, panel1);
			chess.Draw();
			chess.textBox = textBox1;
		}

		private void UnsetFocus(object sender, EventArgs e) => ((TextBox)sender).Parent.Focus();
		protected override void OnPaintBackground(PaintEventArgs e) {
			base.OnPaintBackground(e);
			Graphics g = e.Graphics;
			if(Parent != null) {
				int index = Parent.Controls.GetChildIndex(this);
				for(int i = Parent.Controls.Count - 1; i > index; i--) {
					Control c = Parent.Controls[i];
					if(c.Bounds.IntersectsWith(Bounds) && c.Visible) {
						Bitmap bmp = new Bitmap(c.Width, c.Height, g);
						c.DrawToBitmap(bmp, c.ClientRectangle);
						g.TranslateTransform(c.Left - Left, c.Top - Top);
						g.DrawImageUnscaled(bmp, Point.Empty);
						g.TranslateTransform(Left - c.Left, Top - c.Top);
						bmp.Dispose();
					}
				}
			}
		}
	}
}
