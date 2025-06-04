namespace CSharpFunctionalProgrammingSamples;

/// <summary>
/// 用委托执行互操作性（调用 C/C++ 函数）。
/// </summary>
internal sealed class Lesson05_DelegateInteropSample : Sample
{
	/// <inheritdoc/>
	public override void RunSample()
	{
		int[] array = [3, 8, 1, 6, 5, 4, 7, 2, 9];
		unsafe
		{
			fixed (int* ptr = array)
			{
				qsort(ptr, 9, sizeof(int), new Comparer(cmp));
			}
		}
		Console.WriteLine(string.Join(", ", array));


		static unsafe int cmp(void* a, void* b) => *(int*)a - *(int*)b;


		// 函数指针，compare 函数的指针声明格式是 int (*compare)(const void*, const void*)。
		[DllImport("ucrtbase.dll", EntryPoint = "qsort", CallingConvention = CallingConvention.Cdecl)]
		static extern unsafe void qsort(
			void* array,
			int length,
			uint size,
			Comparer comparer
		);

		// 调用约定 calling convention
		// 变长参数（varargs）
		// 内存回收的机制
		// 返回值 __cdecl printf(格式化字符串, ...)
	}
}

/// <summary>
/// 表示一个比较器的委托。
/// </summary>
/// <param name="left">比较的第一个元素的地址。</param>
/// <param name="right">比较的第二个元素的地址。</param>
/// <returns>比较结果。</returns>
[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
file unsafe delegate int Comparer(void* left, void* right);
