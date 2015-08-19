#
# Generated Makefile - do not edit!
#
# Edit the Makefile in the project folder instead (../Makefile). Each target
# has a -pre and a -post target defined where you can add customized code.
#
# This makefile implements configuration specific macros and targets.


# Environment
MKDIR=mkdir
CP=cp
GREP=grep
NM=nm
CCADMIN=CCadmin
RANLIB=ranlib
CC=gcc
CCC=g++
CXX=g++
FC=gfortran
AS=as

# Macros
CND_PLATFORM=GNU-Linux-x86
CND_DLIB_EXT=so
CND_CONF=Debug
CND_DISTDIR=dist
CND_BUILDDIR=build

# Include project Makefile
include Makefile

# Object Directory
OBJECTDIR=${CND_BUILDDIR}/${CND_CONF}/${CND_PLATFORM}

# Object Files
OBJECTFILES= \
	${OBJECTDIR}/_ext/34498928/AbstractLogicEngineItem.o \
	${OBJECTDIR}/_ext/34498928/AbstractTrackBlock.o \
	${OBJECTDIR}/_ext/34498928/ArgCheck.o \
	${OBJECTDIR}/_ext/34498928/SequenceTrainEngine.o


# C Compiler Flags
CFLAGS=

# CC Compiler Flags
CCFLAGS=-std=c++11 -Wall
CXXFLAGS=-std=c++11 -Wall

# Fortran Compiler Flags
FFLAGS=

# Assembler Flags
ASFLAGS=

# Link Libraries and Options
LDLIBSOPTIONS=

# Build Targets
.build-conf: ${BUILD_SUBPROJECTS}
	"${MAKE}"  -f nbproject/Makefile-${CND_CONF}.mk ${CND_DISTDIR}/${CND_CONF}/${CND_PLATFORM}/libsequence-train-logic.${CND_DLIB_EXT}

${CND_DISTDIR}/${CND_CONF}/${CND_PLATFORM}/libsequence-train-logic.${CND_DLIB_EXT}: ${OBJECTFILES}
	${MKDIR} -p ${CND_DISTDIR}/${CND_CONF}/${CND_PLATFORM}
	${LINK.cc} -o ${CND_DISTDIR}/${CND_CONF}/${CND_PLATFORM}/libsequence-train-logic.${CND_DLIB_EXT} ${OBJECTFILES} ${LDLIBSOPTIONS} -shared -fPIC

${OBJECTDIR}/_ext/34498928/AbstractLogicEngineItem.o: /home/austin/DEV/sequence-train-logic/src/AbstractLogicEngineItem.cpp 
	${MKDIR} -p ${OBJECTDIR}/_ext/34498928
	${RM} "$@.d"
	$(COMPILE.cc) -g -fPIC  -MMD -MP -MF "$@.d" -o ${OBJECTDIR}/_ext/34498928/AbstractLogicEngineItem.o /home/austin/DEV/sequence-train-logic/src/AbstractLogicEngineItem.cpp

${OBJECTDIR}/_ext/34498928/AbstractTrackBlock.o: /home/austin/DEV/sequence-train-logic/src/AbstractTrackBlock.cpp 
	${MKDIR} -p ${OBJECTDIR}/_ext/34498928
	${RM} "$@.d"
	$(COMPILE.cc) -g -fPIC  -MMD -MP -MF "$@.d" -o ${OBJECTDIR}/_ext/34498928/AbstractTrackBlock.o /home/austin/DEV/sequence-train-logic/src/AbstractTrackBlock.cpp

${OBJECTDIR}/_ext/34498928/ArgCheck.o: /home/austin/DEV/sequence-train-logic/src/ArgCheck.cpp 
	${MKDIR} -p ${OBJECTDIR}/_ext/34498928
	${RM} "$@.d"
	$(COMPILE.cc) -g -fPIC  -MMD -MP -MF "$@.d" -o ${OBJECTDIR}/_ext/34498928/ArgCheck.o /home/austin/DEV/sequence-train-logic/src/ArgCheck.cpp

${OBJECTDIR}/_ext/34498928/SequenceTrainEngine.o: /home/austin/DEV/sequence-train-logic/src/SequenceTrainEngine.cpp 
	${MKDIR} -p ${OBJECTDIR}/_ext/34498928
	${RM} "$@.d"
	$(COMPILE.cc) -g -fPIC  -MMD -MP -MF "$@.d" -o ${OBJECTDIR}/_ext/34498928/SequenceTrainEngine.o /home/austin/DEV/sequence-train-logic/src/SequenceTrainEngine.cpp

# Subprojects
.build-subprojects:

# Clean Targets
.clean-conf: ${CLEAN_SUBPROJECTS}
	${RM} -r ${CND_BUILDDIR}/${CND_CONF}
	${RM} ${CND_DISTDIR}/${CND_CONF}/${CND_PLATFORM}/libsequence-train-logic.${CND_DLIB_EXT}

# Subprojects
.clean-subprojects:

# Enable dependency checking
.dep.inc: .depcheck-impl

include .dep.inc
