using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using phone_book_app.Server.Policies;
using phone_book_app.Server.Services.Contracts;

namespace phone_book_app.Server.Controllers
{
    [ApiController]
    [AllowAnonymous]
    [EnableCors(ControllerPolicy.Cors)]
    [Route("api/[controller]")]
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
    }
}
