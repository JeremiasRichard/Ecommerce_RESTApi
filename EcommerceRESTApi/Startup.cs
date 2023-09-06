using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using EcommerceRESTApi.DataBase;
using EcommerceRESTApi.Exceptions;
using EcommerceRESTApi.Interfaces;
using EcommerceRESTApi.Repositories;
using EcommerceRESTApi.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Text.Json.Serialization;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace EcommerceRESTApi
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers().AddJsonOptions(x =>
            x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddAuthorization(options =>
            {
                options.AddPolicy("AdminPolicy", policy => policy.RequireRole("admin"));
                options.AddPolicy("UserPolicy", policy => policy.RequireRole("user"));

            });

            services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {

                    builder.WithOrigins("https://apirequest.io").AllowAnyMethod().AllowAnyHeader().
                    WithExposedHeaders(new string[] { "TotalRegisters", "Records", "RecorsPerPage" });
                });
                services.AddDataProtection();
            });

            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<IProductOriginRepository, ProductOriginRepository>();
            services.AddTransient<IProductTypeRepository, ProductTypeRepository>();
            services.AddTransient<ICategoryRepository, CategoryRepository>();
            services.AddTransient<IItemsRepository, ItemsRepository>();
            services.AddTransient<IDeliveryNoteRepository, DeliveryNoteRepository>();

            services.AddTransient<CategoryServiceImp>();
            services.AddTransient<ProductTypeServiceImp>();
            services.AddTransient<HashService>();
            services.AddTransient<ProductOriginServiceImp>();
            services.AddTransient<ProductServiceImp>();
            services.AddTransient<UserServiceImp>();
            services.AddTransient<DeliveryNoteServiceImp>();
            services.AddTransient<ItemServiceImp>();
            services.AddResponseCaching();

            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

            services.AddEndpointsApiExplorer();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "EcommerceRESTApi", Version = "v1" });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id="Bearer",
                            }
                        },
                        new string []{}
                    }
                });
            });



            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters
                {

                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(Configuration["keyJwt"])),
                    ClockSkew = TimeSpan.Zero,
                });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseRouting();
            app.UseCors();
            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.UseMiddleware<ExceptionMiddleware>();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

          
        }
    }
}
