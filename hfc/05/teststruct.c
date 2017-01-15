#include <stdio.h>
#include "fish.h"

void LabelFish(fish f)
{
	printf("Name:%s,Species:%s,Teeth:%d,Age:%d.\n", f.name, f.species, f.teeth, f.age);
	printf("%s eat %f %s.\n", f.name, f.care.food.weight, f.care.food.ingredients);
	printf("%s %s for %f hours.\n", f.name, f.care.exercise.description, f.care.exercise.duration);
}

int main()
{
	fish snappy = {"Snappy", "Piranha", 69, 4, {{"Meat", 3.5}, {"swim in the jacuzzi", 7.5}}};
	LabelFish(snappy);
	return 0;
}