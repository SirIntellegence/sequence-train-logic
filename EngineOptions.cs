using System;

namespace SequenceTrainLogic {
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
		/// <summary>
		/// Whether or not to allow the game engine to swap out track
		/// that is under the train
		/// </summary>
		public bool canSwapOutTrackUnderTrain;
		/// <summary>
		/// True if the map wraps around (left&lt;-&gt;right and
		/// up&lt;-&gt;down)
		/// </summary>
		public bool mapWraps;
		public int? version;
		internal int trueVersion;
		public ulong seed;
	}
}

