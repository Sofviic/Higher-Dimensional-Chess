using System;
using System.Collections.Generic;
using System.Linq;

namespace _3DChess {
	public static class MathFunc {
		public static Random r = new Random(Guid.NewGuid().GetHashCode());
		public static int Rand(int a = int.MinValue, int b = int.MaxValue) => r.Next(a, b);

		public static (int, int) Add(this (int, int) a, (int, int) b) => (a.Item1 + b.Item1, a.Item2 + b.Item2);
		public static (int, int) AddSub(this (int, int) a, (int, int) b) => (a.Item1 + b.Item1, a.Item2 - b.Item2);
		public static (int, int) SubAdd(this (int, int) a, (int, int) b) => (a.Item1 - b.Item1, a.Item2 + b.Item2);
		public static (int, int) Sub(this (int, int) a, (int, int) b) => (a.Item1 - b.Item1, a.Item2 - b.Item2);
		public static (int, int) Times(this (int, int) a, int b) => (a.Item1 * b, a.Item2 * b);
		public static (int, int) Neg(this (int, int) a) => (-a.Item1, -a.Item2);

		public static IEnumerable<(int, int)> Add(this IEnumerable<(int, int)> a, (int, int) b) => a.Select(x => (x.Item1 + b.Item1, x.Item2 + b.Item2));
		public static IEnumerable<(int, int)> AddSub(this IEnumerable<(int, int)> a, (int, int) b) => a.Select(x => (x.Item1 + b.Item1, x.Item2 - b.Item2));
		public static IEnumerable<(int, int)> SubAdd(this IEnumerable<(int, int)> a, (int, int) b) => a.Select(x => (x.Item1 - b.Item1, x.Item2 + b.Item2));
		public static IEnumerable<(int, int)> Sub(this IEnumerable<(int, int)> a, (int, int) b) => a.Select(x => (x.Item1 - b.Item1, x.Item2 - b.Item2));
		public static IEnumerable<(int, int)> Times(this IEnumerable<(int, int)> a, int b) => a.Select(x => (x.Item1 * b, x.Item2 * b));
		public static IEnumerable<(int, int)> Neg(this IEnumerable<(int, int)> a) => a.Select(x => (-x.Item1, -x.Item2));
	}
}
