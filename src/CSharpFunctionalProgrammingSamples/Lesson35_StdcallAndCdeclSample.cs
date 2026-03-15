namespace CSharpFunctionalProgrammingSamples;

/// <summary>
/// <see langword="stdcall"/> 和 <see langword="cdecl"/> 函数调用约定。
/// </summary>
internal sealed class Lesson35_StdcallAndCdeclSample : Sample
{
	/// <inheritdoc/>
	public override void RunSample()
	{
		// 调用约定（Calling Convention）。
		// 表示 C# 调用 C/C++ 函数的时候，函数在执行传参、栈帧分配等需要操作栈内存空间的行为需要两方约定达成一致的一种存在。
		//    * stdcall：标准调用（Standard Call）- 由自己调用完成后内存释放；
		//    * cdecl：C 函数声明（C Declaration）- 由调用方（什么函数调用他，的那个函数）调用完成后释放内存。
		//       对于 C/C++ 变长参数表列的函数需要使用这个调用约定，因为你无法预先知晓函数的内存分配空间大小，也就无法自己释放。
		//    * thiscall：C++ 的隐式传参 this 的调用约定，这个用于 C++ 里传 this 用。
		//    * fastcall：快速调用。
		//    * vectorcall：向量调用。操作 SIMD（Single Instruction Multiple Data，单个指令操作多个数据）的，参数存入 SIMD 寄存器。

		Console.WriteLine("=== C# 调用 C 函数示例 ===");
		Console.WriteLine();

		// 测试 StdCall 函数。
		Console.WriteLine("1. StdCall 调用约定:");
		int result1 = StdcallAdd(10, 20);
		Console.WriteLine($"   结果: {result1}\n");

		// 测试 Cdecl 函数。
		Console.WriteLine("2. Cdecl 调用约定:");
		int result2 = CdeclAdd(15, 25);
		Console.WriteLine($"   结果: {result2}\n");

		// 测试变参函数（Cdecl）。
		Console.WriteLine("5. Cdecl 变参函数:");
		int sum = VarargsSum(4, __arglist(1, 3, 5, 7)); // 传参（数组长度、指针元素数、__arglist 变长参数元素数）只是一个习惯。
		Console.WriteLine($"   总和: {sum}\n");

		// 如果调用约定写错了，就会造成栈帧要么不会被正确释放，要么被释放两次，总之就是不同步。
		// 然后，栈内存空间变得状态不正确，于是可能会造成两种情况：
		//   1. 抛出 StackOverflowException - 栈内存提前出现溢出或不正确溢出；
		//   2. 抛出 ExecutionEngineException - 告知栈内存损坏了（众多 CLR 层面的异常都会造成这个异常类型抛出，不只是栈内存这个例子）。
	}


	[DllImport("CSharpFunctionalProgrammingSamples.Lesson35.dll", EntryPoint = "stdcall_add", CallingConvention = CallingConvention.StdCall)]
	private static extern int StdcallAdd(int a, int b);

	[DllImport("CSharpFunctionalProgrammingSamples.Lesson35.dll", EntryPoint = "cdecl_add", CallingConvention = CallingConvention.Cdecl)]
	private static extern int CdeclAdd(int a, int b);

	[DllImport("CSharpFunctionalProgrammingSamples.Lesson35.dll", EntryPoint = "vararg_sum", CallingConvention = CallingConvention.Cdecl)]
	private static extern int VarargsSum(int count, __arglist);
}
