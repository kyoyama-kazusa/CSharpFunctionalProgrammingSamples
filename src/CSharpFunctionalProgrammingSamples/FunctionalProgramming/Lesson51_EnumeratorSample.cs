#pragma warning disable

namespace CSharpFunctionalProgrammingSamples.FunctionalProgramming;

/// <summary>
/// 自定义迭代器。
/// </summary>
internal sealed class Lesson51_EnumeratorSample : Sample
{
	/// <inheritdoc/>
	public override void RunSample()
	{
		// 自定义迭代器：
		// 允许一个实例可以参与 foreach 循环需要满足两个特征：
		// 1）这个实例带有 GetEnumerator() 无参的实例方法，返回一个迭代器对象；
		// 2）迭代器对象需要包含两个必需成员：
		//    1. Current 属性 - 表示每一轮迭代过程的时候的数值是多少；
		//    2. MoveNext 方法 - 表示每一轮迭代状态是如何迁移的（你可以类比于数组游标指向下一个元素这个操作的变动是怎么实现的）。
		foreach (var value in new Enumerator())
		{
			Console.WriteLine(value);
		}
	}
}

/// <summary>
/// 迭代器类型演示：返回 100 以内的所有偶数。
/// 迭代器自定义的时候，你要把他类比成一个数组 + 游标移动的过程，不然不好想 MoveNext 方法究竟如何实现。
/// </summary>
file ref struct Enumerator
{
	/// <summary>
	/// 游标。你可以理解成这里存在一个底层的数组，这个 index 指代了当前我指向哪一个元素要返回。
	/// </summary>
	private int _index;


	/// <summary>
	/// 无参构造器 - C# 9 开始允许我们自定义值类型的无参构造器。
	/// </summary>
	public Enumerator() => _index = -1;


	/// <summary>
	/// 当前迭代器指向的元素。
	/// </summary>
	public int Current { get; private set; }


	/// <summary>
	/// 表示是否还有下一个迭代器状态（游标可否移动到下一个元素上去）。如果可以就返回 <see langword="true"/>；否则返回 <see langword="false"/>。
	/// </summary>
	/// <returns>一个布尔值结果。</returns>
	public bool MoveNext()
	{
		// 比较契合编译器底层翻译的状态机的 MoveNext 写法：
		//TryAgain:
		//	_index++;
		//	if (_index < 100)
		//	{
		//		if (_index % 2 == 0)
		//		{
		//			Current = _index;
		//			return true;
		//		}
		//		goto TryAgain;
		//	}
		//	return false;

		// 比较适合我们自己写的写法：
		while (++_index < 100)
		{
			if (_index % 2 == 0)
			{
				Current = _index;
				return true;
			}
		}
		return false;
	}

	/// <summary>
	/// 一个必不可少的方法：保证这个实例本身可以使用 <see langword="foreach"/> 循环。
	/// </summary>
	/// <returns>迭代器对象。</returns>
	public readonly Enumerator GetEnumerator() => this;
}
