using StudentManager.Models;
using System.Collections.Generic;

namespace StudentManager.Data
{
    public interface IStudentRepository
    {
        void Add(Student student);
        void Delete(int id);
        void Update(Student student);
        Student GetById(int id);
        List<Student> GetAll();
        List<Student> Filter(string course, string group);
        List<Student> Search(string query);
        void SaveToJson(string path);
        void LoadFromJson(string path);
        void ExportToCsv(string path);
        void ImportFromCsv(string path);
        Dictionary<string, int> GetCourseStats();
        Dictionary<string, int> GetGroupStats();
    }
}