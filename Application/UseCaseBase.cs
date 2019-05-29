using Application.Interfaces;
using Domain;
using System;
using System.Threading.Tasks;

namespace Application
{
    public class CaseBase
    {
        public async Task<ApplicationBaseResult> DoAsync(Func<ApplicationBaseResult, Task> runMethod)
        {
            var result = new ApplicationBaseResult();
            try
            {
                await runMethod(result);
                result.Code = 0;
            }
            catch (Exception e)
            {
                if (e is ApplicationException || e is DomainException)
                {
                    result.Code = -1;
                    result.Message = e.Message;
                }
                else
                {
                    result.Message = "出错了,请稍后再试";
                }
            }
            return result;
        }

        public ApplicationBaseResult Do(Action<ApplicationBaseResult> runMethod)
        {
            var result = new ApplicationBaseResult();
            try
            {
                runMethod(result);
                result.Code = 0;
            }
            catch (Exception e)
            {
                if (e is ApplicationException || e is DomainException)
                {
                    result.Code = -1;
                    result.Message = e.Message;
                }
                else
                {
                    result.Message = "出错了,请稍后再试";
                }
            }
            return result;
        }
    }
}
