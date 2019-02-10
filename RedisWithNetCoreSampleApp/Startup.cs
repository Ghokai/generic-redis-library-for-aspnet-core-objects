using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RedisWithNetCoreSampleApp.Repository;
using RedisWithNetCoreSampleApp.Models;
using RedisManager;
using RedisWithNetCoreSampleApp.Middleware;

namespace RedisWithNetCoreSampleApp
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
            services.AddMvc();

            string redisConnectionStr= Configuration.GetConnectionString("RedisConnStr");

            services.AddScoped<IRedisConnector, RedisConnector>(_ => new RedisConnector(redisConnectionStr));
            services.AddScoped<IUserCrudRepository, UserCacheRepo>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCustomErrorHandler();

            app.UseMvc();
        }
    }
}
