namespace CSharpFunctionalProgrammingSamples;

/// <summary>
/// 将一个委托类型转换到另外一个委托类型的实例，如果两个委托类型的函数签名一致的话。
/// </summary>
public sealed class Lesson10_DelegateCastingSample : Sample
{
	/// <inheritdoc/>
	public override void RunSample()
	{
		// 委托类型是强类型的，这意味着我们绑定（实例化委托类型时候使用的方法组）一旦赋值给委托类型的实例后，
		// 就无法修改其绑定的委托类型了，这意味着我们无法把一个委托类型的实例赋值给另外一个委托类型的实例，
		// 即使这两个类型的签名（包括参数表列和返回值）都完全一样，也是不行的。
		Func<int, bool> function = IsEven;
		//Predicate<int> predicate = function; // 这样是不行的。因为委托类型是强类型的。
		Console.WriteLine(function(42));

		// 如果你能保证两个绑定的函数（方法）他们的签名是一样的，那么就可以执行如下的转换方式：
		Comparison<int> comparison = CompareInt32;
		int[] array = [3, 8, 1, 6, 5, 4, 7, 2, 9];
		//Sort(array, new Func<int, int, int>(comparison)); // 其中一种转换方式，不过这种不能使用委托推断行为。

		Sort(array, comparison.Invoke); // 直接使用方法组转换的逻辑，将 Invoke 调用作为方法组传入即可。
		Console.WriteLine($"[{string.Join(", ", array)}]");
	}


	private static bool IsEven(int value) => value % 2 == 0;


	private static int CompareInt32(int a, int b)
	{
		return a - b;
	}

	private static void Sort<T>(T[] array, Func<T, T, int> comparison)
	{
		for (var i = 0; i < array.Length - 1; i++)
		{
			for (var j = 0; j < array.Length - 1 - i; j++)
			{
				if (comparison(array[j], array[j + 1]) >= 0)
				{
					(array[j], array[j + 1]) = (array[j + 1], array[j]);
				}
			}
		}
	}
}