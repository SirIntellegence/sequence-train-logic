#include "../include/SequenceTrainEngine.h"
SequenceTrainEngine::SequenceTrainEngine() : engineOptions(){
    //ctor
}

SequenceTrainEngine::~SequenceTrainEngine(){
    //dtor
}

SequenceTrainEngine::SequenceTrainEngine(const SequenceTrainEngine& other) :
    engineOptions(other.engineOptions){
    //copy ctor
}
SequenceTrainEngine::SequenceTrainEngine(EngineOptions options) :
    engineOptions(options){
//	options.
}
