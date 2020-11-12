using System.Drawing;
using System.Windows.Forms;

namespace _3DChess {
	class TransparentPictureBox : PictureBox {
		public TransparentPictureBox() {
			SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer, true);
		}
		
		protected override void OnPaintBackground(PaintEventArgs e) {
			//base.OnPaintBackground(e);
			Graphics g = e.Graphics;
			if(Parent != null) {
				var index = Parent.Controls.GetChildIndex(this);
				for(var i = Parent.Controls.Count - 1; i > index; i--) {
					var c = Parent.Controls[i];
					if(c.Bounds.IntersectsWith(Bounds) && c.Visible) {
						using(var bmp = new Bitmap(c.Width, c.Height, g)) {
							c.DrawToBitmap(bmp, c.ClientRectangle);
							g.TranslateTransform(c.Left - Left, c.Top - Top);
							g.DrawImageUnscaled(bmp, Point.Empty);
							g.TranslateTransform(Left - c.Left, Top - c.Top);
						}
					}
				}
			}
		}
	}
}
