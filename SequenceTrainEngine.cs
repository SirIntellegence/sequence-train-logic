using System;
using System.Collections.ObjectModel;

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
		private Train train;
		private readonly int[] speeds;
		private int curSpeedIndex;
		private bool gameOverHappened = false;
		private int tickCount = 0;
		private int levelTicks = 0;
		public readonly ReadonlyEngineOptions EngineOptions;
		public int currentLevel{ get; private set; }
		public ReadOnlyCollection<AbstractTrainCar> trainList{
			get{
				return train.PublicCarList;
			}
		}


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
			if (_engineOptions.speedCount < 1){
				throw new ArgumentException("Speed count cannot be less than " +
				                            "1!");
			}

			if (_engineOptions.version == null){
				_engineOptions.trueVersion = CurVersion;
			}
			else{
				_engineOptions.trueVersion = (int)_engineOptions.version;
			}
			speeds = new int[_engineOptions.speedCount];
			curSpeedIndex = 0;
			int[] arraySeed = new int[2];
			byte[] bytes = BitConverter.GetBytes(_engineOptions.seed);
			arraySeed[0] = BitConverter.ToInt32(bytes, 0);
			arraySeed[1] = BitConverter.ToInt32(bytes, bytes.Length / 2);
			random.Initialize(arraySeed);
			
			train = new Train(this);
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
			Locomotive locomotive = (Locomotive)train.PublicCarList[0];
			locomotive.x = centerX;
			locomotive.y = centerY;
			locomotive.entry = (TrackSide)(random.Next() % (int)TrackSide.West + 1);
			locomotive.progress = EngineOptions.blockSections / 2;
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
			if (gameOverHappened){
				return false;
			}
			updatePossibleSpeeds();
			moveTrain();
			tickCount++;
			levelTicks++;
			if (tickCount % 100 == 0){
				debugLog(tickCount + " ticks have happened");
			}
			return true;
		}

		private void moveTrain() {
			int speed = speeds[curSpeedIndex];
			AbstractTrainCar crashCar = null;
			foreach (var item in trainList) {
				int sections = item.progress;
				sections += speed;
				AbstractTrackBlock curBlock = this[item.x, item.y];
				TrackSide entry = item.entry;
				while (sections > EngineOptions.blockSections){
					sections -= EngineOptions.blockSections;
					TrackSide entrySide = entry;
					//discover the output edge
					AbstractTrackBlock subBlock = curBlock.getPieceOnSide(
						entrySide);
					TrackEnds endOnSide = subBlock.getEndOnSide(entrySide);
					//find the side the other end is on
					TrackEnds otherEnd = endOnSide == TrackEnds.A ?
						TrackEnds.B : TrackEnds.A;
					TrackSide exit = (TrackSide)(-1);
					TrackEnds[] ends = subBlock.getTrackEnds();
					for(int i = 0; i < (int)TrackSide.West + 1; i++) {
						if (ends[i] == otherEnd){
							exit = (TrackSide)i;
							break;
						}
					}
					AbstractTrackBlock nextBlock = getAdjacentBlock(
						curBlock, exit, true);
					if (nextBlock == null){
						//crash
						crashCar = item;
						//so other blocks will travel the right distance...
						speed -= sections;
						sections = EngineOptions.blockSections;
					}
					else{
						curBlock = nextBlock;
						entry = invertSide(exit);
					}
				}
				item.progress = sections;
				item.x = curBlock.x;
				item.y = curBlock.y;
				item.entry = entry;
				debugLog("Car " + item.trainIndex + " is now at " + item.x +","+
				         item.y+ " and " + item.progress +"sections entering " +
				         	"from side " + item.entry +".");
			}
			if (crashCar != null){
				gameOverHappened = true;
				throw new GameOverException(crashCar);
			}
		}

		void updatePossibleSpeeds() {
			if(levelTicks % 100 != 0) {
				return;
			}
			var multiplier = levelTicks / 100;
			for(int i = 0; i < speeds.Length; i++) {
				int baseSpeed = EngineOptions.blockSections / 100;
				if (baseSpeed == 0){
					baseSpeed = 1;
				}
				speeds[i] = 1;
//					(EngineOptions.blockSections/100) * (currentLevel + 1) *
//					(multiplier + 1 + i);
				debugLog("Speed " + i + " is " + speeds[i]);
			}
		}

		/// <summary>
		/// Get the train block adjacent to this one, optionally only if it
		/// connects to this one. Returns null if one was not found or if
		/// <code>connectingOnly</code> and the found one is not connected.
		/// </summary>
		/// <returns>The adjacent block.</returns>
		/// <param name="source">Source.</param>
		/// <param name="direction">Direction.</param>
		private AbstractTrackBlock getAdjacentBlock(AbstractTrackBlock source,
		                                            TrackSide direction,
		                                            bool connectingOnly){
			TrackEnds side = source.getEndOnSide(direction);
			if (connectingOnly && side == TrackEnds.None){
				return null;
			}
			AbstractTrackBlock result;
			switch (direction) {
				case TrackSide.North:
					if(source.y == 0) {
						return null;
					}
					result = this[source.x, source.y - 1];
					break;
				case TrackSide.East:
					if(source.x == 0) {
						return null;
					}
					result = this[source.x - 1, source.y];
					break;
				case TrackSide.South:
					if(source.y == EngineOptions.gridHeight - 1) {
						return null;
					}
					result = this[source.x, source.y + 1];
					break;
				case TrackSide.West:
					if(source.x == EngineOptions.gridWidth - 1) {
						return null;
					}
					result = this[source.x + 1, source.y];
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
			if (!connectingOnly){
				return result;
			}
			TrackSide otherSide = invertSide(direction);
			TrackEnds otherEnd = result.getEndOnSide(otherSide);
			if (otherEnd == TrackEnds.None){
				return null;
			}
			return result;
		}

		internal static TrackSide invertSide(TrackSide side) {
			//using something more readable than this...
//			return (TrackSide)((int)((int)side + TrackSide.West) % (int)TrackSide.West + 1);
			switch (side) {
				case TrackSide.North:
					return TrackSide.South;
				case TrackSide.East:
					return TrackSide.West;
				case TrackSide.South:
					return TrackSide.North;
				case TrackSide.West:
					return TrackSide.East;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}

		public static event EventHandler<DebugLogEventArgs> DebugLogEvent;
		public event GenericEventHandler<AbstractTrainCar> NewTrainCar;
		public event GenericEventHandler<AbstractTrainCar> TrainCarAttached;


		private void debugLog(object thing){
			if (DebugLogEvent != null){
				DebugLogEventArgs args = new DebugLogEventArgs();
				args.thing = thing;
				DebugLogEvent(this, args);
			}
		}

		internal bool isTrainOnTrack(AbstractTrackBlock block){
			foreach (var item in trainList) {
				if (item.x == block.x && item.y == block.y){
					return true;
				}
			}
			return false;
		}
	}
	public delegate void GenericEventHandler<T>(Object sender, GenericEventArgs<T> args);
	public class DebugLogEventArgs : EventArgs{
		public object thing {get; internal set;}
		public override String ToString(){
			return String.Format("{0}", thing);
		}
	}
	public class GenericEventArgs<T> : EventArgs{
		public T thing { get; private set;}
		public GenericEventArgs(T item){
			thing = item;
		}
	}

}

