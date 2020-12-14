using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3DChess {
	public class Vector2 {
		public int x, y;
		public Vector2(int x = 0, int y = 0) => (this.x, this.y) = (x, y);
		public static implicit operator Vector2((int, int) x) => new Vector2(x.Item1, x.Item2);
		public static implicit operator (int, int) (Vector2 x) => (x.x, x.y);
		public static implicit operator Point(Vector2 x) => new Point(x.x, x.y);
		public static implicit operator Vector2(Point x) => new Vector2(x.X, x.Y);
	}
}
