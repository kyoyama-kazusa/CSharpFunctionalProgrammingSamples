#undef WRONG_IMPL

namespace CSharpFunctionalProgrammingSamples.Lesson24;

/// <summary>
/// 主界面窗口。
/// </summary>
public sealed partial class MainWindow : Window
{
	/// <summary>
	/// 实例化构造器。
	/// </summary>
	public MainWindow() => InitializeComponent();


	/// <summary>
	/// 计算圆周率前指定的位数。
	/// </summary>
	/// <param name="digits">位数。</param>
	/// <returns>字符串。</returns>
	private static string CalculatePiDigits(int digits)
	{
		// 数组长度，用于存储中间计算
		var len = digits * 10 / 3 + 1;
		var a = new int[len + 1]; // 数组初始化（+1 防止索引越界）
		var result = new int[digits]; // 存储结果数字

		// 初始化数组，所有元素设为 2
		for (var i = 0; i < len; i++)
		{
			a[i] = 2;
		}

		var carry = 0; // 进位值

		// 计算每一位数字
		for (var i = 0; i < digits; i++)
		{
			// 从数组末尾开始迭代
			for (var j = len; j > 0; j--)
			{
				// 计算当前值
				var denom = 2 * j - 1;
				var x = a[j] * 10 + carry;
				a[j] = x % denom; // 更新数组值
				carry = x / denom; // 计算进位
				if (j > 1)
				{
					carry *= (j - 1);
				}
			}

			// 处理第一个元素
			var x_first = a[0] * 10 + carry;
			a[0] = x_first % 10; // 更新第一个元素
			carry = x_first / 10; // 计算最终进位

			// 确保进位在 0-9 范围内
			if (carry < 10)
			{
				result[i] = carry;
			}
			else
			{
				// 处理进位大于 9 的情况（理论上不会发生）
				result[i] = carry / 10;
				carry %= 10;
				a[0] = carry;
			}
		}

		// 转换为字符串输出
		var pi = new StringBuilder();
		pi.Append("3."); // 圆周率以 "3." 开头
		for (var i = 1; i < digits; i++) // 从第二位开始
		{
			pi.Append(result[i]);
		}
		return pi.ToString();
	}

	/// <summary>
	/// 保存结果到文件。
	/// </summary>
	/// <param name="result">结果。</param>
	/// <returns>文件。</returns>
	private static async Task SaveResultAsync(string result)
	{
		var saveFileDialog = new SaveFileDialog
		{
			Filter = "文本文件|*.txt|所有文件|*.*",
			DefaultExt = ".txt",
			FileName = "圆周率.txt",
			Title = "请选择保存位置",
			InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
			AddExtension = true,
			OverwritePrompt = true
		};
		if (saveFileDialog.ShowDialog() is true)
		{
			try
			{
				var filePath = saveFileDialog.FileName;
				await File.WriteAllTextAsync(filePath, result);
				MessageBox.Show($"文件已成功保存到：\n{filePath}", "保存成功", MessageBoxButton.OK, MessageBoxImage.Information);
			}
			catch (Exception ex)
			{
				MessageBox.Show($"保存文件时出错：\n{ex.Message}", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
			}
		}
		else
		{
			MessageBox.Show("操作已取消", "提示", MessageBoxButton.OK, MessageBoxImage.Information);
		}
	}


	private async void Button_ClickAsync(object sender, RoutedEventArgs e)
	{
#if WRONG_IMPL
		// 计算前面 10000 位数。
		// 这里计算圆周率的操作是置于 await 语句之前的，这个时候如果遇到他就会同步执行。
		// 运行效果就是主线程（UI 线程）仍然会被卡住，因为他这里启动之后先同步执行一阵子，然后才是异步保存文件什么的）。
		// 该卡住的行为也压根一点都没变。
		var result = CalculatePiDigits(10000);

		// 保存结果到文件。
		await SaveResultAsync(result);
#else
		// 套一层 Task.Run 让密集型操作在异步上下文里执行。
		var task = Task.Run(
			async () =>
			{
				// 计算前面 10000 位数。
				// 这里放在 async lambda 里才是正确的实现，因为他里面有 await 语句，我们也需要将密集型操作置于异步上下文里使用，
				// 所以属于是“既要又要”，async lambda 在这种情况下用才是合适的。
				var result = CalculatePiDigits(10000);

				// 保存结果到文件。
				await SaveResultAsync(result);
			}
		);
		await task;
#endif
	}
}
