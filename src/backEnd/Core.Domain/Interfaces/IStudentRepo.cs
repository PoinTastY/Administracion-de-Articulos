using Core.Domain.Entities;

namespace Core.Domain.Interfaces;

public interface IStudentRepo
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="student"></param>
    /// <returns>
    /// Bool with result, but as the student is passed as reference, 
    /// it will set the PK on it, so you can cathc the false and attemtpt an update
    /// </returns>
    Task<bool> AddStudent(Student student);
    Task<bool> UpdateStudent(Student student);
}
