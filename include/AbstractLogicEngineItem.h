/*
 * File:   AbstractLogicEngineItem.h
 * Author: astephens
 *
 * Created on August 18, 2015, 7:55 PM
 */

#ifndef ABSTRACTLOGICENGINEITEM_H
#define	ABSTRACTLOGICENGINEITEM_H
#include "SequenceTrainEngine.h"
class AbstractLogicEngineItem {
public:
	AbstractLogicEngineItem(SequenceTrainEngine& engine) : parent(engine){
	}
	SequenceTrainEngine& getParent() const {return parent;}
//	AbstractLogicEngineItem(const AbstractLogicEngineItem& orig);
protected:
	virtual ~AbstractLogicEngineItem();
private:
	SequenceTrainEngine& parent;

};

#endif	/* ABSTRACTLOGICENGINEITEM_H */

