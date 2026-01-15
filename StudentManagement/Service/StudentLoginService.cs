using Microsoft.EntityFrameworkCore;
using StudentManagement.Context;
using StudentManagement.Data;
using StudentManagement.Dto;
using StudentManagement.Dto.StudentModel;
using StudentManagement.Service.Interface;
using System.Security.Cryptography;
using System.Text;

namespace StudentManagement.Service
{
    public class StudentLoginService(ApplicationDbContext dbContext) : IStudentLoginService
    {

        private readonly string _hmacKey = "ThisIsASecretKeyForHMAC";
        public async Task<BaseResponse<bool>> Login(StudentLoginDto request, CancellationToken cancellationToken)
        {
           var response = new BaseResponse<bool>();

            try
            {
                var student = await dbContext.Auths.FirstOrDefaultAsync(a => a.Email == request.EmailAddress, cancellationToken);
                if(student == null)
                {
                    response.IsSuccess = false;
                    response.Message = "Email Address{request.Email} is already taken";
                    return response;
                }

                if (!VerifyPassword(request.password, student.PasswordHash))
                {
                    response.IsSuccess = false;
                    response.Message = "invalid password";
                    return response;
                }

                response.IsSuccess = true;
                response.Message = "Successful";
                return response;
            }


            catch (Exception ex)
            {
                return new BaseResponse<bool>
                {
                    IsSuccess = false,
                    Data = false,
                    Message = $"Error: {ex.Message}",
                };
            }
        }


        public async Task<BaseResponse<bool>> RegisterStudent(RegisterStudentDto request, CancellationToken cancellationToken)
        {
            var response = new BaseResponse<bool>();
            if (string.IsNullOrWhiteSpace(request.EmailAddress) || string.IsNullOrWhiteSpace(request.Password))
            {
                response.IsSuccess = false;
                response.Message = "Username and password are required.";
                return response;
            }

            if (await dbContext.Students.AnyAsync(a => a.FirstName == request.FirstName, cancellationToken))
            {
                response.IsSuccess = false;
                response.Message = "Student already exists.";
                return response;
            }

            if (!ValidationService.IsValidEmail(request.EmailAddress))
            {
                response.IsSuccess = false;
                response.Message = "Invalid email format.";
                return response;
            }

            using var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(_hmacKey));
            var passwordHash = Convert.ToBase64String(hmac.ComputeHash(Encoding.UTF8.GetBytes(request.Password)));
            var stud = new Auth
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Gender = request.Gender,
                Email = request.EmailAddress,
                PhoneNumber = request.PhoneNumber,
                Password = passwordHash,
            };

            dbContext.Auths.Add(stud);
            await dbContext.SaveChangesAsync();

            response.IsSuccess = true;
            response.Message = "Student registered successfully";
            return response;
        }
           
        private bool VerifyPassword(string password, object storedHash)
        {
            using var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(_hmacKey));
            var computedHash = Convert.ToBase64String(hmac.ComputeHash(Encoding.UTF8.GetBytes(password)));
            return computedHash == storedHash;
        }
    }
}
