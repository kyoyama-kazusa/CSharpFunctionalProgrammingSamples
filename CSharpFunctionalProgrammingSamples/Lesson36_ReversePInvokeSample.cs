namespace CSharpFunctionalProgrammingSamples;

/// <summary>
/// 调取 <see cref="Marshal.GetFunctionPointerForDelegate{TDelegate}(TDelegate)"/> 方法。
/// </summary>
internal sealed class Lesson36_ReversePInvokeSample : Sample
{
	/// <inheritdoc/>
	public override unsafe void RunSample()
	{
		// 反向 P/Invoke（Reverse P/Invoke）：
		// 本身是由 C/C++ 这边实现函数，然后被 C# 这边调用。
		// 这次我们需要声明的函数回调过程（下面例子里的 comparer 变量）会在 C/C++ 这边起到使用效果。
		// 要注意的是，C# 目前*官方*还不支持将函数使用类似 C/C++ __declspec(dllexport) 的机制将函数导出为 dll 文件里的一个成员，
		// 也就是说，C# 的函数（方法）不能被 C/C++ 直接调取——它没有这个机制。
		// 这里所说的被 C/C++ 这边调取是使用委托变函数指针的形式，参与 C/C++ 执行逻辑里的回调过程。
		// 比如下面的例子展示的是冒泡排序法的过程，其中“比较两个数组里的元素哪边更大”的这个函数使用的是 C# 实现的回调。

		int[] arr = [3, 8, 1, 6, 5, 4, 7, 2, 9];

		// 打印排序前的数组。
		Console.WriteLine($"[{string.Join(',', arr)}]");

		// 1. 定义委托类型示例，一会儿将其转换为非托管函数指针。
		Comparer comparer = static (a, b) => a.CompareTo(b) >= 0;

		// 2. 调取 C 语言函数，将排序算法用到的比较函数传入。
		// Marshal.GetFunctionPointerForDelegate 方法返回的是一个委托类型实例的对应非托管层面可调用的函数指针。
		// 需要注意的是，它需要传入的是一个委托实例，但是这个委托实例不能是泛型类型的，否则会引发 ArgumentException 异常。
		SetValueComparer((delegate* unmanaged<int, int, bool>)Marshal.GetFunctionPointerForDelegate(comparer));

		// 3. 调取 C 语言的冒泡排序法的过程。
		fixed (int* parr = arr)
		{
			BubbleSort(parr, arr.Length);
		}

		// 打印排序后的数组。
		Console.WriteLine($"[{string.Join(',', arr)}]");
	}

	[DllImport("CSharpFunctionalProgrammingSamples.Lesson36.dll", EntryPoint = "set_value_comparer")]
	private static extern unsafe void SetValueComparer(delegate* unmanaged<int, int, bool> comparer);

	[DllImport("CSharpFunctionalProgrammingSamples.Lesson36.dll", EntryPoint = "bubble_sort")]
	private static extern unsafe void BubbleSort(int* arr, int length);
}

[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
[return: MarshalAs(UnmanagedType.Bool)] // CLR 这层一般会给你处理掉，所以不标也可以正常执行。
internal delegate bool Comparer(int left, int right);
