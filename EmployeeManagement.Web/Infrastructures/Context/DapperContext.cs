using Npgsql;
using System.Data;

namespace EmployeeManagement.Web.Infrastructures.Context;

/// <summary>
/// DapperでDB接続を作成するクラス
/// </summary>
public class DapperContext
{
    /// <summary>
    /// DB接続文字列
    /// </summary>
    private readonly string _connectionString;

    /// <summary>
    /// コンストラクタ
    /// </summary>
    public DapperContext(IConfiguration configuration)
    {
        _connectionString =
            configuration.GetConnectionString("PostgreSqlConnection")
            ?? throw new InvalidOperationException(
                "appsettings.json に PostgreSqlConnection が設定されていません。");
    }

    /// <summary>
    /// DB接続を作成する
    /// </summary>
    public IDbConnection CreateConnection()
    {
        return new NpgsqlConnection(_connectionString);
    }
}