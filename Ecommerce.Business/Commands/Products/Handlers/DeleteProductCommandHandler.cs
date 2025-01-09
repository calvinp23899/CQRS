using Ecommerce.Core.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Business.Commands.Products.Handlers
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, bool>
    {
        private IRepository<Core.Models.Product> _repository;

        public DeleteProductCommandHandler(IRepository<Core.Models.Product> repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            bool product = (await _repository.SearchAsync(x=>x.Id.Equals(request.ProductId))).Any();
            if (product == null)
                throw new ArgumentException("Product name is already existed");
            await _repository.SoftDeletedAsync(request.ProductId);
            return true;
        }
    }
}
