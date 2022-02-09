using System.Threading;
using System.Threading.Tasks;
using load_testing_api.Repository;
using load_testing_api.Repository.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace load_testing_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public sealed class PerformanceController : ControllerBase
    {
        private readonly IPersonRepository repository;

        public PerformanceController(IPersonRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Person>> Person(int id, CancellationToken cancellationToken)
        {
            var person = await repository.GetAsync(id, cancellationToken).ConfigureAwait(false);

            if (person is null)
            {
                return NotFound();
            }

            return Ok(person);
        }

        [HttpPost]
        public async Task<IActionResult> Person(Person person, CancellationToken cancellationToken)
        {
            await repository.AddAsync(person, cancellationToken).ConfigureAwait(false);

            return NoContent();
        }
    }
}