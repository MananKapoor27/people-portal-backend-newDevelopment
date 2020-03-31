using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PeoplePortalDataAccessLayer.DbContext;
using PeoplePortalDataAccessLayer.Interfaces;
using PeoplePortalDataAccessLayer.Repositories;
using PeoplePortalServices.Interfaces;
using PeoplePortalServices.Services;
using PeoplePortalServices.Shared.AWS;
using Microsoft.AspNetCore.DataProtection;
using System.IO;
using AutoMapper;
using PeoplePortalDomainLayer.HelperMappers.AutoMappingModels;
using PeoplePortalServices.Shared.ElasticSearch;

namespace PeoplePortal
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public Startup(Microsoft.AspNetCore.Hosting.IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {       
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .WithExposedHeaders("pagination")
                    );
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "PeoplePortal",
                    Description = "PeoplePortal API's",
                });
            });

            //services.AddAuthentication().AddGoogle(googleOptions =>
            //{
            //    googleOptions.ClientId = Configuration["Authentication:Google:ClientId"];
            //    googleOptions.ClientSecret = Configuration["Authentication:Google:ClientSecret"];
            //});

            services.AddControllers();


            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

            services.AddDbContext<PeoplePortalDb>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("PeoplePortalDBString")));

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = actionContext =>
                {
                    var actionExecutingContext =
                        actionContext as Microsoft.AspNetCore.Mvc.Filters.ActionExecutingContext;

                    // if there are modelstate errors & all keys were correctly
                    // found/parsed we're dealing with validation errors
                    if (actionContext.ModelState.ErrorCount > 0
                        && actionExecutingContext?.ActionArguments.Count == actionContext.ActionDescriptor.Parameters.Count)
                    {
                        return new UnprocessableEntityObjectResult(actionContext.ModelState);
                    }

                    // if one of the keys wasn't correctly found / couldn't be parsed
                    // we're dealing with null/unparsable input
                    return new BadRequestObjectResult(actionContext.ModelState);
                };
            });

            services.AddMvc(options =>
            {
                options.Filters.Add(item: new ProducesAttribute("application/json"));
                options.Filters.Add(new ConsumesAttribute("application/json"));
                options.Filters.Add(new ProducesResponseTypeAttribute(StatusCodes.Status404NotFound));
                options.Filters.Add(new ProducesResponseTypeAttribute(StatusCodes.Status500InternalServerError));
                options.EnableEndpointRouting = false;


                options.ReturnHttpNotAcceptable = true;

            });

            services.AddTransient<IDepartmentDesignationService, DepartmentDesignationService>();
            services.AddTransient<IDepartmentService, DepartmentService>();
            services.AddTransient<IDesignationService, DesignationService>();
            services.AddTransient<IEmployeeService, EmployeeService>();
            services.AddTransient<IEmployeeSkillsService, EmployeeSkillsService>();
            services.AddTransient<IEmployeeTypeService, EmployeeTypeService>();
            services.AddTransient<IFeatureService, FeatureService>();
            services.AddTransient<ILevelService, LevelService>();
            services.AddTransient<ISkillService, SkillsService>();
            services.AddTransient<ILoginService, LoginService>();
            services.AddTransient<IProjectService, ProjectService>();
            services.AddTransient<IProjectManagementService, ProjectManagementService>();
            services.AddTransient<IAWSServices, AmazonS3Service>();
            services.AddTransient<IReportingManagerService, ReportingManagerService>();
            services.AddTransient<IProjectRequirementsService, ProjectRequirementsService>();
            services.AddTransient<IElasticSearchService, ElasticSearchService>();
          

            services.AddTransient<IDepartmentDesignationRepository, DepartmentDesignationRepository>();
            services.AddTransient<IDepartmentRepository, DepartmentRepository>();
            services.AddTransient<IDesignationRepository, DesignationRepository>();
            services.AddTransient<IEmployeeRepository, EmployeeRepository>();
            services.AddTransient<IEmployeeSkillsRepository, EmployeeSkillsRepository>();
            services.AddTransient<IEmployeeTypeRepository, EmployeeTypeRepository>();
            services.AddTransient<IFeatureRepository, FeatureRepository>();
            services.AddTransient<ILevelRepository, LevelRepository>();
            services.AddTransient<IDesignationLevelRepository, DesignationLevelRepository>();
            services.AddTransient<ISkillRepository, SkillRepository>();
            services.AddTransient<ILoginRepository, LoginRepository>();
            services.AddTransient<IProjectRepository, ProjectRepository>();
            services.AddTransient<IProjectManagementRepository, ProjectManagementRepository>();
            services.AddTransient<IReportingManagerRepository, ReportingManagerRepository>();
            services.AddTransient<IProjectRequirementsRepository, ProjectRequirementsRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors("AllowAll");

            app.UseMvc();

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "PeoplePortal");
                c.RoutePrefix = string.Empty;
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }
    }
}
