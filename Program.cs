// See https://aka.ms/new-console-template for more information
using KifuwaraperyCS;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;

try
{
    Console.WriteLine("Hello, World!");

    // プログラムの初期化（＾～＾）
    var host = MuzInfrastructure.InitializeProgram(args);

    // ここからメイン処理
    //var myService = host.Services.GetRequiredService<IMyService>();
    //myService.DoSomething();

    // または IHostedService で長時間動かすアプリなら
    // await host.RunAsync();

    // 起動ログ（ILogger が使える）
    var logger = host.Services.GetRequiredService<ILogger<Program>>();
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
    Log.Fatal(ex, "アプリが死んだ... むずでょ泣く");
}
finally
{
    Log.CloseAndFlush();
}
