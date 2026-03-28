namespace KifuwarabeCSharp.Core.Usi.Elements;

/// <summary>
///     <pre>
/// マス番号。
/// 
///		盤面を [0, 80] の整数の index で表す
///		I9 = 1一, I1 = 1九, A1 = 9九
///
///		A9, B9, C9, D9, E9, F9, G9, H9, I9,
///		A8, B8, C8, D8, E8, F8, G8, H8, I8,
///		A7, B7, C7, D7, E7, F7, G7, H7, I7,
///		A6, B6, C6, D6, E6, F6, G6, H6, I6,
///		A5, B5, C5, D5, E5, F5, G5, H5, I5,
///		A4, B4, C4, D4, E4, F4, G4, H4, I4,
///		A3, B3, C3, D3, E3, F3, G3, H3, I3,
///		A2, B2, C2, D2, E2, F2, G2, H2, I2,
///		A1, B1, C1, D1, E1, F1, G1, H1, I1,
///
///		Bitboard のビットが縦に並んでいて、
///		0 ビット目から順に、以下の位置と対応させる。
///     </pre>
/// </summary>
internal enum MuzSquareType
{
    // Apery オリジナルの後に、盤面のマスを 0 から順に再定義していくぜ（＾～＾） 頑固一徹のきふわらべは将棋の符号の筋→段の順に合わせてあるぜ（＾～＾）
    // M は マスの意味だぜ（＾～＾）
    //
    //		-9 なら東へ、-1 なら北へ、+1 なら南へ、+9 なら西へ進むことになるぜ（＾～＾）
    //	  
    //		        (-9)
    //		        東
    //		        ↑
    //		(-1)北←　→南(+1)
    //		        ↓
    //		        西
    //		        (+9)
    //
    START = -1,
    // ←１段目（上端）                ９段目→
    M11, M12, M13, M14, M15, M16, M17, M18, M19,    // １筋目（左端）
    M21, M22, M23, M24, M25, M26, M27, M28, M29,
    M31, M32, M33, M34, M35, M36, M37, M38, M39,
    M41, M42, M43, M44, M45, M46, M47, M48, M49,
    M51, M52, M53, M54, M55, M56, M57, M58, M59,
    M61, M62, M63, M64, M65, M66, M67, M68, M69,
    M71, M72, M73, M74, M75, M76, M77, M78, M79,
    M81, M82, M83, M84, M85, M86, M87, M88, M89,
    M91, M92, M93, M94, M95, M96, M97, M98, M99,    // ９筋目

    SquareNum, // = 81

    SquareNoLeftNum = M61,  // 盤を２バイトで表現しているから（＾～＾）？ 盤面の左側のマスの数。これも 40 である必要はない。

    // 持ち駒も、マス番号を定義するぜ（＾～＾）
    B_hand_pawn = SquareNum + -1,
    B_hand_lance = B_hand_pawn + 18,
    B_hand_knight = B_hand_lance + 4,
    B_hand_silver = B_hand_knight + 4,
    B_hand_gold = B_hand_silver + 4,
    B_hand_bishop = B_hand_gold + 4,
    B_hand_rook = B_hand_bishop + 2,
    W_hand_pawn = B_hand_rook + 2,
    W_hand_lance = W_hand_pawn + 18,
    W_hand_knight = W_hand_lance + 4,
    W_hand_silver = W_hand_knight + 4,
    W_hand_gold = W_hand_silver + 4,
    W_hand_bishop = W_hand_gold + 4,
    W_hand_rook = W_hand_bishop + 2,
    SquareHandNum = W_hand_rook + 3
}
