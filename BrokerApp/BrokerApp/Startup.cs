using BrokerApp.Data.DatabaseContext;
using BrokerApp.Data.DatabaseModels;
using BrokerApp.Data.IRepository;
using BrokerApp.Data.Repository;
using BrokerApp.Service.IManager;
using BrokerApp.Service.Manager;
using BrokerApp.Shared;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BrokerApp
{
    public class Startup
    {
        public string DbPath = @"D:\BrokerApp";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            DbPath = System.IO.Path.Join(path, "Broker.db");
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddDbContext<BrokerDbContext>(options =>options.UseSqlite($"Data Source={DbPath}"));

            services.AddScoped<IEquityManager, EquityManager>();
            services.AddScoped<IFundManager, FundManager>();
            services.AddScoped<IEquityRepository, EquityRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUtility, Utility>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, BrokerDbContext brokerDbContext)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            brokerDbContext.Database.Migrate();
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

    }
}
