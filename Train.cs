using System;

namespace SequenceTrainLogic {
	public class Train {
		private readonly SequenceTrainEngine parent;

		private ReadonlyEngineOptions settings{ get {
				return parent.EngineOptions; } }
		internal Train(SequenceTrainEngine parent) {
			if(parent == null) {
				throw new ArgumentNullException("parent");
			}
			this.parent = parent;
		}

	}
}

