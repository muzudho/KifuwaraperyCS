namespace KifuwaraperyCS.Src.Infrastructure.Logging;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;
using Serilog.Settings.Configuration;

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
    /// ［ホストビルド］の前に、諸々の設定をするぜ（＾～＾）
    /// </summary>
    /// <returns></returns>
    public static async Task SetupBeforeHostBuildAsync(
        HostApplicationBuilder builder,
        Func<Microsoft.Extensions.Logging.ILogger, Task> onBootstrapLoggingEnabled)
    {
        // Serilog のデフォルト状態を先にセットアップ（ホストビルド前に推奨）
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Information()
            .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
            .Enrich.FromLogContext()
            .WriteTo.Console()
            .WriteTo.File(
                "Logs/App-.log",
                rollingInterval: RollingInterval.Day,
                outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}")
            .CreateBootstrapLogger();  // ホストビルド中のログ用

        // ダウンロードしてきたロガーを、Microsoft の ILogger にブリッジ。これで ILogger<T> を使うと Serilog が使われるぜ（＾～＾）
        builder.Services.AddSerilog();
        // 細かく制御したい場合は、ILoggerFactory を直接設定することもできる（＾～＾）！
        //builder.Logging.ClearProviders();
        //builder.Logging.AddSerilog(new LoggerConfiguration()
        //    .MinimumLevel.Information()
        //    .WriteTo.Console()
        //    .WriteTo.File("Logs/App-.log", rollingInterval: RollingInterval.Day)
        //    .CreateLogger());


        // ★ここでマイクロソフトの ILogger を builder から直接作れる★
        var bootstrapLogger = builder.Logging.Services.BuildServiceProvider()
            .GetRequiredService<ILoggerFactory>()
            .CreateLogger("LoggerBeforeHostBuild");   // カテゴリ名は自由（Program とかでもOK）
        try
        {
            MuzLogging.SetupFromConfigurationFile(builder.Configuration); // ［設定ファイル］から［Serilog］の本設定。

            // ロギングはまだ DIサービスに登録できていないが、 bootstrapLogger だけは使えるようにしたぜ（＾～＾）
            await onBootstrapLoggingEnabled(bootstrapLogger);
        }
        catch (Exception ex)
        {
            bootstrapLogger.LogCritical(ex, "アプリが死んだ... むずでょ泣く");
        }
    }


    /// <summary>
    /// ［ホストビルド］の後に、諸々の設定をするぜ（＾～＾）
    /// </summary>
    /// <returns></returns>
    public static async Task SetupAfterHostBuildAsync(
        ConfigurationManager configurationMgr,
        IHost host,
        Func<Task> onLoggingEnabled)
    {
        //// ［設定ファイル］から［Serilog］の本設定。
        //MuzLogging.SetupFromConfigurationFile(configurationMgr);

        // ［ホストビルド］の後なので、ここから、以下のようにして、ロガー（ILogger）を使えるようになったぜ（＾▽＾）！
        //var logger = host.Services.GetRequiredService<ILogger<Program>>();
        await onLoggingEnabled();
    }


    /// <summary>
    /// アプリケーションのログをここで閉じていいのか（＾～＾）？
    /// </summary>
    public static void Cleanup()
    {
        Log.CloseAndFlush();
    }


    /// <summary>
    /// ［設定ファイル］からロガーの設定を行う。
    /// </summary>
    public static void SetupFromConfigurationFile(ConfigurationManager configurationMgr)
    {
        var options = new ConfigurationReaderOptions
        {
            SectionName = "CustomLogging:Serilog"  // ← ここでセクションを指定！
        };
        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(configurationMgr, options)  // ← これで Serilog セクション全部読み込む！
            .Enrich.FromLogContext()  // 任意: 便利な enricher
            .CreateLogger();
    }
}
