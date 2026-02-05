using Core.Domain.Entities;
using Core.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Data.Repos;

public class StudentRepo : IStudentRepo
{
    private readonly ArticleManagementDbContext _dbContext;
    private readonly ILogger<StudentRepo> _logger;
    public StudentRepo(ArticleManagementDbContext dbContext, ILogger<StudentRepo> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task<bool> AddStudent(Student student)
    {
        Student? existingStudent = await _dbContext.Students.AsNoTracking().FirstOrDefaultAsync(s => s.StudentCode == student.StudentCode);
        //if student exists
        if (existingStudent is not null)
        {
            _logger.LogWarning("Attempted to add a student with existing StudentCode {StudentCode}.", student.StudentCode);
            student.Id = existingStudent.Id;
            return false;
        }

        await _dbContext.Students.AddAsync(student);

        await _dbContext.SaveChangesAsync();

        return true;
    }

    public async Task<bool> UpdateStudent(Student student)
    {
        //lets first ensure the student exists
        Student? existingStudent = await _dbContext.Students.AsNoTracking().FirstOrDefaultAsync(s => s.StudentCode == student.StudentCode);

        if (existingStudent is null)
        {
            _logger.LogWarning("Attempted to update a non-existing student with StudentCode {StudentCode}.", student.StudentCode);
            return false;
        }

        existingStudent.UpdateDetails(student);

        _dbContext.Students.Update(existingStudent);

        await _dbContext.SaveChangesAsync();

        return true;
    }
}
