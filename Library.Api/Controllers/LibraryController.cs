using Library.Api.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Library.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class LibraryController : Controller
    {
        private readonly IApiContext _context;

        public LibraryController(ApiContext context)
        {
            _context = context;
        }

        public LibraryController(IApiContext context)
        {
            _context = context;
        }

        [HttpGet]
        public JsonResult GetAllAuthors()
        {
            var result = from author in _context.Authors select author;

            return new JsonResult(result);
        }
    }
}
