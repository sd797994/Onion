using Application.IInfrastructure;

namespace Infrastructure.CapEventBusAccess
{
    public interface ICapTransactionHandle
    {
        void Init(IBeginTransaction tran);
    }
}
