using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using Tema2.DTOs;
using Tema2.Services;

namespace Tema2.Controllers
{
    [ApiController]
    [Route("grades")]
    [Authorize]
    public class GradesController: ControllerBase
    {
        private readonly IGradeService _gradeService;
        public GradesController(IGradeService gradeService)
        {
            _gradeService = gradeService;
        }

        [HttpPost("/add-grade")]
        [Authorize(Roles = "Teacher")]
        public async Task<IActionResult> AddGradeForStudentAtCourse([FromBody] AddGradeDto addGradeDto)
        {
            if(addGradeDto == null)
            {
                return BadRequest("No input!");
            }
            var results = await _gradeService.AddGrade(addGradeDto);

            return Ok(results);
        }

        [HttpPost("/grades-by-course/{studentId}")]
        public async Task<IActionResult> GetGroupedGradesForStudent(int studentId)
        {
            if (studentId < -1)
            {
                return BadRequest("Invalid input!");
            }
            var results = await _gradeService.GetGroupedGradesForStudent(studentId);

            return Ok(results);
        }

        [HttpPost("/total/{studentId}")]
        public async Task<IActionResult> GetAverageGradesForStudent(int studentId)
        {
            if (studentId < -1)
            {
                return BadRequest("Invalid input!");
            }
            var results = await _gradeService.GetTotalForStudent(studentId);

            return Ok(results);
        }
    }
}
