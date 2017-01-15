#include <stdio.h>

int main()
{
	int a = 1000;
	
	change(&a);
	printf("%d", a);
	
	return 0;
}

int change(int *v)
{
	*v = 2000;
	return *v;
}