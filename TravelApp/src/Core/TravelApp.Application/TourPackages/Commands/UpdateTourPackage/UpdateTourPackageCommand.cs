using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TravelApp.Application.Common.Exceptions;
using TravelApp.Application.Common.Interfaces;
using TravelApp.Domain.Entities;
using Microsoft.Extensions.Caching.Distributed;

using FluentValidation;

namespace TravelApp.Application.TourPackages.Commands.UpdateTourPackage
{
    public class UpdateTourPackageCommand : IRequest
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public float Price { get; set; }
    }

    public class UpdateTourPackageCommandValidator : AbstractValidator<UpdateTourPackageCommand>
    {
        public UpdateTourPackageCommandValidator()
        {
            RuleFor(v => v.Name)
                .MaximumLength(200)
                .NotEmpty().WithMessage("Name is required.");

            RuleFor(v => v.Price)
                .GreaterThan(0).WithMessage("Price must be greater than zero.");
        }
    }

    public class UpdateTourPackageCommandHandler : IRequestHandler<UpdateTourPackageCommand>
    {
        private readonly IApplicationDbContext _context;
        private readonly IDistributedCache _cache;

        public UpdateTourPackageCommandHandler(IApplicationDbContext context, IDistributedCache cache)
        {
            _context = context;
            _cache = cache;
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
 
            try
            {
                await _cache.RemoveAsync("TravelApp:TourLists:GetAll", cancellationToken);
            }
            catch { }
        }
    }
}
