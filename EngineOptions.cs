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
		/// <summary>
		/// How many positions long one car is
		/// </summary>
		/// <see cref="blockSections"/>
		public int trainCarLength;
		/// <summary>
		/// How many positions long the coupling inbetween cars is.
		/// </summary>
		public int couplingLength;
		public int? version;
		/// <summary>
		/// How many speed variables there are
		/// </summary>
		public int speedCount;
		internal int trueVersion;
		public ulong seed;
	}
	public class ReadonlyEngineOptions{
		private EngineOptions options;
		public ReadonlyEngineOptions(EngineOptions options){
			this.options = options;
		}
		/// <summary>
		/// The width of the grid.
		/// </summary>
		public int gridWidth { get { return options.gridWidth; } }

		/// <summary>
		/// The height of the grid.
		/// </summary>
		public int gridHeight { get { return options.gridHeight; } }

		/// <summary>
		/// The number of possible positions in a block
		/// </summary>
		public int blockSections { get { return options.blockSections; } }

		/// <summary>
		/// Whether or not the train colliding with itself should cause a game over
		/// </summary>
		public bool trainCanCrashWithSelf { get { return options.trainCanCrashWithSelf; } }

		/// <summary>
		/// Whether or not to allow the game engine to swap out track
		/// that is under the train
		/// </summary>
		public bool canSwapOutTrackUnderTrain { get { return options.canSwapOutTrackUnderTrain; } }

		/// <summary>
		/// True if the map wraps around (left&lt;-&gt;right and
		/// up&lt;-&gt;down)
		/// </summary>
		public bool mapWraps { get { return options.mapWraps; } }

		/// <summary>
		/// How many positions long one car is
		/// </summary>
		/// <see cref="blockSections"/>
		public int trainCarLength { get { return options.trainCarLength; } }

		/// <summary>
		/// How many positions long the coupling inbetween cars is.
		/// </summary>
		public int couplingLength { get { return options.couplingLength; } }

		public int? version { get { return options.version; } }

		internal int trueVersion { get { return options.trueVersion; } }

		public ulong seed { get { return options.seed; } }
		
		/// <summary>
		/// How many speed variables there are
		/// </summary>
		public int speedCount { get { return options.speedCount; } }

	}
}

