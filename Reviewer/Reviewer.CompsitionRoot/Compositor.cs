using Microsoft.Extensions.DependencyInjection;
using Reviewer.DAL.Infrastructure;
using Reviewer.DAL.Interfaces;
using System;

namespace Reviewer.CompsitionRoot
{
    public class Compositor
    {
        public void Compose(IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IUnitOfWork, UnitOfWork>();
            serviceCollection.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        }
    }
}
