using Microsoft.Data.Sqlite;
using Dapper;
using Teachers.Data.Requests.Teachers.Insert;

public class InsertNewTeacher_IntegrationTests
{
    [Fact]
    public async Task InsertNewTeacher_Should_Insert_Row()
    {
        using var conn = new SqliteConnection("Data Source=:memory:");
        await conn.OpenAsync();

        await conn.ExecuteAsync(@"
            CREATE TABLE Teachers (
                TeacherID INTEGER PRIMARY KEY AUTOINCREMENT,
                FirstName TEXT NOT NULL,
                LastName  TEXT NOT NULL,
                SchoolID  INTEGER NOT NULL
            );");

        var request = new InsertNewTeacher("John", "Doe", 1);

        var affected = await conn.ExecuteAsync(request.GetSql(), request.GetParameters());
        Assert.Equal(1, affected);

        var row = await conn.QuerySingleAsync<(string FirstName, string LastName, int SchoolID)>(
            "SELECT FirstName, LastName, SchoolID FROM Teachers LIMIT 1;");
        Assert.Equal(("John", "Doe", 1), row);
    }
}
