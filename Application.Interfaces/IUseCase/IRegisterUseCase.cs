using Application.Interfaces.Dtos;
using System.Threading.Tasks;
using Oxygen.CsharpClientAgent;

namespace Application.Interfaces.IUseCase
{
    [RemoteService("UserService")]
    public interface IRegisterUseCase
    {
        Task<ApplicationBaseResult> Execute(RegisterInput input);
    }
}
