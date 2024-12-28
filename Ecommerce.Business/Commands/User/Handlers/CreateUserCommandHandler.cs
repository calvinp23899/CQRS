using AutoMapper;
using Ecommerce.Core.DTOs.User;
using Ecommerce.Core.Interfaces;
using Ecommerce.Core.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Business.Commands.User.Handlers
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
            var user = new Core.Models.User
            {
                Firstname = request.FirstName,
                Lastname = request.LastName,
                Email = request.Email,
                Username = request.Username,
                Password = request.Password,
            };
            await _repository.InsertAsync(user);
            var result = _mapper.Map<UserDetailDTO>(user);
            return result;
        }
    }
}
