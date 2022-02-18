using ContactCentre.Map;
using ContactCentre.Repository;
using ContactCentre.Service;
using ContactCentre.Service.Validators;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace ContactCentre
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

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ContactCentre", Version = "v1" });
            });

            services.AddAutoMapper(typeof(InteractionMappingProfile));
            services.AddAutoMapper(typeof(EmployeeMappingProfile));

            services.AddTransient<IContactCentreService, ContactCentreService>();
            services.AddTransient<IInteractionEmployeeAssignmentService, InteractionEmployeeAssignmentService>();
            services.AddTransient<IEmployeeService, EmployeeService>();
            services.AddTransient<IHierarchyService, HierarchyService>();
            
            services.AddTransient<IInteractionEmployeeAssignmentRepository, InteractionEmployeeAssignmentRepository>();
            services.AddTransient<IAvailableEmployeeRepository, AvailableEmployeeRepository>();
            services.AddTransient<IEmployeeRepository, EmployeRepository>();

            services.AddTransient<IEmployeeValidator, EmployeeValidator>();
            services.AddTransient<IAvailableEmployeeValidator, AvailableEmployeeValidator>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ContactCentre v1"));
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
