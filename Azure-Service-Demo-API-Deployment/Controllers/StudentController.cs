using Microsoft.AspNetCore.Mvc;
using Azure_Service_Demo_API_Deployment.Models;
using Azure_Service_Demo_API_Deployment.Services;


namespace Azure_Service_Demo_API_Deployment.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentController : Controller
    {
        private readonly IStudentService _studentService;

        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Student>> GetAll()
        {
            return Ok(_studentService.GetAll());
        }

        [HttpGet("{id}")]
        public ActionResult<Student> GetById(int id)
        {
            var student = _studentService.GetById(id);
            if (student is null)
            {
                return NotFound();
            }

            return Ok(student);
        }

        [HttpPost]
        public ActionResult<Student> Create([FromBody] Student student)
        {
            var created = _studentService.Add(student);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Student student)
        {
            var updated = _studentService.Update(id, student);
            if (!updated)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var deleted = _studentService.Delete(id);
            if (!deleted)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
