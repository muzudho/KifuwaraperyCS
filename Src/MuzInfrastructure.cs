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
    public static IHost InitializeProgram(string[] args)
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

        MuzLogging.SetupFromConfigurationFile(builder); // ［設定ファイル］から［Serilog］の本設定。

        // ［設定ファイル］のテスト出力
        var appSettings = host.Services.GetRequiredService<IOptions<MuzAppSettings>>().Value;
        Console.WriteLine($"AppName: {appSettings.AppName}");
        Console.WriteLine($"ShogiEngineName: {appSettings.ShogiEngineName}");

        return host;
    }
}
