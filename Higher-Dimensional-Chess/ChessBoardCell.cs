using System.Drawing;

namespace _3DChess {
	class ChessBoardCell {
		Bitmap def;
		public ChessItem item;
		public Bitmap nilnilnil;
		public Bitmap selection;
		public Bitmap movements;
		public Bitmap ncaptures;
		public Bitmap xcaptures;
		public bool hasMoved = false;
		public ChessBoardCell(Bitmap def) => this.def = def;
		public Bitmap Draw() => def.AddSkipNull(item).AddSkipNull(selection).AddSkipNull(movements).AddSkipNull(ncaptures).AddSkipNull(xcaptures);
		public Bitmap DrawUnDef() => nilnilnil.AddSkipNull(item);
		public Bitmap DrawDef() => def;
	}
}
