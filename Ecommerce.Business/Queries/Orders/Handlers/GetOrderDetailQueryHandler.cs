using AutoMapper;
using Ecommerce.Core.DTOs.Orders;
using Ecommerce.Core.Interfaces;
using Ecommerce.Core.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Business.Queries.Orders.Handlers
{
    public class GetOrderDetailQueryHandler : IRequestHandler<GetOrderDetailQuery, OrderDTO>
    {
        private IRepository<Order> _repository;
        private IMapper _mapper;


        public GetOrderDetailQueryHandler(IRepository<Order> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<OrderDTO> Handle(GetOrderDetailQuery request, CancellationToken cancellationToken)
        {
            var result = (await _repository.SearchAsync(x=>x.Id.Equals(request.Id)))
                .Include(x => x.OrderDetails)
                    .ThenInclude(x => x.Product)
                .Include(x => x.User)
                .AsNoTracking()
                .FirstOrDefault();
            var mappedData = _mapper.Map<OrderDTO>(result);
            return mappedData;
        }
    }
}
