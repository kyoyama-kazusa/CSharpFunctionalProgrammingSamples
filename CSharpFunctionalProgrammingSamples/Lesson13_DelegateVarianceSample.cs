namespace CSharpFunctionalProgrammingSamples;

/// <summary>
/// 委托类型（方法组赋值给委托类型的实例时）的协变性和逆变性。
/// </summary>
internal sealed class Lesson13_DelegateVarianceSample : Sample
{
	/// <inheritdoc/>
	public override void RunSample()
	{
		// 参数逆变性（Contravariance）。
		ParameterHandler handler1 = StringParameterHandler; // 不变（期望 string -> 实际传入 string）
		ParameterHandler handler2 = ObjectParameterHandler; // 参数的逆变（期望 object -> 实际传入 string）
		handler1("hello");
		handler2("world");

		// 返回值协变性（Covariance）。
		ReturnHandler handler3 = StringReturnHandler; // 返回值的协变（实际返回 string -> 拿 object 返回值作接收）
		ReturnHandler handler4 = ObjectReturnHandler; // 不变（实际返回 object -> 拿 object 返回值接收）
		Console.WriteLine(handler3());
		Console.WriteLine(handler4());
	}


	private static void StringParameterHandler(string str)
	{
		Console.WriteLine(str);
	}

	private static void ObjectParameterHandler(object obj)
	{
		Console.WriteLine(obj);
	}

	private static string StringReturnHandler()
	{
		return "hello";
	}

	private static object ObjectReturnHandler()
	{
		return "world";
	}


	private delegate void ParameterHandler(string s);

	private delegate object ReturnHandler();
}
