using Teachers.Data.Requests.Teachers.Insert;
using Xunit;

public class InsertNewTeacherTests
{
    [Fact]
    public void GetParameters_ShouldMapCorrectly()
    {
        var request = new InsertNewTeacher("John", "Doe", 1);

        dynamic p = request.GetParameters(); 
        Assert.Equal("John", (string)p.FirstName);
        Assert.Equal("Doe", (string)p.LastName);
        Assert.Equal(1, (int)p.SchoolID);
    }

    [Fact]
    public void GetSql_ShouldContain_InsertStatement()
    {
        var request = new InsertNewTeacher("John", "Doe", 1);
        var sql = request.GetSql();

        Assert.Contains("INSERT INTO dbo.Teachers", sql, StringComparison.OrdinalIgnoreCase);
        Assert.Contains("(FirstName, LastName, SchoolID)", sql);
        Assert.Contains("VALUES (@FirstName, @LastName, @SchoolID);", sql);
    }
}
