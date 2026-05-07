using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TravelApp.Application.Common.Interfaces;
using TravelApp.Application.Common.Mappings;
using TravelApp.Domain.Entities;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;
using System;

namespace TravelApp.Application.TourLists.Queries.GetTourLists
{
    public class TourListDto : IMapFrom<TourList>
    {
        public int Id { get; set; }
        public string? City { get; set; }
        public string? Country { get; set; }
        public string? About { get; set; }
        public List<TourPackageDto> Packages { get; set; } = new List<TourPackageDto>();

        public void Mapping(Profile profile)
        {
            profile.CreateMap(typeof(TourList), typeof(TourListDto))
                   .ForMember("Packages", opt => opt.MapFrom("Items"));
        }
    }

    public class TourPackageDto : IMapFrom<TourPackage>
    {
        public int Id { get; set; }
        public int ListId { get; set; }
        public string? Name { get; set; }
        public float Price { get; set; }
    }

    public class GetTourListsQuery : IRequest<List<TourListDto>>
    {
    }

    public class GetTourListsQueryHandler : IRequestHandler<GetTourListsQuery, List<TourListDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IDistributedCache _cache;

        public GetTourListsQueryHandler(IApplicationDbContext context, IMapper mapper, IDistributedCache cache)
        {
            _context = context;
            _mapper = mapper;
            _cache = cache;
        }

        public async Task<List<TourListDto>> Handle(GetTourListsQuery request, CancellationToken cancellationToken)
        {
            string cacheKey = "TravelApp:TourLists:GetAll";
            var cachedData = await _cache.GetStringAsync(cacheKey, cancellationToken);

            if (!string.IsNullOrEmpty(cachedData))
            {
                return JsonSerializer.Deserialize<List<TourListDto>>(cachedData)!;
            }

            var lists = await _context.TourLists
                .ProjectTo<TourListDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            var cacheOptions = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5)
            };

            await _cache.SetStringAsync(cacheKey, JsonSerializer.Serialize(lists), cacheOptions, cancellationToken);

            return lists;
        }
    }
}
