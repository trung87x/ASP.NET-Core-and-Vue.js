using System.Collections.Generic;
using TravelApp.Domain.Common;

namespace TravelApp.Domain.Entities
{
    public class TourList : AuditableEntity
    {
        public int Id { get; set; }
        public string? City { get; set; }
        public string? Country { get; set; }
        public string? About { get; set; }

        // Rule: Navigation properties must be initialized to avoid NullReferenceException
        public IList<TourPackage> Items { get; private set; } = new List<TourPackage>();
    }
}
