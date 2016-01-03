using System;

namespace SequenceTrainLogic {
	class DoubleStraightTrackBlock : AbstractTrackBlock{
		private static readonly TrackEnds[] ends = {
			TrackEnds.A,
			TrackEnds.A,
			TrackEnds.B,
			TrackEnds.B
		};
		private readonly DoubleStraightChild a, b;
		public DoubleStraightTrackBlock(int x, int y, int rotationOffset,
        		SequenceTrainEngine parent) : base (x, y, parent, rotationOffset){
			a = new DoubleStraightChild(this, rotationOffset);
			b = new DoubleStraightChild(this, rotationOffset + 1);
		}

		#region implemented abstract members of AbstractTrackBlock

		public override TrackType getTrackType() {
			return TrackType.DoubleStraight;
		}

		protected override TrackEnds[] getTrackEdgeTypes() {
			return ends;
		}

		#endregion

		public override AbstractTrackBlock getPieceOnSide(TrackSide side) {
			TrackEnds end = a.getEndOnSide(side);
			if (end == TrackEnds.None){
				return b;
			}
			return a;
		}

		public override bool rotate(bool clockwize){
			if (!base.rotate(clockwize)){
				return false;
			}
			a.baseRotate(clockwize);
			b.baseRotate(clockwize);
			return true;
		}

		private class DoubleStraightChild : StraightTrackBlock{
			DoubleStraightTrackBlock parentBlock;

			public DoubleStraightChild(DoubleStraightTrackBlock parentBlock,
                   int rotationOffset) : base(parentBlock.x, parentBlock.y,
                   rotationOffset, parentBlock.Parent){
				this.parentBlock = parentBlock;
			}
			public override bool rotate(bool clockwize) {
				return parentBlock.rotate(clockwize);
			}
			internal bool baseRotate(bool clockwize){
				return base.rotate(clockwize);
			}
		}
	}
	class DoubleCurvedTrackBlock : AbstractTrackBlock{
		private static readonly TrackEnds[] ends = {
			TrackEnds.A,
			TrackEnds.B,
			TrackEnds.A,
			TrackEnds.B
		};
		private readonly DoubleCurvedChild a, b;
		public DoubleCurvedTrackBlock(int x, int y, int rotationOffset,
                SequenceTrainEngine parent) : base (x, y, parent, rotationOffset){
			a = new DoubleCurvedChild(this, rotationOffset);
			b = new DoubleCurvedChild(this, rotationOffset + 2);
		}

		#region implemented abstract members of AbstractTrackBlock

		public override TrackType getTrackType() {
			return TrackType.DoubleCurved;
		}

		protected override TrackEnds[] getTrackEdgeTypes() {
			return ends;
		}

		#endregion

		public override AbstractTrackBlock getPieceOnSide(TrackSide side) {
			TrackEnds end = a.getEndOnSide(side);
			if (end == TrackEnds.None){
				return b;
			}
			return a;
		}

		public override bool rotate(bool clockwize){
			if (!base.rotate(clockwize)){
				return false;
			}
			a.baseRotate(clockwize);
			b.baseRotate(clockwize);
			return true;
		}

		private class DoubleCurvedChild : CurvedTrackBlock{
			DoubleCurvedTrackBlock parentBlock;

			public DoubleCurvedChild(DoubleCurvedTrackBlock parentBlock,
                	int rotationOffset) : base(parentBlock.x, parentBlock.y,
			        rotationOffset, parentBlock.Parent){
				this.parentBlock = parentBlock;
			}
			public override bool rotate(bool clockwize) {
				return parentBlock.rotate(clockwize);
			}
			internal bool baseRotate(bool clockwize){
				return base.rotate(clockwize);
			}
		}
	}
}

