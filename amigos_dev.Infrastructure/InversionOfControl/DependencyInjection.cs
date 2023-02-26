using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.EntityFrameworkCore;
using amigos_dev.Domain.Interfaces;
using amigos_dev.Infrastructure.Services;
using amigos_dev.Application.Interfaces;
using amigos_dev.Infrastructure.Repositories;

namespace amigos_dev.Infrastructure.InversionOfControl
{
    public class DependencyInjection
    {
        public static void Inject(IServiceCollection service, ConfigurationManager config) 
        {
            service.AddScoped<IFriendService, FriendService>();
            service.AddScoped<IFriendRepository, FriendRepository>();
            service.AddDbContext<FDBContext>(options =>
            {
                options.UseSqlServer(config.GetConnectionString("FriendDB"));
            });
        }
    }
}
