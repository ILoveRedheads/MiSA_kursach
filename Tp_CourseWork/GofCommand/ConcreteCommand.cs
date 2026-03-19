using Tp_CourseWork.DB;
using Tp_CourseWork.Models;

namespace Tp_CourseWork.GofComand
{
    public class GetLocalitiesCommand : ICommand
    {
        ReceiverGetLocalities receiver;
        public GetLocalitiesCommand(ReceiverGetLocalities r)
        {
            receiver = r;
        }
        public object Execute(ApplicationContext ctx)
        {
            return receiver.GetLocalities(ctx);
        }
    }

    public class GetLocalitiesByMajorCommand : ICommand
    {
        ReceiverGetLocalitiesByMajor receiver;
        public GetLocalitiesByMajorCommand(ReceiverGetLocalitiesByMajor r)
        {
            receiver = r;
        }
        public object Execute(ApplicationContext ctx)
        {
            return receiver.GetLocalitiesByMayor(ctx);
        }
    }

    public class GetBudgetsCommand : ICommand
    {
        ReceiverGetBudgets receiver;
        public GetBudgetsCommand(ReceiverGetBudgets r)
        {
            receiver = r;
        }
        public object Execute(ApplicationContext ctx)
        {
            return receiver.GetBudgets(ctx);
        }
    }

    public class GetNumberResidantsCommand : ICommand
    {
        ReceiverGetNumberResidants receiver;
        public GetNumberResidantsCommand(ReceiverGetNumberResidants r)
        {
            receiver = r;
        }
        public object Execute(ApplicationContext ctx)
        {
            return receiver.GetNumberResidants(ctx);
        }
    }

    public class GetLocalityByIdCommand : ICommand
    {
        ReceiverGetLocalityById receiver;
        public GetLocalityByIdCommand(ReceiverGetLocalityById r)
        {
            receiver = r;
        }
        public object Execute(ApplicationContext ctx)
        {
            return receiver.GetLocalityById(ctx);
        }
    }

    public class CreateLocalityCommand : ICommand
    {
        ReceiverCreateLocality receiver;
        public CreateLocalityCommand(ReceiverCreateLocality r)
        {
            receiver = r;
        }
        public object Execute(ApplicationContext ctx)
        {
            return receiver.CreateLocality(ctx);
        }
    }

    public class UpdateLocalityCommand : ICommand
    {
        ReceiverUpdateLocality receiver;
        public UpdateLocalityCommand(ReceiverUpdateLocality r)
        {
            receiver = r;
        }
        public object Execute(ApplicationContext ctx)
        {
            return receiver.UpdateLocality(ctx);
        }
    }

    public class DeleteLocalityCommand : ICommand
    {
        ReceiverDeleteLocality receiver;
        public DeleteLocalityCommand(ReceiverDeleteLocality r)
        {
            receiver = r;
        }
        public object Execute(ApplicationContext ctx)
        {
            return receiver.DeleteLocality(ctx);
        }
    }

    public class GetStatisticCommand : ICommand
    {
        ReceiverGetStatistic receiver;
        public GetStatisticCommand(ReceiverGetStatistic r)
        {
            receiver = r;
        }
        public object Execute(ApplicationContext ctx)
        {
            return receiver.GetStatistic();
        }
    }
}
