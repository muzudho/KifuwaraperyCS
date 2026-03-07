// See https://aka.ms/new-console-template for more information
using KifuwaraperyCS.Src.Infrastructure;
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

    Log.Information("アプリが正常に起動しました！");

    Console.WriteLine("アプリ終了！ Enter押してね");
    Console.ReadLine();

    Log.Information("ログ、出てるかだぜ（＾～＾）？");
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
