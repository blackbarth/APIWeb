using APIWEB.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using APIWEB.Filters;
using APIWEB.Extensions;
using Microsoft.Extensions.Logging;
using APIWEB.Logging;
using APIWEB.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;

namespace APIWEB
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

            //Habilitar cors com configuraçao no configure()
            //configuraçao #01
            services.AddCors();

            //habilitar cors com configuracao aqui com os atributos
            //configuraçao #02
            //services.AddCors(options =>
            //{
            //    options.AddPolicy("PermitirApiRequest",
            //        builder => builder.WithOrigins("http://www.apirequest.io")
            //        .WithMethods("GET"));
            //});



            //configuraçao da versionamento
            //services.AddApiVersioning(options =>
            //{
            //    options.AssumeDefaultVersionWhenUnspecified = true; // assume a versao padrao quando nenhuma versao for informada
            //    options.DefaultApiVersion = new ApiVersion(1,0); //define a versao padrao é 1.0
            //    options.ReportApiVersions = true; //permite informar no response do request a informaçao de compactibilidade da versao

            //});

            services.AddApiVersioning(options => 
            {
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.ReportApiVersions = true;
                options.ApiVersionReader = new HeaderApiVersionReader("x-api-version"); //exibir no header da api

            });

            services.AddScoped<ApiLoggingFilter>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddDbContext<APPDBContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));


            ///Implementar Identity
            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<APPDBContext>()
                .AddDefaultTokenProviders();


            //JWT
            //adiciona o manipulador de autenticacao e define o 
            //esquema de autenticacao usado : Bearer
            //valida o emissor, a audiencia e a chave
            //usando a chave secreta valida a assinatura
            services.AddAuthentication(
                JwtBearerDefaults.AuthenticationScheme).
                AddJwtBearer(options => options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidAudience = Configuration["TokenConfiguration:Audience"],
                    ValidIssuer = Configuration["TokenConfiguration:Issuer"],
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(Configuration["Jwt:key"]))
                });

            //Microsoft.AspNetCore.Mvc.NewtonsoftJson
            services.AddControllers()
                .AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            loggerFactory.AddProvider(new CustomLoggerProvider(new CustomLoggerProviderConfiguration()
            {
                LogLevel = LogLevel.Information
            }));

            // meu middler de erro global
            app.ConfigureExceptionHandler();

            app.UseHttpsRedirection();

            app.UseRouting();

            //incluir middleware de autorizacao
            app.UseAuthentication();

            app.UseAuthorization();

            //habilitar configuraçao do CORS
            //configuraçao #01
            app.UseCors(options => options.AllowAnyOrigin()
            .AllowAnyMethod());


            //habilitar configura do CORS
            //configuracao #02
            //app.UseCors();


            //app.UseCors(options => options.WithOrigins("http://www.apirequest.io"));

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
