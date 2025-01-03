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

namespace Ecommerce.Business.Commands.Orders.Handlers
{
    public class CreateOrderBillCommandHandler : IRequestHandler<CreateOrderBillCommand, bool>
    {
        private IRepository<Order> _repository;
        private IRepository<OrderDetail> _repositoryOrderDetail;
        private IRepository<Core.Models.Product> _repositoryProduct;
        private IUserService _userService;

        public CreateOrderBillCommandHandler(IRepository<Order> repository, IUserService userService, IRepository<OrderDetail> repositoryOrderDetail, IRepository<Product> repositoryProduct)
        {
            _repository = repository;
            _userService = userService;
            _repositoryOrderDetail = repositoryOrderDetail;
            _repositoryProduct = repositoryProduct;
        }

        public async Task<bool> Handle(CreateOrderBillCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var userId = _userService.GetUserId();
                var listOrderDetail = await NewListOrderDetail(request);
                decimal total = listOrderDetail.Sum(x => x.UnitPrice * x.Quantity).Value;
                var order = new Order
                {
                    UserId = userId,
                    OrderDetails = listOrderDetail,
                    TotalPrice = total
                };
                await _repository.CreateAsync(order);
                return true;
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.InnerException.Message);
            }
        }

        private async Task<List<OrderDetail>> NewListOrderDetail(CreateOrderBillCommand request)
        {
            var result = new List<OrderDetail>();
            foreach (var item in request.Data)
            {
                var product = await _repositoryProduct.GetByIdAsync(item.ProductId);
                if (product == null)
                    throw new ArgumentException($"Invalid product id {item.ProductId}");
                var orderDetails = new OrderDetail
                {
                    ProductId = product.Id,
                    Quantity = item.Quantity,
                    ProductName = product.ProductName,
                    UnitPrice = product.UnitPrice,
                };
                result.Add(orderDetails);
                await _repositoryOrderDetail.CreateAsync(orderDetails);
            }
            return result;
        }
    }
}
