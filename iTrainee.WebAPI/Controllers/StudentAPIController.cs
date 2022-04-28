using Microsoft.AspNetCore.Mvc;
using iTrainee.Models;
using System.Collections.Generic;
using System;

namespace iTrainee.APIs.Controllers
{
    [Route("api/[controller]/[action]")]
    public class StudentAPIController : Controller
    {
        // GET: api/GetAllStudents  
        [HttpGet]
        public IEnumerable<User> GetAllStudents()
        {
            List<User> users = new List<User>();
           User user1 = new User();
                user1.Id = 1;
                user1.FirstName = "user1";
                user1.LastName = "lastname1";
                user1.Qualification = "B.E";
                user1.UserName = "my userid";
            DateTime date2 = new DateTime(2012, 12, 25, 10, 30, 50);
            user1.DOB = date2;

            User user2 = new User();
            user2.Id = 1;
            user2.FirstName = "user1";
            user2.LastName = "lastname1";
            user2.Qualification = "B.E";
            user2.UserName = "my userid";
            user2.DOB = date2;

            return users;
        }
    }
}
