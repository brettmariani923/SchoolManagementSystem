using System;
using System.Collections.Generic;
using System.Linq;
using Teachers.Data.Rows;
using Teachers.Data.Requests.Teachers.Insert;
using Xunit;

public class InsertTests
{
    private const string ExpectedSql =
        "INSERT INTO dbo.Teachers (FirstName, LastName, SchoolID) " +
         "VALUES (@FirstName, @LastName, @SchoolID);";

    [Fact]
    public void InsertNewTeacher_GetSql_IsExact()
    {
        var row = new Teachers_Row { FirstName = "John", LastName = "Doe", SchoolID = 1 };
        var req = new InsertNewTeacher(row);

        Assert.Equal(ExpectedSql, req.GetSql());
    }

    [Fact]
    public void InsertNewTeacher_GetParameters_ProjectsFields()
    {
        var row = new Teachers_Row { FirstName = "John", LastName = "Doe", SchoolID = 1 };
        var req = new InsertNewTeacher(row);

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
        var teachers = new[]
        {
            new Teachers_Row { FirstName = "John", LastName = "Doe", SchoolID = 1 }
        };

        var req = new InsertBulkNewTeachers(teachers);

        Assert.Equal(ExpectedSql, req.GetSql());
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
        var list = ((IEnumerable<object>)req.GetParameters()!).ToList();

        Assert.Equal(teachers.Count, list.Count);
    }

    [Fact]
    public void InsertBulkNewTeachers_EmptyList_Throws()
    {
        var empty = new List<Teachers_Row>();
        Assert.Throws<ArgumentException>(() => new InsertBulkNewTeachers(empty));
    }
}

