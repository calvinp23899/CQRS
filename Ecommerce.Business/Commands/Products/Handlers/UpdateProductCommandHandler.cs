using Ecommerce.Core.Extensions;
using Ecommerce.Core.Interfaces;
using Ecommerce.Core.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Business.Commands.Products.Handlers
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, bool>
    {
        private IRepository<Core.Models.Product> _repository;

        public UpdateProductCommandHandler(IRepository<Core.Models.Product> repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var product = (await _repository.SearchAsync(x=>x.Id.Equals(request.ProductId))).FirstOrDefault();
            if (product == null)
            {
                throw new ArgumentException("Invalid Id");
            }
            product.ProductName = await ValidateProductName(request);
            product.UnitPrice = request.Price;
            await _repository.UpdateAsync(product);
            return true;
        }

        private async Task<string> ValidateProductName(UpdateProductCommand req)
        {
            Expression<Func<Product, bool>>? filter = null;
            filter = filter.AddFilter(x=>x.ProductName.ToLower().Equals(req.ProductName.ToLower()));
            filter = filter.AddFilter(x=> !x.Id.Equals(req.ProductId));
            var IsExistedProductName = (await _repository.SearchAsync(filter)).Any();
            if (IsExistedProductName)
                throw new ArgumentException($"Product name: {req.ProductName} is already existed");
            return req.ProductName;
        }
    }
}
