using ApiProject.Lesson.Models;
using ApiProject.Lesson.Persistence.Configuration;
using ApiProject.Lesson.Utils;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiProject.Lesson
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public IApplicationBuilder _app;
        public IWebHostEnvironment _env;
        public string _connStrn;

        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            _env = env;  
            
            var builder = new ConfigurationBuilder()
                .SetBasePath(_env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{_env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

       

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) // DI Container
        {

            OptionsConfigurationServiceCollectionExtensions
              .Configure<AppSettings>(services, Configuration.GetSection("MySettings"));

             _connStrn = Configuration["ConnectionString"];

            if (_env.IsDevelopment())
            {
                // Use SQL Lite
                services.AddDbContext<DatabaseCxt>(options =>
                // options.UseSqlite(Configuration.GetConnectionString("ConnectionString")));
                options.UseSqlServer(_connStrn));
            }
            else if (_env.IsStaging())
            {
                // for staging 
            }
            else if (_env.IsProduction())
            {
                services.AddDbContext<DatabaseCxt>(
                opts =>
                opts.UseSqlServer(_connStrn)

              );
            }
            else
            {
                /// TODO
            }
            services.AddControllers();


            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ApiProject.Lesson", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, DatabaseCxt context)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
               
            }
            app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ApiProject.Lesson v1"));
            

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            SeedData.SeedDatabase(context);
        }
       
    }
}
