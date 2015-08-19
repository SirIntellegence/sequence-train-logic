#include <stdexcept>
#include "../include/ArgCheck.h"

using namespace std;
namespace ArgCheck{
	template <typename T> void notNull(T* item, string& argName){
//		if (argName == nullptr){ can never be null....
//			throw new invalid_argument("argName");
//		}
		if (!item){
			throw new invalid_argument(argName);
		}
	}
	
}