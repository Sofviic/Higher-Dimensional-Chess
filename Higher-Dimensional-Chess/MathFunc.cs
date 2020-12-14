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
	}
}
