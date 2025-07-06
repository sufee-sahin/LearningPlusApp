using LearninPlusDAL.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearninPlusDAL
{
    public class LearningPlusRepository
    {
        private LearningPlusContext _context;
        private LearningPlusRepository repository;

        public LearningPlusRepository(LearningPlusContext context)
        {
            _context = context;
        }


        public LearningPlusRepository(LearningPlusRepository repository)
        {
            this.repository = repository;
        }

        // consumeing stored procedure user registration
       public int AddUser(string firstName,string lastName,string email,long mobileNo,char gender,string password,out int userId ,out int roleId)
        {
            userId = 0;
            roleId = 0;
            int noOfrowsAffected = 0;
            int result = 0;
            
                // Input parameters for the stored procedure
                SqlParameter prmFirstName = new SqlParameter("@firstName", firstName);
                SqlParameter prmLastName = new SqlParameter("@lastName", lastName);
                SqlParameter prmEmail = new SqlParameter("@email", email);
                SqlParameter prmMobileNo = new SqlParameter("mobileNumber", mobileNo);
                SqlParameter prmGender = new SqlParameter("@gender", gender);
                SqlParameter prmPassword = new SqlParameter("@password", password);
                // Output parameters to get userId and roleId
                SqlParameter prmUserId = new SqlParameter("@userId", System.Data.SqlDbType.Int);
                prmUserId.Direction = System.Data.ParameterDirection.Output;
                SqlParameter prmRoleId = new SqlParameter("@roleId", System.Data.SqlDbType.Int);
                prmRoleId.Direction = System.Data.ParameterDirection.Output;
                //return value to store return value from stored procedure
                SqlParameter prmReturnRes = new SqlParameter("@returnRes", System.Data.SqlDbType.Int);
                prmReturnRes.Direction = System.Data.ParameterDirection.Output;
            // Execute the stored procedure
            try
            {
             noOfrowsAffected=  _context.Database.ExecuteSqlRaw("EXECUTE @returnRes = dbo.usp_userRegistration @firstName, @lastName, @email, @mobileNumber, @gender, @password, @userId OUT, @roleId OUT", prmReturnRes,
                    prmFirstName, prmLastName, prmEmail, prmMobileNo, prmGender, prmPassword, prmUserId, prmRoleId);
                // Get the output values
                result = Convert.ToInt32(prmReturnRes.Value);
                userId = Convert.ToInt32(prmUserId.Value);
                roleId = Convert.ToInt32(prmRoleId.Value);
            }
            catch (Exception)
            {
                userId = 0;
                roleId = 0;
                result = -99;
                noOfrowsAffected = -1;

            }

            return result;


        }
    } 
}
