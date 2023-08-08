using Newtonsoft.Json;
using SQLite;
using SQLiteDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml;

namespace SQLiteDemo.Services
{
    public class StudentService : IStudentService
    {

        public List<StudentModel> _data = new List<StudentModel>();
        private string _jsonFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Data.JSON");


        private async Task SetUpDb()
        {
            if (!File.Exists(_jsonFilePath))
            {
                List<StudentModel> students = new List<StudentModel>
                {
                };
                string jsonData = System.Text.Json.JsonSerializer.Serialize(students, new JsonSerializerOptions { WriteIndented = true });
                await File.WriteAllTextAsync(_jsonFilePath, jsonData);
            }
        }

        public async Task<int> AddStudent(StudentModel student)
        {
            _data.Add(student);
            await SaveData();
            return 1;
        }


        public async Task<int> DeleteStudent(StudentModel student)
        {
            StudentModel itemToDelete = _data.FirstOrDefault(student);
            if (itemToDelete != null)
            {
                _data.Remove(itemToDelete);
                await SaveData();
            }
            return 1;
        }


        public async Task<List<StudentModel>> GetStudentList()
        {
            await SetUpDb();
            string json = File.ReadAllText(_jsonFilePath);
            try
            {
                List<StudentModel> studentList = await ReadStudentDataFromJsonFileAsync(_jsonFilePath);
                return _data;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task SaveData()
        {
            if (File.Exists(_jsonFilePath))
            {
                string jsonstr = System.Text.Json.JsonSerializer.Serialize(_data, new JsonSerializerOptions { WriteIndented = true });
                await File.WriteAllTextAsync(_jsonFilePath, jsonstr);
            }
            else
            {
                _data = new List<StudentModel>();
            }
        }

        public async Task<int> UpdateStudent(StudentModel studentModel)
        {
            await SaveData();

            return 1;
        }
        public static async Task<List<StudentModel>> ReadStudentDataFromJsonFileAsync(string filePath)
        {
            using (FileStream fs = File.OpenRead(filePath))
            {
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                return await System.Text.Json.JsonSerializer.DeserializeAsync<List<StudentModel>>(fs, options);
            }
        }
    }
}
