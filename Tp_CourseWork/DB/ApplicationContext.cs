using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Reflection.Metadata;
using Tp_CourseWork.Models;

namespace Tp_CourseWork.DB
{
    public class ApplicationContext: DbContext
    {
        public DbSet<Locality> Localities { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            // Для учебного проекта используем EnsureCreated на новой базе Localitydb2.
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Определение локации
            Locality locality1 = new Locality
            {
                id = 1,
                Name = "Республика Коми",
                Type = "Region",
                NumberResidantsTh = 18800.00,
                BudgetMlrd = 16.65,
                SquareKm = 416774.5,
                Mayor = "Владимир Викторович Уйба"
            };
            Locality locality2 = new Locality
            {
                id = 2,
                Name = "Волгоград",
                Type = "City",
                NumberResidantsTh = 1025.66,
                BudgetMlrd = 40.6,
                SquareKm = 859.3,
                Mayor = "Владимир Васильевич Марченко"
            };
            Locality locality3 = new Locality
            {
                id = 3,
                Name = "Владивосток",
                Type = "City",
                NumberResidantsTh = 597.24,
                BudgetMlrd = 24.9,
                SquareKm = 325.99,
                Mayor = "Константин Владимирович Шестаков"
            };
            Locality locality4 = new Locality
            {
                id = 4,
                Name = "Москва",
                Type = "City",
                NumberResidantsTh = 18800.00,
                BudgetMlrd = 4800.0,
                SquareKm = 2561.5,
                Mayor = "Сергей Семёнович Собянин"
            };
            Locality locality5 = new Locality
            {
                id = 5,
                Name = "Нижегородская область",
                Type = "Region",
                NumberResidantsTh = 3081.81,
                BudgetMlrd = 316.6,
                SquareKm = 76624.0,
                Mayor = "Глеб Сергеевич Никитин"
            };
            


            modelBuilder.Entity<Locality>().HasData(locality1);
            modelBuilder.Entity<Locality>().HasData(locality2);
            modelBuilder.Entity<Locality>().HasData(locality3);
            modelBuilder.Entity<Locality>().HasData(locality4);
            modelBuilder.Entity<Locality>().HasData(locality5);
        }
    }
}
