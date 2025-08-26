using Teachers.Application.DTO;
using Moq;
using Teachers.Domain.Interfaces;
using Teachers.Application.Services;
using Teachers.Data.Rows;

public class StudentServiceTests
{
    private readonly Mock<IDataAccess> _dataAccessMock;
    private readonly StudentService _service;

    public StudentServiceTests()
    {
        _dataAccessMock = new Mock<IDataAccess>();
        _service = new StudentService(_dataAccessMock.Object);
    }

    [Fact]
    public async Task GetByIdAsync_ReturnsNull_WhenRowIsNull()
    {
        _dataAccessMock
            .Setup(x => x.FetchAsync(It.IsAny<IDataFetch<Students_Row>>()))
            .ReturnsAsync((Students_Row)null);

        var result = await _service.GetByIdAsync(1);

        Assert.Null(result);
    }

    [Fact]
    public async Task GetByIdAsync_ReturnsDto_WhenRowExists()
    {
        var row = new Students_Row { StudentID = 1, FirstName = "John", LastName = "Doe", Year = 2, SchoolID = 3 };
        _dataAccessMock
            .Setup(x => x.FetchAsync(It.IsAny<IDataFetch<Students_Row>>()))
            .ReturnsAsync(row);

        var result = await _service.GetByIdAsync(1);

        Assert.NotNull(result);
        Assert.Equal(row.StudentID, result.StudentID);
        Assert.Equal(row.FirstName, result.FirstName);
        Assert.Equal(row.LastName, result.LastName);
        Assert.Equal(row.Year, result.Year);
        Assert.Equal(row.SchoolID, result.SchoolID);
    }

    [Fact]
    public async Task GetAllAsync_ReturnsMappedDtos()
    {
        var rows = new List<Students_Row>
        {
            new Students_Row { StudentID = 1, FirstName = "John", LastName = "Doe", Year = 2, SchoolID = 3 },
            new Students_Row { StudentID = 2, FirstName = "Jane", LastName = "Smith", Year = 3, SchoolID = 4 }
        };
        _dataAccessMock
            .Setup(x => x.FetchListAsync(It.IsAny<IDataFetchList<Students_Row>>()))
            .ReturnsAsync(rows);

        var result = (await _service.GetAllAsync()).ToList();

        Assert.Equal(2, result.Count);
        Assert.Equal("John", result[0].FirstName);
        Assert.Equal("Jane", result[1].FirstName);
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
    public async Task InsertAsync_CallsDataAccess()
    {
        var request = new StudentRequest { FirstName = "John", LastName = "Doe", Year = 2, SchoolID = 3 };
        _dataAccessMock
            .Setup(x => x.ExecuteAsync(It.IsAny<IDataExecute>()))
            .ReturnsAsync(1);

        var result = await _service.InsertAsync(request);

        Assert.Equal(1, result);
    }

    [Fact]
    public async Task UpdateAsync_CallsDataAccess()
    {
        var dto = new Students_DTO { StudentID = 1, FirstName = "John", LastName = "Doe", Year = 2, SchoolID = 3 };
        _dataAccessMock
            .Setup(x => x.ExecuteAsync(It.IsAny<IDataExecute>()))
            .ReturnsAsync(1);

        var result = await _service.UpdateAsync(dto);

        Assert.Equal(1, result);
    }
}