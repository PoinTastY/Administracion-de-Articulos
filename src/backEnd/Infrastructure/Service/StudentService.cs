using Core.Application.DTOs;
using Core.Application.Interfaces;
using Core.Domain.Entities;
using Core.Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Service;

public class StudentService : IStudentService
{
    private readonly IStudentRepo _studentRepo;
    private readonly ILogger<StudentService> _logger;
    public StudentService(IStudentRepo studentRepo, ILogger<StudentService> logger)
    {
        _studentRepo = studentRepo;
        _logger = logger;
    }

    public async Task<bool> UpdateAsync(StudentDto student)
    {
        try
        {
            Student studentEntity = new()
            {
                StudentCode = student.StudentCode,
                FirstName = student.FirstName,
                SecondName = student.SecondName,
                Lastname = student.Lastname,
                SecondLastName = student.SecondLastName,
                Email = student.Email,
                CareerStart = student.CareerStart
            };

            return await _studentRepo.UpdateStudent(studentEntity);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while updating the student with ID {StudentId}.", student.Id);
            return false;
        }
    }

    public async Task<int> AddAsync(StudentDto student)
    {
        try
        {
            Student studentEntity = new Student
            {
                StudentCode = student.StudentCode,
                FirstName = student.FirstName,
                SecondName = student.SecondName,
                Lastname = student.Lastname,
                SecondLastName = student.SecondLastName,
                Email = student.Email,
                CareerStart = student.CareerStart
            };

            bool isAdded = await _studentRepo.AddStudent(studentEntity);

            if (isAdded)
                return studentEntity.Id;

            if (studentEntity.Id <= 0)
            {
                _logger.LogError("Failed to add a new student with StudentCode {StudentCode}, and no existing student found.", student.StudentCode);
                return 0;
            }

            if (await UpdateAsync(student))
            {
                _logger.LogInformation("Student with ID {StudentId} updated successfully after failed add attempt.", studentEntity.Id);
                return studentEntity.Id;
            }

            _logger.LogError("Failed to add or update student with ID {StudentId}.", studentEntity.Id);
            return 0;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while adding a new student with StudentCode {StudentCode}.", student.StudentCode);
            return 0;
        }
    }

    public async Task<StudentDto?> GetByCodeAsync(string studentCode)
    {
        try
        {
            Student? studentEntity = await _studentRepo.GetByCodeAsync(studentCode);

            if (studentEntity == null)
            {
                _logger.LogInformation("No student found with StudentCode {StudentCode}.", studentCode);
                return null;
            }

            return new StudentDto
            {
                Id = studentEntity.Id,
                StudentCode = studentEntity.StudentCode,
                FirstName = studentEntity.FirstName,
                SecondName = studentEntity.SecondName,
                Lastname = studentEntity.Lastname,
                SecondLastName = studentEntity.SecondLastName,
                Email = studentEntity.Email,
                CareerStart = studentEntity.CareerStart
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while retrieving the student with StudentCode {StudentCode}.", studentCode);
            return null;
        }

    }
}
