using System;
using System.Collections.Generic;
using System.Linq;

namespace _3DChess {
	public static class MathFunc {
		public static Random r = new Random(Guid.NewGuid().GetHashCode());
		public static int Rand(int a = int.MinValue, int b = int.MaxValue) => r.Next(a, b);

		public static IEnumerable<Vector2> Add(this IEnumerable<Vector2> a, Vector2 b) => a.Select(x => x.Add(b));
		public static IEnumerable<Vector2> AddSub(this IEnumerable<Vector2> a, Vector2 b) => a.Select(x => x.AddSub(b));
		public static IEnumerable<Vector2> SubAdd(this IEnumerable<Vector2> a, Vector2 b) => a.Select(x => x.SubAdd(b));
		public static IEnumerable<Vector2> Sub(this IEnumerable<Vector2> a, Vector2 b) => a.Select(x => x.Sub(b));
		public static IEnumerable<Vector2> Times(this IEnumerable<Vector2> a, int b) => a.Select(x => x.Times(b));
		public static IEnumerable<Vector2> Neg(this IEnumerable<Vector2> a) => a.Select(x => x.Neg());

		public static Vector2[] Add(this Vector2[] a, Vector2 b) => a.Select(x => x.Add(b)).ToArray();
		public static Vector2[] AddSub(this Vector2[] a, Vector2 b) => a.Select(x => x.AddSub(b)).ToArray();
		public static Vector2[] SubAdd(this Vector2[] a, Vector2 b) => a.Select(x => x.SubAdd(b)).ToArray();
		public static Vector2[] Sub(this Vector2[] a, Vector2 b) => a.Select(x => x.Sub(b)).ToArray();
		public static Vector2[] Times(this Vector2[] a, int b) => a.Select(x => x.Times(b)).ToArray();
		public static Vector2[] Neg(this Vector2[] a) => a.Select(x => x.Neg()).ToArray();

		public static int Mod(this int a, int n) => ((a % n) + n) % n;
		public static int Clamp(this int x, int a = int.MinValue, int b = int.MaxValue) => x < a ? a : x > b ? b : x;
		public static Vector2 Clamp(this Vector2 x, Vector2 a, Vector2 b) => (x.x.Clamp(a.x, b.x), x.y.Clamp(a.y, b.y));
		public static bool Iff(this bool a, bool b) => a ? !b : b;
	}
}
