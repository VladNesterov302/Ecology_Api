﻿using AutoMapper;
using Ecology.DataAccess.Common.Models.Users;
using Ecology.DataAccess.Common.Repositories.Users;
using Ecology.Logic.Common.Models.Users;
using Ecology.Logic.Common.Services.Users;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Ecology.Logic.Services.Users
{
    public class RegistrationService : IRegistrationService
    {
        private readonly IUserRepository _repo;

        private readonly IMapper _mapper;

        private bool _disposed;

        public RegistrationService(IUserRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public void Dispose()
        {
            if (!_disposed)
            {
                _repo?.Dispose();
                _disposed = true;
                GC.SuppressFinalize(this);
            }
        }

        public async Task<string> Registration(User user)
        {
            try
            {
                if (!await _repo.IsUserNameAvailable(user.UserName))
                {
                    // throws 409 conflict
                    return null;
                }
                var id = await _repo.Registration(_mapper.Map<UserDb>(user)).ContinueWith(t => t.Result);

                return id;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        ~RegistrationService()
        {
            Dispose();
        }
    }
}

