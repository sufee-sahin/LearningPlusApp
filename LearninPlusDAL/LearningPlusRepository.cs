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
        // consuming stored procedure for user login
        public int UserLogin(string email,long mobileNumber,string password,out int userId, out int roleId)
        {
            int notRowsAffected = 0;
            userId = 0;
            roleId = 0;
            int result = 0;
            // Input parameters for the stored procedure
            SqlParameter prmEmail = new SqlParameter("@email", System.Data.SqlDbType.VarChar, 50);
            prmEmail.Value = string.IsNullOrEmpty(email) ? (object)DBNull.Value : email;

            SqlParameter prmMobileNumber = new SqlParameter("@mobileNumber", System.Data.SqlDbType.BigInt);
            prmMobileNumber.Value = mobileNumber == 0 ? (object)DBNull.Value : mobileNumber;

            SqlParameter prmPassword = new SqlParameter("@password", password);
            // Output parameters to get userId and roleId
            SqlParameter prmUserId = new SqlParameter("@userId", System.Data.SqlDbType.Int);
            prmUserId.Direction = System.Data.ParameterDirection.Output;
            SqlParameter prmRoleId = new SqlParameter("@roleId", System.Data.SqlDbType.Int);    
            prmRoleId.Direction = System.Data.ParameterDirection.Output;
            // Return value to store return value from stored procedure
            SqlParameter prmReturnRes = new SqlParameter("@returnRes", System.Data.SqlDbType.Int);
            prmReturnRes.Direction = System.Data.ParameterDirection.Output;
            try
            {
                notRowsAffected = _context.Database.ExecuteSqlRaw("EXEC @returnRes = usp_userLogin @email,@mobileNumber,@password,@userId OUT, @roleId OUT", prmReturnRes, prmEmail,prmMobileNumber,prmPassword, prmUserId, prmRoleId);
              
                    // Get the output values
                    result = Convert.ToInt32(prmReturnRes.Value);
                    userId = Convert.ToInt32(prmUserId.Value);
                    roleId = Convert.ToInt32(prmRoleId.Value);
            }
            catch (Exception)
            {
                notRowsAffected = -1;
                userId = 0;
                roleId = 0;
                result = -99;

            }
            return result;

        }
    }
}
