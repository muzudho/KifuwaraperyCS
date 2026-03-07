namespace KifuwaraperyCS.infrastructure;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;

internal static class MuzInfrastructure
{
    /// <summary>
    /// インフラストラクチャの初期化
    /// </summary>
    public static void InitializeProgram(string[] args)
    {
        // Serilog を先にセットアップ（ホストビルド前に推奨）
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Information()
            .WriteTo.Console()
            .WriteTo.File("logs/app-.log", rollingInterval: RollingInterval.Day)
            .CreateBootstrapLogger();  // ホストビルド中のログ用

        try
        {
            var builder = Host.CreateApplicationBuilder(args);

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

            // ここからメイン処理
            //var myService = host.Services.GetRequiredService<IMyService>();
            //myService.DoSomething();

            // または IHostedService で長時間動かすアプリなら
            // await host.RunAsync();

            Log.Information("アプリが正常に起動しました！");

            Console.WriteLine("アプリ終了！ Enter押してね");
            Console.ReadLine();
        }
        catch (Exception ex)
        {
            Log.Fatal(ex, "アプリが死んだ... むずでょ泣く");
        }
        finally
        {
            Log.CloseAndFlush();
        }
    }
}
