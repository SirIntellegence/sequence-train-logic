using System;

namespace SequenceTrainLogic {
	public abstract class AbstractLogicEngineItem {
		protected internal readonly SequenceTrainEngine Parent;
		protected AbstractLogicEngineItem(SequenceTrainEngine engine) {
			if (engine == null)
				throw new ArgumentNullException("engine");
			Parent = engine;
		}
	}
}

