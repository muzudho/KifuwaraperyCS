namespace KifuwaraperyCS.Infrastructure;

using System;
using System.Collections.Generic;
using System.Text;

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

    /// <summary>
    /// ログ・レベル。
    /// 
    ///     - 例: "Information", "Debug", "Error" など。
    /// </summary>
    public string LogLevel { get; set; } = "Information";
}
