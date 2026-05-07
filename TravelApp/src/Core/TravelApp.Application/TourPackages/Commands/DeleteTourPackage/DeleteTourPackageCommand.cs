using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TravelApp.Application.Common.Exceptions;
using TravelApp.Application.Common.Interfaces;
using TravelApp.Domain.Entities;
using Microsoft.Extensions.Caching.Distributed;

namespace TravelApp.Application.TourPackages.Commands.DeleteTourPackage
{
    public class DeleteTourPackageCommand : IRequest
    {
        public int Id { get; set; }
    }

    public class DeleteTourPackageCommandHandler : IRequestHandler<DeleteTourPackageCommand>
    {
        private readonly IApplicationDbContext _context;
        private readonly IDistributedCache _cache;

        public DeleteTourPackageCommandHandler(IApplicationDbContext context, IDistributedCache cache)
        {
            _context = context;
            _cache = cache;
        }

        public async Task Handle(DeleteTourPackageCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.TourPackages.FindAsync(new object[] { request.Id }, cancellationToken);

            if (entity == null)
            {
                throw new NotFoundException(nameof(TourPackage), request.Id);
            }

            _context.TourPackages.Remove(entity);

            await _context.SaveChangesAsync(cancellationToken);
 
            try
            {
                await _cache.RemoveAsync("TravelApp:TourLists:GetAll", cancellationToken);
            }
            catch { }
        }
    }
}
