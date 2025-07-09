using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using LearninPlusDAL;
using LearninPlusDAL.Models;
namespace LearningPlusServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LearningPlusController : Controller
    {
        LearningPlusRepository repository;
        public LearningPlusController(LearningPlusRepository repository)
        {
            this.repository = repository;
        }

        [HttpPost]
        public JsonResult adduser(string firstName, string lastName, string email, long mobileNo, char gender, string password)
        {
            int result = 0;
            int userId = 0;
            int roleId = 0;
            
            string message = string.Empty;
            try
            {
                result = repository.AddUser(firstName, lastName, email, mobileNo,gender, password, out userId, out roleId);
                if (result == 1)
                {
                   message = "User registered successfully.";
                }
                else
                {
                    message = "User registration failed. Please try again.";
                }
            }
            catch (Exception)
            {

                message = "An error occurred while processing your request. Please try again later.";

            }
            return Json(new
            {
                success = result == 1,
                message,
                userId = result == 1 ? userId : (int?)null,
                roleId = result == 1 ? roleId : (int?)null
            });
        }
        [HttpPost("validateUser")]
        public JsonResult validateUser(string? email, long? mobileNumber, string password)
        {
            int userId = 0;
            int roleId = 0;
            int result = 0;
            string message = string.Empty;

            try
            {
                result = repository.UserLogin(email, mobileNumber ?? 0, password, out userId, out roleId);

                if (result == 1)
                {
                    message = "User login successful.";
                }
                else if (result == -2)
                {
                    message = "Invalid credentials.";
                }
                else if (result == -1)
                {
                    message = "Please provide either email or mobile number.";
                }
                else
                {
                    message = "Login failed. Please try again.";
                }
            }
            catch (Exception)
            {
                result = -99;
                message = "An error occurred while processing your request.";
            }
            return Json(new 
            {
                success = result == 1,
                message,
                userId = result == 1 ? userId : (int?)null,
                roleId = result == 1 ? roleId : (int?)null
            });
                
        }



    }

}
