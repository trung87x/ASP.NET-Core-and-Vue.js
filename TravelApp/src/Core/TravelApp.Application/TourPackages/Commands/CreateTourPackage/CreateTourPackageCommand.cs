using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TravelApp.Application.Common.Interfaces;
using TravelApp.Domain.Entities;
using TravelApp.Domain.Enums;
using Microsoft.Extensions.Caching.Distributed;

namespace TravelApp.Application.TourPackages.Commands.CreateTourPackage
{
    public class CreateTourPackageCommand : IRequest<int>
    {
        public int ListId { get; set; }
        public string? Name { get; set; }
        public string? WhatToExpect { get; set; }
        public string? MapLocation { get; set; }
        public float Price { get; set; }
        public Currency Currency { get; set; }
        public int Duration { get; set; }
        public bool InstantConfirmation { get; set; }
    }

    public class CreateTourPackageCommandHandler : IRequestHandler<CreateTourPackageCommand, int>
    {
        private readonly IApplicationDbContext _context;
        private readonly IDistributedCache _cache;

        public CreateTourPackageCommandHandler(IApplicationDbContext context, IDistributedCache cache)
        {
            _context = context;
            _cache = cache;
        }

        public async Task<int> Handle(CreateTourPackageCommand request, CancellationToken cancellationToken)
        {
            var entity = new TourPackage
            {
                ListId = request.ListId,
                Name = request.Name,
                WhatToExpect = request.WhatToExpect,
                MapLocation = request.MapLocation,
                Price = request.Price,
                Currency = request.Currency,
                Duration = request.Duration,
                InstantConfirmation = request.InstantConfirmation
            };

            _context.TourPackages.Add(entity);

            await _context.SaveChangesAsync(cancellationToken);
 
            try
            {
                await _cache.RemoveAsync("TravelApp:TourLists:GetAll", cancellationToken);
            }
            catch { }

            return entity.Id;
        }
    }
}
