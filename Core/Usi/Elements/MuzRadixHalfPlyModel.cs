namespace KifuwaraperyCS.Core.Usi.Elements;

using System;
using System.Collections.Generic;
using System.Text;

/// <summary>
///     <pre>
/// 0 から始まる手数を表すぜ（＾～＾）！
/// 黒番が指せば 1 増え、白番が指せばさらに 1 増える数だぜ（＾～＾）！
///     </pre>
/// </summary>
internal class MuzRadixHalfPlyModel
{


    // ========================================
    // 生成／破棄
    // ========================================


    public MuzRadixHalfPlyModel(int value)
    {
        this.Value = value;
    }


    // ========================================
    // 窓口プロパティ
    // ========================================


    public int Value { get; init; }
}
