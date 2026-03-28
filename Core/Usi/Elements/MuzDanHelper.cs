namespace KifuwaraperyCS.Core.Usi.Elements;

using System.Diagnostics;

internal static class MuzDanHelper
{
    static MuzDanHelper()
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

        // 明示的に範囲チェック
        Debug.Assert(
            '0' + 1 == '1'
            && '1' + 1 == '2'
            && '2' + 1 == '3'
            && '3' + 1 == '4'
            && '4' + 1 == '5'
            && '5' + 1 == '6'
            && '6' + 1 == '7'
            && '7' + 1 == '8'
            && '8' + 1 == '9'
            , "このコードはASCIIの0〜9が連続している前提で書かれています");
    }

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
    ///     <pre>
    /// TODO: アルファベットが辞書順に並んでいない処理系があるなら対応すること。
    /// rank は 0～8という想定☆
    /// Dan1 == 0 == 'a', Dan2 == 'b', ..., Dan9 == 'i'
    ///     </pre>
    /// </summary>
    /// <param name="dan">段</param>
    /// <returns></returns>
    public static char ToUSIChar(MuzDanType dan) => (char)('a' + (int)dan);


    /// <summary>
    /// 
    /// </summary>
    /// <param name="dan">段</param>
    /// <returns></returns>
    public static char ToCSAChar(MuzDanType dan) => (char)('1' + (int)dan);


    /// <summary>
    /// 
    /// </summary>
    /// <param name="dan">段</param>
    /// <returns></returns>
    public static MuzDanType FromCSAChar(MuzDanType dan) => (MuzDanType)((int)dan - '1');


    /// <summary>
    /// 
    /// </summary>
    /// <param name="ch">文字</param>
    /// <returns></returns>
    public static MuzDanType FromUSIChar(char ch) => (MuzDanType)(ch - 'a');
}
