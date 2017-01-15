#include <stdio.h>
#include "funcorder.h"

int main()
{
	float f = 1.0;
	
	f = MyInc(f);
	printf("f=%f", f);
	
	return 0;
}

float MyInc(float f)
{
	return f + 2;
}