using Cats.Infrastructure;
using Cats.Domain;
using Cats.Application;
using Cats.Application.Cats;
using Microsoft.AspNetCore.Mvc;
using MediatR;

namespace Cats.API.Controllers
{
    public class CatsController : BaseApiController
    {
        private readonly DataContext _context;
        private readonly IMediator _mediator;
        public CatsController(DataContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }
        [HttpGet]
        public async Task<ActionResult<List<Cat>>> GetCats()
        {
            return await _mediator.Send(new List.Query());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Cat>> GetCat(Guid id)
        {
            var result = await _mediator.Send(new Details.Query { Id = id });
            if (result == null)
                return NotFound();
            return result;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditCat(Guid id, Cat cat)
        {
            var result = await _mediator.Send(new Edit.Command { cat = cat });
            if (result == null)
                return NotFound();
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> CreateCat(Cat cat)
        {
            var result = await _mediator.Send(new Create.Command { cat = cat });
            return Ok(result);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCat(Guid id)
        {
            try
            {
                await _mediator.Send(new Delete.Command { Id = id });
                return Ok();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }
    }
}
