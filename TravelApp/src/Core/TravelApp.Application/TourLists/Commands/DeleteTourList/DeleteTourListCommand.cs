using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using TravelApp.Application.Common.Exceptions;
using TravelApp.Application.Common.Interfaces;
using TravelApp.Domain.Entities;
using Microsoft.Extensions.Caching.Distributed;

namespace TravelApp.Application.TourLists.Commands.DeleteTourList
{
    public class DeleteTourListCommand : IRequest
    {
        public int Id { get; set; }
    }

    public class DeleteTourListCommandHandler : IRequestHandler<DeleteTourListCommand>
    {
        private readonly IApplicationDbContext _context;
        private readonly IDistributedCache _cache;

        public DeleteTourListCommandHandler(IApplicationDbContext context, IDistributedCache cache)
        {
            _context = context;
            _cache = cache;
        }

        public async Task Handle(DeleteTourListCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.TourLists
                .Where(l => l.Id == request.Id)
                .SingleOrDefaultAsync(cancellationToken);

            if (entity == null)
            {
                throw new NotFoundException(nameof(TourList), request.Id);
            }

            _context.TourLists.Remove(entity);

            await _context.SaveChangesAsync(cancellationToken);

            await _cache.RemoveAsync("TravelApp:TourLists:GetAll", cancellationToken);
        }
    }
}
