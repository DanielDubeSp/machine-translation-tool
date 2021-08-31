using Application.Validators;
using Domain.Shared.Interfaces;
using Instrastructure.TranslationApis;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Serilog;
using Services.MachineTranslationTool.API.Services;
using System;
using System.IO;
using System.Reflection;

namespace Services.MachineTranslationTool
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddSingleton<Serilog.ILogger>(x =>
            {
                return new LoggerConfiguration().ReadFrom.Configuration(Configuration).CreateLogger();
            });
            services.AddSingleton<IAllowedLanguagesValidator, AllowedLanguagesValidator>();

            services.AddTransient<ITranslateService, TranslateService>();

            // Decide on runtime startup wich translator inject
            if (true)
                services.AddTransient<ITranslator, ZaacGoogleTranslate>();
            else
                services.AddTransient<ITranslator, GoogleTransNew>();


            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("translate_services",
                    new OpenApiInfo
                    {
                        Title = "Machine Translation Tool",
                        Description = "Translate operations",
                        Version = "v1",
                        TermsOfService = new Uri("https://example.com/terms"),
                        Contact = new OpenApiContact
                        {
                            Name = "Benito Flores",
                            Email = "benito.flores@bimbo.com",
                            Url = new Uri("https://twitter.com/bflores"),
                        },
                    });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/translate_services/swagger.json", "MachineTranslationTool v1"));
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
