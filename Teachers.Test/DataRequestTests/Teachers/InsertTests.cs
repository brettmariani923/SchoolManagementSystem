using Teachers.Data.DTO;
using Teachers.Data.Requests.Teachers.Insert;

public class InsertTests
{
    [Fact]
    public void InsertNewTeacher_GetSql_IsExact()
    {
        var req = new InsertNewTeacher("John", "Doe", 1);

        const string expected =
            "INSERT INTO dbo.Teachers (FirstName, LastName, SchoolID) " +
            "VALUES (@FirstName, @LastName, @SchoolID);";
        Assert.Equal(expected, req.GetSql());
    }

    [Fact]
    public void InsertNewTeacher_GetParameters_ProjectsFields()
    {
        var req = new InsertNewTeacher("John", "Doe", 1);
        var p = req.GetParameters()!;
        var t = p.GetType();

        Assert.Equal("John", (string)t.GetProperty("FirstName")!.GetValue(p)!);
        Assert.Equal("Doe", (string)t.GetProperty("LastName")!.GetValue(p)!);
        Assert.Equal(1, (int)t.GetProperty("SchoolID")!.GetValue(p)!);
    }

    [Fact]
    public void InsertBulkNewTeachers_Ctor_Null_Throws()
    {
        IEnumerable<Teachers_Row>? input = null;
        Assert.Throws<ArgumentNullException>(() => new InsertBulkNewTeachers(input!));
    }

    [Fact]
    public void InsertBulkNewTeachers_GetSql_IsExact()
    {
        var req = new InsertBulkNewTeachers(new List<Teachers_Row>());

        const string expected =
            "INSERT INTO dbo.Teachers (TeacherID, FirstName, LastName, SchoolID)" +
            "VALUES (@TeacherID, @FirstName, @LastName, @SchoolID);";

        Assert.Equal(expected, req.GetSql());
    }

    [Fact]
    public void InsertBulkNewTeachers_GetParameters_CountMatches()
    {
        var teachers = new List<Teachers_Row>
        {
            new() { FirstName = "John", LastName = "Doe",      SchoolID = 1 },
            new() { FirstName = "Jane", LastName = "Smith",    SchoolID = 1 },
            new() { FirstName = "Ada",  LastName = "Lovelace", SchoolID = 2 }
        };

        var req = new InsertBulkNewTeachers(teachers);
        var rows = ((IEnumerable<object>)req.GetParameters()!).ToList();

        Assert.Equal(teachers.Count, rows.Count);
    }

    [Fact]
    public void InsertBulkNewTeachers_GetParameters_EmptyList_ReturnsEmpty()
    {
        var req = new InsertBulkNewTeachers(new List<Teachers_Row>());
        var rows = (IEnumerable<object>)req.GetParameters()!;

        Assert.Empty(rows);
    }
}
