using System;

namespace SequenceTrainLogic {
	public delegate T Func<T>();
	public class LazyInit<T> {
		bool inited = false;
		private T val;
		private readonly Func<T> initFunc;
		public LazyInit(Func<T> initFunc) {
			this.initFunc = initFunc;
		}
		public LazyInit(T val){
			this.val = val;
			inited = true;
		}

		public static implicit operator T(LazyInit<T> input){
			if (input == null){
				throw new ArgumentNullException("input");
			}
			if (!input.inited){
				input.val = input.initFunc();
				input.inited = true;
			}
			return input.val;
		}

		public static implicit operator LazyInit<T>(T input){
			return new LazyInit<T>(input);
		}
	}
}

