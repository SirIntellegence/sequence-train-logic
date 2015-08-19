#ifndef SEQUENCETRAINENGINE_H
#define SEQUENCETRAINENGINE_H
#include "EngineOptions.h"
class SequenceTrainEngine{
    public:
        SequenceTrainEngine();
        virtual ~SequenceTrainEngine();
        SequenceTrainEngine(const SequenceTrainEngine& other);
		SequenceTrainEngine(EngineOptions options);
    protected:
    private:
        EngineOptions engineOptions;
};

#endif // SEQUENCETRAINENGINE_H
