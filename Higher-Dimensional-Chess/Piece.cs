namespace Higher_Dimensional_Chess {
	class Piece {
		public string ID;
		public string Name;
		public bool Royal;
		public Movement Movement;
		public Start Start;
		public Image Image;
	}
	class Movement {
		public int Hops;
		public int[][] Move;
		public bool CaptureWithMove;
		public int[][] Capture;
		public bool FirstMove;
		public int FHops;
		public int[][] FMove;
	}
	class Start {
		public int[] W;
		public int[] B;
	}
	class Image {
		public string W;
		public string B;
	}
}
