using System.ComponentModel.DataAnnotations;
using Core.Domain.Entities.Base;
using Microsoft.EntityFrameworkCore;

namespace Core.Domain.Entities;

[Index(nameof(StudentCode), IsUnique = true)]
public class Student : BaseEntity
{
    [StringLength(9)]
    public required string StudentCode { get; set; }

    /// <summary>
    /// Maybe add an email validation attribute later
    /// </summary>
    public required string Email { get; set; }

    [StringLength(60)]
    public required string FirstName { get; set; }
    [StringLength(60)]
    public string? SecondName { get; set; }
    [StringLength(60)]
    public required string Lastname { get; set; }
    [StringLength(60)]
    public string? SecondLastName { get; set; }
    public required DateOnly CareerStart { get; set; }
    public virtual ICollection<ExtendRequest> ExtendRequests { get; set; } = [];

    public void UpdateDetails(Student mappedStudent)
    {
        FirstName = mappedStudent.FirstName;
        SecondName = mappedStudent.SecondName;
        Lastname = mappedStudent.Lastname;
        SecondLastName = mappedStudent.SecondLastName;
        Email = mappedStudent.Email;
        CareerStart = mappedStudent.CareerStart;
    }
}
