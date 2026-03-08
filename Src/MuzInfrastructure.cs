namespace KifuwaraperyCS;

using KifuwaraperyCS.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

internal static class MuzInfrastructure
{
    /// <summary>
    /// 設定ファイルを使えるようにするぜ（＾～＾）
    /// </summary>
    public static async Task ActivateConfigurationAsync(
        string[] args,
        HostApplicationBuilder builder,
        Func<HostApplicationBuilder, IHost, Task> onConfigurationEnable)
    {
        MuzLogging.InitializeBeforeHostBuild(); // ホストビルド前にログの初期化を行う（＾～＾）

        MuzAppSettingsOperations.Setup(builder);    // ［設定ファイル］の設定（＾～＾）
        MuzLogging.BridgeSerilogToILogger(builder); // （ビルド前に行う）ダウンロードしてきたロガーを、Microsoft の ILogger にブリッジ。

        // ここで DI コンテナにサービスを登録（＾～＾）
        builder.Services.Configure<MuzAppSettings>(builder.Configuration);  // ［設定ファイル操作］を MuzAppSettings クラスにバインド

        // 自分のサービスを登録（例）
        //builder.Services.AddSingleton<IMyService, MyService>();
        //builder.Services.AddTransient<SomeOtherService>();

        var host = builder.Build();

        // ここから［設定ファイル］を使える（＾～＾）
        await onConfigurationEnable(builder, host);
    }


    /// <summary>
    /// ［ロギング］できるようにするぜ（＾～＾）！
    /// </summary>
    /// <returns></returns>
    public static async Task ActivateLoggingAsync(
        HostApplicationBuilder builder,
        IHost host,
        Func<Task> onLoggingEnable)
    {
        try
        {

            // 起動ログ（ILogger が使える）
            var logger = host.Services.GetRequiredService<ILogger<Program>>();

            try
            {
                MuzLogging.SetupFromConfigurationFile(builder); // ［設定ファイル］から［Serilog］の本設定。

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
