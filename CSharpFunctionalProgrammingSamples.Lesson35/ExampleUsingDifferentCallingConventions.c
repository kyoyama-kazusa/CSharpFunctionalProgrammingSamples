#include "ExampleUsingDifferentCallingConventions.h"
#include <stdio.h>
#include <varargs.h>

int __stdcall stdcall_add(int a, int b)
{
	printf("StdCall: 计算 %d + %d\n", a, b);
	return a + b;
}

int __cdecl cdecl_add(int a, int b)
{
	printf("CDecl: 计算 %d + %d\n", a, b);
	return a + b;
}

int __cdecl vararg_sum(int count, ...)
{
	va_list args;
#ifdef _MSC_VER
	// Visual Studio 的 va_start 只传一个参数，但是标准定义是两个参数（count 要传入进去）。
	va_start(args);
#else
	// GCC/MinGW 等其他编译器。
	va_start(args, count);
#endif

	printf("CDecl 变参函数: 参数个数 %d\n", va_arg(args, int));

	// 计算求和。
	int sum = 0;
	for (int i = 0; i < count; i++) {
		int value = va_arg(args, int);
		printf("  参数 %d: %d\n", i + 1, value);
		sum += value;
	}

	va_end(args);
	return sum;
}
