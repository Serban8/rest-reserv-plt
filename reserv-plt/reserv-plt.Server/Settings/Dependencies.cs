using Core.Services;
using Microsoft.EntityFrameworkCore;
using DataLayer;
using System.Text.Json.Serialization;
using DataLayer.Repositories;

namespace reserv_plt.Server.Settings
{
    public static class Dependencies
    {
        public static void Inject(WebApplicationBuilder applicationBuilder)
        {
            applicationBuilder.Services.AddControllers().AddJsonOptions(opts =>
            {
                var enumConverter = new JsonStringEnumConverter();
                opts.JsonSerializerOptions.Converters.Add(enumConverter);
            });
            applicationBuilder.Services.AddSwaggerGen();

            applicationBuilder.Services.AddDbContext<AppDbContext>();

            AddRepositories(applicationBuilder.Services);
            AddServices(applicationBuilder.Services);
        }

        private static void AddServices(IServiceCollection services)
        {
            services.AddScoped<TableService>();
            services.AddScoped<FeedbackService>();
        }

        private static void AddRepositories(IServiceCollection services)
        {
            services.AddScoped<TableRepository>();
            services.AddScoped<FeedbackRepository>();
            services.AddScoped<ReservationRepository>();
            services.AddScoped<UserRepository>();
        }
    }
}
