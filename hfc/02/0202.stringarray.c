#include <stdio.h>

int main()
{
	char s1[5];
	s1[0] = 'a';
	s1[1] = 'B';
	
	printf("%s\n", s1);
	printf("%p\n", &s1);
	printf("%p\n", &s1[4]);
	printf("%p\n", &s1[10]);
	char c = 'S';
	printf("%p\n", &c);
	char s2[] = "abcdeaaaaaaaaaaaaaaaaaaaaaaaaaaaaab";
	printf("%s\n", s2);
	printf("%c\n", s2[4]);
	printf("%c\n", s2[5]);
	printf("%c\n", 4[s2]);
	printf("%p\n", &s2);
	printf("%p\n", &s2[5]);
	printf("%d\n", &s2[5]);
	
	//char name[20];
	//scanf("%19s", name);
	//printf("%s \n", name);
	
	char overflow[5];
	/*
	char *of = &overflow[0];
	fgets(of, 5, stdin);
	printf("%s \n", overflow);
	*/
	printf("%d", sizeof(overflow));
	return 0;
}