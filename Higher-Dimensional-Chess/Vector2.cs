using System.Drawing;

namespace _3DChess {
	public class Vector2 {
		public static readonly Vector2 MinValue = (int.MinValue, int.MinValue);
		public static readonly Vector2 MaxValue = (int.MaxValue, int.MaxValue);
		public static readonly Vector2 Zero = (0, 0);
		public static readonly Vector2 One = (1, 1);

		public int x, y;
		public Vector2(int x = 0, int y = 0) => (this.x, this.y) = (x, y);
		public static implicit operator Vector2((int, int) x) => new Vector2(x.Item1, x.Item2);
		public static implicit operator (int, int) (Vector2 x) => (x.x, x.y);
		public static implicit operator Point(Vector2 x) => new Point(x.x, x.y);
		public static implicit operator Vector2(Point x) => new Vector2(x.X, x.Y);
		public static implicit operator Size(Vector2 x) => new Size(x.x, x.y);
		public static implicit operator Vector2(Size x) => new Vector2(x.Width, x.Height);

		public static Vector2 operator +(Vector2 a, Vector2 b) => a.Add(b);
		public static Vector2 operator -(Vector2 a, Vector2 b) => a.Sub(b);
		public static Vector2 operator *(Vector2 a, Vector2 b) => a.Times(b);
		public static Vector2 operator /(Vector2 a, Vector2 b) => a.Over(b);
		public static Vector2 operator %(Vector2 a, Vector2 b) => a.Mod(b);
		public static Vector2 operator *(Vector2 a, int b) => a.Times(b);
		public static Vector2 operator /(Vector2 a, int b) => a.Over(b);
		public static Vector2 operator *(int a, Vector2 b) => b.Times(a);
		public static Vector2 operator /(int a, Vector2 b) => b.Over(a);
		public static Vector2 operator %(Vector2 a, int b) => a.Mod(b);
		public static Vector2 operator -(Vector2 a) => a.Neg();
		public static bool operator ==(Vector2 a, Vector2 b) => a.x == b.x && a.y == b.y;
		public static bool operator !=(Vector2 a, Vector2 b) => a.x != b.x || a.y != b.y;
		public static bool operator ==(Vector2 a, (int, int) b) => a.x == b.Item1 && a.y == b.Item2;
		public static bool operator !=(Vector2 a, (int, int) b) => a.x != b.Item1 || a.y != b.Item2;

		public Vector2 Add(Vector2 b) => (x + b.x, y + b.y);
		public Vector2 AddSub(Vector2 b) => (x + b.x, y - b.y);
		public Vector2 SubAdd(Vector2 b) => (x - b.x, y + b.y);
		public Vector2 Sub(Vector2 b) => (x - b.x, y - b.y);
		public Vector2 Times(Vector2 b) => (x * b.x, y * b.y);
		public Vector2 Over(Vector2 b) => (x / b.x, y / b.y);
		public Vector2 Mod(Vector2 b) => (x.Mod(b.x), y.Mod(b.y));
		public Vector2 Times(int b) => (x * b, y * b);
		public Vector2 Over(int b) => (x / b, y / b);
		public Vector2 Mod(int b) => (x.Mod(b), y.Mod(b));
		public Vector2 Neg() => (-x, -y);
		public int SqLength() => x * x + y * y;

		public override bool Equals(object obj) => obj as Vector2 != null && x == (obj as Vector2).x && y == (obj as Vector2).y;
		public override int GetHashCode() {
			var hashCode = 1502939027;
			hashCode = hashCode * -1521134295 + x.GetHashCode();
			hashCode = hashCode * -1521134295 + y.GetHashCode();
			return hashCode;
		}

		public static Vector2 Random() => Random(One * int.MinValue, One * int.MaxValue);
		public static Vector2 Random(Vector2 from) => Random(from, One * int.MaxValue);
		public static Vector2 Random(Vector2 from, Vector2 to) => (MathFunc.Rand(from.x, to.x), MathFunc.Rand(from.y, to.y));
	}
}
