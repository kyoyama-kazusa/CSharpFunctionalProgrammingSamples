#include "CallCSharpMethod.h"
#include <stdbool.h>

static _Bool (*value_comparer)(int, int);


void set_value_comparer(_Bool (*comparer)(int, int))
{
	value_comparer = comparer;
}

void bubble_sort(int* values, int length)
{
	if (!value_comparer)
	{
		return;
	}

	for (int i = 0; i < length - 1; i++)
	{
		for (int j = 0; j < length - 1 - i; j++)
		{
			if ((*value_comparer)(values[j], values[j + 1]))
			{
				int temp = values[j];
				values[j] = values[j + 1];
				values[j + 1] = temp;
			}
		}
	}
}
