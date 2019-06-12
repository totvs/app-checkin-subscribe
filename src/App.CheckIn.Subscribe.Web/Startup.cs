using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using App.CheckIn.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Converters;
using Swashbuckle.AspNetCore.Swagger;
using Tnf.Configuration;

namespace AppCheckInSubscribe.Web
{
    public class Startup
    {
        private readonly DatabaseConfiguration _databaseConfiguration;
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            _databaseConfiguration = new DatabaseConfiguration(configuration);
        }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services
                .AddMvc()
                .AddJsonOptions(jsonOptions =>
                {
                    jsonOptions.SerializerSettings.Converters.Add(new StringEnumConverter());
                })
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddHealthChecks()
                .AddDbContextCheck<AppCheckInDbContext>();

            services.AddTnfAspNetCore();
            services.AddTnfAspNetCoreSecurity(Configuration);
            services.AddApplication(_databaseConfiguration);

            services
                .AddResponseCompression()
                .AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("v1", new Info { Title = "Event Subscription API", Version = "v1" });
                    c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "App.CheckIn.Subscribe.Web.xml"));
                    c.DescribeAllEnumsAsStrings();

                    c.AddSecurityDefinition("Bearer", new ApiKeyScheme
                    {
                        In = "header",
                        Description = "Please enter JWT with Bearer into field",
                        Name = "Authorization",
                        Type = "apiKey"
                    });

                    c.AddSecurityRequirement(new Dictionary<string, IEnumerable<string>>
                    {
                        { "Bearer", Enumerable.Empty<string>() },
                    });
                });

            return services.BuildServiceProvider();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHealthChecks("/health_check");

            app.UseTnfAspNetCore(configuration =>
            {
                configuration.ConfigureApplicationLocalization();
                configuration.MultiTenancy(c => c.IsEnabled = true);
            });

            app.UseTnfAspNetCoreSecurity();

            app.UseMvc();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("../swagger/v1/swagger.json", "Event Subscription V1");
            });
        }
    }
}
