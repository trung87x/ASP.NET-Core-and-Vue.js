using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TravelApp.Application.TourLists.Commands.CreateTourList;
using TravelApp.Application.TourLists.Commands.UpdateTourList;
using TravelApp.Application.TourLists.Commands.DeleteTourList;
using TravelApp.Application.TourLists.Queries.GetTourLists;

namespace TravelApp.WebApi.Controllers
{
    public class TourListsController : ApiController
    {
        [HttpGet]
        public async Task<ActionResult<List<TourListDto>>> Get()
        {
            return await Mediator.Send(new GetTourListsQuery());
        }

        [HttpPost]
        public async Task<ActionResult<int>> Create(CreateTourListCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, UpdateTourListCommand command)
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
            await Mediator.Send(new DeleteTourListCommand { Id = id });

            return NoContent();
        }
    }
}
