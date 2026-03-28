// See https://aka.ms/new-console-template for more information
using KifuwarabeCSharp;
using KifuwarabeCSharp.Core.Usi;
using KifuwarabeCSharp.Core.Usi.Models;
using KifuwarabeCSharp.Infrastructure.Configuration;
using KifuwarabeCSharp.Infrastructure.Logging;
using KifuwarabeCSharp.Models;
using KifuwarabeCSharp.Views;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

try
{
    //Console.WriteLine("Hello, World!");

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
            //Console.WriteLine($"AppName: {appSettings.AppName}");
            //Console.WriteLine($"ShogiEngineName: {appSettings.ShogiEngineName}");

            // ［ロガー］を動作確認してみようぜ（＾～＾）
            //var logger = host.Services.GetRequiredService<ILogger<Program>>();
            //logger.LogInformation("デフォルトのログを書き込むぜ～（＾～＾）！");

            // ［ロガー別のログ］を動作確認してみようぜ（＾～＾）
            var loggingSvc = host.Services.GetRequiredService<IMuzLoggingService>();
            //loggingSvc.Others.LogInformation("その他のログだぜ（＾～＾）");
            //loggingSvc.Verbose.LogInformation("大量のログだぜ（＾～＾）");

            // または IHostedService で長時間動かすアプリなら
            // await host.RunAsync();

            await MuzUsiLoop.RunAsync(
                appSettings,
                loggingSvc,
                onExternalCommand: async (pos, commandName, argsStr) =>
                {
                    switch (commandName)
                    {
                        // ----------------------------------------
                        // 以下、独自実装
                        // ----------------------------------------
                        // ----------------------------------------
                        // 局面の表示
                        // ----------------------------------------
                        case "pos":
                            var text = MuzPositionView.GetPositionViewString(new MuzCoreReadonly(pos));
                            MuzUsiLoop.SendOutput($"{text}\n", loggingSvc);
                            break;

                        // ----------------------------------------
                        // 無いよ
                        // ----------------------------------------
                        default:
                            MuzUsiLoop.SendOutput("そんなコマンド無い（＾～＾）\n", loggingSvc);
                            break;
                    }
                });

            //Console.WriteLine("アプリ終了！ Enter押してね");
            //Console.ReadLine();

            //Console.WriteLine("どやさ（＾～＾）！");
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
