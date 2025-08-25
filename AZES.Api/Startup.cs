global using BRIT.Services.UserService;
using System;
using System.IO;
using System.Reflection;
using System.Text;
using AZES.Api.Filters;
using BRIT.Dal.EfStructures;
using BRIT.Dal.Initialization;
using BRIT.Dal.Repos;
using BRIT.Dal.Repos.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
//using BRIT.Services.Logging;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;

namespace AZES.Api
{
    public class Startup
    {
        private readonly IWebHostEnvironment _env;

        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            _env = env;
            Configuration = configuration;
        }
        
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            
            services.AddControllers(
                   config => config.Filters.Add(new CustomExceptionFilter(_env))
                )
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.PropertyNamingPolicy = null;
                    options.JsonSerializerOptions.WriteIndented = true;
                })
                .ConfigureApiBehaviorOptions(options =>
                {
                    //options.SuppressModelStateInvalidFilter = true;
                    //options.SuppressInferBindingSourcesForParameters = true;
                    //options.SuppressConsumesConstraintForFormFileParameters = true;
                    //options.SuppressMapClientErrors = true;
                    //options.ClientErrorMapping[StatusCodes.Status404NotFound].Link = "https://httpstatuses.com/404";
                });

            services.AddEndpointsApiExplorer();

            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", builder =>
                {
                    builder
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowAnyOrigin();
                });
            });
            var connectionString = Configuration.GetConnectionString("AzesMMSQLConnectionString");
            //var connectionString = Configuration.GetConnectionString("AzesPostgreeConnectionString"); 
            services.AddDbContextPool<ApplicationDbContext>(
                options => options.UseSqlServer(connectionString,
                //options => options.UseNpgsql(connectionString,
                    sqlOptions => sqlOptions.EnableRetryOnFailure().CommandTimeout(60)));

            //services.AddScoped(typeof(IAppLogging<>), typeof(AppLogging<>));
            
            services.AddScoped<IUserService, UserService>();

            services.AddScoped<IAngestellteRepo, AngestellteRepo>();
            services.AddScoped<IArbeitsandauerRepo, ArbeitsandauerRepo>();
            services.AddScoped<IArbeitsortRepo, ArbeitsortRepo>();
            services.AddScoped<IArbeitszeiterfassungRepo, ArbeitszeiterfassungRepo>();
            services.AddScoped<IFundortRepo, FundortRepo>();
            services.AddScoped<IHausanschriftRepo, HausanschriftRepo>();
            services.AddScoped<IKennwortRepo, KennwortRepo>();
            services.AddScoped<IRolleRepo, RolleRepo>();
            services.AddScoped<IStadtRepo, StadtRepo>();

            services.AddHttpContextAccessor();
            
            services.AddSwaggerGen(c =>
            {
                c.EnableAnnotations();
                c.SwaggerDoc("v1",
                    new OpenApiInfo
                    {
                        Title = "Arbeitszeiterfassungssystem Service",
                        Version = "v1",
                        Description = "Support-Service für Arbeitszeiterfassungssystem",
                        License = new OpenApiLicense
                        {
                            Name = "Inc",//"Skimedic Inc",
                            Url = new Uri("https://www.mydocuboxbr-it.de/Vadim/") /*"http://www.skimedic.com,,,,,,"*/
                        }
                    });

                //var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                //var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                //c.IncludeXmlComments(xmlPath);
                c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Description = "Standard Authorization header using the Bearer scheme(\"bearer {token}\")",
                    In = ParameterLocation.Header,
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });

                c.OperationFilter<SecurityRequirementsOperationFilter>();

            });
            services.AddAutoMapper(typeof(Program).Assembly);
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options => {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
                        .GetBytes(Configuration.GetSection("AppSettings:Token").Value)),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };

                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ApplicationDbContext context)
        {
            if (env.IsDevelopment())
            {
                //If in development environment, display debug info
                app.UseDeveloperExceptionPage();
                //Original code
                //app.UseSwagger();
                //app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "AZES.Api v1"));
                
                //Initialize the database
                if (Configuration.GetValue<bool>("RebuildDataBase"))
                {
                    SampleDataInitializer.ClearAndReseedDatabase(context);
                }
                
            }

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();
            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "Arbeitszeiterfassungssystem Service v1"); });

            //redirect http traffic to https
            app.UseHttpsRedirection();
            //opt-in to routing
            app.UseRouting();

            //Add CORS Policy
            app.UseCors("AllowAll");

            //enable authentication checks: verifies the useres identity. 
            app.UseAuthentication();
            
            //enable authorization checks: verifies the useres rights(die Rechte, Prava pol'zovaniya metodami: uroven' dostupa: Admin, client, etc.). 
            app.UseAuthorization();
            //opt-in to using endpoint routing
            //use attribute routing on controllers
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }


    }
}
