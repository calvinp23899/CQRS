using AutoMapper;
using Ecommerce.Business.Queries.User;
using Ecommerce.Core.DTOs.Products;
using Ecommerce.Core.DTOs.User;
using Ecommerce.Core.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Business.Queries.Products.Handlers
{
    public class GetAllProductQueryHandler : IRequestHandler<GetAllProductQuery, List<ProductDTO>>
    {
        private IRepository<Core.Models.Product> _repository;
        private IMapper _mapper;
        public GetAllProductQueryHandler(IRepository<Core.Models.Product> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<ProductDTO>> Handle(GetAllProductQuery request, CancellationToken cancellationToken)
        {
            var result = (await _repository.GetAllAsync()).AsNoTracking().ToList();
            var mappedData = _mapper.Map<List<ProductDTO>>(result);
            return mappedData;
        }
    }
}
