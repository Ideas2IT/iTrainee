using iTrainee.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace iTrainee.APIs.Controllers
{
    [ApiController]
    public class AdminController : Controller
    {
        private IUserService _userService;
        public AdminController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpGet]
        [Route("Admin/GetUser/{Id}")]
        public IActionResult GetUser(int id)
        {
            var user = _userService.GetUser(id);
            return View(user);
        }
    }
}
