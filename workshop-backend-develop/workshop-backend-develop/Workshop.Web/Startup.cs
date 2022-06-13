using System;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text.Json.Serialization;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Net.Http.Headers;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Workshop.Core.Domain.Model.ApiKey;
using Workshop.Core.Domain.Model.Auditorium;
using Workshop.Core.Domain.Model.Competency;
using Workshop.Core.Domain.Model.Event;
using Workshop.Core.Domain.Model.KeyTechnology;
using Workshop.Core.Domain.Model.LifeScenario;
using Workshop.Core.Domain.Model.Project;
using Workshop.Core.Domain.Model.ProjectProposal;
using Workshop.Core.Domain.Model.RequestProposal;
using Workshop.Core.Domain.Model.Role;
using Workshop.Core.Domain.Model.Session;
using Workshop.Core.Domain.Model.Team;
using Workshop.Core.Domain.Model.TeamSlot;
using Workshop.Core.Domain.Model.User;
using Workshop.Core.Domain.Model.UserCompetency;
using Workshop.Core.Domain.SharedKernel.Repository;
using Workshop.Core.Services.Security;
using Workshop.Infrastructure.Storage;
using Workshop.Infrastructure.Storage.Repositories;
using Workshop.Web.Controllers;
using Workshop.Web.Dtos.Admin.Project;
using Workshop.Web.Features.Admin.Project.Command;
using Workshop.Web.Mapping;
using Workshop.Web.WebSecurity;

namespace Workshop.Web
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

            services.AddMvcCore(options => options.EnableEndpointRouting = false)
                .AddApiExplorer()
                .AddNewtonsoftJson();
            services.AddDbContext<WorkshopDbContext>(opt =>
            {
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                    opt.UseSqlServer(Configuration.GetConnectionString("WorkshopDbContext"));
                
            });

            var mappingConfig = new MapperConfiguration(mc => { mc.AddProfile(new MappingProfile()); });

            AddRepositories(services);

            var mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<IUserProfileProvider, UserProfileProvider>();
            services.AddMediatR(typeof(BaseWebController).GetTypeInfo().Assembly,
                typeof(WorkshopDbContext).GetTypeInfo().Assembly);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo {Title = "My API", Version = "v1"});
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder
                        .WithExposedHeaders("X-Pagination")
                );
            });

            services
                .AddMvc(o => o.Filters.Add(typeof(AuthorizationFilter)))
                .AddNewtonsoftJson(opts =>
                {
                    opts.SerializerSettings.Converters.Add(new StringEnumConverter());
                    opts.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                })
                .AddJsonOptions(opts => opts.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();


            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

            if (!env.IsProduction())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1"); });
            }

            app.UseHttpsRedirection();
            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseMvc();

            //для работы роутинга в на фронте
            app.MapWhen(x => !x.Request.Path.Value.StartsWith("/api"), builder =>
            {
                app.Run(async context =>
                {
                    // Add Header
                    context.Response.Headers["Access-Control-Allow-Origin"] = "*";

                    // Call next middleware
                    context.Response.ContentType = "text/html";
                    context.Response.Headers[HeaderNames.CacheControl] = "no-store, no-cache, must-revalidate";
                    await context.Response.SendFileAsync(Path.Combine(env.WebRootPath, "index.html"));
                });
            });
        }

        private static void AddRepositories(IServiceCollection services)
        {
            services.RegisterRepository<User, IUserRepository, IReadOnlyRepository<User>, UserRepository>();
            services.RegisterRepository<Session, ISessionRepository, IReadOnlyRepository<Session>, SessionRepository>();
            services
                .RegisterRepository<Auditorium, IAuditoriumRepository, IReadOnlyRepository<Auditorium>,
                    AuditoriumRepository>();
            services
                .RegisterRepository<Competency, ICompetencyRepository, IReadOnlyRepository<Competency>,
                    CompetencyRepository>();
            services
                .RegisterRepository<KeyTechnology, IKeyTechnologyRepository, IReadOnlyRepository<KeyTechnology>,
                    KeyTechnologyRepository>();
            services
                .RegisterRepository<LifeScenario, ILifeScenarioRepository, IReadOnlyRepository<LifeScenario>,
                    LifeScenarioRepository>();
            services.RegisterRepository<Project, IProjectRepository, IReadOnlyRepository<Project>, ProjectRepository>();
            services.RegisterRepository<Team, ITeamRepository, IReadOnlyRepository<Team>, TeamRepository>();
            services.RegisterRepository<Event, IEventRepository, IReadOnlyRepository<Event>, EventRepository>();
            services.RegisterRepository<Role, IRoleRepository, IReadOnlyRepository<Role>, RoleRepository>();
            services.RegisterRepository<ProjectProposal, IProjectProposalRepository,
                IReadOnlyRepository<ProjectProposal>, ProjectProposalRepository>();
            services
                .RegisterRepository<TeamSlot, ITeamSlotsRepository, IReadOnlyRepository<TeamSlot>,
                    TeamSlotsRepository>();
            services.RegisterRepository<ApiKey, IApiKeyRepository, IReadOnlyRepository<ApiKey>, ApiKeyRepository>();
            services
                .RegisterRepository<RequestProposal, IRequestProposalRepository, IReadOnlyRepository<RequestProposal>,
                    RequestProposalRepository>();
            services.RegisterRepository<UserCompetency,IUserCompetencyRepository,IReadOnlyRepository<UserCompetency>,UserCompetencyRepository>();
        }
    }
}