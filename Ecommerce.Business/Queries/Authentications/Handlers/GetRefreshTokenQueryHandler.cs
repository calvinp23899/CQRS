using Ecommerce.Business.Queries.User;
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

namespace Ecommerce.Business.Queries.Authentications.Handlers
{
    public class GetRefreshTokenQueryHandler : IRequestHandler<GetRefreshTokenQuery, TokenDTO>
    {
        private IRepository<Core.Models.User> _repository;
        private ITokenService _tokenService;
        public GetRefreshTokenQueryHandler(IRepository<Core.Models.User> repository, ITokenService tokenService)
        {
            _repository = repository;
            _tokenService = tokenService;
        }

        public async Task<TokenDTO> Handle(GetRefreshTokenQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<Core.Models.User, bool>>? filter = null;
            filter = filter.AddFilter(x => x.IsDeleted == false);
            filter = filter.AddFilter(x => x.Id.Equals(request.Id));
            filter = filter.AddFilter(x => x.RefreshToken.Equals(request.RefreshToken));
            var user = (await _repository.SearchAsync(filter)).FirstOrDefault();
            if (user == null)
                throw new ArgumentException("Invalid Id or refresh token");
            if (user.ExpiredRefreshToken < DateTime.UtcNow)
                throw new ArgumentException("Refresh token is expired. Please try to login again.");
            var result = new TokenDTO();
            var accessTokenTask = _tokenService.GenerateAccessToken(user);
            var refreshTokenTask = _tokenService.GenerateRefreshToken(user);
            await Task.WhenAll(accessTokenTask, refreshTokenTask);
            result.AccessToken = await accessTokenTask;
            result.RefreshToken = await refreshTokenTask;
            await _repository.UpdateAsync(user);
            await _repository.SaveAsync();
            return result;
        }
    }
}
