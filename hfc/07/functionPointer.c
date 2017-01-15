#include <stdio.h>

int Sum(int a, int b)
{
	return a + b;
}

int Times(int a, int b)
{
	return a * b;
}

int calc(int (*calcFunct)(int, int), int a, int b)
{
	//return (*calcFunct)(a, b);
	return calcFunct(a, b);
}

int main()
{
	printf("%d\n", calc(&Sum, 4, 5));
	printf("%d\n", calc(Sum, 4, 5));
	printf("%d\n", calc(Times, 4, 5));
	return 0;
}