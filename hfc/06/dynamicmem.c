#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include "island.h"

island* Create(char *name)
{
	island *i = malloc(sizeof(island));
	i->name = strdup(name);
	i->opens = "09:00";
	i->closes = "18:00";
	i->next = NULL;
	
	return i;
}

void Release(island *start)
{
	island *i = start;
	island *next = NULL;
	for(; i != NULL; i = next)
	{
		next = i->next;
		free(i->name);
		free(i);
	}
}

int main()
{
	//char *name = malloc(sizeof(char));
	//name = "taiwan";
	//char name[80];
	//fgets(name, 80, stdin);
	//island *island0 = Create(name);
	
	//name = malloc(sizeof(char));
	//name = "hainan";	
	//fgets(name, 80, stdin);
	//island *island1 = Create(name);
	//island0->next = island1;
	
	char name[80];
	island *start = NULL;
	island *i = NULL;
	island *next = NULL;
	for(; fgets(name, 80, stdin) != NULL; i = next)
	{
		next = Create(name);
		if (start = NULL)
		{
			start = next;
		}
		if (i != NULL)
		{
			i->next = next;
			
			printf("island name is %s\n", i->name);
		}
	}
	
	Release(start);
	
	return 0;
}