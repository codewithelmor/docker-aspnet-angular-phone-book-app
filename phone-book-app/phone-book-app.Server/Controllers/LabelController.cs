﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using phone_book_app.Server.Policies;
using phone_book_app.Server.Services.Contracts;

namespace phone_book_app.Server.Controllers
{
    [ApiController]
    [AllowAnonymous]
    [EnableCors(ControllerPolicy.Cors)]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class LabelController : ControllerBase
    {
        private readonly ILabelService _service;

        public LabelController(
            ILabelService service)
        {
            _service = service;
        }

        [HttpGet("dropdown")]
        public async Task<IActionResult> DropdownAsync()
        {
            return Ok(await _service.AsSelectListAsync());
        }
    }
}
