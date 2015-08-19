/* 
 * File:   ArgCheck.h
 * Author: astephens
 *
 * Created on August 18, 2015, 7:58 PM
 */

#ifndef ARGCHECK_H
#define	ARGCHECK_H
#include <string>
namespace ArgCheck{
	using namespace std;
	template <typename T> void notNull(T* item, string& argName);
}


#endif	/* ARGCHECK_H */

