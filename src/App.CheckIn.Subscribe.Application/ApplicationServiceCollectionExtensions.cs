using App.CheckIn.EntityFrameworkCore;
using AppCheckInSubscribe.Application;
using AppCheckInSubscribe.Application.Localization;
using Tnf.Configuration;
using Tnf.Localization;
using Tnf.Localization.Dictionaries;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ApplicationServiceCollectionExtensions
    {
        /// <summary>
        /// Adds application Services
        /// </summary>
        public static IServiceCollection AddApplication(this IServiceCollection services, DatabaseConfiguration databaseConfiguration)
        {
            services.AddEntityFrameworkCore(databaseConfiguration);

            services.AddScoped<ISubscriptionService, SubscriptionService>();
            services.AddHostedService<StartupMigrator>();

            return services;
        }

        /// <summary>
        /// Configures localization for the application
        /// </summary>
        public static ITnfConfiguration ConfigureApplicationLocalization(this ITnfConfiguration configuration)
        {
            configuration.Localization.Languages.Add(new LanguageInfo("pt-BR", "Português", isDefault: true));

            configuration.Localization.Sources.Add(
                new DictionaryBasedLocalizationSource(LocalizationSources.Application,
                    new JsonEmbeddedFileLocalizationDictionaryProvider(
                        typeof(ApplicationServiceCollectionExtensions).Assembly,
                        "App.CheckIn.Subscribe.Application.Localization.SourceFiles"
                    )
                )
            );

            return configuration;
        }
    }
}
