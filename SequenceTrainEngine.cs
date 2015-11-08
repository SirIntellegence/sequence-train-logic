using System;

namespace SequenceTrainLogic {
	public class SequenceTrainEngine {
		public const int CurVersion= 0;
		internal const int MaxTrackTypes = 4;
		const int MinWidth= 4;
		const int MinHeight= 4;
		readonly MersenneTwister random = new MersenneTwister();
		/// <summary>
		/// The center coords
		/// </summary>
		private int centerX, centerY;
		private readonly EngineOptions _engineOptions;
		public readonly ReadonlyEngineOptions EngineOptions;
		public int currentLevel{ get; private set; }

		private readonly AbstractTrackBlock[,] grid;
		public SequenceTrainEngine(EngineOptions options){
			_engineOptions = options;
			this.EngineOptions = new ReadonlyEngineOptions(options);
			if (_engineOptions.gridWidth < MinWidth){
				throw new ArgumentException("gridWidth cannot be less than " + MinWidth);
			}
			if (_engineOptions.gridHeight < MinHeight){
				throw new ArgumentException("gridHeight cannot be less than " + MinHeight);
			}
			if (_engineOptions.mapWraps){
				throw new InvalidOperationException("Map wrapping is not " +
					"supported yet");
			}
			if (_engineOptions.canSwapOutTrackUnderTrain){
				throw new InvalidOperationException("Swapping out track under " +
                    "the train is not supported yet.");
			}
			if (_engineOptions.couplingLength < 0){
				throw new ArgumentException("Coupling length cannot be less " +
					"than 0!");
			}
			if (_engineOptions.trainCarLength < 1){
				throw new ArgumentException("Train car length must be greather " +
				                            "than 0!");
			}

			if (_engineOptions.version == null){
				_engineOptions.trueVersion = CurVersion;
			}
			else{
				_engineOptions.trueVersion = (int)_engineOptions.version;
			}
			int[] arraySeed = new int[2];
			byte[] bytes = BitConverter.GetBytes(_engineOptions.seed);
			arraySeed[0] = BitConverter.ToInt32(bytes, 0);
			arraySeed[1] = BitConverter.ToInt32(bytes, bytes.Length / 2);
			random.Initialize(arraySeed);

			grid = new AbstractTrackBlock[_engineOptions.gridWidth,
			                              _engineOptions.gridHeight];
			fillGrid();
		}

		public AbstractTrackBlock this[int x, int y]{
			get{
				return grid[x, y];
			}
		}

		void fillGrid() {
			centerX = _engineOptions.gridWidth / 2;
			centerY = _engineOptions.gridHeight / 2;
			for(int x = 0; x < _engineOptions.gridWidth; x++) {
				for (int y = 0; y < _engineOptions.gridHeight; y++) {
					AbstractTrackBlock item;
					if (x == centerX && y == centerY){
						item = new DoubleStraightTrackBlock(x, y, 0, this);
					}
					else{
						item = generateTrackPiece(x, y);
					}
					grid[x, y] = item;
				}
			}
		}

		private AbstractTrackBlock generateTrackPiece(int x, int y){
			//give random spin
			int rotation = random.Next();
			AbstractTrackBlock result;
			if (!_engineOptions.mapWraps){
				if(x == 0 || x == _engineOptions.gridWidth - 1 ||
					y == 0 || y == _engineOptions.gridHeight - 1) {
					// it is on the edge
					debugLog(String.Format("[{0}, {1}] is on the edge", x, y));
					if(random.Next() % 2 == 0) {
						return new DoubleCurvedTrackBlock(x, y, rotation, this);
					}
					return new CurvedTrackBlock(x, y, rotation, this);
				}
				if ((x == 1 || x == _engineOptions.gridWidth - 2) &&
				         (y == 1 || y == _engineOptions.gridHeight -2)){
					if (random.Next() % 10 == 0){
							return new DoubleStraightTrackBlock(x, y, rotation,
								this);
					}
					return new DoubleCurvedTrackBlock(x, y, rotation, this);
				}
			}
			
			TrackType type = (TrackType)(random.Next() %
				(int)TrackType.Station);//it is assumed that the station is the last one
			//TODO: add weights
			switch (type){
				case TrackType.Curved:
					result = new CurvedTrackBlock(x, y, rotation,
					                            this);
					break;
					case TrackType.Straight:
					result = new StraightTrackBlock(x, y, rotation,
					                              this);
					break;
				case TrackType.DoubleCurved:
					result = new DoubleCurvedTrackBlock(x, y,
					                                  rotation, this);
					break;
				case TrackType.DoubleStraight:
					result = new DoubleStraightTrackBlock(x, y,
					                                    rotation, this);
					break;
				default:
					throw new InvalidOperationException(
						"Unexpected track type " + type + "!");
			}
			return result;
		}
		/// <summary>
		/// Perform one tick. Returns true if a tick happened. Throws a
		/// <see cref="GameOverException"/> if the train crashes
		/// </summary>
		public bool tick(){
			return false;
		}

		public static event EventHandler<DebugLogEventArgs> DebugLogEvent;

		private void debugLog(object thing){
			if (DebugLogEvent != null){
				DebugLogEventArgs args = new DebugLogEventArgs();
				args.thing = thing;
				DebugLogEvent(this, args);
			}
		}
		public class DebugLogEventArgs : EventArgs{
			public object thing { get; internal set;}
			public override string ToString() {
				return string.Format("{0}", thing);
			}
		}
	}
}

