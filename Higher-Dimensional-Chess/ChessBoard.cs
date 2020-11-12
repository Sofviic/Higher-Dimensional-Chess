using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace _3DChess {
	class ChessBoard {
		public static Bitmap sel;
		public static Bitmap cap;
		public static Bitmap mov;
		public static Bitmap nil;
		public static Bitmap whiteCell;
		public static Bitmap blackCell;
		public static Bitmap WK, WQ, WB, WN, WR, WP;
		public static Bitmap BK, BQ, BB, BN, BR, BP;
		public enum P { WK, WQ, WB, WN, WR, WP, BK, BQ, BB, BN, BR, BP, NA }
		public enum PColour { W, B }
		P[,] cp;
		public Dictionary<P, Bitmap> pimg = new Dictionary<P, Bitmap>();
		ChessBoardCell[,] cells;
		(int, int) holdp = (-1, -1);
		public PictureBox[,] pbs;
		public TransparentPictureBox holdpb;
		public readonly int r, c;
		public readonly NonFlickeringPanel p;
		public TextBox textBox;
		public ChessBoard(int r, int c, NonFlickeringPanel p) {
			cells = new ChessBoardCell[r, c];
			pbs = new PictureBox[r, c];
			cp = new P[r, c];
			this.r = r;
			this.c = c;
			this.p = p;

			SetupResources();
			SetupBoard();
		}

		//======================================================================================================================================================================DRAWING
		public void Draw() {
			Bitmap bmp;
			for(int j = 0; j < r; ++j) for(int i = 0; i < c; ++i) {

					bmp = cells[j, i].Draw();

					pbs[j, i] = new PictureBox();
					p.Controls.Add(pbs[j, i]);
					pbs[j, i].Location = new Point(p.Size.Width / c * i, p.Size.Height / r * j);
					pbs[j, i].Size = new Size(p.Size.Width / c, p.Size.Height / r);

					pbs[j, i].Image = bmp;

					pbs[j, i].Click += ChessBoard_Click;
					pbs[j, i].MouseUp += ChessBoard_MouseUp;
					pbs[j, i].MouseDown += ChessBoard_MouseDown;
					pbs[j, i].MouseMove += ChessBoard_MouseMove;
				}
			holdpb = new TransparentPictureBox {
				Location = new Point(p.Size.Width / 2, p.Size.Height / 2),
				Size = new Size(p.Size.Width / c, p.Size.Height / r),
				Visible = false,
			};
			holdpb.BackgroundImage = null;
			holdpb.BackColor = Color.Transparent;
			p.Controls.Add(holdpb);
			holdpb.BringToFront();
			p.Refresh();
		}

		public void Redraw() {
			for(int j = 0; j < r; ++j) for(int i = 0; i < c; ++i) pbs[j, i].Image = cells[j, i].Draw();
			holdpb.Image = null;
			holdpb.Visible = false;
			p.Refresh();
		}
		public void Redraw(int y, int x, int my, int mx, bool nonheldDraw) {
			Point mloc = p.PointToClient(new Point(mx, my));
			holdpb.Visible = true;
			if(nonheldDraw) for(int j = 0; j < r; ++j) for(int i = 0; i < c; ++i) pbs[j, i].Image = j == y && i == x ? cells[j, i].DrawDef() : cells[j, i].Draw();
			holdpb.Location = new Point(mx - holdpb.Size.Width / 2, my - holdpb.Size.Height / 2);
			holdpb.Image = cells[y, x].DrawUnDef();
			holdpb.Refresh();
			p.Refresh();
		}

		//======================================================================================================================================================================SETUP
		public void SetupResources(){
			whiteCell = new Bitmap(@"..\..\Resources\c_White.png");
			blackCell = new Bitmap(@"..\..\Resources\c_Black.png");
			pimg[P.WK] = WK = new Bitmap(@"..\..\Resources\p_WK.png");
			pimg[P.WQ] = WQ = new Bitmap(@"..\..\Resources\p_WQ.png");
			pimg[P.WB] = WB = new Bitmap(@"..\..\Resources\p_WB.png");
			pimg[P.WN] = WN = new Bitmap(@"..\..\Resources\p_WN.png");
			pimg[P.WR] = WR = new Bitmap(@"..\..\Resources\p_WR.png");
			pimg[P.WP] = WP = new Bitmap(@"..\..\Resources\p_WP.png");
			pimg[P.BK] = BK = new Bitmap(@"..\..\Resources\p_BK.png");
			pimg[P.BQ] = BQ = new Bitmap(@"..\..\Resources\p_BQ.png");
			pimg[P.BB] = BB = new Bitmap(@"..\..\Resources\p_BB.png");
			pimg[P.BN] = BN = new Bitmap(@"..\..\Resources\p_BN.png");
			pimg[P.BR] = BR = new Bitmap(@"..\..\Resources\p_BR.png");
			pimg[P.BP] = BP = new Bitmap(@"..\..\Resources\p_BP.png");
			sel = new Bitmap(@"..\..\Resources\s_sel.png");
			cap = new Bitmap(@"..\..\Resources\s_cap.png");
			mov = new Bitmap(@"..\..\Resources\s_mov.png");
			nil = new Bitmap(@"..\..\Resources\s_nil.png");
		}
		public void SetupBoard() {
			SetupBoardCells();
			SetupBoardItems();
		}
		public void SetupBoardCells() {
			for(int j = 0; j < r; ++j) for(int i = 0; i < c; ++i) {
				cells[j, i] = new ChessBoardCell((i + j) % 2 == 0 ? whiteCell : blackCell);
				cells[j, i].nilnilnil = nil;
			}
		}
		public void SetupBoardItems() {
			cp = CLASSIC_BOARD;

			for(int j = 0; j < r; ++j) for(int i = 0; i < c; ++i) if(cp[j, i] != P.NA) cells[j, i].item = new ChessItem(this, pimg[cp[j, i]], cp[j, i]);
		}

		//======================================================================================================================================================================IO
		private void ChessBoard_Click(object sender, EventArgs e) {
			Point loc = Indices((PictureBox)sender);
			ChessBoardClick(loc.X, loc.Y);
		}
		private void ChessBoard_MouseUp(object sender, MouseEventArgs e) {
			Point loc = Indices((PictureBox)sender);
			Point Mouse = new Point(loc.X * p.Size.Width / c + e.X, loc.Y * p.Size.Height / r + e.Y);
			ChessBoardUndrag(loc.X, loc.Y, Mouse.X, Mouse.Y);
		}
		private void ChessBoard_MouseDown(object sender, MouseEventArgs e) {
			Point loc = Indices((PictureBox)sender);
			Point Mouse = new Point(loc.X * p.Size.Width / c + e.X, loc.Y * p.Size.Height / r + e.Y);
			ChessBoardClick(loc.X, loc.Y);
			ChessBoardDrag(loc.X, loc.Y, Mouse.X, Mouse.Y);
		}
		private void ChessBoard_MouseMove(object sender, MouseEventArgs e) {
			Point loc = Indices((PictureBox)sender);
			Point Mouse = new Point(loc.X * p.Size.Width / c + e.X, loc.Y * p.Size.Height / r + e.Y);
			ChessBoardMove(Mouse.X, Mouse.Y);
		}
		private Point Indices(PictureBox pb) => new Point(pb.Location.X * c / p.Size.Width, pb.Location.Y * r / p.Size.Height);
		private Point Indices(int x, int y) => new Point(x * c / p.Size.Width, y * r / p.Size.Height);
		public void ChessBoardClick(int x, int y) {
			textBox.Text = string.Format("({0}, {1})", x, y);
			ClearSelection();
			cells[y, x].selection = sel;
			if(!(cells[y, x].item is null || cells[y, x].item.p == P.NA)) {
				(int, int)[] possibleMoves = cells[y, x].item.Movement(x, y);
				foreach((int, int) index in possibleMoves) cells[index.Item2, index.Item1].movements = mov;
			}
			Redraw();
		}
		public void ChessBoardUndrag(int x, int y, int mx, int my) {
			holdp = (-1, -1);
			Redraw();
		}
		public void ChessBoardDrag(int x, int y, int mx, int my) {
			holdp = (x, y);
			Redraw(y, x, my, mx, true);
		}
		public void ChessBoardMove(int mx, int my) {
			if(holdp == (-1, -1)) return;
			Redraw(holdp.Item2, holdp.Item1, my, mx, false);
		}
		public void ClearSelection(){
			for(int j = 0; j < r; ++j) for(int i = 0; i < c; ++i) {
				cells[j, i].selection = null;
				cells[j, i].movements = null;
				cells[j, i].ncaptures = null;
				cells[j, i].xcaptures = null;
			}
		}
		//======================================================================================================================================================================MOVMENT

































		//======================================================================================================================================================================SETUP
		public readonly P[,] CLASSIC_BOARD = new P[,]{
						{ P.BR, P.BN, P.BB, P.BQ, P.BK, P.BB, P.BN, P.BR },
						{ P.BP, P.BP, P.BP, P.BP, P.BP, P.BP, P.BP, P.BP },
						{ P.NA, P.NA, P.NA, P.NA, P.NA, P.NA, P.NA, P.NA },
						{ P.NA, P.NA, P.NA, P.NA, P.NA, P.NA, P.NA, P.NA },
						{ P.NA, P.NA, P.NA, P.NA, P.NA, P.NA, P.NA, P.NA },
						{ P.NA, P.NA, P.NA, P.NA, P.NA, P.NA, P.NA, P.NA },
						{ P.WP, P.WP, P.WP, P.WP, P.WP, P.WP, P.WP, P.WP },
						{ P.WR, P.WN, P.WB, P.WQ, P.WK, P.WB, P.WN, P.WR },};
	}
}
