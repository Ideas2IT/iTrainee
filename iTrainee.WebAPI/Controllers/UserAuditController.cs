using iTrainee.Models;
using iTrainee.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace iTrainee.APIs.Controllers
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserAuditController : Controller
    {
        IUserAuditService _userAuditService = null;

        public UserAuditController(IUserAuditService userAuditService)
        {
            _userAuditService = userAuditService;
        }

        [HttpGet]
        [AllowAnonymous]
        public IEnumerable<UserAudit> GetAllUserAudit()
        {
            return _userAuditService.GetAllUserAudit();
        }

        [HttpGet]
        [AllowAnonymous]
        public UserAudit GetUserAudit(int id)
        {
            return _userAuditService.GetUserAudit(id);
        }

        [HttpPost]
        public int InsertUserAudit(UserAudit userAudit)
        {
            return _userAuditService.InsertUserAudit(userAudit);
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
