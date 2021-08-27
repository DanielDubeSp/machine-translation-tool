using Application.Validators;
using Domain.Shared.Interfaces;
using Instrastructure.TranslationApis;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Services.MachineTranslationTool.API.Services;
using System;

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
            services.AddSingleton<IAllowedLanguagesValidator, AllowedLanguagesValidator>();
            services.AddTransient<ITranslator, ZaacGoogleTranslate>();
            services.AddTransient<ITranslateService, TranslateService>();

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("translate_services",
                    new OpenApiInfo
                    {
                        Title = "MachineTranslationTool",
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
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
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
