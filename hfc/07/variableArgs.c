#include <stdio.h>
#include <stdarg.h>

void print_ints(int args, ...)
{
	va_list ap;
	va_start(ap, args);
	int i;
	for(i = 0; i < args; i++)
	{
		printf("%d\n", va_arg(ap, int));
	}
}

int main()
{
	print_ints(6, 3, 6, 1, 7, 8);
	return 0;
}