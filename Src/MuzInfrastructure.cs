namespace KifuwaraperyCS;

using KifuwaraperyCS.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

internal static class MuzInfrastructure
{
    /// <summary>
    /// インフラストラクチャの初期化。
    /// </summary>
    public static async Task PrepareLoggingAsync(
        string[] args,
        Func<HostApplicationBuilder, IHost, Task> onConfigurationEnable)
    {
        MuzLogging.InitializeBeforeHostBuild(); // ホストビルド前にログの初期化を行う（＾～＾）

        var builder = Host.CreateApplicationBuilder(args);  // ホストビルド（＾～＾）

        MuzAppSettingsOperations.Setup(builder);    // ［設定ファイル］の設定（＾～＾）
        MuzLogging.BridgeSerilogToILogger(builder); // ダウンロードしてきたロガーを、Microsoft の ILogger にブリッジ。

        // ここで DI コンテナにサービスを登録（＾～＾）
        builder.Services.Configure<MuzAppSettings>(builder.Configuration);  // ［設定ファイル操作］を MuzAppSettings クラスにバインド

        // 自分のサービスを登録（例）
        //builder.Services.AddSingleton<IMyService, MyService>();
        //builder.Services.AddTransient<SomeOtherService>();

        var host = builder.Build();

        // ここから［設定ファイル］を使える（＾～＾）
        await onConfigurationEnable(builder, host);
    }
}
