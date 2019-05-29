using Application.IInfrastructure.IRepositories;
using Application.Interfaces;
using Application.Interfaces.Dtos;
using Application.Interfaces.IUseCase;
using System.Threading.Tasks;

namespace Application.UseCase.AccountCancellation
{
    public class AccountCancellationUseCase: CaseBase, IAccountCancellationUseCase
    {
        private readonly IUserRepository _userRepository;
        public AccountCancellationUseCase(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<ApplicationBaseResult> Execute(AccountCancellationInput input)
        {
            return await DoAsync(async x =>
            {
                var user = await _userRepository.GetAsync(input.Id);
                if (user == null)
                {
                    throw new ApplicationException("用户不存在,请重试!");
                }
                user.Cancellation();
                await _userRepository.SaveAsync();
            });
        }
    }
}
