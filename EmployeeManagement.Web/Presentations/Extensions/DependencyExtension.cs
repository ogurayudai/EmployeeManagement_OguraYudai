using EmployeeManagement.Web.Applications.Repositories;
using EmployeeManagement.Web.Applications.Services;
using EmployeeManagement.Web.Infrastructures.Adapters;
using EmployeeManagement.Web.Infrastructures.Context;
using EmployeeManagement.Web.Infrastructures.Repositories;

namespace EmployeeManagement.Web.Presentations.Extensions;

/// <summary>
/// 依存定義および依存性注入クラス
/// </summary>
public static class DependencyExtension
{
    public static void SettingDependencyInjection(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        SettingInfrastructures(services);
        SettingApplications(services);
        SettingPresentations(services);
    }

    /// <summary>
    /// インフラストラクチャ層の依存定義
    /// </summary>
    private static void SettingInfrastructures(IServiceCollection services)
    {
        // Dapper用DB接続
        services.AddScoped<DapperContext>();

        // Adapter
        services.AddScoped<EmployeeEntityAdapter>();
        services.AddScoped<DepartmentEntityAdapter>();
        services.AddScoped<LoginEntityAdapter>();

        // Repository
        services.AddScoped<IEmployeeRepository, EmployeeRepository>();
        services.AddScoped<IDepartmentRepository, DepartmentRepository>();
        services.AddScoped<ILoginRepository, LoginRepository>();
    }

    /// <summary>
    /// アプリケーション層の依存定義
    /// </summary>
    private static void SettingApplications(IServiceCollection services)
    {
        // Service
        services.AddScoped<EmployeeService>();
        services.AddScoped<DepartmentService>();
        services.AddScoped<LoginService>();
    }

    /// <summary>
    /// プレゼンテーション層の依存定義
    /// </summary>
    private static void SettingPresentations(IServiceCollection services)
    {
        // 今回は必要になったらViewModelAdapterなどを追加する
    }
}