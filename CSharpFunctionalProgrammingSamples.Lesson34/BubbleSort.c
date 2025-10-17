#include "BubbleSort.h"

/// <summary>
/// 冒泡排序。
/// </summary>
/// <param name="arr">数组，传指针。</param>
/// <param name="length">长度。</param>
/// <param name="comparer">表明排序的规则。</param>
void bubble_sort(int* arr, int length, int (*comparer)(int, int))
{
	for (int i = 0; i < length - 1; i++)
	{
		for (int j = 0; j < length - 1 - i; j++)
		{
			if (comparer(arr[j], arr[j + 1]) >= 0)
			{
				int temp = arr[j];
				arr[j] = arr[j + 1];
				arr[j + 1] = temp;
			}
		}
	}
}
