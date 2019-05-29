using Application.Interfaces.Dtos;
using System.Threading.Tasks;
using Oxygen.CsharpClientAgent;

namespace Application.Interfaces.IUseCase
{
    [RemoteService("UserService")]
    public interface ILoginUseCase
    {
        Task<ApplicationBaseResult> Execute(LoginInput input);
    }
}
