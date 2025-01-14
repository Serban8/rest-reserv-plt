using Core.Services;
using Microsoft.EntityFrameworkCore;
using DataLayer;
using System.Text.Json.Serialization;
using DataLayer.Repositories;
using Core.Services.BackgroundServices;

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
            services.AddScoped<UserService>();
            services.AddScoped<RestaurantService>();
            services.AddScoped<ReservationService>();

            services.AddScoped<AuthorizationService>();
            services.AddScoped<AuthenticationService>();

            services.AddSingleton<EmailService>();

            services.AddHostedService<ReservationUpdateService>();
        }

        private static void AddRepositories(IServiceCollection services)
        {
            services.AddScoped<TableRepository>();
            services.AddScoped<FeedbackRepository>();
            services.AddScoped<ReservationRepository>();
            services.AddScoped<UserRepository>();
            services.AddScoped<RestaurantRepository>();
            services.AddScoped<ConfirmationRepository>();
        }
    }
}
