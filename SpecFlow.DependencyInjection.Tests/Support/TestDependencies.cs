using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SolidToken.SpecFlow.DependencyInjection.Tests.Steps;

namespace SolidToken.SpecFlow.DependencyInjection.Tests.Support
{
    public static class Dependencies
    {
        [ScenarioDependencies]
        public static IServiceCollection CreateServices()
        {
            var services = new ServiceCollection();

            // Add test dependencies
            services.AddTransient<ITestService, TestService>();

            // ContextInjectionScope (by using AddScoped instead of AddTransient, the context will be scoped to the Feature across bindings)
            services.AddScoped<TestContext>();

            // Calculator
            services.AddScoped<ICalculator, Calculator>();
            
            // NB: This breaks when parallelizeTestCollections = true (xunit.runner.json)
            //      When debugging, the error does not occur, presumably because the tests are not running in parallel
            var configuration = new ConfigurationBuilder();
            configuration.AddSystemsManager(c =>
            {
                c.Path = "path";
                c.Optional = true;
            });
            configuration.Build();

            return services;
        }
    }
}
