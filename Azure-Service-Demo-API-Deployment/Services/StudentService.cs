using System.Collections.Concurrent;
using Azure_Service_Demo_API_Deployment.Models;

namespace Azure_Service_Demo_API_Deployment.Services
{

    public class StudentService : IStudentService
    {

        private readonly ConcurrentDictionary<int, Student> _students = new ConcurrentDictionary<int, Student>()
        {
            [1] = new Student { Id = 1, Name = "Alice Johnson", Age = 20,Email = "alice.johnson@example.com" },
            [2] = new Student { Id = 2, Name = "Bob Smith", Age = 22, Email = "bob.smith@example.com" },
            [3] = new Student { Id = 3, Name = "Charlie Davis", Age = 21, Email = "charlie.davis@example.com" }
        };
        private int _nextId = 4;

        public IEnumerable<Student> GetAll()
        {
            return _students.Values.OrderBy(s => s.Id);
        }

        public Student? GetById(int id)
        {
            _students.TryGetValue(id, out var student);
            return student;
        }

        public Student Add(Student student)
        {
            student.Id = Interlocked.Increment(ref _nextId) - 1;
            _students[student.Id] = student;
            return student;
        }

        public bool Update(int id, Student student)
        {
            if (!_students.ContainsKey(id))
            {
                return false;
            }

            student.Id = id;
            _students[id] = student;
            return true;
        }

        public bool Delete(int id)
        {
            return _students.TryRemove(id, out _);
        }
    }
}
