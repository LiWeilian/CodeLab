#include <stdio.h>
//#include <stdlib.h>

int compare_int(void *a, void *b)
{
	int sa = *(int *)a;
	int sb = *(int *)b;
	return sa -sb;
}

int main()
{
	int ia[] = {2, 45,53, 5, 7, 9};
	qsort(ia, 6, sizeof(int), compare_int);
	int i;
	for(i = 0; i < 6; i++)
	{
		printf("%d\n", ia[i]);
	}
	return 0;
}