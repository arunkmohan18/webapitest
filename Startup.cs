using System.Net;
using AspNetCoreWebAPI.Data;
using Microsoft.AspNetCore.Diagnostics;
using AspNetCoreWebAPI.Helpers;

namespace AspNetCoreWebAPI
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
            //services.AddCors();
            services.AddControllers();
            services.AddScoped<IAuthRepository, AuthRepository>();
            services.AddScoped<IBookRepository, BookRepository>();
            // services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).
            // AddJwtBearer(options =>
            // {
            //     options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
            //     {
            //         ValidateIssuerSigningKey = true,
            //         IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration.GetSection("AppSettings:Token").Value)),
            //         ValidateIssuer = false,
            //         ValidateAudience = false
            //     };
            // });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseRouting();

            app.UseCors(x => x.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
            app.UseAuthentication();
            

        }
    }
}
