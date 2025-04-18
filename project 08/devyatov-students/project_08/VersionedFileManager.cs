using System;
using System.Text.RegularExpressions;

namespace StudentManager.Services
{
    public static class ValidatorService
    {
        public static bool ValidateEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            string pattern = @"^[a-zA-Z0-9._%+-]{3,}@(yandex\.ru|gmail\.com|icloud\.com)$";
            return Regex.IsMatch(email, pattern);
        }

        public static bool ValidatePhone(string phone)
        {
            if (string.IsNullOrWhiteSpace(phone))
                return false;

            return Regex.IsMatch(phone, @"^\+7-\d{3}-\d{3}-\d{2}-\d{2}$");
        }

        public static bool ValidateBirthDate(DateTime date)
        {
            DateTime minDate = new DateTime(1991, 12, 25);
            return date >= minDate && date <= DateTime.Today;
        }

        public static bool ValidateName(string name)
        {
            return !string.IsNullOrWhiteSpace(name) && name.Length >= 2;
        }

        public static bool ValidateCourse(int course)
        {
            return course >= 1 && course <= 6;
        }

        public static bool ValidateGroup(string group)
        {
            return !string.IsNullOrWhiteSpace(group) && group.Length >= 2;
        }
    }
}