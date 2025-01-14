using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services.BackgroundServices
{
    public class ReservationUpdateService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;

        public ReservationUpdateService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    // get estimation for all rooms
                    var reservationService = scope.ServiceProvider.GetRequiredService<ReservationService>();
                    var reservations = await reservationService.GetAll();

                    foreach (var reservation in reservations)
                    {
                        //check if the current time is 1h past the reservation date
                        if (DateTime.Now > reservation.ReservationDate.AddHours(1))
                        {
                            //update the reservation status
                            await reservationService.FinishReservation(reservation.Id);
                        }
                    }
                }

                // Wait for 1 minute
                await Task.Delay(TimeSpan.FromSeconds(60), stoppingToken);
            }

        }
    }
}
