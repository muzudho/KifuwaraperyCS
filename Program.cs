// See https://aka.ms/new-console-template for more information
using KifuwaraperyCS;
using KifuwaraperyCS.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

try
{
    Console.WriteLine("Hello, World!");

    var builder = Host.CreateApplicationBuilder(args);  // ビルダー作成（＾～＾）

    // ホストビルド前に用意すること一覧（＾～＾）
    MuzAppSettingsService.PrepareBeforeHostBuild(builder);   // ［設定ファイル］
    MuzLogging.PrepareBeforeHostBuild(builder); // ［ロギング］

    await MuzInfrastructure.ActivateLoggingBeforeHostBuildAsync(
        builder: builder,
        configurationMgr: builder.Configuration,
        onLoggingEnable: async (logger) =>
        {
            // ここから［ロギング］できる（＾～＾）！
            logger.LogInformation("ホストビルド前のログだぜ（＾～＾）！");
        });

    // ホストビルド（＾～＾）
    var host = builder.Build();

    await MuzInfrastructure.ActivateLoggingAfterHostBuildAsync(
        configurationMgr: builder.Configuration,
        host: host,
        onLoggingEnable: async () =>
        {
            // ここから［ロギング］できる（＾～＾）！

            // ［設定ファイル］のテスト出力
            var appSettings = host.Services.GetRequiredService<IOptions<MuzAppSettings>>().Value;
            Console.WriteLine($"AppName: {appSettings.AppName}");
            Console.WriteLine($"ShogiEngineName: {appSettings.ShogiEngineName}");

            // 起動ログ（ILogger が使える）
            var logger = host.Services.GetRequiredService<ILogger<Program>>();

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
        });
}
catch (Exception ex)
{
    Console.WriteLine($"アプリが死んだ... ログも取れない、むずでょ泣く。{ex}");
}
