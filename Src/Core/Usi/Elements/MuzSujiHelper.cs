namespace KifuwaraperyCS.Src.Core.Usi.Elements;

/// <summary>
/// 筋の操作。
/// </summary>
internal static class MuzSujiHelper
{


    /// <summary>
    /// 左右変換
    /// </summary>
    /// <param name="suji">筋</param>
    /// <returns></returns>
    public static MuzSujiType Inverse(MuzSujiType suji) => (MuzSujiType)(MuzSujiType.SujiNum - 1 - suji);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="suji">筋</param>
    /// <returns></returns>
    public static bool ContainsOf(MuzSujiType suji) => (0 <= suji) && (suji < MuzSujiType.SujiNum);


    /// <summary>
    /// 
    /// </summary>
    /// <param name="suji">筋</param>
    /// <returns></returns>
    public static char ToUSIChar(MuzSujiType suji) => (char)('1' + suji);


    /// <summary>
    /// 
    /// </summary>
    /// <param name="suji">筋</param>
    /// <returns></returns>
    public static char ToCSAChar(MuzSujiType suji) => (char)('1' + suji);


    /// <summary>
    /// 
    /// </summary>
    /// <param name="ch">文字</param>
    /// <returns></returns>
    public static MuzSujiType FromCSASuji(char ch) => (MuzSujiType)(ch - '1');


    /// <summary>
    /// 
    /// </summary>
    /// <param name="ch">文字</param>
    /// <returns></returns>
    public static MuzSujiType FromUSIChar(char ch) => (MuzSujiType)(ch - '1');
}
