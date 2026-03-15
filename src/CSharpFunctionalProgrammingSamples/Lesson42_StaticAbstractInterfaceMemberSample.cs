namespace CSharpFunctionalProgrammingSamples;

/// <summary>
/// C# 11 新的委托方法组的绑定类型。
/// </summary>
internal sealed class Lesson42_StaticAbstractInterfaceMemberSample : Sample
{
	/// <inheritdoc/>
	public override void RunSample()
	{
		MyInteger[] array = [3, 8, 1, 6, 5, 4, 7, 2, 9];
		Console.WriteLine($"[{string.Join(',', array)}]");

		sort(array);
		Console.WriteLine($"[{string.Join(',', array)}]");


		static void sort<T>(T[] array) where T : ISorter<T>
		{
			// 接口里的静态抽象方法的方法组声明：
			// <code>
			// <see langword="var"/> 委托变量 = <typeparamref name="T"/>.方法名;
			// </code>
			var comparer = T.CompareTwoValues;
			for (var i = 0; i < array.Length - 1; i++)
			{
				for (var j = 0; j < array.Length - 1 - i; j++)
				{
					if (comparer(array[j], array[j + 1]) >= 0)
					{
						(array[j], array[j + 1]) = (array[j + 1], array[j]);
					}
				}
			}
		}
	}
}

file interface ISorter<T>
{
	static abstract int CompareTwoValues(T left, T right);
}

file readonly struct MyInteger(int value) : ISorter<MyInteger>
{
	private readonly int _value = value;


	public override string ToString() => _value.ToString();

	public static int CompareTwoValues(MyInteger left, MyInteger right) => left._value.CompareTo(right._value);

	public static implicit operator MyInteger(int value) => new(value);
}
