namespace KifuwaraperyCS.Infrastructure.Configuration;

/// <summary>
///     <pre>
/// appsettings.json ファイルのオブジェクト。
///     </pre>
/// </summary>
internal class MuzAppSettings
{
    /// <summary>
    /// アプリケーション名。
    /// </summary>
    public string AppName { get; set; } = string.Empty;

    /// <summary>
    /// 将棋の思考エンジン名。
    /// </summary>
    public string ShogiEngineName { get; set; } = string.Empty;

    /// <summary>
    /// 将棋エンジンの開発者名。
    /// </summary>
    public string ShogiEngineAuthor { get; set; } = string.Empty;
}
