namespace KifuwaraperyCS.Core.Usi.Elements;

/// <summary>
/// 黒番と白番だぜ（＾▽＾）！
/// </summary>
internal class MuzColorModel
{


    // ========================================
    // 生成／破棄
    // ========================================


    public MuzColorModel(MuzColorType value)
    {
        this.Value = value;
    }


    // ========================================
    // 窓口プロパティ
    // ========================================


    public MuzColorType Value { get; init; }


    // ========================================
    // 窓口メソッド
    // ========================================


    public override string ToString()
    {
        switch (this.Value)
        {
            case MuzColorType.Black:
                return "b";
            case MuzColorType.White:
                return "w";
            default:
                return "."; // 構文エラーにさせる文字を返すぜ（＾～＾）！
        }
    }
}
