using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TravelApp.Application.Common.Exceptions;
using TravelApp.Application.Common.Interfaces;
using TravelApp.Domain.Entities;

namespace TravelApp.Application.TourPackages.Commands.UpdateTourPackage
{
    public class UpdateTourPackageCommand : IRequest
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public float Price { get; set; }
    }

    public class UpdateTourPackageCommandHandler : IRequestHandler<UpdateTourPackageCommand>
    {
        private readonly IApplicationDbContext _context;

        public UpdateTourPackageCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Handle(UpdateTourPackageCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.TourPackages.FindAsync(new object[] { request.Id }, cancellationToken);

            if (entity == null)
            {
                throw new NotFoundException(nameof(TourPackage), request.Id);
            }

            entity.Name = request.Name;
            entity.Price = request.Price;

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
