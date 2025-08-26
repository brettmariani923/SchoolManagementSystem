using System;
using System.Collections.Generic;
using System.Linq;
using Teachers.Domain.Interfaces;
using Teachers.Data.Rows;

public sealed class UpdateBulkStudents : IDataExecute
{
    private readonly IEnumerable<Students_Row> _students;

    public UpdateBulkStudents(IEnumerable<Students_Row> students)
    {
        _students = students ?? throw new ArgumentNullException(nameof(students));
        if (!_students.Any())
            throw new ArgumentException("At least one student is required.", nameof(students));

        // Basic sanity checks (optional, but helpful)
        foreach (var s in _students)
        {
            if (s.StudentID <= 0)
                throw new ArgumentException("Each student must have a positive existing StudentID.", nameof(students));
            if (s.SchoolID <= 0)
                throw new ArgumentException("Each student must have a valid SchoolID.", nameof(students));
            if (s.FirstName is null || s.LastName is null)
                throw new ArgumentException("FirstName/LastName cannot be null.", nameof(students));
        }
    }

    public string GetSql() =>
        @"UPDATE dbo.Students
          SET FirstName = @FirstName,
              LastName  = @LastName,
              [Year]    = @Year,
              SchoolID  = @SchoolID
          WHERE StudentID = @StudentID;";

    public object GetParameters() => _students;
}
