using Application.Interfaces.Dtos;
using System.Threading.Tasks;
using Oxygen.CsharpClientAgent;

namespace Application.Interfaces.IUseCase
{
    [RemoteService("UserService")]
    public interface IAccountCancellationUseCase
    {

        Task<ApplicationBaseResult> Execute(AccountCancellationInput input);
    }
}
