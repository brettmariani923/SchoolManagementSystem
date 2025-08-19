using Teachers.Data.Requests.Teachers.Insert;
using Teachers.Data.DTO;

public class InsertTests
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

    [Fact]
    public void Ctor_GivenNullTeachers_Throws()
    {
        // Arrange
        IEnumerable<Teachers_DTO>? input = null;

        // Act
        var ex = Assert.Throws<ArgumentNullException>(() => new InsertBulkNewTeachers(input!));

        // Assert
        Assert.Equal("teachers", ex.ParamName);
    }

    [Fact]
    public void GetSql_ShouldReturnExpectedInsertStatement()
    {
        // Arrange
        var sut = new InsertBulkNewTeachers(new List<Teachers_DTO>());

        // Act
        var sql = sut.GetSql();

        // Assert
        var expected =
            @"INSERT INTO Teachers (FirstName, LastName, SchoolID)
                  VALUES (@FirstName, @LastName, @SchoolID);";

        string Normalize(string s) => new string(s.Where(c => !char.IsWhiteSpace(c)).ToArray());
        Assert.Equal(Normalize(expected), Normalize(sql));
    }

    [Fact]
    public void GetParameters_ShouldProjectAllFields_ForEachTeacher()
    {
        // Arrange
        var teachers = new List<Teachers_DTO>
            {
                new() { FirstName = "John", LastName = "Doe",     SchoolID = 1 },
                new() { FirstName = "Jane", LastName = "Smith",   SchoolID = 1 },
                new() { FirstName = "Ada",  LastName = "Lovelace",SchoolID = 2 }
            };
        var sut = new InsertBulkNewTeachers(teachers);

        // Act
        var enumerable = sut.GetParameters() as IEnumerable<object>;
        Assert.NotNull(enumerable);

        var parameters = enumerable!.ToArray();

        // Assert
        Assert.Equal(teachers.Count, parameters.Length);

        // Assert
        for (int i = 0; i < teachers.Count; i++)
        {
            var p = parameters[i];
            var t = teachers[i];

            var first = p.GetType().GetProperty("FirstName")!.GetValue(p);
            var last = p.GetType().GetProperty("LastName")!.GetValue(p);
            var sid = p.GetType().GetProperty("SchoolID")!.GetValue(p);

            Assert.Equal(t.FirstName, first);
            Assert.Equal(t.LastName, last);
            Assert.Equal(t.SchoolID, sid);
        }
    }

    [Fact]
    public void GetParameters_GivenEmptyList_ReturnsEmptySequence()
    {
        // Arrange
        var sut = new InsertBulkNewTeachers(new List<Teachers_DTO>());

        // Act
        var enumerable = sut.GetParameters() as IEnumerable<object>;

        // Assert
        Assert.NotNull(enumerable);
        Assert.Empty(enumerable!);
    }
}
