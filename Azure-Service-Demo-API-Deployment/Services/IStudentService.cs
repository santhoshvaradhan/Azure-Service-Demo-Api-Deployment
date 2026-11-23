using Azure_Service_Demo_API_Deployment.Models;

namespace Azure_Service_Demo_API_Deployment.Services
{
    public interface IStudentService
    {
        IEnumerable<Student> GetAll();
        Student? GetById(int id);
        Student Add(Student student);
        bool Update(int id, Student student);
        bool Delete(int id);
    }
}
