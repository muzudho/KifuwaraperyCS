// See https://aka.ms/new-console-template for more information
using KifuwaraperyCS.Src;
using KifuwaraperyCS.Src.Infrastructure.Configuration;
using KifuwaraperyCS.Src.Infrastructure.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

try
{
    Console.WriteLine("Hello, World!");

    // ホストビルドするぜ（＾～＾）！
    // ［ホスト］ってのは［汎用ホスト］のことで、いろいろ［サービス］っていう便利機能を付け加えることができるフレームワークみたいなもんだぜ（＾～＾）
    // それを［ビルド］するぜ（＾▽＾）
    await MuzInfrastructureHelper.BuildHostAsync(
        commandLineArgs: args,
        onHostEnabled: async (host) =>
        {
            // ここからビルドされた［汎用ホスト］（host）が使えるぜ（＾▽＾）！

            // ［アプリケーション設定ファイル］を動作確認してみようぜ（＾～＾）
            var appSettings = host.Services.GetRequiredService<IOptions<MuzAppSettings>>().Value;
            Console.WriteLine($"AppName: {appSettings.AppName}");
            Console.WriteLine($"ShogiEngineName: {appSettings.ShogiEngineName}");

            // ［ロガー］を動作確認してみようぜ（＾～＾）
            var logger = host.Services.GetRequiredService<ILogger<Program>>();
            logger.LogInformation("ログを書き込むぜ～（＾～＾）！");

            // ［ロガー別のログ］を動作確認してみようぜ（＾～＾）
            var loggingSvc = host.Services.GetRequiredService<IMuzLoggingService>();
            loggingSvc.Others.LogInformation("その他のログだぜ（＾～＾）");
            loggingSvc.Verbose.LogInformation("大量のログだぜ（＾～＾）");

            // または IHostedService で長時間動かすアプリなら
            // await host.RunAsync();

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
finally
{
    Console.WriteLine("アプリが終了するぜ（＾～＾）！");
    await MuzInfrastructureHelper.Cleanup();
}

// Program.cs を最後まで実行しても、必ずしもアプリケーションが終了するわけじゃないぜ（＾～＾）！
// ［汎用ホスト］が動いている限りは、アプリケーションは終了しないぜ（＾～＾）！
