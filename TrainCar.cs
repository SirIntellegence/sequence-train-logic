using System;

namespace SequenceTrainLogic {
	public abstract class TrainCar {
		private readonly Train parent;
		internal TrainCar(Train parent) {
			if(parent == null)
				throw new ArgumentNullException("parent");
			this.parent = parent;
		}
		public int x { get; internal set;}
		public int y { get; internal set;}
		public TrackEnds entry{ get; internal set; }
		/// <summary>
		/// How far the Train Car is through the current track block.
		/// </summary>
		/// <value>The progress.</value>
		/// <see cref="EngineOptions.blockSections"/>
		public int progress { get; internal set; }
		/// <summary>
		/// Index of the car in the train. The train engine is 0, the caboose
		/// is -1
		/// </summary>
		/// <value>The index of the train.</value>
		public abstract int trainIndex{ get; }
	}
}

