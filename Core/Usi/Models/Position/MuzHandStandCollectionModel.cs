namespace KifuwarabeCSharp.Core.Usi.Models.Position;
/// <summary>
///     <pre>
/// 両駒台の持ち駒の枚数だぜ（＾～＾）！
/// 
/// メモリを節約するため、駒の種類ごとに枚数を数えるぜ（＾～＾）！
/// 
///		下位ビットから上位ビットへ向かって並ぶ。
///		21 19 17  14  11   8   5
///		 .. .. ... ... ... ... .....
///		  R  B   G   S   N   L     P
/// 
///		42 40 38  35  32  29  26
///		 .. .. ... ... ... ... .....
///		  r  b   g   s   n   l     p
///
/// 
///     </pre>
/// </summary>
internal class MuzHandStandCollectionModel
{


    // ========================================
    // 生成／破棄
    // ========================================


    public static MuzHandStandCollectionModel FromPieces(
        byte bPawn,
        byte bLance,
        byte bKnight,
        byte bSilver,
        byte bGold,
        byte bBishop,
        byte bRook,
        byte wPawn,
        byte wLance,
        byte wKnight,
        byte wSilver,
        byte wGold,
        byte wBishop,
        byte wRook)
    {
        return new MuzHandStandCollectionModel(
            bitField: ((ulong)bPawn) +
            ((ulong)bLance << 5) +
            ((ulong)bKnight << 8) +
            ((ulong)bSilver << 11) +
            ((ulong)bGold << 14) +
            ((ulong)bBishop << 17) +
            ((ulong)bRook << 19) +
            ((ulong)wPawn << 21) +
            ((ulong)wLance << 26) +
            ((ulong)wKnight << 29) +
            ((ulong)wSilver << 32) +
            ((ulong)wGold << 35) +
            ((ulong)wBishop << 38) +
            ((ulong)wRook << 40));
    }


    public MuzHandStandCollectionModel(ulong bitField)
    {
        this.BitField = bitField;
    }


    // ========================================
    // 窓口プロパティ
    // ========================================


    public ulong BitField { get; init; }

    public byte BPawn => (byte)(this.BitField & 0b11111);
    public byte BLance => (byte)((this.BitField >> 5) & 0b111);
    public byte BKnight => (byte)((this.BitField >> 8) & 0b111);
    public byte BSilver => (byte)((this.BitField >> 11) & 0b111);
    public byte BGold => (byte)((this.BitField >> 14) & 0b111);
    public byte BBishop => (byte)((this.BitField >> 17) & 0b11);
    public byte BRook => (byte)((this.BitField >> 19) & 0b11);
    public byte WPawn => (byte)((this.BitField >> 21) & 0b11111);
    public byte WLance => (byte)((this.BitField >> 26) & 0b111);
    public byte WKnight => (byte)((this.BitField >> 29) & 0b111);
    public byte WSilver => (byte)((this.BitField >> 32) & 0b111);
    public byte WGold => (byte)((this.BitField >> 35) & 0b111);
    public byte WBishop => (byte)((this.BitField >> 38) & 0b11);
    public byte WRook => (byte)((this.BitField >> 40) & 0b11);


    // ========================================
    // 窓口メソッド
    // ========================================


    public override string ToString()
    {
        return $"BP = {this.BPawn}, BL = {this.BLance}, BN = {this.BKnight}, BS = {this.BSilver}, BG = {this.BGold}, BB = {this.BBishop}, BR = {this.BRook}, WP = {this.WPawn}, WL = {this.WLance}, WN = {this.WKnight}, WS = {this.WSilver}, WG = {this.WGold}, WB = {this.WBishop}, WR = {this.WRook}";
    }
}
