using Library.Api.Data;
using Library.Api.Models;
using Library.Api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Library.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class LibraryController : Controller
    {
        private readonly LibraryService _service;

        public LibraryController(LibraryService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAuthors(int? index, int? quantity)
        {
            try
            {
                var result = await _service.GetAllAuthors(index ?? 1, quantity ?? 2);
                return Ok(result);
            }
            catch (Exception exception)
            {
                return new JsonResult(BadRequest(exception.Message));
            }
        }

        [HttpPost]
        public IActionResult AddAuthor(Author newAuthor)
        {
            try
            {
                var result = _service.AddAuthor(newAuthor);
                return new JsonResult(Ok(result));
            }
            catch (Exception exception)
            {
                return new JsonResult(BadRequest(exception.Message));
            }
        }

        [HttpDelete]
        public IActionResult DeleteAuthor(Author author)
        {
            try
            {
                var result = _service.DeleteAuthor(author);
                return new JsonResult(Ok(result));
            }
            catch (Exception exception)
            {
                return new JsonResult(NotFound(exception.Message));
            }
        }

        [HttpPost]
        public IActionResult UpdateAuthor(Author author)
        {
            try
            {
                var result = _service.UpdateAuthor(author);
                return new JsonResult(Ok(result));
            }
            catch (Exception exception)
            {
                return new JsonResult(BadRequest(exception.Message));
            }
        }
    }
}
