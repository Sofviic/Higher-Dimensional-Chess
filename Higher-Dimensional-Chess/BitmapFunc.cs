using System.Drawing;

namespace _3DChess {
	static class BitmapFunc {
		public static Bitmap SolidColour(int r, int g, int b, int w, int h) {
			Bitmap bmp = new Bitmap(w, h);
			using(Graphics gfx = Graphics.FromImage(bmp)) using(SolidBrush brush = new SolidBrush(Color.FromArgb(r, g, b))) gfx.FillRectangle(brush, 0, 0, w, h);
			return bmp;
		}
		public static Bitmap SolidColour(int r, int g, int b, int a, int w, int h) {
			Bitmap bmp = new Bitmap(w, h);
			using(Graphics gfx = Graphics.FromImage(bmp)) using(SolidBrush brush = new SolidBrush(Color.FromArgb(a, r, g, b))) gfx.FillRectangle(brush, 0, 0, w, h);
			return bmp;
		}
		/// <summary>
		/// Add smaller Bitmap b on top of Bitmap a.
		/// </summary>
		/// <param name="a"></param>
		/// <param name="b"></param>
		/// <returns></returns>
		public static Bitmap Add(this Bitmap a, Bitmap b) {
			Bitmap bmp = new Bitmap(a.Width, a.Height);
			using(Graphics gfx = Graphics.FromImage(bmp)) {
				gfx.DrawImage(a, 0, 0);
				gfx.DrawImage(b, ((Vector2)(a.Size - b.Size) / 2).Clamp(Vector2.Zero, a.Size - b.Size));
			}
			return bmp;
		}
		/// <summary>
		/// Add smaller Bitmap b on top of Bitmap a at a certain location loc.
		/// </summary>
		/// <param name="a"></param>
		/// <param name="b"></param>
		/// <param name="loc"></param>
		/// <returns></returns>
		public static Bitmap Add(this Bitmap a, Bitmap b, Vector2 loc, bool centred = false) {
			Bitmap bmp = new Bitmap(a.Width, a.Height);
			using(Graphics gfx = Graphics.FromImage(bmp)) {
				gfx.DrawImage(a, 0, 0);
				gfx.DrawImage(b, (loc - (centred ? b.Centre() : Vector2.Zero)).Clamp(Vector2.Zero, a.Size - b.Size));
			}
			return bmp;
		}
		/// <summary>
		/// Add smaller Bitmap b on top of Bitmap a at a certain location loc and according to the size of another bitmap c.
		/// </summary>
		/// <param name="a"></param>
		/// <param name="b"></param>
		/// <returns></returns>
		public static Bitmap Add(this Bitmap a, Bitmap b, Vector2 loc, Bitmap c, bool centred = false) {
			Bitmap bmp = new Bitmap(a.Width, a.Height);
			using(Graphics gfx = Graphics.FromImage(bmp)) {
				gfx.DrawImage(a, 0, 0);
				gfx.DrawImage(b, (loc + c.Centre() - b.Centre() - (centred ? b.Centre() : Vector2.Zero)).Clamp(Vector2.Zero, a.Size - b.Size));
			}
			return bmp;
		}
		//public static Bitmap AddSkipNull(this Bitmap a, Bitmap b) => a is null ? (b is null ? null : b) : (b is null ? a : a.Add(b));
		public static Vector2 Centre(this Bitmap a) => a is null ? null : (Vector2)(a.Width, a.Height) / 2;
	}
}


































//public static Bitmap CopyDataToBitmap(byte[] data, int width, int height) {

//	Bitmap bmp = new Bitmap(width, height, PixelFormat.Format24bppRgb);

//	//Create BitmapData and lock pixels
//	BitmapData bmpData = bmp.LockBits(
//						 new Rectangle(0, 0, bmp.Width, bmp.Height),
//						 ImageLockMode.WriteOnly, bmp.PixelFormat);

//	//Copy data from byte array to BitmapData.Scan0
//	Marshal.Copy(data, 0, bmpData.Scan0, data.Length);

//	//Unlock pixels
//	bmp.UnlockBits(bmpData);

//	return bmp;
//}
//public static Bitmap GetDataPicture(byte[] data, int w, int h) {
//	Bitmap pic = new Bitmap(w, h, PixelFormat.Format32bppArgb);
//	for(int j = 0; j < h; ++j) for(int i = 0; i < w; ++i) {
//			Color c = Color.FromArgb(0,
//								data[3 * (j * w + i) + 0],
//								data[3 * (j * w + i) + 1],
//								data[3 * (j * w + i) + 2]);
//			pic.SetPixel(i, j, c);
//		}

//	return pic;
//}