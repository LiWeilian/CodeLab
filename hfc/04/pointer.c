#include <stdio.h>

int main()
{
	int a;
	*(&a) = 123;
	int *b;
	b = &a;
	printf("%d,%d\n", a, *b);
	return 0;
}