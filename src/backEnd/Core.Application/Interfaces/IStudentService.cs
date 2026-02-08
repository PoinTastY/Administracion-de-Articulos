using Core.Application.DTOs;

namespace Core.Application.Interfaces;

public interface IStudentService
{
    /// <summary>
    /// Add a new student.
    /// If it already exists, it attempts an update.
    /// </summary>
    /// <param name="student"></param>
    /// <returns>PK of the created entity</returns>
    Task<int> AddAsync(StudentDto student);

    /// <summary>
    /// Update an existing student.
    /// </summary>
    /// <param name="student"></param>
    /// <returns>True if the update was successful, otherwise false</returns>
    Task<bool> UpdateAsync(StudentDto student);

    Task<StudentDto?> GetByCodeAsync(string studentCode);
}
