#include <stdio.h>
#include <limits.h>
#include <float.h>

int main()
{
	printf("The value of CHAR_MAX is %i.\n", CHAR_MAX);
	printf("The value of CHAR_MIN is %i.\n", CHAR_MIN);
	printf("An char takes %hi bytes.\n", sizeof(char));
	
	printf("The value of INT_MAX is %i.\n", INT_MAX);
	printf("The value of INT_MIN is %i.\n", INT_MIN);
	printf("An int takes %hi bytes.\n", sizeof(int));
	
	printf("The value of SHRT_MAX is %i.\n", SHRT_MAX);
	printf("The value of SHRT_MIN is %i.\n", SHRT_MIN);
	printf("An short takes %hi bytes.\n", sizeof(short));
	
	printf("The value of LONG_MAX is %li.\n", LONG_MAX);
	printf("The value of LONG_MIN is %li.\n", LONG_MIN);
	printf("An long takes %hi bytes.\n", sizeof(long));
	
	printf("The value of FLT_MAX is %f.\n", FLT_MAX);
	printf("The value of FLT_MIN is %.50f.\n", FLT_MIN);
	printf("An float takes %hi bytes.\n", sizeof(float));
	
	printf("The value of DBL_MAX is %.100f.\n", DBL_MAX);
	printf("The value of DBL_MIN is %.200f.\n", DBL_MIN);
	printf("An double takes %hi bytes.\n", sizeof(double));
	return 0;
}