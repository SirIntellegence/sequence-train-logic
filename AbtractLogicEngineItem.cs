using System;

namespace SequenceTrainLogic {
	public abstract class AbtractLogicEngineItem {
		protected readonly SequenceTrainEngine parent;
		protected AbtractLogicEngineItem(SequenceTrainEngine engine) {
			if (engine == null)
				throw new ArgumentNullException("engine");
			parent = engine;
		}
	}
}

