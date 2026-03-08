namespace KifuwaraperyCS.Infrastructure;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

internal static class MuzAppSettingsService
{
    /// <summary>
    /// ［設定ファイル］の設定（＾～＾）
    /// </summary>
    public static void PrepareBeforeHostBuild(HostApplicationBuilder builder)
    {
        builder.Configuration
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)  // 必須ファイル
            .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
            .AddEnvironmentVariables();  // 環境変数で［環境の名前］を使って、設定のカスケード（上書き）を可能にするぜ（＾～＾）

        // ここで DI コンテナにサービスを登録（＾～＾）
        builder.Services.Configure<MuzAppSettings>(builder.Configuration);  // ［設定ファイル操作］を MuzAppSettings クラスにバインド
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
