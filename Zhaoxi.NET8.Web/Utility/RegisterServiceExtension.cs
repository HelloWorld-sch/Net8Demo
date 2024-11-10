using Zhaoxi.NET8.Business.Interfaces;
using Zhaoxi.NET8.Business.Services;

namespace Zhaoxi.NET8.Web.Utility
{
    public static class RegisterServiceExtension
    { 
        public static void RegisterExt(this IServiceCollection services)
        {
            services.AddTransient<IMicrophone, Microphone>();
            services.AddTransient<IPower, Power>();
            services.AddTransient<IHeadphone, Headphone>();
            services.AddTransient<IPhone, ApplePhone>();
        }
    }
}
