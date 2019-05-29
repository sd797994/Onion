using Application.Interfaces.Dtos;
using System.Threading.Tasks;
using Oxygen.CsharpClientAgent;

namespace Application.Interfaces.IUseCase
{
    [RemoteService("UserService")]
    public interface IChangeAmountUseCase
    {
        Task<ApplicationBaseResult> Execute(ChangeAmountInput input);
    }
}
