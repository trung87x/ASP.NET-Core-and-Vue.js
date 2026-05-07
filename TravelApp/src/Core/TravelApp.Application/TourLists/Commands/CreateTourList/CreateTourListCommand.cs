using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using TravelApp.Application.Common.Interfaces;
using TravelApp.Domain.Entities;
using Microsoft.Extensions.Caching.Distributed;

namespace TravelApp.Application.TourLists.Commands.CreateTourList
{
    public class CreateTourListCommand : IRequest<int>
    {
        public string? City { get; set; }
        public string? Country { get; set; }
        public string? About { get; set; }
    }

    public class CreateTourListCommandValidator : AbstractValidator<CreateTourListCommand>
    {
        public CreateTourListCommandValidator()
        {
            RuleFor(v => v.City)
                .MaximumLength(200)
                .NotEmpty().WithMessage("City is required.");

            RuleFor(v => v.Country)
                .NotEmpty().WithMessage("Country is required.");
        }
    }

    public class CreateTourListCommandHandler : IRequestHandler<CreateTourListCommand, int>
    {
        private readonly IApplicationDbContext _context;
        private readonly IDistributedCache _cache;

        public CreateTourListCommandHandler(IApplicationDbContext context, IDistributedCache cache)
        {
            _context = context;
            _cache = cache;
        }

        public async Task<int> Handle(CreateTourListCommand request, CancellationToken cancellationToken)
        {
            var entity = new TourList
            {
                City = request.City,
                Country = request.Country,
                About = request.About
            };

            _context.TourLists.Add(entity);

            await _context.SaveChangesAsync(cancellationToken);

            try
            {
                await _cache.RemoveAsync("TravelApp:TourLists:GetAll", cancellationToken);
            }
            catch { /* Ignore if Redis is down */ }

            return entity.Id;
        }
    }
}
