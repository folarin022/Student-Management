using System.Text.RegularExpressions;

namespace StudentManagement.Service
{
    public  static class ValidationService
    {
        public static bool IsValidEmail(string email) =>
            Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");

    }
}
