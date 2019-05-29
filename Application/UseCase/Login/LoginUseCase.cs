using Application.IInfrastructure;
using Application.IInfrastructure.IRepositories;
using Application.Interfaces;
using Application.Interfaces.Dtos;
using Application.Interfaces.IUseCase;
using Domain.User;
using Domain.User.Specifications;
using System;
using System.Threading.Tasks;

namespace Application.UseCase.Login
{
    public class LoginUseCase : CaseBase, ILoginUseCase
    {
        private readonly IUserRepository _userRepository;
        private readonly ICommon _common;
        private readonly ICacheServer _cacheServer;
        private readonly IEventBus _eventBus;
        public LoginUseCase(IUserRepository userRepository, ICommon common, ICacheServer cacheServer, IEventBus eventBus)
        {
            _common = common;
            _userRepository = userRepository;
            _cacheServer = cacheServer;
            _eventBus = eventBus;
        }

        public async Task<ApplicationBaseResult> Execute(LoginInput input)
        {
            return await DoAsync(async (x) =>
            {
                //校验用户名重复
                var loginInfo = await _userRepository.GetAsync(new UserExistByNameSpceifications(input.UserName));
                if (loginInfo == null || !loginInfo.Password.Equals(_common.ShaEncrypt(loginInfo.Id.ToString(), input.Password)))
                {
                    throw new ApplicationException("账号或密码错误,请重试!");
                }
                if (loginInfo.State == UserStateEnum.Cancellation)
                {
                    throw new ApplicationException("用户已注销,无法登录!");
                }
                x.Data = _common.GetJwtToken(new{
                    Id= loginInfo.Id.ToString(),
                    loginInfo.UserName,
                    loginInfo.NickName
                });
                _cacheServer.SetCache("HexagonFramework.UserLoginInfo." + loginInfo.Id, x.Data, TimeSpan.FromDays(30));
                await _eventBus.PublishAsync("Microservice.User.LoginHandle", new UserLoginEvent(loginInfo.NickName, DateTime.Now));
            });
        }
    }
}
