#pragma warning disable

namespace CSharpFunctionalProgrammingSamples;

/// <summary>
/// 闭包 bug（造成的副作用）的例子。
/// </summary>
internal sealed class Lesson16_ClosureBugSample : Sample
{
	/// <inheritdoc/>
	public override void RunSample()
	{
		// 一个例子：定义一个 Action 数组，将所有元素都赋值一个 lambda 表达式。
		// lambda 表达式会捕获一个循环变量。
		//Action[] actions = new Action[3];
		//for (int i = 0; i < 3; i++)
		//{
		//	actions[i] = () => Console.WriteLine(i);
		//}
		//foreach (var action in actions)
		//{
		//	action();
		//}

		// 完整的翻译。
		//Action[] actions = new Action[3];
		//var instance = new LambdaClosure();
		//for (instance.i = 0; instance.i < 3; instance.i++)
		//{
		//	actions[instance.i] = instance.action; // 绑定实例方法组。
		//}
		//foreach (var action in actions)
		//{
		//	action();
		//}


		// 要想解决这个捕获变量全是最后一个数值的结果的问题，我们应该这么做：
		Action[] actions = new Action[3];
		for (int i = 0; i < 3; i++)
		{
			int tempVariable = i; // 不是被三个 lambda 公用的临时变量。
			actions[i] = () => Console.WriteLine(tempVariable);
		}
		foreach (var action in actions)
		{
			action();
		}


		// 总结：
		// 只要多个 lambda 表达式（不同的 lambda 表达式），捕获了相同的变量和实例、且该变量和实例的修改行为很频繁，
		// 就会产生这个问题。
		// 解决方法就是，让不同的 lambda 表达式产生捕获机制的时候，将捕获变量抄写一份副本，
		// 以保证不同的 lambda 表达式捕获的不是同一个实例就行。
	}


	private sealed class LambdaClosure
	{
		public int i;


		public void action()
		{
			Console.WriteLine(i);
		}
	}
}
