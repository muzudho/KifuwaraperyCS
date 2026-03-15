namespace KifuwaraperyCS;

using KifuwaraperyCS.Src.Infrastructure.Configuration;
using KifuwaraperyCS.Src.Infrastructure.Logging;
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

        await MuzLogging.SetupAfterHostBuildAsync(
            configurationMgr: builder.Configuration,
            host: host,
            onLoggingServiceEnabled: async () =>
            {
                // ここから、以下のようにして、ロガー（ILogger）を使えるようになったぜ（＾▽＾）！
                //var logger = host.Services.GetRequiredService<ILogger<Program>>();

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

        MuzAppSettingsHelper.SetupBeforeHostBuild(builder);   // ［アプリケーション設定ファイル］を読み書きできるようにするための準備をするぜ（＾～＾）！

        await MuzLogging.SetupBeforeHostBuildAsync( // ［ロギング］するための準備をするぜ（＾～＾）！
            builder: builder,
            onBootstrapLoggingEnabled: async (bootstrapLogger) =>
            {
                // ここから `bootstrapLogger` を使った［ロギング］できる（＾～＾）！
                bootstrapLogger.LogInformation("ホストビルド前だが、ブートストラップ・ログは出せるぜ（＾～＾）！");
            });
    }


    /// <summary>
    /// アプリケーション終了時に片付けるぜ（＾▽＾）
    /// </summary>
    /// <returns></returns>
    public static async Task Cleanup()
    {
        MuzLogging.Cleanup(); // ロガーのクリーンアップ（＾～＾）
    }
}
