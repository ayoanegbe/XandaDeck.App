using GreenPipes;
using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using XandaApp.Infra.Consumer;
using XandaApp.Infra.Services;
using XandaApp.Data.Context;
using XandaApp.Data.Models;

namespace XandaApp.App
{
    public class Startup
    {
        private static readonly string baseDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; private set; }

        public ServiceProvider ServiceProvider { get; private set; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            Configuration = AppServices.GetConfiguration();

            services.AddIdentity<ApplicationUser, IdentityRole>();

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlite("Data Source=" + baseDirectory + "\\xandadocs\\database\\xandaDB.db"));

            services.Configure<EmailSettings>(Configuration.GetSection("EmailSettings"));
           
            services.AddSingleton<Main>()
                   .AddLogging(configure => configure.AddConsole());
            //services.AddSingleton<Login>()
            //       .AddLogging(configure => configure.AddConsole());
            services.AddTransient<IEmailSender, EmailSender>();
            services.AddTransient<IUserService, UserService>();
 

            ServiceProvider = services.BuildServiceProvider();

            //Adding Athentication - JWT
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(o =>
                {
                    o.RequireHttpsMetadata = false;
                    o.SaveToken = false;
                    o.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.Zero,
                        ValidIssuer = Configuration["JWT:Issuer"],
                        ValidAudience = Configuration["JWT:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:Key"]))
                    };
                });

            services.AddCors();
            services.AddControllers();
            services.AddMvc(option => option.EnableEndpointRouting = false);
            services.AddSwaggerGen(c => {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "XandaDeck API", Version = "V1", Description = "APIs for XandaDeck Client" });
                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            //services.AddMassTransit(x =>
            //{
            //    x.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(config =>
            //    {
            //        config.Host(new Uri(Configuration["RabbitMQ:Uri"]), h =>
            //        {
            //            h.Username(Configuration["RabbitMQ:Username"]);
            //            h.Password(Configuration["RabbitMQ:Password"]);
            //        });
            //        config.ReceiveEndpoint("mediaQueue", ep =>
            //        {
            //            ep.PrefetchCount = 16;
            //            ep.UseMessageRetry(r => r.Interval(2, 100));
            //            ep.Consumer<MediaConsumer>(provider);
            //        });
            //    }));
            //});
            //services.AddMassTransitHostedService();

            services.AddAntiforgery(options =>
            {
                // Set Cookie properties using CookieBuilder properties†.
                //options.FormFieldName = "AntiforgeryFieldname";
                options.HeaderName = "XSRF-TOKEN";
                options.SuppressXFrameOptionsHeader = false;
            });           
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                
            }

            loggerFactory.AddFile($"{env.ContentRootPath}\\Logs\\Log.json");

            app.UseSwagger();
            app.UseSwaggerUI(c => {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "XandaDeck API V1");
            });

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseCors("CorsPolicy");

            app.Use(async (context, next) =>
            {
                context.Response.Headers.Add("X-XSS-Protection", "1; mode=block");
                context.Response.Headers.Add("X-Frame-Options", "DENY");
                context.Response.Headers.Add("X-Content-Type-Options", "nosniff");
                context.Response.Headers.Add("Strict-Transport-Security", "max-age=86400");
                context.Response.Headers.Add("Cache-Control", "no-cache");
                context.Response.Headers.Add("Referrer-Policy", "no-referrer");
                context.Response.Headers.Add("X-Permitted-Cross-Domain-Policies", "none");
                //context.Response.Headers.Add("Access-Control-Allow-Origin", "*");
                context.Response.Headers.Remove("X-Powered-By");

                await next();
            });           

            app.UseMvc();
            
        }
    }
}
