namespace KifuwaraperyCS.Src.Core.Usi.Elements;

/// <summary>
/// 筋の操作。
/// </summary>
internal static class MuzSujiHelper
{


    /// <summary>
    /// 左右変換
    /// </summary>
    /// <param name="file"></param>
    /// <returns></returns>
    public static MuzSujiType Inverse(MuzSujiType file) => (MuzSujiType)(MuzSujiType.SujiNum - 1 - file);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="file"></param>
    /// <returns></returns>
    public static bool ContainsOf(MuzSujiType file) => (0 <= file) && (file < MuzSujiType.SujiNum);

    /*
    /// <summary>
    /// 
    /// </summary>
    /// <param name="f"></param>
    /// <returns></returns>
    static inline char TO_CHAR_USI(const File f) { return '1' + f; }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="f"></param>
    /// <returns></returns>
    static inline char TO_CHAR_CSA(const File f) { return '1' + f; }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="c"></param>
    /// <returns></returns>
    static inline File FROM_CHAR_CSA(const char c) { return static_cast<File>(c - '1'); }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="c"></param>
    /// <returns></returns>
    static inline File FROM_CHAR_USI(const char c) { return static_cast<File>(c - '1'); }
    */
}
