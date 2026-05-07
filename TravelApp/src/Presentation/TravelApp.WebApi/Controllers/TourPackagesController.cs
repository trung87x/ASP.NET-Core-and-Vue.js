using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TravelApp.Application.TourPackages.Commands.CreateTourPackage;
using TravelApp.Application.TourPackages.Commands.UpdateTourPackage;
using TravelApp.Application.TourPackages.Commands.DeleteTourPackage;
using TravelApp.WebApi.Controllers;

namespace TravelApp.Presentation.WebApi.Controllers
{
    public class TourPackagesController : ApiController
    {
        [HttpPost]
        public async Task<ActionResult<int>> Create(CreateTourPackageCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, UpdateTourPackageCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }

            await Mediator.Send(command);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await Mediator.Send(new DeleteTourPackageCommand { Id = id });

            return NoContent();
        }
    }
}
