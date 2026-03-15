namespace KifuwaraperyCS.Src.Core.Usi;

using KifuwaraperyCS.Src.Infrastructure.Configuration;
using KifuwaraperyCS.Src.Infrastructure.Logging;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;

internal static class MuzUsiLoop
{


    // ========================================
    // 窓口メソッド
    // ========================================


    public static async Task RunAsync(
        MuzAppSettings appSettings,
        IMuzLoggingService loggingSvc)
    {
        // TODO: アプリのメイン処理をここに書く（＾～＾）！ USIプロトコルの処理とか（＾～＾）！
        // 返り値は空文字列ではないぜ（＾～＾）
        var input = GetInput(loggingSvc);

        while (true)
        {
            // 最初のスペースで分割（2つに分ける）
            string[] parts = input.Split(' ', 2, StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length == 0) throw new UnreachableException("空っぽだぜ");

            // "Apple Banana Cherry" なら。
            string commandName = parts[0];                    // "Apple"
            string rest = parts.Length > 1 ? parts[1] : "";  // "Banana Cherry"

            loggingSvc.Others.LogDebug($"最初の部分   : {commandName}");
            loggingSvc.Others.LogDebug($"残りの部分   : {rest}");

            if (commandName == "quit") break;
            if (commandName == "usi")
            {
                // 将棋の思考エンジンの名前と開発者名を返すぜ（＾▽＾）
                SendOutput($"id name {appSettings.ShogiEngineName}\nid author {appSettings.ShogiEngineAuthor}\nusiok\n", loggingSvc);
            }

            // 返り値は空文字列ではないぜ（＾～＾）
            input = GetInput(loggingSvc);
        }
    }


    // ========================================
    // 内部メソッド
    // ========================================


    public static string GetInput(IMuzLoggingService loggingSvc)
    {
        Console.Write("コマンドを入力: ");

        string input;

        while (true)
        {
            input = Console.ReadLine()?.Trim() ?? "";

            // 空文字列なら、ループを続けるぜ（＾～＾）！ そうすれば、ユーザーが何か入力するまで待ち続けることができるぜ（＾～＾）！
            if (!string.IsNullOrWhiteSpace(input)) break;
        }

        loggingSvc.Operation.LogInformation($"[{input}]コマンドを入力しました。");

        return input;
    }


    /// <summary>
    /// USIメッセージの出力用（＾～＾）
    /// </summary>
    /// <param name="message">USIメッセージ</param>
    /// <param name="loggingSvc"></param>
    public static void SendOutput(string message, IMuzLoggingService loggingSvc)
    {
        Console.WriteLine(message);
        loggingSvc.USIProtocol.LogInformation(message);
    }
}
