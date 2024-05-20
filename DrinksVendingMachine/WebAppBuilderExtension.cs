using DrinksVendingMachine.Backend;
using DrinksVendingMachine.Backend.Services.Administrative;
using DrinksVendingMachine.Backend.Services.DrinksVending;
using DrinksVendingMachine.Backend.Services.Payment;
using Microsoft.EntityFrameworkCore;

namespace DrinksVendingMachine
{
    public static class WebAppBuilderExtension
    {
        public static void ConfigureBuilder(this WebApplicationBuilder builder)
        {
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.ConfigureEntityServices();
            builder.Services.AddDbContextPool<DrinksVendingMachineDbContext>(options => options.UseSqlServer(connectionString, b => b.MigrationsAssembly("DrinksVendingMachine")));
            builder.Services.AddControllers();
            builder.Services.AddSwaggerGen();
        }
        public static void ConfigureEntityServices(this IServiceCollection services)
        {
            services.AddScoped<IAdministrativeService, AdministrativeService>();
            services.AddScoped<IDrinksVendingService, DrinksVendingService>();
            services.AddScoped<IPaymentService, PaymentService>();
            services.AddSingleton<ICurrentBalanceStorage, CurrentBalanceStorage>();
            services.AddSingleton<IDrinksQueue, DrinksQueue>();
        }
    }
}
