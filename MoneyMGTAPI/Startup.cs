using DataLayer;
using DataLayer.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Services.Interfaces;
using Services.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoneyMGTAPI
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
            services.AddControllers();

            #region Repositories
            services.AddTransient<IBankRepository, BankRepository>();
            services.AddTransient<IAccountRepository, AccountRepository>();
            services.AddTransient<IPayeeRepository, PayeeRepository>();
            services.AddTransient<IBankTransactionRepository, BankTransactionRepository>();
            services.AddTransient<ICreditcardRepository, CreditcardRepository>();
            services.AddTransient<ISourceRepository, SourceRepository>();            
            services.AddTransient<IEntityMonitorRepository, EntityMonitorRepository>();
            #endregion

            #region MoneyMGTContext
            services.AddDbContext<MoneyMGTContext>(options =>
                    options.UseSqlServer(
                      Configuration.GetConnectionString("MoneyMGTConnection"),
                      b => b.MigrationsAssembly(typeof(MoneyMGTContext).Assembly.FullName)));
            #endregion

            #region cors
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
            });
            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors("CorsPolicy");

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
