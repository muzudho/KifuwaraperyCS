namespace KifuwaraperyCS.Infrastructure.Logging;

using Microsoft.Extensions.Logging;

internal class MuzLoggingService : IMuzLoggingService
{


    // ========================================
    // 生成／破棄
    // ========================================


    /// <summary>
    /// 
    /// </summary>
    /// <param name="loggerFactory">DI から取る</param>
    public MuzLoggingService(ILoggerFactory loggerFactory)
    {
        // ［ロガー］を分ける動作確認してみようぜ（＾～＾） ［カテゴリー名］でロガー作成（ここがポイント！）
        this.Others = loggerFactory.CreateLogger("MuzOthersLogger");
        this.Verbose = loggerFactory.CreateLogger("MuzVerboseLogger");
        this.Operation = loggerFactory.CreateLogger("MuzOperationLogger");
        this.USIProtocol = loggerFactory.CreateLogger("MuzUSIProtocolLogger");
    }


    // ========================================
    // 窓口プロパティ
    // ========================================


    public ILogger Others { get; init; }

    public ILogger Verbose { get; init; }

    public ILogger Operation { get; init; }

    public ILogger USIProtocol { get; init; }
}
