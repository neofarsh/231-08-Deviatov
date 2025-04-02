using CsvHelper;
using StudentManager.Models;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq; // Добавьте эту строку вверху файла

namespace StudentManager.Services
{
    public class CsvService
    {
        public void ExportToCsv(List<Student> students, string path)
        {
            using (var writer = new StreamWriter(path))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteRecords(students);
            }
        }

        public List<Student> ImportFromCsv(string path)
        {
            using (var reader = new StreamReader(path))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                return csv.GetRecords<Student>().ToList();
            }
        }
    }
}