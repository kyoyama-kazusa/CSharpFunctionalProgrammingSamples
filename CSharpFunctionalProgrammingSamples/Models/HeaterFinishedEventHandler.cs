namespace CSharpFunctionalProgrammingSamples.Models;

/// <summary>
/// 表示热水器完成烧水的过程，开始通知用户，在通知的时候需要处理的操作（你要在热水器烧开后干什么）。
/// </summary>
/// <param name="sender">表示热水器的实例（哪一个热水器烧开了）。</param>
/// <param name="e">热水器的额外参数信息（当烧开水了之后热水器的一些核心状态的信息）。</param>
internal delegate void HeaterFinishedEventHandler(Heater sender, HeaterFinishedEventArgs e);
