using Moq;
using Teachers.Domain.Interfaces;
using Teachers.Application.Services;
using Teachers.Data.Rows;
using Teachers.Application.DTO;

public class TeacherServiceTests
{
    private readonly Mock<IDataAccess> _dataAccessMock;
    private readonly TeacherService _service;

    public TeacherServiceTests()
    {
        _dataAccessMock = new Mock<IDataAccess>();
        _service = new TeacherService(_dataAccessMock.Object);
    }

    [Fact]
    public async Task GetByIdAsync_ReturnsNull_WhenRowIsNull()
    {
        _dataAccessMock
            .Setup(x => x.FetchAsync(It.IsAny<IDataFetch<Teachers_Row>>()))
            .ReturnsAsync((Teachers_Row)null);

        var result = await _service.GetByIdAsync(1);

        Assert.Null(result);
    }

    [Fact]
    public async Task GetByIdAsync_ReturnsDto_WhenRowExists()
    {
        var row = new Teachers_Row { TeacherID = 1, FirstName = "Alice", LastName = "Johnson", SchoolID = 2 };
        _dataAccessMock
            .Setup(x => x.FetchAsync(It.IsAny<IDataFetch<Teachers_Row>>()))
            .ReturnsAsync(row);

        var result = await _service.GetByIdAsync(1);

        Assert.NotNull(result);
        Assert.Equal(row.TeacherID, result.TeacherID);
        Assert.Equal(row.FirstName, result.FirstName);
        Assert.Equal(row.LastName, result.LastName);
        Assert.Equal(row.SchoolID, result.SchoolID);
    }

    [Fact]
    public async Task GetAllAsync_ReturnsMappedDtos()
    {
        var rows = new List<Teachers_Row>
        {
            new Teachers_Row { TeacherID = 1, FirstName = "Alice", LastName = "Johnson", SchoolID = 2 },
            new Teachers_Row { TeacherID = 2, FirstName = "Bob", LastName = "Smith", SchoolID = 3 }
        };
        _dataAccessMock
            .Setup(x => x.FetchListAsync(It.IsAny<IDataFetchList<Teachers_Row>>()))
            .ReturnsAsync(rows);

        var result = (await _service.GetAllAsync()).ToList();

        Assert.Equal(2, result.Count);
        Assert.Equal("Alice", result[0].FirstName);
        Assert.Equal("Bob", result[1].FirstName);
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
        var dto = new Teachers_DTO { FirstName = "Alice", LastName = "Johnson", SchoolID = 2 };
        _dataAccessMock
            .Setup(x => x.ExecuteAsync(It.IsAny<IDataExecute>()))
            .ReturnsAsync(1);

        var result = await _service.InsertAsync(dto);

        Assert.Equal(1, result);
    }

    [Fact]
    public async Task UpdateAsync_CallsDataAccess()
    {
        var dto = new Teachers_DTO { TeacherID = 1, FirstName = "Alice", LastName = "Johnson", SchoolID = 2 };
        _dataAccessMock
            .Setup(x => x.ExecuteAsync(It.IsAny<IDataExecute>()))
            .ReturnsAsync(1);

        var result = await _service.UpdateAsync(dto);

        Assert.Equal(1, result);
    }
}