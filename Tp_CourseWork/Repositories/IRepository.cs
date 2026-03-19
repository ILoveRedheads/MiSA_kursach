using Tp_CourseWork.Models;
using Tp_CourseWork.Models.ViewModels;

namespace Tp_CourseWork.Repositories
{
    public interface IRepository
    {
        List<Locality> GetLocalities();
        double[] GetBudgets();
        List<Statistic> GetStatistic(double[]? budgets, double[]? numberResidants);
        bool DeleteLocality(int id);
        bool UpdateLocality(Locality loc);
        bool CreateLocality(Locality loc);
        List<Locality> GetLocalitiesByMayor(string Mayor);
        Locality GetLocalityById(int Id);

    }
}
