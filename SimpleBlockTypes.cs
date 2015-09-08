using System;

namespace SequenceTrainLogic {
	class StraightTrackBlock : AbstractTrackBlock {
		private static readonly TrackEnds[] ends = {
			TrackEnds.A,
			TrackEnds.None,
			TrackEnds.B,
			TrackEnds.None
		};
		public StraightTrackBlock(int x, int y, int rotationOffset,
				SequenceTrainEngine parent) :base(x, y, parent, rotationOffset) { }


		#region implemented abstract members of AbstractTrackBlock
		public override TrackType getTrackType() {
			return TrackType.Straight;
		}
		protected override TrackEnds[] getTrackEdgeTypes() {
			return ends;
		}
		#endregion
	}
	class CurvedTrackBlock : AbstractTrackBlock{
		private static readonly TrackEnds[] ends = {
			TrackEnds.A,
			TrackEnds.B,
			TrackEnds.None,
			TrackEnds.None
		};
		public CurvedTrackBlock(int x, int y, int rotationOffset,
        		SequenceTrainEngine parent) :base(x, y, parent, rotationOffset) { }


		#region implemented abstract members of AbstractTrackBlock
		public override TrackType getTrackType() {
			return TrackType.Curved;
		}
		protected override TrackEnds[] getTrackEdgeTypes() {
			return ends;
		}
		#endregion
	}
}

