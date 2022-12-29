using CQRS.Commands;
using CQRS.Models;
using CQRS.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CQRS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IMediator _mediator;

        public StudentController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<List<Student>> Get()
        {
            return await _mediator.Send(new GetStudentListQuery());
        }

        [HttpPost]
        public async Task<Student> Post([FromBody] Student value)
        {
            var model = new AddStudentCommand(value.FirstName, value.LastName, value.Age);
            return await _mediator.Send(model);
        }
    }
}
