// See https://aka.ms/new-console-template for more information
using KifuwaraperyCS;
using KifuwaraperyCS.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

try
{
    Console.WriteLine("Hello, World!");

    // ホストビルドするぜ（＾～＾）！
    // ［ホスト］ってのは、いろいろ［サービス］っていう便利機能が付け加えられた、お前のアプリケーションみたいなもんだぜ（＾～＾）
    await MuzInfrastructureService.BuildHostAsync(
        args: args,
        onHostEnable: async (host) =>
        {
            // ここからビルドされた［ホスト］が使えるぜ（＾～＾）！

            // ［設定ファイル］の動作確認してみようぜ（＾～＾）
            var appSettings = host.Services.GetRequiredService<IOptions<MuzAppSettings>>().Value;
            Console.WriteLine($"AppName: {appSettings.AppName}");
            Console.WriteLine($"ShogiEngineName: {appSettings.ShogiEngineName}");

            // ［ロガー］の動作確認してみようぜ（＾～＾）
            var logger = host.Services.GetRequiredService<ILogger<Program>>();
            logger.LogInformation("ログを書き込むぜ～（＾～＾）！");

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
