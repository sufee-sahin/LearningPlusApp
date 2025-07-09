using LearninPlusDAL;
using LearninPlusDAL.Models;

namespace LearningPlusDALConsoleAPP
{
    public class Program
    {
        static LearningPlusContext context;
        static LearningPlusRepository repository;

        static Program()
        {
            context = new LearningPlusContext();
            repository = new LearningPlusRepository(context);
        }
        static void validateUser(string email,long mobileNumber, string password)
        {
            int userId = 0;
            int roleId = 0;
            int result = repository.UserLogin(email,mobileNumber,password, out userId, out roleId);
            if (result > 0)
            {
                Console.WriteLine("User validated successfully!");
                Console.WriteLine($"User ID: {userId}, Role ID: {roleId}");
            }
            else
            {
                Console.WriteLine("Invalid email or password!");
            }
           
        }
        static void Main(string[] args)
        {
            //int userId = 0;
            //int roleId = 0;
            //int result = repository.AddUser("Ruhi","Raj","Ruhi12345@gmail.com",9988776653L, 'F', "Jon12345", out userId, out roleId);
            //if (result > 0)
            //{
            //    Console.WriteLine("User registered successfully!");
            //}
            //else
            //{
            //    Console.WriteLine("User registration failed!");
            //}
            validateUser(null, 1234567894, "Severus@123");
        }
    }
}
