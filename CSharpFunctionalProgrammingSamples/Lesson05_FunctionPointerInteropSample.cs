namespace CSharpFunctionalProgrammingSamples;

/// <summary>
/// 函数指针进行互操作性。
/// </summary>
/// <remarks>
/// 函数指针是 C# 9 的概念。原生 C# 并不支持函数指针，一般用的是委托来解决问题；
/// 但是可以从 <see cref="Lesson05_DelegateInteropSample"/> 类型里看到，它的操作非常繁琐，所以 C# 9 就有了新的处理方式。
/// </remarks>
internal sealed class Lesson05_FunctionPointerInteropSample : Sample
{
	/// <inheritdoc/>
	public override void RunSample()
	{
		int[] array = [3, 8, 1, 6, 5, 4, 7, 2, 9];
		unsafe
		{
			fixed (int* ptr = array)
			{
				qsort(ptr, 9, sizeof(int), &cmp);
			}
		}
		Console.WriteLine(string.Join(", ", array));


		[UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
		static unsafe int cmp(void* a, void* b) => *(int*)a - *(int*)b;


		// compare 函数的指针声明格式是 int (*compare)(const void*, const void*)。
		// C# 9 以前是没有函数指针的语法格式的。
		// C# 9 开始可以使用 delegate*<...> 的方式声明一个函数指针。
		[DllImport("ucrtbase.dll", EntryPoint = "qsort", CallingConvention = CallingConvention.Cdecl)]
		static extern unsafe void qsort(
			void* array,
			int length,
			uint size,
			delegate* unmanaged[Cdecl]<void*, void*, int> comparer
		);
	}
}
