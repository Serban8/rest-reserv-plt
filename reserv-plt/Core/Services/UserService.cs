using Core.Dtos;
using DataLayer.Models;
using DataLayer.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public class UserService
    {
        private readonly AuthenticationService _authenticationService;
        private readonly AuthorizationService _authorizationService;
        private readonly UserRepository _userRepository;

        public UserService(AuthenticationService authenticationService, AuthorizationService authorizationService, UserRepository userRepository)
        {
            _authenticationService = authenticationService;
            _authorizationService = authorizationService;
            _userRepository = userRepository;
        }

        public async Task Register(UserRegisterDto registerData)
        {
            var hashedPassword = _authenticationService.HashPassword(registerData.Password);

            var user = new User
            {
                Password = hashedPassword,
                Email = registerData.Email,
                FirstName = registerData.FirstName,
                LastName = registerData.LastName,
            };

            try
            {
                await _userRepository.AddAsync(user);
                await _userRepository.SaveAllChangesAsync();
            }
            catch (DbUpdateException e)
            {
                throw new ArgumentException("Email is already registered", e);
            }
            catch (Exception e)
            {
                throw new Exception("Failed to register user", e);
            }
        }

        public async Task<string> Login(UserLoginDto loginData)
        {
            if (string.IsNullOrEmpty(loginData.Email) || string.IsNullOrEmpty(loginData.Password))
            {
                throw new ArgumentException("Missing username or password");
            }

            var user = (await _userRepository.GetByEmailAsync(loginData.Email)) ?? throw new Exception("Invalid username or password");

            if (!_authenticationService.VerifyHashedPassword(user.Password, loginData.Password))
            {
                throw new UnauthorizedAccessException("Invalid username or password");
            }

            // return token
            return _authorizationService.GetToken(user, "RRPUser");
        }

        public async Task<string> GetEmail(Guid userId)
        {
            var user = await _userRepository.GetByIdAsync(userId)
                ?? throw new Exception("User not found");

            return user.Email;
        }

        
        public async Task<string> GetFirstName(Guid userId)
        {
            var user = await _userRepository.GetByIdAsync(userId)
                ?? throw new Exception("User not found");

            return user.FirstName;
        }
    }


}

