namespace KifuwaraperyCS.Src.Core.Usi;

using System;

internal static class MuzUsiLoop
{
    public static async Task StartLoopAsync()
    {
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
    }
}
