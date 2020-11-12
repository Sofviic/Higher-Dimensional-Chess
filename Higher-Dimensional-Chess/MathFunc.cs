using System;

namespace _3DChess {
	public static class MathFunc {
		public static Random r = new Random(Guid.NewGuid().GetHashCode());
		public static int Rand(int a = int.MinValue, int b = int.MaxValue) => r.Next(a, b);
	}
}
