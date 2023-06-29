
using Azure.Storage.Blobs;
using Core.Identity;
using Core.Interfaces;
using EventManagementApp.Helpers;
using Infrastructure.Data;
using Infrastructure.Identity;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Text;
using System.Text.Json;
using static Core.Interfaces.IGenericRepo;


namespace EventManagementApp
{
    public class Program
    {
        public static async Task Main(string[] args)
        {

            var builder = WebApplication.CreateBuilder(args);
            ConfigurationManager configuration = builder.Configuration;
            string txt = "hi";

            //AddCors
            builder.Services.AddCors(options =>
            {
                options.AddPolicy(txt, builder =>
                {
                    builder.AllowAnyMethod();
                    builder.AllowAnyOrigin();
                    builder.AllowAnyHeader();
                });
            });

            // Add services to the container.
            builder.Services.AddDbContext<EventManagementContext>(options =>
                    options.UseSqlServer(builder.Configuration.GetConnectionString("EventDB")));

            builder.Services.AddDbContext<AppIdentityDbContext>(opt =>

                opt.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection")));

            builder.Services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("this is my custom Secret key for authentication")),
                    ValidateAudience = false,
                    ValidateIssuer = false,
                    ClockSkew = TimeSpan.Zero
                };
            });




            builder.Services.AddScoped(typeof(IGenericRepo<>), typeof(GenericRepo<>));
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            builder.Services.AddScoped<ITokenServices, TokenServices>();
            builder.Services.AddScoped<IEventVenueRepo, EventVenueRepo>();
            builder.Services.AddScoped<IEventRepo, EventRepo>();
            builder.Services.AddScoped<IEventScheduleRepo, EventScheduleRepo>();
            builder.Services.AddScoped<ITicketRepo, TicketRepo>();
            builder.Services.AddScoped<ITicketPurchasesRepo, TicketPurchasesRepo>();
            builder.Services.AddScoped<ISpeakerRepo, SpeakerRepo>();
            builder.Services.AddScoped<IGallaryRepo, GallaryRepo>();
            builder.Services.AddScoped<ISponsorRepo, SponsorRepo>();
            builder.Services.AddScoped<IHotelRepo, HotelRepo>();
            builder.Services.AddScoped<IEmailRepo,EmailRepo>();


            #region AzureUpload
            builder.Services.AddSingleton(e =>
                new BlobServiceClient(builder.Configuration["AzureStorage:ConnectionString"])
            );
            builder.Services.AddSingleton(e =>
                e.GetRequiredService<BlobServiceClient>().GetBlobContainerClient(builder.Configuration["AzureStorage:ImageContainer"])
            );
            builder.Services.AddSingleton<UploadImage>();
            #endregion

            builder.Services.AddControllers().AddJsonOptions(option =>
                // option.JsonSerializerOptions.ReadCommentHandling = JsonCommentHandling.Skip,
                option.JsonSerializerOptions.AllowTrailingCommas = true
                );




            //builder.Services.AddControllers();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            //add swager autherize 
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Next Driven API", Version = "v1" });
                c.ResolveConflictingActions(x => x.First());
                // Swagger 2.+ support
                c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\""
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                    {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                },
                                //Scheme = "oauth2",
                                Name = "Bearer",
                                In = ParameterLocation.Header,
                            },
                            new string[] {}
                        }
                    });
            });


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseAuthentication();
            //app.UseRouting();
            app.UseAuthorization();
            app.UseCors(txt);
            app.MapControllers();


            app.Run();
        }
    }
}