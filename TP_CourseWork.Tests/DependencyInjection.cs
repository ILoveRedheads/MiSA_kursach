using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tp_CourseWork.DB;

namespace TP_CourseWork.Tests
{
    internal class DependencyInjection
    {
        public static ServiceCollection InitilizeServices()
        {
            var services = new ServiceCollection();
            var options = new DbContextOptionsBuilder<ApplicationContext>().UseInMemoryDatabase("Localitydb").Options;
            services.AddScoped(_ => new ApplicationContext(options));
            return services;
        }
    }
}
