using Application.IInfrastructure;

namespace Infrastructure.EntityFrameworkDataAccess
{
    public interface IEfTransactionHandle
    {
        void Init(IBeginTransaction tran);
    }
}
