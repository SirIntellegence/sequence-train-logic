using System;

namespace sequencetrainlogic {
	public abstract class AbstractTrackBlock : AbtractLogicEngineItem {
		protected const int edgesSize = 4;
		private readonly TrackEnds[] edges;
		/// <summary>
		/// This is used to determine the current rotation of the block
		/// <see cref="getEndOnSide"/>
		/// </summary>
		private int rotationOffset = 0;

		public int X { get; private set;}
		public int Y { get; private set;}
		protected AbstractTrackBlock(int x, int y, SequenceTrainEngine engine) : base (engine) {
			this.X = x;
			this.Y = y;
			edges = getTrackEdgeTypes();
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
			if (trackEnd == TrackEnds.NONE){
				return null;
			}
			return this;
		}

		public TrackEnds getEndOnSide(TrackSide side){
			int index = (rotationOffset + side) % edgesSize;
			return edges[index];
		}

		public void rotate(bool clockwize){
			int increment = Convert.ToInt32(clockwize) * 2 - 1;
			rotationOffset += increment;
		}

		public void rotateClockwize() {
			rotate(true);
		}

		public void rotateCounterClockwize() {
			rotate(false);
		}

	}
}

