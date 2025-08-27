using Teachers.Application.DTO; 
using Moq;
using Teachers.Domain.Interfaces;
using Teachers.Application.Services;
using Teachers.Data.Rows;

public class CourseServiceTests
{
    private readonly Mock<IDataAccess> _dataAccessMock;
    private readonly CourseService _service;

    public CourseServiceTests()
    {
        _dataAccessMock = new Mock<IDataAccess>();
        _service = new CourseService(_dataAccessMock.Object);
    }

    [Fact]
    public async Task GetByIdAsync_ReturnsNull_WhenRowIsNull()
    {
        _dataAccessMock
            .Setup(x => x.FetchAsync(It.IsAny<IDataFetch<Courses_Row>>()))
            .ReturnsAsync((Courses_Row)null);

        var result = await _service.GetByIdAsync(1);

        Assert.Null(result);
    }

    [Fact]
    public async Task GetByIdAsync_ReturnsDto_WhenRowIsNotNull()
    {
        var row = new Courses_Row { CourseID = 1, CourseName = "Math", Credits = 3, SchoolID = 2 };
        _dataAccessMock
            .Setup(x => x.FetchAsync(It.IsAny<IDataFetch<Courses_Row>>()))
            .ReturnsAsync(row);

        var result = await _service.GetByIdAsync(1);

        Assert.NotNull(result);
        Assert.Equal(row.CourseID, result.CourseID);
        Assert.Equal(row.CourseName, result.CourseName);
        Assert.Equal(row.Credits, result.Credits);
        Assert.Equal(row.SchoolID, result.SchoolID);
    }

    [Fact]
    public async Task GetAllAsync_ReturnsMappedDtos()
    {
        var rows = new List<Courses_Row>
        {
            new Courses_Row { CourseID = 1, CourseName = "Math", Credits = 3, SchoolID = 2 },
            new Courses_Row { CourseID = 2, CourseName = "Science", Credits = 4, SchoolID = 2 }
        };
        _dataAccessMock
            .Setup(x => x.FetchListAsync(It.IsAny<IDataFetchList<Courses_Row>>()))
            .ReturnsAsync(rows);

        var result = (await _service.GetAllAsync()).ToList();

        Assert.Equal(2, result.Count);
        Assert.Equal("Math", result[0].CourseName);
        Assert.Equal("Science", result[1].CourseName);
    }

    [Fact]
    public async Task RemoveByIdAsync_CallsDataAccess()
    {
        _dataAccessMock
            .Setup(x => x.ExecuteAsync(It.IsAny<IDataExecute>()))
            .ReturnsAsync(1);

        var result = await _service.RemoveByIdAsync(1);

        Assert.Equal(1, result);
    }

    [Fact]
    public void Default_Properties_AreInitialized()
    {
        var request = new CourseRequest();
        Assert.Equal("", request.CourseName);
        Assert.Equal(0, request.Credits);
        Assert.Equal(0, request.SchoolID);
    }

    [Fact]
    public void Properties_CanBeSet()
    {
        var request = new CourseRequest
        {
            CourseName = "Physics",
            Credits = 4,
            SchoolID = 2
        };

        Assert.Equal("Physics", request.CourseName);
        Assert.Equal(4, request.Credits);
        Assert.Equal(2, request.SchoolID);
    }

    [Fact]
    public void Default_Properties_Initialized()
    {
        var request = new CourseRequest();
        Assert.Equal("", request.CourseName);
        Assert.Equal(0, request.Credits);
        Assert.Equal(0, request.SchoolID);
    }

    [Fact]
    public void Properties_CanBeSet_Update()
    {
        var request = new CourseRequest
        {
            CourseName = "Math",
            Credits = 3,
            SchoolID = 1
        };

        Assert.Equal("Math", request.CourseName);
        Assert.Equal(3, request.Credits);
        Assert.Equal(1, request.SchoolID);
    }


    [Fact]
    public async Task UpdateAsync_CallsDataAccess_AndReturnsResult()
    {
        var dto = new Courses_DTO { CourseID = 1, CourseName = "Chemistry", Credits = 4, SchoolID = 3 };
        _dataAccessMock
            .Setup(x => x.ExecuteAsync(It.IsAny<IDataExecute>()))
            .ReturnsAsync(1);

        var result = await _service.UpdateAsync(dto);

        Assert.Equal(1, result);
        _dataAccessMock.Verify(x => x.ExecuteAsync(It.IsAny<IDataExecute>()), Times.Once);
    }

    [Fact]
    public async Task RemoveBulkAsync_CallsDataAccess_AndReturnsResult()
    {
        var ids = new List<int> { 1, 2, 3 };
        _dataAccessMock
            .Setup(x => x.ExecuteAsync(It.IsAny<IDataExecute>()))
            .ReturnsAsync(ids.Count);

        var result = await _service.RemoveBulkAsync(ids);

        Assert.Equal(ids.Count, result);
        _dataAccessMock.Verify(x => x.ExecuteAsync(It.IsAny<IDataExecute>()), Times.Once);
    }

    [Fact]
    public async Task UpdateBulkAsync_CallsDataAccess_AndReturnsResult()
    {
        var dtos = new List<Courses_DTO>
        {
            new Courses_DTO { CourseID = 1, CourseName = "Math", Credits = 3, SchoolID = 2 },
            new Courses_DTO { CourseID = 2, CourseName = "Science", Credits = 4, SchoolID = 2 }
        };
        _dataAccessMock
            .Setup(x => x.ExecuteAsync(It.IsAny<IDataExecute>()))
            .ReturnsAsync(dtos.Count);

        var result = await _service.UpdateBulkAsync(dtos);

        Assert.Equal(dtos.Count, result);
        _dataAccessMock.Verify(x => x.ExecuteAsync(It.IsAny<IDataExecute>()), Times.Once);
    }

    [Fact]
    public async Task InsertBulkAsync_CallsDataAccess_AndReturnsResult()
    {
        var dtos = new List<CourseRequest>
        {
            new CourseRequest { CourseName = "Math", Credits = 3, SchoolID = 2 },
            new CourseRequest { CourseName = "Science", Credits = 4, SchoolID = 2 }
        };
        _dataAccessMock
            .Setup(x => x.ExecuteAsync(It.IsAny<IDataExecute>()))
            .ReturnsAsync(dtos.Count);

        var result = await _service.InsertBulkAsync(dtos);

        Assert.Equal(dtos.Count, result);
        _dataAccessMock.Verify(x => x.ExecuteAsync(It.IsAny<IDataExecute>()), Times.Once);
    }

    [Fact]
    public void Default_Properties_Row_AreInitialized()
    {
        var row = new CoursesRequestRow();
        Assert.Equal(null, row.CourseName);
        Assert.Equal(0, row.Credits);
        Assert.Equal(0, row.SchoolID);
    }

    [Fact]
    public void Properties_Row_CanBeSet()
    {
        var row = new CoursesRequestRow
        {
            CourseName = "Physics",
            Credits = 4,
            SchoolID = 7
        };

        Assert.Equal("Physics", row.CourseName);
        Assert.Equal(4, row.Credits);
        Assert.Equal(7, row.SchoolID);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("Advanced Math")]
    public void CourseName_CanBeSetToVariousValues(string name)
    {
        var row = new CoursesRequestRow { CourseName = name };
        Assert.Equal(name, row.CourseName);
    }
}