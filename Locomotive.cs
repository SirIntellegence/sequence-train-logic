using System;

namespace SequenceTrainLogic {
	/// <summary>
	/// The engine of the train. Using locomotive to reduce abiguity with
	/// <see cref="SequenceTrainEngine"/>.
	/// </summary>
	public class Locomotive : AbstractTrainCar {
		internal Locomotive(Train parent) : base(parent) {
		}

		#region implemented abstract members of AbstractTrainCar

		public override int trainIndex {
			get {
				return 0;
			}
		}

		#endregion
	}
}

