namespace KifuwaraperyCS.Src.Infrastructure.Configuration;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

internal static class MuzAppSettingsService
{
    /// <summary>
    /// ［設定ファイル］の設定（＾～＾）
    /// </summary>
    public static void SetupBeforeHostBuild(HostApplicationBuilder builder)
    {
        // ［設定ファイル］の設定（＾～＾）
        builder.Configuration
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)  // 必須ファイル。ファイル編集後にはリロードするぜ（＾～＾）！
            .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)   // 環境ごとのファイル。ファイル編集後にはリロードするぜ（＾～＾）！　必須じゃないぜ（＾～＾）！
            .AddEnvironmentVariables();  // 環境変数で［環境の名前］を使って、設定のカスケード（上書き）を可能にするぜ（＾～＾）

        // ここで DI コンテナにサービスを登録（＾～＾）
        builder.Services.Configure<MuzAppSettings>(builder.Configuration);  // さっきの［設定ファイルの設定］を MuzAppSettings クラスにバインド（＾～＾）
    }
}


/// <summary>
///     <pre>
/// appsettings.json ファイルのオブジェクト。
///     </pre>
/// </summary>
internal class MuzAppSettings
{
    /// <summary>
    /// アプリケーション名。
    /// </summary>
    public string AppName { get; set; } = string.Empty;

    /// <summary>
    /// 将棋の思考エンジン名。
    /// </summary>
    public string ShogiEngineName { get; set; } = string.Empty;
}
