using MediatR;
using Microsoft.AspNetCore.Mvc;
using toDoApi.Commands;
using toDoApi.Models;
using toDoApi.Queries;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace toDoApi.Controllers
{
    [Route("api/ToDos")]
    [ApiController]
    public class ToDosController : ControllerBase
    {
        private IMediator _mediator { get; }
        public ToDosController(IMediator mediator)
        {
            _mediator = mediator;
        }
        // GET: api/ToDos
        [HttpGet]
        public async Task<IEnumerable<ToDo>> Get()
        {
            // get todos directly from cosmosDB or from cache
            return await _mediator.Send(new GetAllToDos());
        }

        // GET api/ToDos/5
        [HttpGet("{id}")]
        public string Get(int id)
        {

            // get todo directly from cosmosDB or from cache
            return "value";
        }

        // POST api/ToDos
        [HttpPost]
        public async Task<ToDo> Post([FromBody] CreateToDo createToDoCommand)
        {
            Guid id = createToDoCommand.Id;
            //check if ID exists from cache maybe before sending command to avoid duplicate
            return await _mediator.Send(createToDoCommand);
        }

        // PUT api/ToDos/5
        [HttpPut("{id}")]
        public async Task<ToDo> Put([FromBody] UpdateToDo updateToDoCommand)
        {
            Guid id = updateToDoCommand.Id;
            //check if ID exists from cache maybe before sending command to avoid errors deeper down
            return await _mediator.Send(updateToDoCommand);
        }

        // DELETE api/ToDos/5
        [HttpDelete("{id}")]
        public void Delete([FromRoute] DeleteToDo deleteToDoCommand)
        {
            Guid id = deleteToDoCommand.Id;
            //check if ID exists from cache maybe before sending command to avoid errors deeper down
            _mediator.Send(deleteToDoCommand);
        }
    }
}
