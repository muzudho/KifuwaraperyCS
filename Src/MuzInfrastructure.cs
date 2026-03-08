namespace KifuwaraperyCS;

using KifuwaraperyCS.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

internal static class MuzInfrastructure
{
    /// <summary>
    /// 設定ファイルを使えるようにするぜ（＾～＾）
    /// </summary>
    public static async Task ActivateConfigurationAsync(
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


    /// <summary>
    /// ［ロギング］できるようにするぜ（＾～＾）！
    /// </summary>
    /// <returns></returns>
    public static async Task ActivateLoggingAsync(
        IHost host)
    {
        try
        {
            // 起動ログ（ILogger が使える）
            var logger = host.Services.GetRequiredService<ILogger<Program>>();

            // ここから［ロギング］できる（＾～＾）
            try
            {
                // ここからメイン処理
                //var myService = host.Services.GetRequiredService<IMyService>();
                //myService.DoSomething();

                // または IHostedService で長時間動かすアプリなら
                // await host.RunAsync();

                logger.LogInformation("ログを書き込むぜ～（＾～＾）！");

                // TODO: アプリのメイン処理をここに書く（＾～＾）！ USIプロトコルの処理とか（＾～＾）！
                Console.Write("コマンドを入力: ");
                string input = Console.ReadLine()?.Trim() ?? "";

                if (string.IsNullOrWhiteSpace(input))
                {
                    Console.WriteLine("何も入力されてないぜ（＾～＾）");
                    return;
                }

                // 最初のスペースで分割（2つに分ける）
                string[] parts = input.Split(' ', 2, StringSplitOptions.RemoveEmptyEntries);

                if (parts.Length == 0)
                {
                    Console.WriteLine("空っぽだぜ");
                    return;
                }

                string first = parts[0];                    // "Apple"
                string rest = parts.Length > 1 ? parts[1] : "";  // "Banana Cherry"

                Console.WriteLine($"最初の部分   : {first}");
                Console.WriteLine($"残りの部分   : {rest}");

                Console.WriteLine("アプリ終了！ Enter押してね");
                Console.ReadLine();

                Console.WriteLine("どやさ（＾～＾）！");
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
