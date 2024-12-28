using AutoMapper;
using Ecommerce.Core.DTOs.User;
using Ecommerce.Core.Extensions;
using Ecommerce.Core.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Business.Commands.Users.Handlers
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, bool>
    {
        private IRepository<Core.Models.User> _repository;
        public UpdateUserCommandHandler(IRepository<Core.Models.User> repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _repository.GetByIdAsync(request.Id);
            if(user == null)
            {
                throw new ArgumentException("Invalid Id");
            }
            user.Firstname = string.IsNullOrEmpty(request.FirstName) ? user.Firstname : request.FirstName;  
            user.Lastname = string.IsNullOrEmpty(request.LastName) ? user.Lastname : request.LastName;  
            user.PhoneNumber = string.IsNullOrEmpty(request.PhoneNumber) ? user.PhoneNumber : request.PhoneNumber;  
            user.Password = string.IsNullOrEmpty(request.Password) ? user.Password : request.Password.EncryptData();
            await _repository.UpdateAsync(user);
            return true;
        }

    }
}
