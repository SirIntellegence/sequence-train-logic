using System;

namespace SequenceTrainLogic {
	/// <summary>
	/// Last Train car in the train
	/// </summary>
	public class Caboose : AbstractTrainCar {
		public Caboose(Train parent) : base(parent) {
		}

		#region implemented abstract members of AbstractTrainCar

		public override int trainIndex {
			get {
				return -1;
			}
		}

		#endregion
	}
}

