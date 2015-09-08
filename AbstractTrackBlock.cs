using System;

namespace SequenceTrainLogic {
	public abstract class AbstractTrackBlock : AbtractLogicEngineItem {
		protected const int EdgesSize = 4;
		private LazyInit<TrackEnds[]> edges;
		/// <summary>
		/// This is used to determine the current rotation of the block
		/// <see cref="getEndOnSide"/>
		/// </summary>
		private uint rotationOffset = 0;

		public int X { get; private set;}
		public int Y { get; private set;}
		protected AbstractTrackBlock(int x, int y, SequenceTrainEngine engine,
            	int rotationOffset) : base (engine) {
			this.X = x;
			this.Y = y;
			this.rotationOffset = unchecked((uint)rotationOffset);
			edges = new LazyInit<TrackEnds[]>(getTrackEdgeTypes);
		}

		public abstract TrackType getTrackType();
		/// <summary>
		/// Get the value for the edges for this type of Track.
		/// The returned array is expected to be 4 elements long
		/// </summary>
		/// <returns>The track edge types.</returns>
		protected abstract TrackEnds[] getTrackEdgeTypes();
		/// <summary>
		/// (For multi-piece support)
		/// Returns the piece that connects to the given side or null if there is none
		/// </summary>
		/// <returns>The piece on side.</returns>
		/// <param name="side">Side.</param>
		public virtual AbstractTrackBlock getPieceOnSide(TrackSide side){
			TrackEnds trackEnd = getEndOnSide(side);
			if (trackEnd == TrackEnds.None){
				return null;
			}
			return this;
		}

		public TrackEnds getEndOnSide(TrackSide side){
			int index = (int)((rotationOffset + (int)side) % EdgesSize);
			return ((TrackEnds[])edges)[index];
		}

		public TrackEnds[] getTrackEnds(){
			TrackEnds[] result = new TrackEnds[4];
			for(TrackSide side = TrackSide.North;
			    side <= TrackSide.West; side++) {
				result[(int)side] = getEndOnSide(side);
			}
#if DEBUG
			//sanity check!
			int aCount = 0;
			int bCount = 0;
			foreach (var item in result) {
				if (item == TrackEnds.A){
					aCount++;
				}
				else if (item == TrackEnds.B){
					bCount++;
				}
			}
			if (aCount != bCount){
				throw new InvalidOperationException(aCount + "!=" + bCount);
			}
#endif
			return result;
		}

		// virtual to allow complex blocks to listen to rotations
		/// <summary>
		/// Rotate the block.
		/// </summary>
		/// <param name="clockwize">If set to <c>true</c> then rotate
		/// clockwize.</param>
		/// <returns>true if the tile was rotated, false otherwise. One reason
		/// a block won't rotate is if the train is on top of it.</returns>
		public virtual bool rotate(bool clockwize){
			if (clockwize){
				rotationOffset++;
			}
			else{
				rotationOffset--;
			}
			return true;
		}

		public bool rotateClockwize() {
			return rotate(true);
		}

		public bool rotateCounterClockwize() {
			return rotate(false);
		}

	}
}

