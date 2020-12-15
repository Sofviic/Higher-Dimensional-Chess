using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3DChess {
	public static class ExtendFunc {
		public static Dictionary<T, K> StripDictionary<T, K, A, B>(this Dictionary<T, (K, A, B)> d) {
			Dictionary<T, K> r = new Dictionary<T, K>();
			foreach(var TK in d) r.Add(TK.Key, TK.Value.Item1);
			return r;
		}
	}
}
