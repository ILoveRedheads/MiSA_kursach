using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tp_CourseWork.Controllers;
using Tp_CourseWork.DB;
using Tp_CourseWork.Models;
using Tp_CourseWork.Repositories;
using Xunit;

namespace TP_CourseWork.Tests
{
    public class BaseRepositoryTests
    {
        private readonly Mock<ILogger<BaseRepository>> _mock = new();
        private readonly IServiceProvider _serviceProvider;

        public BaseRepositoryTests()
        {
            _serviceProvider = DependencyInjection.InitilizeServices().BuildServiceProvider();
        }

        [Fact]
        public async Task Get_ReturnsListLocalities_WithLocalities()
        {
            var db = _serviceProvider.GetRequiredService<ApplicationContext>();

            var repo = new BaseRepository(db, _mock.Object);

            db.Localities.AddRange(GetTestLocalities());

            await db.SaveChangesAsync();
            var result = repo.GetLocalities();

            Assert.IsType<List<Locality>>(result);
        }

        [Fact]
        public async Task Get_ReturnsLocality_WithLocalities()
        {
            var db = _serviceProvider.GetRequiredService<ApplicationContext>();

            var repo = new BaseRepository(db, _mock.Object);

            db.Localities.AddRange(GetTestLocalities());

            await db.SaveChangesAsync();
            var result = repo.GetLocalityById(1);

            Assert.IsType<Locality>(result);
        }

        [Fact]
        public async Task Get_ReturnsNull_WithLocalitiesIncorrectId()
        {
            var db = _serviceProvider.GetRequiredService<ApplicationContext>();

            var repo = new BaseRepository(db, _mock.Object);

            db.Localities.AddRange(GetTestLocalities());

            await db.SaveChangesAsync();
            var result = repo.GetLocalityById(20);

            Assert.Null(result);
        }

        private List<Locality> GetTestLocalities()
        {
            var localities = new List<Locality>
            {
                new Locality { Name="Волгоград", BudgetMlrd=12, Mayor = "Владимир Васильевич Марченко", NumberResidantsTh = 100, Type = "City"},
                new Locality { Name="Волжский", BudgetMlrd=20, Mayor = "Игорь Николаевич Воронин", NumberResidantsTh = 321, Type = "City"},
                new Locality { Name="Москва", BudgetMlrd=30, Mayor = "Сергей Семёнович Собянин", NumberResidantsTh = 1301, Type = "City"}
            };
            return localities;
        }
    }
}
