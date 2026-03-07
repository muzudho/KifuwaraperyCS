namespace KifuwaraperyCS.src.infrastructure;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;

internal static class MuzInfrastructure
{
    /// <summary>
    /// インフラストラクチャの初期化
    /// </summary>
    public static IHost InitializeProgram(string[] args)
    {
        // Serilog を先にセットアップ（ホストビルド前に推奨）
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

        // 自分のサービスを登録（例）
        //builder.Services.AddSingleton<IMyService, MyService>();
        //builder.Services.AddTransient<SomeOtherService>();

        var host = builder.Build();

        // ［設定ファイル］のテスト出力
        var config = host.Services.GetRequiredService<IConfiguration>();
        Console.WriteLine($"AppName: {config["AppName"]}");
        Console.WriteLine($"ShogiEngineName: {config["ShogiEngineName"]}");
        Console.WriteLine($"LogLevel: {config["LogLevel"]}");

        return host;
    }
}
