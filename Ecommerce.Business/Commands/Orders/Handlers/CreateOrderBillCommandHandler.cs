using Ecommerce.Business.Commands.Users;
using Ecommerce.Core.Constants;
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
        private IRepository<Core.Models.User> _repositoryUser;
        private IUserService _userService;
        private IEmailService _emailService;

        public CreateOrderBillCommandHandler(
            IRepository<Order> repository, 
            IUserService userService, 
            IRepository<OrderDetail> repositoryOrderDetail, 
            IRepository<Product> repositoryProduct,
            IEmailService emailService,
            IRepository<User> repositoryUser)
        {
            _repository = repository;
            _userService = userService;
            _repositoryOrderDetail = repositoryOrderDetail;
            _repositoryProduct = repositoryProduct;
            _emailService = emailService;
            _repositoryUser = repositoryUser;
        }

        public async Task<bool> Handle(CreateOrderBillCommand request, CancellationToken cancellationToken)
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
            await SendEmail(userId);
            return true;
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

        private async Task<bool> SendEmail(int userId)
        {
            var user = await _repositoryUser.GetByIdAsync(userId);
            var listEmails = new List<string>();
            listEmails.Add(user.Email);
            var body = string.Format(EmailBodyConstant.OrderSuccessMessage, new Guid());
            await _emailService.SendEmailAsync(
                listEmails,
                "Your Order is placed successfully",
                body,
                null);
            return true;
        }
    }
}
