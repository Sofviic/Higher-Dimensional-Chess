using System.Windows.Forms;

namespace System.Windows.Forms {
	class NonFlickeringPanel : Panel {
		public NonFlickeringPanel() {
			SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer, true);
		}
	}
}
