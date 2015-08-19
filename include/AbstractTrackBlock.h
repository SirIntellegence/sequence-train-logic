/*
 * File:   AbstractTrackBlock.h
 * Author: astephens
 *
 * Created on August 15, 2015, 2:50 PM
 */

#ifndef ABSTRACTTRACKBLOCK_H
#define	ABSTRACTTRACKBLOCK_H
#include <memory>
#include <boost/shared_array.hpp>
#include "Enums.h"
#include "AbstractLogicEngineItem.h"

class AbstractTrackBlock : public AbstractLogicEngineItem {
public:
	//	AbstractTrackBlock();
	AbstractTrackBlock(SequenceTrainEngine& parent, int x, int y);
	AbstractTrackBlock(const AbstractTrackBlock& orig);
	virtual ~AbstractTrackBlock();

	int getX() const {
		return x;
	}

	int getY() const {
		return y;
	}
	virtual TrackType getTrackType() const = 0;
	TrackEnds getEndOnSide(TrackSide side) const;
	void rotate(bool clockwize);

	void rotateClockwize() {
		rotate(true);
	}

	void rotateCounterClockwize() {
		rotate(false);
	}
	/*
	 * (For multi-piece support)
	 * Returns a reference to the piece that connects to the given side.
	 * If there is no piece in this block that connects to the given side,
	 * nullptr is returned
	 */
	virtual AbstractTrackBlock* getPieceOnSide(TrackSide side);

protected:
	static const int edgesSize = 4;
	/*
	 * Get the value for the edges for this type of Track.
	 * WARNING: the returned array is expected to be 4 elements long and a
	 * static value. As such it will not be deleted by the deconstructor.
	 */
	virtual boost::shared_array<TrackEnds> getTrackEdgeTypes() = 0;

private:
	int x, y;
    boost::shared_array<TrackEnds> edges;
	/*
	 * This is used to determine the current rotation of the block.
	 * See getEndOnSide
	 */
	int rotationOffset;

};

#endif	/* ABSTRACTTRACKBLOCK_H */

