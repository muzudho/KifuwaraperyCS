namespace KifuwarabeCSharp.Core.Usi;

using KifuwarabeCSharp.Core.Usi.Elements;
using KifuwarabeCSharp.Core.Usi.Models;
using KifuwarabeCSharp.Core.Usi.Models.Position;
using KifuwarabeCSharp.Infrastructure.Configuration;
using KifuwarabeCSharp.Infrastructure.Logging;
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
        IMuzLoggingService loggingSvc,
        Func<IMuzPositionReadonly, string, string, Task> onExternalCommand)
    {
        var pos = new MuzPositionModel();

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
            // 局面の表示
            // ----------------------------------------
            else if (commandName == "pos")
            {
                await onExternalCommand(pos, commandName, rest);
            }
            // ----------------------------------------
            // 盤上の先後付き駒種類
            // ----------------------------------------
            else if (commandName == "test-piece-type")
            {

            }
            // ----------------------------------------
            // 単体テスト　＞　マス位置
            // ----------------------------------------
            else if (commandName == "test-square-delta")
            {

            }
            else if (commandName == "test-square")
            {

            }
            else if (commandName == "test-rank")
            {

            }
            else if (commandName == "test-suji")
            {
#if !DEBUG
                SendOutput("このテストはデバッグビルドでのみ実行されます（＾～＾）\n", loggingSvc);
#endif
                // 問題なければ何も表示されません。
                DebugAssert(title: "左右反転", expected: MuzSujiType.Suji9, actual: MuzSujiHelper.Inverse(MuzSujiType.Suji1), loggingSvc);
                DebugAssert(title: "左右反転", expected: MuzSujiType.Suji5, actual: MuzSujiHelper.Inverse(MuzSujiType.Suji5), loggingSvc);
            }
            // ----------------------------------------
            // 単体テスト　＞　両駒台の持ち駒の枚数
            // ----------------------------------------
            else if (commandName == "test-hand-stand-collection")
            {
                SendOutput($"持ち駒の表示テスト = {MuzHandStandCollectionModel.FromPieces(
                    bPawn: 17, bLance: 2, bKnight: 3, bSilver: 4, bGold: 1, bBishop: 2, bRook: 1,
                    wPawn: 16, wLance: 1, wKnight: 2, wSilver: 3, wGold: 4, wBishop: 1, wRook: 2).ToString()}\n", loggingSvc);
            }
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


    [Conditional("DEBUG")]
    public static void DebugAssert<T>(string title, T expected, T actual, IMuzLoggingService loggingSvc)
    {
        if (!object.Equals(expected, actual))
        {
            var msg = $"Fail　{title}　期待値: {expected}, 実際の値: {actual}\n";
            SendOutput(msg, loggingSvc);
            //Debug.Assert(false, msg);
        }
    }
}
