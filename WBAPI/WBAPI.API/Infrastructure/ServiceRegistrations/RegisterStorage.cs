using WBAPI.API.Infraestructure.Extensions;
using WBAPI.API.Infraestructure.OptionsSettings;
using WBAPI.API.Infrastructure.OptionsSettings;
using WBAPI.API.Services.PetNameService;
using WBAPI.API.Storage;

namespace WBAPI.API.Infrastructure.ServiceRegistrations
{
    public class RegisterStorage : IServiceRegistration
    {
        public void RegisterAppServices(IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<StorageSettings>(configuration.GetSection(nameof(StorageSettings)));

            services.AddScoped(typeof(IStorage), typeof(Storage.Storage));
        }
    }
}
