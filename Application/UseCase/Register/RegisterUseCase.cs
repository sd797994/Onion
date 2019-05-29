using Application.IInfrastructure;
using Application.IInfrastructure.IRepositories;
using Application.Interfaces;
using Application.Interfaces.Dtos;
using Application.Interfaces.IUseCase;
using Domain.User;
using Domain.User.Specifications;
using System.Threading.Tasks;

namespace Application.UseCase.Register
{
    public class RegisterUseCase : CaseBase, IRegisterUseCase
    {
        private readonly IUserRepository _userRepository;
        private readonly ICommon _common;
        private readonly IEventBus _eventBus;
        private readonly IBeginTransaction _beginTransaction;
        public RegisterUseCase(
            IUserRepository userRepository, 
            ICommon common, IEventBus eventBus, IBeginTransaction beginTransaction
            )
        {
            _userRepository = userRepository;
            _common = common;
            _eventBus = eventBus;
            _beginTransaction = beginTransaction;
        }

        public async Task<ApplicationBaseResult> Execute(RegisterInput input)
        {
            return await DoAsync(async (x) =>
            {
                using (var begintran = _beginTransaction.BeginTransaction(_eventBus))
                {
                    //校验用户名重复
                    if (await _userRepository.AnyAsync(new UserExistByNameSpceifications(input.UserName)))
                    {
                        throw new ApplicationException("注册用户名重复,请重试!");
                    }
                    //实例化用户领域实体
                    var user = new User();
                    //判断并注册用户
                    user.Register(input.UserName, input.Password, input.NickName);
                    //设置加密密码
                    user.SetPassword(_common.ShaEncrypt(user.Id.ToString(), input.Password));
                    //持久化
                    _userRepository.Add(user);
                    await _userRepository.SaveAsync();
                    x.Message = "用户注册成功!";
                    await _eventBus.PublishAsync("Microservice.User.RegisterHandle", new UserRegisterEvent(user.NickName));
                    //throw new ApplicationException("测试事务异常!");
                    begintran.Commit(_eventBus);
                }
            });
        }
    }
}
