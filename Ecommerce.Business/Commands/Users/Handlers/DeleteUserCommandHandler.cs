using Ecommerce.Core.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Business.Commands.Users.Handlers
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, bool>
    {
        private IRepository<Core.Models.User> _repository;

        public DeleteUserCommandHandler(IRepository<Core.Models.User> repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _repository.GetByIdAsync(request.Id);
            if( user == null)
            {
                throw new ArgumentException("Invalid Id");
            }
            user.IsDeleted = true;
            await _repository.UpdateAsync(user);
            return true;
        }
    }
}
