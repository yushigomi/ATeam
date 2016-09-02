﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sabio.Web.Core.Services
{
    public interface IIdentityProvider<T>
    {
        T GetCurrentUserId();
    }
}
