#include <stdio.h>

typedef struct{
	char *name;
	char *opens;
	char *closes;
	struct island *next;
}island;

int main()
{
	island taiwan = {"taiwan", "09:00", "17:00", NULL};
	island hainan = {"hainan", "10:00", "18:00", NULL};
	island shangchuan = {"shangchuan", "08:00", "18:00", NULL};
	island xiachuan = {"xiachuan", "08:00", "18:00", NULL};
	taiwan.next = &hainan;
	hainan.next = &shangchuan;
	
	island *islnd = &taiwan;
	while (islnd != NULL)
	{
		printf("%s opens at %s, closes at %s.\n", islnd->name, islnd->opens, islnd->closes);
		islnd = islnd->next;
	}
	
	taiwan.next = &xiachuan;
	xiachuan.next = &shangchuan;
	islnd = &taiwan;
	while (islnd != NULL)
	{
		printf("%s opens at %s, closes at %s.\n", islnd->name, islnd->opens, islnd->closes);
		islnd = islnd->next;
	}
	return 0;
}