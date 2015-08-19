/*
 * File:   EngineOptions.h
 * Author: astephens
 *
 * Created on August 15, 2015, 2:36 PM
 */

#ifndef ENGINEOPTIONS_H
#define	ENGINEOPTIONS_H

struct EngineOptions{
	public:
		/*
		 * Width and height of the grid.
		 */
		int gridWidth, gridHeight;
		/*
		 * The number of possible positions in a block
		 */
		int blockSections;
		/*
		 * Whether or not the train colliding with itself should cause a
		 * game over
		 */
		bool trainCanCrashWithSelf;
		
};


#endif	/* ENGINEOPTIONS_H */

