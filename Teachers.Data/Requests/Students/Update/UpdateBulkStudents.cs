using Teachers.Domain.Interfaces;
using Teachers.Data.Rows;   

public class UpdateBulkStudents : IDataExecute
{
    private readonly IEnumerable<Students_Row> _students;
    public UpdateBulkStudents(IEnumerable<Students_Row> students)
    {
        _students = students ?? throw new ArgumentNullException(nameof(students));
        if (!_students.Any())
            throw new ArgumentException("At least one student is required.", nameof(students));
    }
    public string GetSql() =>
        @"UPDATE dbo.Students
          SET FirstName = @FirstName,
              LastName = @LastName,
              [Year] = @Year,
              SchoolID = @SchoolID
          WHERE StudentID = @StudentID;";

    public object? GetParameters() => _students.Select(s => new
    {
        s.StudentID,
        s.FirstName,
        s.LastName,
        s.Year,
        s.SchoolID
    });
}
