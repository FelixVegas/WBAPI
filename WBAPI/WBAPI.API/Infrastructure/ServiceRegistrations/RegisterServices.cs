using WBAPI.API.Infraestructure.Extensions;
using WBAPI.API.Services.AuthService;
using WBAPI.API.Services.PetNameService;

namespace WBAPI.API.Infraestructure.ServiceRegistrations
{
    public class RegisterServices : IServiceRegistration
    {
        public void RegisterAppServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped(typeof(IAuthService), typeof(AuthService));
            services.AddScoped(typeof(IPetService), typeof(PetService));
        }
    }
}
