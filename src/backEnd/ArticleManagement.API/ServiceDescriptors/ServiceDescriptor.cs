using Core.Application.Interfaces;
using Core.Domain.Interfaces;
using Infrastructure.Data;
using Infrastructure.Data.Repos;
using Infrastructure.Service;
using Microsoft.EntityFrameworkCore;

namespace ArticleManagement.API.ServiceDescriptors;

public static class ServiceDescriptor
{
    public static IServiceCollection AddArticleManagementServices(this IServiceCollection services)
    {
        services.AddDbContext();

        services.AddRepositories();

        services.AddApplicationServices();

        return services;
    }

    public static IServiceCollection AddDbContext(this IServiceCollection services)
    {
        string articleManagementConnectionString = Environment.GetEnvironmentVariable("ARTICLE_MANAGEMENT_DB_CONNECTION")
            ?? throw new InvalidOperationException("Environment variable 'ARTICLE_MANAGEMENT_DB_CONNECTION' is not set.");

        services.AddDbContext<ArticleManagementDbContext>(options =>
        {
            options.UseNpgsql(articleManagementConnectionString);
        });

        return services;
    }

    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IStudentRepo, StudentRepo>();
        services.AddScoped<IExtendRepo, ExtendRepo>();

        return services;
    }

    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IStudentService, StudentService>();
        services.AddScoped<IExtendService, ExtendService>();
        services.AddScoped<IBlobStorageClient, AzureBlobStorageClient>();

        return services;
    }
}
