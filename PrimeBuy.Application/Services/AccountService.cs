using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using PrimeBuy.Application.Services.Interfaces;
using PrimeBuy.Application.ViewModels;
using PrimeBuy.Data.Repositories.Interfaces;
using PrimeBuy.Entities.Models;

namespace PrimeBuy.Application.Services
{
    public class AccountService : IAccountService
    {
        private readonly IRepository _repository;
        private readonly IAccountRepository _accountRepository;
        private readonly IMapper _mapper;

        public AccountService(IAccountRepository accountRepository, IRepository repository, IMapper mapper)
        {
            _accountRepository = accountRepository;
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<bool> Register(AccountRegisterModel model)
        {
            var user = _mapper.Map<Customer>(model);
            user.EmailConfirmed = true;
            user.Birthdate = DateTime.UtcNow;
            user.Id = new Guid().ToString();
            var response = await _repository.CreateUser(user, model.Password);
            if(response == true)
                return true;
            return false;
        }
        public async Task<Customer> Login(AccountLoginModel model)
        {
            var user = await _accountRepository.GetUserByLogin(model.UserName);
            if(user == null)
                return null;
            var response = await _accountRepository.CheckPassword(user, model.Password);
            if(response == true)
                return user;
            return null;
        }

        public async Task<List<string>> GetUserRoles(string username)
        {
            var roles = await _accountRepository.GetUserRoles(username);
            return roles;
        }
    }
}