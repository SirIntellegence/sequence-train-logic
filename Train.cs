using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace SequenceTrainLogic {
	public class Train : AbstractLogicEngineItem {
		private readonly Locomotive locomotive;
		private readonly Caboose caboose;
		//It is not likely that someone will clear level 8, I don't think I
		//have...
		private readonly List<AbstractTrainCar> carList =
			new List<AbstractTrainCar>(10);
		public readonly ReadOnlyCollection<AbstractTrainCar> PublicCarList;
		private ReadonlyEngineOptions settings{ get {
				return Parent.EngineOptions; } }


		internal Train(SequenceTrainEngine parent) : base(parent) {
			PublicCarList = carList.AsReadOnly();
			locomotive = new Locomotive(this);
			caboose = new Caboose(this);
			//the locomotive is always the first thing in the list
			carList.Add(locomotive);
		}




	}
}

