using BlogTemplate.Application.Abstractions;
using BlogTemplate.Domain.Models;

using Microsoft.AspNetCore.Identity;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogTemplate.Application.Features
{
    public class SignInManagerProxy<T> : ISignInManagerProxy<T> where T : class
    {
        private readonly SignInManager<T> _signInManager;

        public SignInManagerProxy(SignInManager<T> signInManager)
        {
            _signInManager = signInManager;
        }

        public async Task<bool> PasswordSignInAsync(string username, string password, bool rememberMe, bool isLockout)
        {
            return (await _signInManager.PasswordSignInAsync(username, password, rememberMe, isLockout)).Succeeded;
        }

        public async Task SignOutAsync()
        {
            await _signInManager.SignOutAsync();
        }
    }
}
