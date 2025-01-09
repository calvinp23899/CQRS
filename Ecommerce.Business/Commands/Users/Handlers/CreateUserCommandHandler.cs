using AutoMapper;
using Ecommerce.Core.DTOs.User;
using Ecommerce.Core.Extensions;
using Ecommerce.Core.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Ecommerce.Business.Commands.Users.Handlers
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, UserDetailDTO>
    {
        private IRepository<Core.Models.User> _repository;
        private IMapper _mapper;

        public CreateUserCommandHandler(IRepository<Core.Models.User> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<UserDetailDTO> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var isEmailExisted = await ValidateEmail(request.Email);
            if (isEmailExisted)
            {
                throw new ArgumentException("Email is already existed");
            }
            var isUsernameExisted = await ValidateUsername(request.Username);
            if (isUsernameExisted)
            {
                throw new ArgumentException("Username is already existed");
            }
            var user = new Core.Models.User
            {
                Firstname = request.FirstName,
                Lastname = request.LastName,
                Email = request.Email,
                Username = request.Username,
                Password = request.Password.EncryptData(),
            };
            await _repository.InsertAsync(user);
            var result = _mapper.Map<UserDetailDTO>(user);
            return result;
        }

        private async Task<bool> ValidateEmail(string email)
        {
            Expression<Func<Core.Models.User, bool>>? filter = null;
            filter = filter.AddFilter(x => x.IsDeleted == false);
            filter = filter.AddFilter(x => x.Email.ToLower().Equals(email.ToLower()));
            var IsDataExisted = (await _repository.SearchAsync(filter)).AsNoTracking().Any();
            return IsDataExisted;
        }

        private async Task<bool> ValidateUsername(string username)
        {
            Expression<Func<Core.Models.User, bool>>? filter = null;
            filter = filter.AddFilter(x => x.IsDeleted == false);
            filter = filter.AddFilter(x => x.Username.ToLower().Equals(username.ToLower()));
            var IsDataExisted = (await _repository.SearchAsync(filter)).AsNoTracking().Any();
            return IsDataExisted;
        }
    }
}
