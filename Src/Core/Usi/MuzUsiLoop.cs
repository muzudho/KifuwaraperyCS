namespace KifuwaraperyCS.Src.Core.Usi;

using KifuwaraperyCS.Src.Core.Usi.Elements;
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
            else if (commandName == "isready")
            {
                // エンジンが準備できたら、"readyok" を返すぜ（＾▽＾）
                SendOutput($"readyok\n", loggingSvc);
            }
            else if (commandName == "setoption")
            {
                // TODO: エンジンのオプションを設定するコマンド。これが来たら、オプションを変更する。
            }
            else if (commandName == "usinewgame")
            {
                // 新しいゲームの開始を知らせるコマンド。これが来たら、前のゲームの情報をクリアする。
            }
            // ----------------------------------------
            // 局面
            // ----------------------------------------
            //      - 例： `position sfen lnsgkgsnl/9/ppppppppp/9/9/9/PPPPPPPPP/1B5R1/LNSGKGSNL w - 1 moves 5a6b 7g7f 3a3b`
            else if (commandName == "position")
            {
            }
            else if (commandName == "go")
            {
                // TODO: 思考開始のコマンド。これが来たら、思考を開始する。
                //usiOperation.Go(gameStats, pos, ssCmd);

                SendOutput($"bestmove resign\n", loggingSvc);   // とりあえず投了を返すぜ（＾ｑ＾）
            }
            // ----------------------------------------
            // 以下、独自実装
            // ----------------------------------------
            // ----------------------------------------
            // 単体テスト　＞　手番
            // ----------------------------------------
            else if (commandName == "test-turn")
            {
                SendOutput($"黒を表示 = {new MuzColorModel(MuzColorType.Black).ToString()}\n", loggingSvc);
                SendOutput($"白を表示 = {new MuzColorModel(MuzColorType.White).ToString()}\n", loggingSvc);
                SendOutput($"エラー表示 = {new MuzColorModel(MuzColorType.None).ToString()}\n", loggingSvc);
            }
            // ----------------------------------------
            // 単体テスト　＞　手数
            // ----------------------------------------
            else if (commandName == "test-radix-half-ply")
            {
                SendOutput($"5を表示 = {new MuzRadixHalfPlyModel(5).Value}\n", loggingSvc);
            }
            // ----------------------------------------
            // 無いよ
            // ----------------------------------------
            else
            {
                SendOutput("そんなコマンド無い（＾～＾）\n", loggingSvc);
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
        //Console.Write("コマンドを入力: ");

        string input;

        while (true)
        {
            input = Console.ReadLine()?.Trim() ?? "";

            // 空文字列なら、ループを続けるぜ（＾～＾）！ そうすれば、ユーザーが何か入力するまで待ち続けることができるぜ（＾～＾）！
            if (!string.IsNullOrWhiteSpace(input)) break;
        }

        loggingSvc.USIProtocol.LogInformation($"{input}\n");
        loggingSvc.Operation.LogInformation($"[{input}]コマンドを入力しました。\n");

        return input;
    }


    /// <summary>
    /// USIメッセージの出力用（＾～＾）
    /// </summary>
    /// <param name="message">USIメッセージ</param>
    /// <param name="loggingSvc"></param>
    public static void SendOutput(string message, IMuzLoggingService loggingSvc)
    {
        Console.Write(message); // 改行はもう付いてるから、ここでは付けないぜ（＾～＾）！
        loggingSvc.USIProtocol.LogInformation(message);
    }
}
