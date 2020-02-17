using System;
using System.Reflection;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using BeComfy.Common.Authentication;
using BeComfy.Common.EFCore;
using BeComfy.Common.Jaeger;
using BeComfy.Common.Mongo;
using BeComfy.Services.Identity.Domain;
using BeComfy.Services.Identity.EF;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BeComfy.Services.Identity
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public IContainer Container { get; private set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddJwt();
            services.AddControllers()
                .AddNewtonsoftJson();
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", cors => 
                        cors.AllowAnyOrigin()
                            .AllowAnyMethod()
                            .AllowAnyHeader());
            });
            services.AddJaeger();
            services.AddOpenTracing();
            services.AddMongo();
            services.AddMongoRepository<User>("Flights");
            services.AddEFCoreContext<IdentityContext>();

            var builder = new ContainerBuilder();
            builder.RegisterAssemblyTypes(Assembly.GetEntryAssembly())
                .AsImplementedInterfaces();
            builder.Populate(services);
            builder.RegisterType<PasswordHasher<User>>().As<IPasswordHasher<User>>();

            Container = builder.Build();

            return new AutofacServiceProvider(Container);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseRouting();
            app.UseCors("CorsPolicy");
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
