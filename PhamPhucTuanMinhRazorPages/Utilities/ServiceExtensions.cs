using DAOs;
using Repositories;
using Repositories.Impl;

namespace PhamPhucTuanMinhRazorPages.Utilities
{
    public static class ServiceExtensions
    {
        public static void ConfigureServices(this IServiceCollection services)
        {
            services.AddSingleton<CustomerDAO>();
            services.AddSingleton<DetailDAO>();
            services.AddSingleton<ReservationDAO>();
            services.AddSingleton<RoomDAO>();
            services.AddSingleton<RoomTypeDAO>();
            services.AddTransient<ICustomerRepository, CustomerRepository>();
            services.AddTransient<IDetailRepository, DetailRepository>();
            services.AddTransient<IReservationRepository, ReservationRepository>();
            services.AddTransient<IRoomRepository, RoomRepository>();
            services.AddTransient<IRoomTypeRepository, RoomTypeRepository>();
        }
    }
}
