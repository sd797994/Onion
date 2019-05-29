using System;

namespace Application
{
    public class ApplicationException : Exception
    {
        internal ApplicationException(string businessMessage)
            : base(businessMessage)
        {
        }
    }
}
