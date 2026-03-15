namespace KifuwaraperyCS;

using KifuwaraperyCS.Infrastructure;
using KifuwaraperyCS.Src.Infrastructure.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;

/// <summary>
/// どんなコンソール・アプリを作るときでも、本題に入る前に似たようなコードを書くことになる……、そんな似たコード［ホストビルド］をまとめたヘルパークラスだぜ（＾～＾）！
/// </summary>
internal static class MuzInfrastructureHelper
{
    public static async Task BuildHostAsync(
        string[] commandLineArgs,
        Func<IHost, Task> onHostEnabled)
    {
        var builder = Host.CreateApplicationBuilder(commandLineArgs);  // ビルダー作成（＾～＾）
        await SetupBeforeBuildAsync(builder);    // ビルド前の処理（＾～＾）
        var host = builder.Build(); // ホストビルド（＾～＾）

        await MuzLogging.ActivateLoggingAfterHostBuildAsync(
            configurationMgr: builder.Configuration,
            host: host,
            onLoggingEnable: async () =>
            {
                // ここから［ロギング］できる（＾～＾）！

                await onHostEnabled(host);
            });
    }


    /// <summary>
    /// ホストビルドする前にやることがあればここでやるぜ（＾～＾）！例えば、［サービス］を追加したりとか、そういうのだぜ（＾～＾）！
    /// </summary>
    /// <param name="builder"></param>
    /// <returns></returns>
    private static async Task SetupBeforeBuildAsync(HostApplicationBuilder builder)
    {
        // お前のアプリケーションに合わせて、［サービス］を追加していってくれだぜ（＾～＾）！

        MuzAppSettingsService.SetupBeforeHostBuild(builder);   // ［設定ファイル］

        await MuzLogging.ActivateLoggingBeforeHostBuildAsync( // ［ロギング］
            builder: builder,
            configurationMgr: builder.Configuration,    // ［設定ファイル］設定後（＾～＾）
            onLoggingEnable: async (logger) =>
            {
                // ここから［ロギング］できる（＾～＾）！
                logger.LogInformation("ホストビルド前のログだぜ（＾～＾）！");
            });
    }
}
