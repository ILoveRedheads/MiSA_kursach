using Tp_CourseWork.DB;
using Tp_CourseWork.Models;

namespace Tp_CourseWork.GofComand
{
    public interface ICommand
    {
        object Execute(ApplicationContext ctx);
    }
}
