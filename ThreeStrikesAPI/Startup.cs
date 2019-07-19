using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ThreeStrikesAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace ThreeStrikesAPI
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
            if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Production")
            {
                services.AddDbContext<ThreeStrikesItemContext>(options => options.UseSqlServer(Configuration.GetConnectionString("azure db")));
            }
            else
            {
                services.AddDbContext<ThreeStrikesItemContext>(opt => opt.UseInMemoryDatabase("ThreeStrikesList"));
                services.AddCors(options => options.AddPolicy("LocalHostPolicy", builder =>
                {
                    builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
                }));
            }


            // Automatically perform database migration
            //services.BuildServiceProvider().GetService<MyDatabaseContext>().Database.Migrate();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development")
            {
                app.UseCors("LocalHostPolicy");
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
