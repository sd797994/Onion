using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Common
{
    public interface ISnowFlakeHelper
    {
        long GetId();
    }
}
