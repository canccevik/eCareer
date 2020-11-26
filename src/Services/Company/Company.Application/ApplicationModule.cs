using AutoMapper;
using Career.Configuration;
using Career.EntityFramework;
using Career.IoC.IoCModule;
using Career.MediatR;
using Company.Application.Services;
using Company.Application.Services.Abstractions;
using Company.Domain.Repository;
using Company.Infrastructure.Repositories;
using Definition.HttpClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Company.Application
{
    public class ApplicationModule : Module
    {
        protected override void Load(IServiceCollection services)
        {
            IConfiguration configuration = ConfigurationHelper.GetConfiguration();
            var definitionApiEndPoint = configuration.GetSection("ApiEndpoints:DefinitionApi").Get<ApiEndpointOptions>();

            services.AddDefinitionApiHttpClient(definitionApiEndPoint);
            services.AddMediatRWithFluentValidation(typeof(ApplicationModule));

            services.AddUnitOfWork();

            services.AddScoped<ILocationService, LocationService>();
            services.AddScoped<ISectorService, SectorService>();
            services.AddScoped<ICompanyRepository, CompanyRepository>();
            services.AddScoped<ICompanyFollowerRepository, CompanyFollowerRepository>();

            services.AddAutoMapper(typeof(CompanyMappinProfile));
        }
    }
}