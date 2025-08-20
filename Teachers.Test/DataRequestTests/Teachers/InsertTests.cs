using System;
using System.Collections.Generic;
using System.Linq;
using Teachers.Data.DTO;
using Teachers.Data.Requests.Teachers.Insert;
using Xunit;

public class InsertTests
{
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
    public void InsertNewTeacher_GetSql_ContainsInsertStatement()
    {
        var req = new InsertNewTeacher("John", "Doe", 1);
        var sql = req.GetSql();

        Assert.Contains("INSERT INTO", sql, StringComparison.OrdinalIgnoreCase);
        Assert.Contains("(FirstName, LastName, SchoolID)", sql);
        Assert.Contains("VALUES (@FirstName, @LastName, @SchoolID);", sql);
    }

    [Fact]
    public void InsertBulkNewTeachers_Ctor_Null_Throws()
    {
        IEnumerable<Teachers_DTO>? input = null;
        Assert.Throws<ArgumentNullException>(() => new InsertBulkNewTeachers(input!));
    }

    [Fact]
    public void InsertBulkNewTeachers_GetSql_ContainsInsertStatement()
    {
        var req = new InsertBulkNewTeachers(new List<Teachers_DTO>());
        var sql = req.GetSql();

        Assert.Contains("INSERT INTO", sql, StringComparison.OrdinalIgnoreCase);
        Assert.Contains("(FirstName, LastName, SchoolID)", sql);
        Assert.Contains("VALUES (@FirstName, @LastName, @SchoolID);", sql);
    }

    [Fact]
    public void InsertBulkNewTeachers_GetParameters_CountMatches()
    {
        var teachers = new List<Teachers_DTO>
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
        var req = new InsertBulkNewTeachers(new List<Teachers_DTO>());
        var rows = (IEnumerable<object>)req.GetParameters()!;

        Assert.Empty(rows);
    }
}
