using AutoMapper;
using Ecommerce.Business.Commands.Users;
using Ecommerce.Core.DTOs.User;
using Ecommerce.Core.Interfaces;
using Ecommerce.Core.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Business.Commands.Products.Handlers
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, bool>
    {
        private IRepository<Core.Models.Product> _repository;
        public CreateProductCommandHandler(IRepository<Core.Models.Product> repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            bool IsExistedProduct = (await _repository.SearchAsync(x => x.ProductName.ToLower().Equals(request.ProductName.ToLower()))).Any();
            if (IsExistedProduct)
                throw new ArgumentException("Product name is already existed");
            var product = new Product
            {
                ProductName = request.ProductName,
                UnitPrice = request.Price,
            };
            await _repository.InsertAsync(product);
            return true;
        }
    }
}
