using MediatR;

namespace Ecommerce.Business.Queries.Orders.Handlers
{
    public class DownloadOrderDetailQueryHandler : IRequestHandler<DownloadOrderDetailQuery, bool>
    {
        public DownloadOrderDetailQueryHandler()
        {
        }

        public async Task<bool> Handle(DownloadOrderDetailQuery request, CancellationToken cancellationToken)
        {
            return true;
        }

        
    }
}
