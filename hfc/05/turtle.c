#include <stdio.h>

typedef struct{
	char *name;
	int age;
}turtle;

void HappyBirthday(turtle *t)
{
	(*t).age += 1;
	printf("Happy Birthday %s! You are now %d years old.\n", (*t).name, (*t).age);
}

void CheckAddress(turtle *t)
{
	/*
	printf("%s's address is %p, %s's address of name is %p, %s's address of age is %p.\n", 
		(*t).name, &(*t), (*t).name, &((*t).name), (*t).name, &((*t).age));
	*/
	printf("%s's address is %p, %s's address of name is %p, %s's address of age is %p.\n", 
		t->name, t, t->name, &(t->name), t->name, &(t->age));
}

void CheckAddress2(turtle t)
{
	printf("%s's address is %p, %s's address of name is %p, %s's address of age is %p.\n", 
		t.name, &t, t.name, &(t.name), t.name, &(t.age));
}

int main()
{
	turtle t = {"Peter", 99};
	//HappyBirthday(&t);
	//printf("%s is now %d years old.\n", t.name, t.age);
	printf("%s's address is %p, %s's address of name is %p, %s's address of age is %p.\n", t.name, &t, t.name, &(t.name), t.name, &(t.age));
	CheckAddress2(t);
	CheckAddress(&t);
	return 0;
}