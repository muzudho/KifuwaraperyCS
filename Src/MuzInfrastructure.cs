namespace KifuwaraperyCS;

using KifuwaraperyCS.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

internal static class MuzInfrastructure
{
    /// <summary>
    /// ［ロギング］できるようにするぜ（＾～＾）！
    /// </summary>
    /// <returns></returns>
    public static async Task ActivateLoggingBeforeHostBuildAsync(
        HostApplicationBuilder builder,
        ConfigurationManager configurationMgr,
        Func<ILogger, Task> onLoggingEnable)
    {
        try
        {

            // ★ここで ILogger を builder から直接作れる★
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
}
