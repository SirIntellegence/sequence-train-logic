using System;

namespace SequenceTrainLogic {
	/// <summary>
	/// Just your ordinary train car, that you need to pickup
	/// </summary>
	public class TrainCar : AbstractTrainCar {
		int num;

		internal TrainCar(int num, Train parent) : base(parent) {
			if (num < 1){
				throw new ArgumentOutOfRangeException("num", num, "Value must " +
				                                      "be greater than 0!");
			}
			this.num = num;
		}

		#region implemented abstract members of AbstractTrainCar

		public override int trainIndex {
			get {
				return num;
			}
		}

		#endregion
	}
}

