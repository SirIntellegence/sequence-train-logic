using System;

namespace sequencetrainlogic {
	public struct EngineOptions {
		/// <summary>
		/// The width of the grid.
		/// </summary>
		public int gridWidth;
		/// <summary>
		/// The height of the grid.
		/// </summary>
		public int gridHeight;
		/// <summary>
		/// The number of possible positions in a block
		/// </summary>
		public int blockSections;
		/// <summary>
		/// Whether or not the train colliding with itself should cause a game over
		/// </summary>
		public bool trainCanCrashWithSelf;
	}
}

