namespace KifuwaraperyCS.Src.Core.Usi.Elements;

using System.Diagnostics;

internal static class MuzDanHelper
{
    /// <summary>
    ///     <pre>
    /// 盤に含まれるか。
    ///     </pre>
    /// </summary>
    /// <param name="dan">段</param>
    /// <returns></returns>
    public static bool ContainsOf(MuzDanType dan) => (0 <= dan) && (dan < MuzDanType.DanNum);

    /// <summary>
    /// 上下変換
    /// </summary>
    /// <param name="r"></param>
    /// <returns></returns>
    public static MuzDanType Inverse(MuzDanType dan) => (MuzDanType)(MuzDanType.DanNum - 1 - dan);

    /// <summary>
    /// todo: アルファベットが辞書順に並んでいない処理系があるなら対応すること。
    /// rank は 0～8という想定☆
    /// </summary>
    /// <param name="r"></param>
    /// <returns></returns>
    public static char ToUSIChar(MuzDanType dan)
    {
        // 明示的に範囲チェック
        Debug.Assert(
            'a' + 1 == 'b'
            && 'b' + 1 == 'c'
            && 'c' + 1 == 'd'
            && 'd' + 1 == 'e'
            && 'e' + 1 == 'f'
            && 'f' + 1 == 'g'
            && 'g' + 1 == 'h'
            && 'h' + 1 == 'i'
            , "このコードはASCIIのa〜iが連続している前提で書かれています");

        return (char)('a' + (int)dan);  // Dan1 == 0 == 'a', Dan2 == 'b', ..., Dan9 == 'i'
    }

    /*





/// <summary>
/// 
/// </summary>
/// <param name="r"></param>
/// <returns></returns>
static inline char TO_CHAR_CSA10(const Rank r) { return '1' + r; }


/// <summary>
/// 
/// </summary>
/// <param name="c"></param>
/// <returns></returns>
static inline Rank FROM_CHAR_CSA10(const char c) { return static_cast<Rank>(c - '1'); }

/// <summary>
/// 
/// </summary>
/// <param name="c"></param>
/// <returns></returns>
static inline Rank FROM_CHAR_USI10(const char c) { return static_cast<Rank>(c - 'a'); }
    */
}
