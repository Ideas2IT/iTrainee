using iTrainee.Data;
using iTrainee.Data.DataManager;
using iTrainee.Data.Interfaces;
using iTrainee.Services;
using iTrainee.Services.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using iTrainee.Services.Implementations;

namespace iTrainee.WebAPI
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
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "iTrainee.WebAPI", Version = "v1" });
            });

            services.AddSingleton<IDataManager>(x => new DataManager(this.Configuration.GetConnectionString("TraineeDB")));
            services.AddSingleton<IStreamRepository, StreamRepository>();
            services.AddSingleton<IBatchRepository, BatchRepository>();
            services.AddSingleton<ITopicsRepository, TopicsRepository>();
            services.AddSingleton<IUserRepository, UserRepository>();
            services.AddSingleton<IBatchUserRepository, BatchUserRepository>();
            services.AddSingleton<IBatchStreamRepository, BatchStreamRepository>();

            services.AddSingleton<ISubTopicsRepository, SubTopicsRepository>();

            services.AddSingleton<IStreamService, StreamService>();
            services.AddSingleton<IBatchService, BatchService>();
            services.AddSingleton<ITopicsService, TopicsService>();
            services.AddSingleton<IUserService, UserService>();
            services.AddSingleton<IBatchUserService, BatchUserService>();
            services.AddSingleton<IBatchStreamService, BatchStreamService>();
            services.AddSingleton<ISubTopicsService, SubTopicsService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "iTrainee.APIs v1"));
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
