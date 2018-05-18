using Reviewer.Core.Interfaces;
using Reviewer.Core.Models;
using Reviewer.DAL.Entities;
using Reviewer.DAL.Interfaces;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System;

namespace Reviewer.Core.Infrastructure
{
    public class RegistrationService : IRegistrationService
    {
        public readonly IUnitOfWork unitOfWork;
        public readonly ICryptoContext cryptoContext;
        public readonly ITokenService tokenService;

        public RegistrationService(IUnitOfWork _unitOfWork, ICryptoContext _cryptoContext, ITokenService _tokenService)
        {
            unitOfWork = _unitOfWork;
            cryptoContext = _cryptoContext;
            tokenService = _tokenService;
        }

        public RegisteredUserModel Register(UserRegistrationModel model)
        {
            if(unitOfWork.Repository<UserEntity>().Set.Any(x => x.Email == model.Email))
            {
                return null;
            }

            string salt = cryptoContext.GenerateSaltAsBase64();
            var user = new UserEntity
            {
                Id = Guid.NewGuid(),
                Name = $"{model.Firstname} {model.Lastname}",
                DateOfBirth = model.DateOfBirth,
                Email = model.Email,
                Type = model.UserType,
                Password = Convert.ToBase64String(cryptoContext.DeriveKey(model.Password, salt)),
                Salt = salt,
                Receivers = new List<ReviewReceiverEntity>()
            };

            user.Receivers.Add(new ReviewReceiverEntity {
                Id = Guid.NewGuid()
            });

            unitOfWork.Repository<UserEntity>().Insert(user);
            unitOfWork.SaveChanges();

            return new RegisteredUserModel
            {
                Id = user.Id,
                FullName = user.Name,
                Token = tokenService.GetToken(new LoginCredentials
                {
                    Email = user.Email,
                    GrantType = "password",
                    Password = model.Password
                })
            };
        }
    }
}
