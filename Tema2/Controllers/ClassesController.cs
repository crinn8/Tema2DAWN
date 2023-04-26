using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tema2.DTOs;
using Tema2.Models;
using Tema2.Services;

namespace Tema2.Controllers
{
    [ApiController]
    [Route("api/classes")]
    [Authorize]
    public class ClassesController : ControllerBase
    {
        private readonly IClassService _classService;

        public ClassesController(IClassService classService)
        {
            _classService = classService;
        }



        [HttpGet("get-all-classes")]
        public async Task<ActionResult<List<ClassViewDto>>> GetAll()
        {
            var result = await _classService.GetAllClasses();

            return Ok(result);
        }

        [HttpGet("get-class-by-id")]
        public async Task<ActionResult<ClassViewDto>> GetById([FromBody] int id)
        {
            var result = await _classService.GetClass(id);

            return Ok(result);
        }

        [HttpPost("add-class")]
        [Authorize(Roles = "Teacher")]
        public async Task<IActionResult> Add(ClassAddDto payload)
        {
            Class newClass = new Class() { Name = payload.Name};
            var result = await _classService.CreateClass(newClass);

            if (!result)
            {
                return BadRequest("Cannot add class!");
            }

            return Ok(result);
        }
    }
}
