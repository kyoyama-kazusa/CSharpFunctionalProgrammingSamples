namespace CSharpFunctionalProgrammingSamples;

/// <summary>
/// 实例方法组转换的用法。
/// </summary>
/// <remarks>
/// 实例方法组指的是给委托赋值之前绑定一个实例方法的调用操作，并不直接使用小括号调用它的写法，其实就是
/// <c>instance.Method(参数)</c> 的不传参写法 <c>instance.Method</c>。
/// </remarks>
internal sealed class Lesson09_InstanceMethodGroupConversionSample : Sample
{
	/// <inheritdoc/>
	public override void RunSample()
	{
		// 实例方法组。
		// 实例方法组就是绑定了一个实例，调用一个实例方法的过程。语法是 实例.方法名，和一般的实例方法调用没有区别，只是不使用小括号参与调用过程。

		string str = "Hello world";

		Func<string> converter = str.ToLower; // 实例方法组，绑定一个实例方法（标准用法）。
		string result = converter();
		Console.WriteLine(result);

		Func<string> converter2 = str.ToPascalCase; // 实例方法组，但是这里用的是扩展方法的情况。
		string result2 = converter2();
		Console.WriteLine(result2);
	}
}

/// <summary>
/// 提供一个字符串转换器机制的扩展方法。
/// </summary>
file static class LocalExtensions
{
	/// <summary>
	/// 将字符串里的空格后的字符转为大写字母，首字母也大写，即帕斯卡命名法。
	/// </summary>
	/// <param name="this">当前字符串。</param>
	/// <returns>帕斯卡命名法转换后的字符串。</returns>
	public static string ToPascalCase(this string @this)
	{
		var sb = new StringBuilder();
		for (var i = 0; i < @this.Length;)
		{
			if (i == 0)
			{
				sb.Append(char.ToUpper(@this[0]));
				i++;
				continue;
			}

			if (@this[i] == ' ')
			{
				if (i + 1 < @this.Length)
				{
					// i 没有越界（空格后有字符）。
					var nextChar = @this[i + 1];
					sb.Append(char.ToUpper(nextChar));
					i += 2;
				}
			}
			else
			{
				sb.Append(@this[i]);
				i++;
			}
		}
		return sb.ToString();
	}
}
