namespace KifuwaraperyCS.Infrastructure;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Serilog;
using Serilog.Settings.Configuration;

internal static class MuzInfrastructure
{
    /// <summary>
    /// インフラストラクチャの初期化。
    /// </summary>
    public static IHost InitializeProgram(string[] args)
    {
        // ホストビルド前にログの初期化を行う（＾～＾）
        MuzLogging.InitializeBeforeHostBuild();

        // ホストビルド（＾～＾）
        var builder = Host.CreateApplicationBuilder(args);

        // ［設定ファイル］の設定（＾～＾）
        builder.Configuration
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)  // 必須ファイル
            .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
            .AddEnvironmentVariables();  // 環境変数で［環境の名前］を使って、設定のカスケード（上書き）を可能にするぜ（＾～＾）

        // ダウンロードしてきたロガーを、Microsoft の ILogger にブリッジ。
        MuzLogging.BridgeSerilogToILogger(builder);

        // ここで DI コンテナにサービスを登録（＾～＾）
        builder.Services.Configure<MuzAppSettings>(builder.Configuration);  // ［設定ファイル操作］を MuzAppSettings クラスにバインド

        // 自分のサービスを登録（例）
        //builder.Services.AddSingleton<IMyService, MyService>();
        //builder.Services.AddTransient<SomeOtherService>();

        var host = builder.Build();

        // ［設定ファイル］から［Serilog］の本設定。
        MuzLogging.SetupFromConfigurationFile(builder);

        // ［設定ファイル］のテスト出力
        var appSettings = host.Services.GetRequiredService<IOptions<MuzAppSettings>>().Value;
        Console.WriteLine($"AppName: {appSettings.AppName}");
        Console.WriteLine($"ShogiEngineName: {appSettings.ShogiEngineName}");

        return host;
    }
}
