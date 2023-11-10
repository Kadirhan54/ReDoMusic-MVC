using Microsoft.Extensions.DependencyInjection;
using ReDoMusic.Shared.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReDoMusic.Shared
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddSharedServices(this IServiceCollection services)
        {
            services.AddSingleton<GuidGeneratorService>();
            
            return services;
        }
    }
}
