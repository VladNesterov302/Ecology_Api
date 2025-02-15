﻿using Ecology.Logic.Common.Models.Users;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Ecology.Logic.Common.Services.Users
{
    /// <summary>
    /// Standart BL level interface provides standart methods of working with User model.
    /// </summary>
    public interface IRegistrationService : IDisposable
    {
      
        Task<string> Registration(User user);
    }
}
