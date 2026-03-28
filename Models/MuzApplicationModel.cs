namespace KifuwarabeCSharp.Models;

using KifuwarabeCSharp.Core.Usi.Models;

/// <summary>
/// アプリケーションの全部のモデルだぜ（＾～＾）！
/// </summary>
internal class MuzApplicationModel
{


    // ========================================
    // 生成／破棄
    // ========================================


    internal MuzApplicationModel(
        MuzPositionModel position)
    {
        this.Position = position;
    }


    // ========================================
    // 窓口プロパティ
    // ========================================


    internal MuzPositionModel Position { get; init; } = default!;
}
