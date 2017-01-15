/*
struct exercise{
	const char *description;
	float duration;
};

struct meal{
	const char *ingredients;
	float weight;
};

struct preferences{
	struct meal food;
	struct exercise exercise;
};

struct fish{
	const char *name;
	const char *species;
	int teeth;
	int age;	
	struct preferences care;
};
*/
typedef struct exercise{
	const char *description;
	float duration;
}exercise;

typedef struct meal{
	const char *ingredients;
	float weight;
}meal;

typedef struct preferences{
	meal food;
	exercise exercise;
}preferences;

typedef struct fish{
	const char *name;
	const char *species;
	int teeth;
	int age;	
	preferences care;
}fish;