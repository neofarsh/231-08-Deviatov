using CsvHelper;
using StudentManager.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace StudentManager.Data
{
    public class StudentRepository : IStudentRepository
    {
        private List<Student> _students = new List<Student>();
        private int _nextId = 1;

        public void Add(Student student)
        {
            student.Id = _nextId++;
            _students.Add(student);
        }

        public void Delete(int id)
        {
            _students.RemoveAll(s => s.Id == id);
        }

        public void Update(Student student)
        {
            var index = _students.FindIndex(s => s.Id == student.Id);
            if (index != -1)
            {
                _students[index] = student;
            }
        }

        public Student GetById(int id)
        {
            return _students.FirstOrDefault(s => s.Id == id);
        }

        public List<Student> GetAll()
        {
            return _students;
        }

        public List<Student> Filter(string course, string group)
        {
            return _students
                .Where(s => (string.IsNullOrEmpty(course) || s.Course.ToString() == course) &&
                           (string.IsNullOrEmpty(group) || s.Group == group))
                .ToList(); // ToList вызывается для IEnumerable<Student>, а не для bool
        }

        public List<Student> Search(string query)
        {
            return _students.Where(s =>
                s.LastName.Contains(query) ||
                s.FirstName.Contains(query) ||
                s.MiddleName.Contains(query))
                .ToList();
        }

        public void SaveToJson(string path)
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(_students, options);
            File.WriteAllText(path, json);

            // Создаем backup
            string backupPath = Path.Combine(
                Path.GetDirectoryName(path),
                $"{Path.GetFileNameWithoutExtension(path)}_backup_{DateTime.Now:yyyyMMddHHmmss}.json");
            File.WriteAllText(backupPath, json);
        }

        public void LoadFromJson(string path)
        {
            if (File.Exists(path))
            {
                string json = File.ReadAllText(path);
                _students = JsonSerializer.Deserialize<List<Student>>(json);
                _nextId = _students.Count > 0 ? _students.Max(s => s.Id) + 1 : 1;
            }
        }

        public void ExportToCsv(string path)
        {
            using (var writer = new StreamWriter(path))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteRecords(_students);
            }
        }

        public void ImportFromCsv(string path)
        {
            using (var reader = new StreamReader(path))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                var records = csv.GetRecords<Student>().ToList();
                _students.AddRange(records);
                _nextId = _students.Count > 0 ? _students.Max(s => s.Id) + 1 : 1;
            }
        }

        public Dictionary<string, int> GetCourseStats()
        {
            return _students
                .GroupBy(s => s.Course)
                .OrderBy(g => g.Key)
                .ToDictionary(g => $"Курс {g.Key}", g => g.Count());
        }

        public Dictionary<string, int> GetGroupStats()
        {
            return _students
                .GroupBy(s => s.Group)
                .OrderBy(g => g.Key)
                .ToDictionary(g => $"Группа {g.Key}", g => g.Count());
        }
    }
}