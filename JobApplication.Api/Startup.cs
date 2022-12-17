using JobApplication.Api.ExFilter;
using JobApplication.Database;
using JobApplication.Database.Infrastructure;
using JobApplication.Database.Repositories;
using JobApplication.Service.AccountService;
using JobApplication.Service.AdminService;
using JobApplication.Service.EmailService;
using JobApplication.Service.JobService;
using JobApplication.Service.OtpService;
using JobApplication.Service.RecruiterService;
using JobApplication.Service.RoleMapService;
using JobApplication.Service.RoleService;
using JobApplication.Service.UserService;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace JobApplication.Api
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

            services.AddControllers(option => option
                  .Filters.Add(typeof(ExceptionFilter)));
            services.AddDbContext<JobApplicationDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("Conn")));

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })

                    .AddJwtBearer(options =>
                    {
                        options.SaveToken = true;
                        options.RequireHttpsMetadata = false;
                        options.TokenValidationParameters = new TokenValidationParameters()
                        {
                            ValidateIssuer = true,
                            ValidateAudience = true,
                            ValidAudience = Configuration["JWT:ValidAudience"],
                            ValidIssuer = Configuration["JWT:ValidIssuer"],
                            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:Secret"]))
                        };
                    });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("AdminAndRecruiter", policy => policy.RequireRole("Admin", "Recruiter"));
                options.AddPolicy("AllAllowed", policy => policy.RequireRole("Admin", "Recruiter", "Candidate"));
                options.AddPolicy("AdminAndCandidate", policy => policy.RequireRole("Admin", "Candidate"));
                options.AddPolicy("Admin", policy => policy.RequireRole("Admin"));
            });

            services.AddScoped<IUserService, UserService>()
                    .AddScoped<IUserRepository, UserRepository>()
                    .AddScoped<IRoleService, RoleService>()
                    .AddScoped<IRoleRepository, RoleRepository>()
                    .AddScoped<IRoleMappingRepository ,RoleMappingRepository>()
                    .AddScoped<IRoleMappingService ,RoleMappingService>()
                    .AddScoped<IOtpService, OtpService>()
                    .AddScoped<IOtpRepository, OtpRepository>()
                    .AddScoped<IJobService, JobService>()
                    .AddScoped<IJobRepository, JobRepository>()
                    .AddScoped<IEmailService, EmailService>()
                    .AddScoped<IAdminService, AdminService>()
                    .AddScoped<IAdminRepository, AdminRepository>()
                    .AddScoped<IAccountService, AccountService>()
                    .AddScoped<IAccountRepository, AccountRepository>()
                    .AddScoped<ICandidateRepository, CandidateRepository>()
                    .AddScoped<IRecruiterService, RecruiterService>()
                    .AddScoped<IRecruiterRepository, RecruiterRepository>();


            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "JobApplication.Api", Version = "v1" });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Enter 'Bearer' [space] and then your valid token in the text input below.\r\n\r\nExample: \"Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9\""
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[]{}
                    }
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "JobApplication.Api V1");
                });

            }

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
