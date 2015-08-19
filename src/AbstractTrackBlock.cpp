/*
 * File:   AbstractTrackBlock.cpp
 * Author: astephens
 *
 * Created on August 15, 2015, 2:50 PM
 */

#include "../include/AbstractTrackBlock.h"



//    #pragma GCC diagnostic push
//    //ignore the pure virtual called from constructor warning
//    #pragma GCC diagnostic ignored "-Wall"
AbstractTrackBlock::AbstractTrackBlock(SequenceTrainEngine& engine,
    int posX, int posY) : AbstractLogicEngineItem(engine),x(posX), y(posY),
	edges(getTrackEdgeTypes()), rotationOffset(0) {
//	#pragma GCC diagnostic pop
}

AbstractTrackBlock::AbstractTrackBlock(const AbstractTrackBlock& orig) :
	AbstractLogicEngineItem(orig.getParent()), x(orig.x), y(orig.y),
	edges(orig.edges), rotationOffset(orig.rotationOffset) {
}

AbstractTrackBlock::~AbstractTrackBlock() {
}

AbstractTrackBlock* AbstractTrackBlock::getPieceOnSide(TrackSide side) {
	TrackEnds trackEnd = getEndOnSide(side);
	if (trackEnd == NONE) {
		return NULL;
	}
	return this;
}
TrackEnds AbstractTrackBlock::getEndOnSide(TrackSide side) const {
	int index = (rotationOffset + side) % edgesSize;
	return edges[index];
}
void AbstractTrackBlock::rotate(bool clockwize) {
	int increment = clockwize * 2 - 1;
	rotationOffset += increment;
}

