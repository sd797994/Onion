using Application.IInfrastructure.IRepositories;
using Application.Interfaces;
using Application.Interfaces.Dtos;
using Application.Interfaces.IUseCase;
using System.Threading.Tasks;

namespace Application.UseCase.ChangeAmount
{
    public class ChangeAmountUseCase : CaseBase, IChangeAmountUseCase
    {
        private readonly IUserRepository _userRepository;
        public ChangeAmountUseCase(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<ApplicationBaseResult> Execute(ChangeAmountInput input)
        {
            return await DoAsync(async x =>
            {
                var user = await _userRepository.GetAsync(input.Id);
                if (user == null)
                {
                    throw new ApplicationException("用户不存在,请重试!");
                }
                user.ChangeAmount(input.Plus, input.Amount);
                await _userRepository.SaveAsync();
            });
        }
    }
}
