using System;

namespace StudentManager.Utils
{
    public static class Constants
    {
        public const string AllowedEmailDomains = "yandex.ru, gmail.com, icloud.com";
        public static readonly DateTime MinBirthDate = new DateTime(1991, 12, 25);
        public static readonly string DefaultSavePath = "students.json";
    }
}
