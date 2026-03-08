// See https://aka.ms/new-console-template for more information
using KifuwaraperyCS;
using KifuwaraperyCS.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

try
{
    Console.WriteLine("Hello, World!");

    // ［設定］ファイルを使えるようにするぜ（＾～＾）
    await MuzInfrastructure.ActivateConfigurationAsync(
        args: args,
        onConfigurationEnable: async (builder, host) =>
        {
            // ここから［設定ファイル］を使える（＾～＾）！
            MuzLogging.SetupFromConfigurationFile(builder); // ［設定ファイル］から［Serilog］の本設定。

            // ［設定ファイル］のテスト出力
            var appSettings = host.Services.GetRequiredService<IOptions<MuzAppSettings>>().Value;
            Console.WriteLine($"AppName: {appSettings.AppName}");
            Console.WriteLine($"ShogiEngineName: {appSettings.ShogiEngineName}");

            // ［ロギング］を使えるようにするぜ（＾～＾）！
            await MuzInfrastructure.ActivateLoggingAsync(host);
        });

}
catch (Exception ex)
{
    Console.WriteLine($"アプリが死んだ... ログも取れない、むずでょ泣く。{ex}");
}
