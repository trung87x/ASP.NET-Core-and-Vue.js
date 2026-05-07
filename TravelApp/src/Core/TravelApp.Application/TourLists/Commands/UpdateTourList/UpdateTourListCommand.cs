using System.Threading;
using System.Threading.Tasks;
using MediatR;
using FluentValidation;
using TravelApp.Application.Common.Exceptions;
using TravelApp.Application.Common.Interfaces;
using TravelApp.Domain.Entities;
using Microsoft.Extensions.Caching.Distributed;

namespace TravelApp.Application.TourLists.Commands.UpdateTourList
{
    public class UpdateTourListCommand : IRequest
    {
        public int Id { get; set; }
        public string? City { get; set; }
        public string? About { get; set; }
    }

    public class UpdateTourListCommandValidator : FluentValidation.AbstractValidator<UpdateTourListCommand>
    {
        public UpdateTourListCommandValidator()
        {
            RuleFor(v => v.City)
                .MaximumLength(200)
                .NotEmpty().WithMessage("City is required.");
        }
    }

    public class UpdateTourListCommandHandler : IRequestHandler<UpdateTourListCommand>
    {
        private readonly IApplicationDbContext _context;
        private readonly IDistributedCache _cache;

        public UpdateTourListCommandHandler(IApplicationDbContext context, IDistributedCache cache)
        {
            _context = context;
            _cache = cache;
        }

        public async Task Handle(UpdateTourListCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.TourLists.FindAsync(new object[] { request.Id }, cancellationToken);

            if (entity == null)
            {
                throw new NotFoundException(nameof(TourList), request.Id);
            }

            entity.City = request.City;
            entity.About = request.About;

            await _context.SaveChangesAsync(cancellationToken);

            try
            {
                await _cache.RemoveAsync("TravelApp:TourLists:GetAll", cancellationToken);
            }
            catch { }
        }
    }
}
