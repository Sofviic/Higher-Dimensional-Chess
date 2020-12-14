using System.Drawing;

namespace _3DChess {
	public class Vector2 {
		public int x, y;
		public Vector2(int x = 0, int y = 0) => (this.x, this.y) = (x, y);
		public static implicit operator Vector2((int, int) x) => new Vector2(x.Item1, x.Item2);
		public static implicit operator (int, int) (Vector2 x) => (x.x, x.y);
		public static implicit operator Point(Vector2 x) => new Point(x.x, x.y);
		public static implicit operator Vector2(Point x) => new Vector2(x.X, x.Y);

		public static Vector2 operator +(Vector2 a, Vector2 b) => a.Add(b);
		public static Vector2 operator -(Vector2 a, Vector2 b) => a.Sub(b);
		public static Vector2 operator *(Vector2 a, int b) => a.Times(b);
		public static Vector2 operator -(Vector2 a) => a.Neg();
		public static bool operator ==(Vector2 a, Vector2 b) => a.x == b.x && a.y == b.y;
		public static bool operator !=(Vector2 a, Vector2 b) => a.x != b.x || a.y != b.y;
		public static bool operator ==(Vector2 a, (int, int) b) => a.x == b.Item1 && a.y == b.Item2;
		public static bool operator !=(Vector2 a, (int, int) b) => a.x != b.Item1 || a.y != b.Item2;

		public Vector2 Add(Vector2 b) => (x + b.x, y + b.y);
		public Vector2 AddSub(Vector2 b) => (x + b.x, y - b.y);
		public Vector2 SubAdd(Vector2 b) => (x - b.x, y + b.y);
		public Vector2 Sub(Vector2 b) => (x - b.x, y - b.y);
		public Vector2 Times(int b) => (x * b, y * b);
		public Vector2 Neg() => (-x, -y);

		public override bool Equals(object obj) => obj as Vector2 != null && x == (obj as Vector2).x && y == (obj as Vector2).y;
		public override int GetHashCode() {
			var hashCode = 1502939027;
			hashCode = hashCode * -1521134295 + x.GetHashCode();
			hashCode = hashCode * -1521134295 + y.GetHashCode();
			return hashCode;
		}
	}
}
