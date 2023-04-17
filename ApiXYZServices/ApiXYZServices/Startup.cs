using ApiXYZServices.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace ApiXYZServices
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
            string[] allowedOrigins = Configuration["AppConfig:ALLOWEDORIGINS"].Split('|');

            services.AddCors(o => o.AddPolicy("CorsPolicy", builder =>
            {
                builder
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials()
                .WithOrigins(allowedOrigins);
            }));

            services.AddSwaggerGen(sw =>
            {
                sw.SwaggerDoc(name: "v2", new OpenApiInfo { Title = "XYZ Api Administrator", Version = "v2", Description = "Net Core Api For XYZ Comunications" });
            });

            services.AddSignalR();

            services.AddTransient<FamilyRepository>();
            services.AddTransient<UserRepository>();
            services.AddTransient<PostsRepository>();
            services.AddTransient<CommentsRepository>();

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors("CorsPolicy");
            //app.UseSignalR(routes =>
            //{
            //    routes.MapHub<BroadcastHub>("/notify");
            //});

            app.UseSwagger();
            app.UseSwaggerUI(sw =>
            {
                sw.SwaggerEndpoint(url: "/swagger/v2/swagger.json", name: "XYZ Api Administrator");
            });

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }


    }
}
