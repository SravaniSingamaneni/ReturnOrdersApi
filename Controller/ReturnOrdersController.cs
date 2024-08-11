using Microsoft.AspNetCore.Mvc;
using ReturnOrdersApi.Model;
using ReturnOrdersApi.Service;

namespace ReturnOrdersApi.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReturnOrdersController : ControllerBase
    {
        private readonly ReturnOrdersService _returnOrdersService;

        public ReturnOrdersController(ReturnOrdersService returnOrdersService)
        {
            _returnOrdersService = returnOrdersService;
        }

        [HttpGet]
        public async Task<ActionResult<List<ReturnOrder>>> Get() =>
            await _returnOrdersService.GetAsync();

        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<ReturnOrder>> Get(string id)
        {
            var order = await _returnOrdersService.GetAsync(id);

            if (order is null)
            {
                return NotFound();
            }

            return order;
        }

        [HttpPost]
        public async Task<IActionResult> Post(ReturnOrder newOrder)
        {
            await _returnOrdersService.CreateAsync(newOrder);
            return CreatedAtAction(nameof(Get), new { id = newOrder.Id }, newOrder);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, ReturnOrder updatedOrder)
        {
            var order = await _returnOrdersService.GetAsync(id);

            if (order is null)
            {
                return NotFound();
            }

            updatedOrder.Id = order.Id;
            await _returnOrdersService.UpdateAsync(id, updatedOrder);
            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var order = await _returnOrdersService.GetAsync(id);

            if (order is null)
            {
                return NotFound();
            }

            await _returnOrdersService.RemoveAsync(id);
            return NoContent();
        }
    }
}
