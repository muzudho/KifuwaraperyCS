namespace KifuwaraperyCS.Infrastructure;

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
    /// ［ロギング］できるようにするぜ（＾～＾）！
    /// </summary>
    /// <returns></returns>
    public static async Task ActivateLoggingBeforeHostBuildAsync(
        HostApplicationBuilder builder,
        ConfigurationManager configurationMgr,
        Func<Microsoft.Extensions.Logging.ILogger, Task> onLoggingEnable)
    {
        try
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
            var loggerBeforeHostBuild = builder.Logging.Services.BuildServiceProvider()
                .GetRequiredService<ILoggerFactory>()
                .CreateLogger("LoggerBeforeHostBuild");   // カテゴリ名は自由（Program とかでもOK）
            try
            {
                MuzLogging.SetupFromConfigurationFile(configurationMgr); // ［設定ファイル］から［Serilog］の本設定。

                // ここから［ロギング］できる（＾～＾）
                await onLoggingEnable(loggerBeforeHostBuild);
            }
            catch (Exception ex)
            {
                loggerBeforeHostBuild.LogCritical(ex, "アプリが死んだ... むずでょ泣く");
            }
        }
        finally
        {
            MuzLogging.Cleanup(); // ロガーのクリーンアップ（＾～＾）
        }
    }


    /// <summary>
    /// ［ロギング］できるようにするぜ（＾～＾）！
    /// </summary>
    /// <returns></returns>
    public static async Task ActivateLoggingAfterHostBuildAsync(
        ConfigurationManager configurationMgr,
        IHost host,
        Func<Task> onLoggingEnable)
    {
        try
        {

            // 起動ログ（ILogger が使える）
            var logger = host.Services.GetRequiredService<ILogger<Program>>();

            try
            {
                MuzLogging.SetupFromConfigurationFile(configurationMgr); // ［設定ファイル］から［Serilog］の本設定。

                // ここから［ロギング］できる（＾～＾）
                await onLoggingEnable();
            }
            catch (Exception ex)
            {
                logger.LogCritical(ex, "アプリが死んだ... むずでょ泣く");
            }
        }
        finally
        {
            MuzLogging.Cleanup(); // ロガーのクリーンアップ（＾～＾）
        }
    }


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
