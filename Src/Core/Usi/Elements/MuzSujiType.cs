namespace KifuwaraperyCS.Src.Core.Usi.Elements;

/// <summary>
///     <pre>
///     0, 1, 2 ... と並んでいるとして、下図の I9 は 0、 I1 は 8、 A9 は 72、 A1 は 80 だぜ（＾～＾）！
/// 
///     ←１段目（上端）       ９段目→
///     I9, I8, I7, I6, I5, I4, I3, I2, I1,		// １筋目（左端）
///     H9, H8, H7, H6, H5, H4, H3, H2, H1,
///     G9, G8, G7, G6, G5, G4, G3, G2, G1,		// │
///     F9, F8, F7, F6, F5, F4, F3, F2, F1,		// │ 下に行くほど段（Y軸）は上に行くぜ（＾～＾）
///     E9, E8, E7, E6, E5, E4, E3, E2, E1,		// ↓
///     D9, D8, D7, D6, D5, D4, D3, D2, D1,
///     C9, C8, C7, C6, C5, C4, C3, C2, C1,
///     B9, B8, B7, B6, B5, B4, B3, B2, B1,
///     A9, A8, A7, A6, A5, A4, A3, A2, A1,		// ９筋目
///     </pre>
/// </summary>
internal enum MuzSujiType
{
    /// <summary>
    ///     <pre>
    /// 平手黒番から見て、１筋目（左端）。
    /// 
    ///     - Apery では `FileI`。
    ///     </pre>
    /// </summary>
    Suji1 = 0,

    Suji2, Suji3, Suji4, Suji5, Suji6, Suji7, Suji8,

    /// <summary>
    ///     <pre>
    /// 平手黒番から見て、９筋目（右端）。
    /// 
    ///     - Apery では `FileA`。
    ///     </pre>
    /// </summary>
    Suji9,

    /// <summary>
    /// 列挙型の終端
    /// </summary>
    SujiNum,
    None = SujiNum,

    /// <summary>
    /// 
    /// </summary>
    FileNoLeftNum = Suji6
}
