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
            try
            {
                return Ok(await _service.ListAsync());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("")]
        public async Task<IActionResult> CreateAsync([FromBody] ContactInputModel model)
        {
            try
            {
                return Ok(await _service.CreateAsync(model));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync([FromRoute] int id, [FromBody] ContactInputModel model)
        {
            try
            {
                model.SetId(id);
                return Ok(await _service.UpdateAsync(model));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] int id)
        {
            try
            {
                BaseInputModel model = new();
                model.SetId(id);
                await _service.DeleteAsync(model);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
