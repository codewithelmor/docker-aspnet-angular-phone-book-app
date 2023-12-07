using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using phone_book_app.Server.InputModels;
using phone_book_app.Server.Policies;
using phone_book_app.Server.Services.Contracts;

namespace phone_book_app.Server.Controllers
{
    [ApiController]
    [AllowAnonymous]
    [EnableCors(ControllerPolicy.Cors)]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class ContactController : ControllerBase
    {
        private readonly IContactService _service;

        public ContactController(
            IContactService service)
        {
            _service = service;
        }

        [HttpGet("")]
        public async Task<IActionResult> ListAsync()
        {
            return Ok(await _service.ListAsync());
        }

        [HttpPost("")]
        public async Task<IActionResult> CreateAsync([FromBody] ContactInputModel model)
        {
            return Ok(await _service.CreateAsync(model));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync([FromRoute] int id, [FromBody] ContactInputModel model)
        {
            model.SetId(id);
            return Ok(await _service.UpdateAsync(model));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] int id)
        {
            BaseInputModel model = new();
            model.SetId(id);
            await _service.DeleteAsync(model);
            return Ok();
        }
    }
}
