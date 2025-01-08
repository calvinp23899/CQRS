using AutoMapper;
using Ecommerce.Core.DTOs.User;
using Ecommerce.Core.Interfaces;
using Ecommerce.Core.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Business.Queries.User.Handlers
{
    public class GetUserOrderHistoryQueryHandler : IRequestHandler<GetUserOrderHistoryQuery, UserOrderHistoryDTO>
    {
        private IRepository<Core.Models.User> _repository;
        private IMapper _mapper;

        public GetUserOrderHistoryQueryHandler(IRepository<Core.Models.User> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<UserOrderHistoryDTO> Handle(GetUserOrderHistoryQuery request, CancellationToken cancellationToken)
        {
            //Noted: not mapping done yet
            var data = (await _repository.SearchAsync(x => x.Id.Equals(request.Id)))
                .Include(x => x.Orders)
                    .ThenInclude(x => x.OrderDetails)
                        .ThenInclude(x => x.Product)
                .AsNoTracking()
                .FirstOrDefault();
            var result = _mapper.Map<UserOrderHistoryDTO>(data);
            return result;
        }
    }
}
