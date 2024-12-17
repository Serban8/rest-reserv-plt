using Core.Services;
using Microsoft.EntityFrameworkCore;
using reserv_plt.DataLayer;
using System.Text.Json.Serialization;

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
        }
    }
}
