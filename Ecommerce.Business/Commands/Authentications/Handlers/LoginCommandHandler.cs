using AutoMapper;
using Ecommerce.Business.Commands.Users;
using Ecommerce.Core.DTOs;
using Ecommerce.Core.DTOs.User;
using Ecommerce.Core.Extensions;
using Ecommerce.Core.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Business.Commands.Authentications.Handlers
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, TokenDTO>
    {
        private IRepository<Core.Models.User> _repository;
        private ITokenService _tokenService;
        public LoginCommandHandler(IRepository<Core.Models.User> repository, ITokenService tokenService)
        {
            _repository = repository;
            _tokenService = tokenService;
        }

        public async Task<TokenDTO> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            Expression<Func<Core.Models.User, bool>>? filter = null;
            filter = filter.AddFilter(x => x.IsDeleted == false);
            filter = filter.AddFilter(x => x.Username.ToLower().Equals(request.Username.ToLower()));
            filter = filter.AddFilter(x => x.Password.ToLower().Equals(request.Password.EncryptData().ToLower()));
            var user = (await _repository.SearchAsync(filter)).FirstOrDefault();
            if (user == null)
                throw new ArgumentException("Invalid Username or Password");
            //add token to user
            var result = new TokenDTO();
            var accessTokenTask = _tokenService.GenerateAccessToken(user);
            var refreshTokenTask = _tokenService.GenerateRefreshToken(user);
            await Task.WhenAll(accessTokenTask, refreshTokenTask);
            result.AccessToken = await accessTokenTask;
            result.RefreshToken = await refreshTokenTask;
            await _repository.UpdateAsync(user);
            return result;
        }
    }
}
