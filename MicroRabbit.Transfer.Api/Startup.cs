using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using MicroRabbit.Banking.Domain.Events;
using MicroRabbit.Domain.Core.Bus;
using MicroRabbit.Transfer.Data.Context;
using MicroRabbit.Transfer.Domain.EventHandlers;
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
using Rabbit.Infra.IoC;
using TransferCreatedEvent = MicroRabbit.Transfer.Domain.Events.TransferCreatedEvent;

namespace MicroRabbit.Transfer.Api
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
            services.AddDbContext<TransferDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("TransferDbConnection"));
            });
            services.AddControllers();
            RegisterServices(services);
            
            services.AddSwaggerGen(
                c=> {
                    c.SwaggerDoc("v1", new OpenApiInfo
                    {
                        Version = "v1",

                        Title = "SMS Application API",
                        Description = "This is the QPay users API documentations.",
                        TermsOfService = new Uri("https://example.com/terms"),
                        Contact = new OpenApiContact
                        {
                            Name = "Biswanath Ghosh Tapos",
                            Email = "tapos.aa@gmail.com",
                            Url = new Uri("https://www.linkedin.com/in/taposg")
                        },
                        License = new OpenApiLicense
                        {
                            Name = "Use under LICX",
                            Url = new Uri("https://example.com/license")
                        }
                    });
                }
            );
            services.AddMediatR(typeof(Startup));
        }
        private void RegisterServices(IServiceCollection services)
        {
           DependencyContainer.RegisterServices(services);
        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();
            app.UseSwagger();
            app.UseSwaggerUI(
                c =>
                {
                    c.SwaggerEndpoint(  "/swagger/v1/swagger.json","Transfer MicroService V1");
                }
            );
            ConfigureEventBus(app);
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }

        private static void ConfigureEventBus(IApplicationBuilder app)
        {
            var eventBus = app.ApplicationServices.GetRequiredService<IEventBus>();
            eventBus.Subscribe<TransferCreatedEvent,TransferEventHandler>();

        }
    }
}