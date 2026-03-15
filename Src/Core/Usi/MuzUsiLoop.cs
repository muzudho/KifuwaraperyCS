namespace KifuwaraperyCS.Src.Core.Usi;

using KifuwaraperyCS.Src.Infrastructure.Logging;
using Microsoft.Extensions.Logging;
using System;

internal static class MuzUsiLoop
{


    // ========================================
    // 窓口メソッド
    // ========================================


    public static async Task RunAsync(IMuzLoggingService loggingSvc)
    {
        // TODO: アプリのメイン処理をここに書く（＾～＾）！ USIプロトコルの処理とか（＾～＾）！
        var input = GetInput(loggingSvc);

        if (string.IsNullOrWhiteSpace(input))
        {
            Console.WriteLine("何も入力されてないぜ（＾～＾）");
            return;
        }

        while (true)
        {
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

            input = GetInput(loggingSvc);

            if (string.IsNullOrWhiteSpace(input))
            {
                Console.WriteLine("何も入力されてないぜ（＾～＾）");
                return;
            }
        }
    }


    // ========================================
    // 内部メソッド
    // ========================================

    public static string GetInput(IMuzLoggingService loggingSvc)
    {
        Console.Write("コマンドを入力: ");
        string input = Console.ReadLine()?.Trim() ?? "";
        loggingSvc.Operation.LogInformation($"[{input}]コマンドを入力しました。");

        return input;
    }
}
