using AutoMapper;
using Ecommerce.Core.DTOs.Exceptions;
using Ecommerce.Core.DTOs.User;
using Ecommerce.Core.Interfaces;
using Ecommerce.Core.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Business.Queries.User.Handlers
{
    public class GetUserQueryHandler : IRequestHandler<GetUserQuery, List<UserDTO>>
    {
        private IRepository<Core.Models.User> _repository;
        private IMapper _mapper;
        public GetUserQueryHandler(IRepository<Core.Models.User> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<UserDTO>> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            var users = (await _repository.GetAllAsync()).ToList();
            var result = _mapper.Map<List<UserDTO>>(users);
            return result;
        }
    }
}
