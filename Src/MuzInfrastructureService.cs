namespace KifuwaraperyCS;

using KifuwaraperyCS.Infrastructure;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;

internal static class MuzInfrastructureService
{
    public static async Task BuildHostAsync(
        string[] args,
        Func<IHost, Task> onHostEnable)
    {

        var builder = Host.CreateApplicationBuilder(args);  // ビルダー作成（＾～＾）

        // ホストビルド前に用意すること一覧（＾～＾）
        MuzAppSettingsService.PrepareBeforeHostBuild(builder);   // ［設定ファイル］
        await MuzLogging.ActivateLoggingBeforeHostBuildAsync( // ［ロギング］
            builder: builder,
            configurationMgr: builder.Configuration,    // ［設定ファイル］設定後（＾～＾）
            onLoggingEnable: async (logger) =>
            {
                // ここから［ロギング］できる（＾～＾）！
                logger.LogInformation("ホストビルド前のログだぜ（＾～＾）！");
            });

        // ホストビルド（＾～＾）
        var host = builder.Build();

        await MuzLogging.ActivateLoggingAfterHostBuildAsync(
            configurationMgr: builder.Configuration,
            host: host,
            onLoggingEnable: async () =>
            {
                // ここから［ロギング］できる（＾～＾）！

                await onHostEnable(host);
            });
    }
}
