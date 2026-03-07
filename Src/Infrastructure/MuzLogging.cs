namespace KifuwaraperyCS.Infrastructure;

using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;

/// <summary>
///     <pre>
/// ロギング。
/// 
///     - ロギングは自分でプログラムを書かず、Serilog に任せるぜ（＾～＾）！
///     </pre>
/// </summary>
internal static class MuzLogging
{
    /// <summary>
    ///     <pre>
    /// ロガーの初期化。
    /// 
    ///     - ホストビルド前に呼び出すことを推奨（＾～＾）！
    ///     </pre>
    /// </summary>
    public static void InitializeBeforeHostBuild()
    {
        // Serilog のデフォルト状態を先にセットアップ（ホストビルド前に推奨）
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Information()
            .WriteTo.Console()
            .WriteTo.File("logs/app-.log", rollingInterval: RollingInterval.Day)
            .CreateBootstrapLogger();  // ホストビルド中のログ用
    }


    /// <summary>
    ///     <pre>
    /// ダウンロードしてきたロガーを、Microsoft の ILogger にブリッジ。
    /// 
    ///     - これで ILogger<T> が使える。
    ///     </pre>
    /// </summary>
    /// <param name="builder"></param>
    public static void BridgeSerilogToILogger(HostApplicationBuilder builder)
    {
        builder.Logging.ClearProviders();
        builder.Logging.AddSerilog(new LoggerConfiguration()
            .MinimumLevel.Information()
            .WriteTo.Console()
            .WriteTo.File("logs/app-.log", rollingInterval: RollingInterval.Day)
            .CreateLogger());
    }
}
