using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        public string[] students = new string[] { "Diego", "Karla", "Tiara", "Sebastian", "Jorge" };

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(students);
        }
    }
}
