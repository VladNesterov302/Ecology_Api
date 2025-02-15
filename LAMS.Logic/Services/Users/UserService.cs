﻿using AutoMapper;
using Ecology.DataAccess.Common.Repositories.Users;
using Ecology.Logic.Common.Models.Users;
using Ecology.Logic.Common.Services.Users;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace LAMS.Logic.Services.Users
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repo;

        private readonly IMapper _mapper;

        private bool _disposed;

        public UserService(IUserRepository repo, IMapper mapper)
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

        public async Task<User> SignIn(string userName, string password)
        {
            return await _repo.SignIn(userName, password)
                .ContinueWith(t => _mapper.Map<User>(t.Result));
        }

        public async Task<User> GetUserInfo(string userName)
        {
            return await _repo.GetUserInfo(userName)
                .ContinueWith(t => _mapper.Map<User>(t.Result));
        } 
        
        public async Task<User> DelUser(string Id)
        {
            return await _repo.DelUser(Id)
                .ContinueWith(t => _mapper.Map<User>(t.Result));
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            return await _repo.GetUsers()
                .ContinueWith(t => _mapper.Map<IEnumerable<User>>(t.Result));
        }
        public async Task<string> Block(string id)
        {
            return await _repo.Block(_mapper.Map<string>(id)).ContinueWith(t => t.Result);

        }
        public async Task<string> Unblock(string id)
        {
            return await _repo.Unblock(_mapper.Map<string>(id)).ContinueWith(t => t.Result);
        }
        ~UserService()
        {
            Dispose();
        }
    }
}
