using AutoMapper;
using Ecommerce.Business.Queries.Products;
using Ecommerce.Core.DTOs.Orders;
using Ecommerce.Core.DTOs.Products;
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
    public class GetAllOrdersQueryHandler : IRequestHandler<GetAllOrdersQuery, List<OrderDTO>>
    {
        private IRepository<Order> _repository;
        private IMapper _mapper;

        public GetAllOrdersQueryHandler(IRepository<Order> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<OrderDTO>> Handle(GetAllOrdersQuery request, CancellationToken cancellationToken)
        {
            var result = (await _repository.GetAllAsync())
                .Include(x=>x.OrderDetails)
                    .ThenInclude(x=>x.Product)
                .Include(x=>x.User)
                .AsNoTracking()
                .ToList();
            var mappedData = _mapper.Map<List<OrderDTO>>(result);
            return mappedData;
        }
    }
}
