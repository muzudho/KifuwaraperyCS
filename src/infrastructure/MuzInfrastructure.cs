namespace KifuwaraperyCS.Infrastructure;

using KifuwaraperyCS.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Serilog;
using Serilog.Settings.Configuration;

internal static class MuzInfrastructure
{
    /// <summary>
    /// インフラストラクチャの初期化
    /// </summary>
    public static IHost InitializeProgram(string[] args)
    {
        // Serilog のデフォルト状態を先にセットアップ（ホストビルド前に推奨）
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Information()
            .WriteTo.Console()
            .WriteTo.File("logs/app-.log", rollingInterval: RollingInterval.Day)
            .CreateBootstrapLogger();  // ホストビルド中のログ用

        var builder = Host.CreateApplicationBuilder(args);

        // ［設定ファイル］の設定（＾～＾）
        builder.Configuration
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)  // 必須ファイル
            .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
            .AddEnvironmentVariables();  // 環境変数で［環境の名前］を使って、設定のカスケード（上書き）を可能にするぜ（＾～＾）

        // Serilog を ILogger にブリッジ（これで ILogger<T> が使える）
        builder.Logging.ClearProviders();
        builder.Logging.AddSerilog(new LoggerConfiguration()
            .MinimumLevel.Information()
            .WriteTo.Console()
            .WriteTo.File("logs/app-.log", rollingInterval: RollingInterval.Day)
            .CreateLogger());

        // ここで DI コンテナにサービスを登録（＾～＾）
        builder.Services.Configure<MuzAppSettings>(builder.Configuration);  // 設定を MuzAppSettings クラスにバインド

        // 自分のサービスを登録（例）
        //builder.Services.AddSingleton<IMyService, MyService>();
        //builder.Services.AddTransient<SomeOtherService>();

        var host = builder.Build();

        // ［設定ファイル］から［Serilog］の本設定。
        var options = new ConfigurationReaderOptions
        {
            SectionName = "CustomLogging:Serilog"  // ← ここでセクションを指定！
        };
        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(builder.Configuration, options)  // ← これで Serilog セクション全部読み込む！
            .Enrich.FromLogContext()  // 任意: 便利な enricher
            .CreateLogger();

        // ［設定ファイル］のテスト出力
        var appSettings = host.Services.GetRequiredService<IOptions<MuzAppSettings>>().Value;
        Console.WriteLine($"AppName: {appSettings.AppName}");
        Console.WriteLine($"ShogiEngineName: {appSettings.ShogiEngineName}");

        return host;
    }
}
