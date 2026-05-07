using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TravelApp.Application.Common.Exceptions;
using TravelApp.Application.Common.Interfaces;
using TravelApp.Domain.Entities;

namespace TravelApp.Application.TourPackages.Commands.DeleteTourPackage
{
    public class DeleteTourPackageCommand : IRequest
    {
        public int Id { get; set; }
    }

    public class DeleteTourPackageCommandHandler : IRequestHandler<DeleteTourPackageCommand>
    {
        private readonly IApplicationDbContext _context;

        public DeleteTourPackageCommandHandler(IApplicationDbContext context)
        {
            _context = context;
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
        }
    }
}
