using Moq;
using Teachers.Domain.Interfaces;
using Teachers.Application.Services;
using Teachers.Data.Rows;
using Teachers.Application.DTO;

public class EnrollmentServiceTests
{
    private readonly Mock<IDataAccess> _dataAccessMock;
    private readonly EnrollmentService _service;

    public EnrollmentServiceTests()
    {
        _dataAccessMock = new Mock<IDataAccess>();
        _service = new EnrollmentService(_dataAccessMock.Object);
    }

    [Fact]
    public async Task GetByIdAsync_ReturnsNull_WhenNoRows()
    {
        _dataAccessMock
            .Setup(x => x.FetchListAsync(It.IsAny<IDataFetchList<Enrollments_Row>>()))
            .ReturnsAsync(new List<Enrollments_Row>());

        var result = await _service.GetByIdAsync(1);

        Assert.Null(result);
    }

    [Fact]
    public async Task GetByIdAsync_ReturnsDto_WhenRowExists()
    {
        var row = new Enrollments_Row { EnrollmentID = 1, StudentID = 2, TeacherID = 3, CourseID = 4, SchoolID = 5 };
        _dataAccessMock
            .Setup(x => x.FetchListAsync(It.IsAny<IDataFetchList<Enrollments_Row>>()))
            .ReturnsAsync(new List<Enrollments_Row> { row });

        var result = await _service.GetByIdAsync(1);

        Assert.NotNull(result);
        Assert.Equal(row.EnrollmentID, result.EnrollmentID);
        Assert.Equal(row.StudentID, result.StudentID);
        Assert.Equal(row.TeacherID, result.TeacherID);
        Assert.Equal(row.CourseID, result.CourseID);
        Assert.Equal(row.SchoolID, result.SchoolID);
    }

    [Fact]
    public async Task GetAllAsync_ReturnsMappedDtos()
    {
        var rows = new List<Enrollments_Row>
        {
            new Enrollments_Row { EnrollmentID = 1, StudentID = 2, TeacherID = 3, CourseID = 4, SchoolID = 5 },
            new Enrollments_Row { EnrollmentID = 2, StudentID = 3, TeacherID = 4, CourseID = 5, SchoolID = 6 }
        };
        _dataAccessMock
            .Setup(x => x.FetchListAsync(It.IsAny<IDataFetchList<Enrollments_Row>>()))
            .ReturnsAsync(rows);

        var result = (await _service.GetAllAsync()).ToList();

        Assert.Equal(2, result.Count);
        Assert.Equal(2, result[0].StudentID);
        Assert.Equal(3, result[1].StudentID);
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
        var dto = new Enrollments_DTO { StudentID = 2, TeacherID = 3, CourseID = 4, SchoolID = 5 };
        _dataAccessMock
            .Setup(x => x.ExecuteAsync(It.IsAny<IDataExecute>()))
            .ReturnsAsync(1);

        var result = await _service.InsertAsync(dto);

        Assert.Equal(1, result);
    }

    [Fact]
    public async Task UpdateAsync_CallsDataAccess()
    {
        var dto = new Enrollments_DTO { EnrollmentID = 1, StudentID = 2, TeacherID = 3, CourseID = 4, SchoolID = 5 };
        _dataAccessMock
            .Setup(x => x.ExecuteAsync(It.IsAny<IDataExecute>()))
            .ReturnsAsync(1);

        var result = await _service.UpdateAsync(dto);

        Assert.Equal(1, result);
    }

    [Fact]
    public async Task GetAllAsync_ReturnsEmpty_WhenNoRows()
    {
        _dataAccessMock
            .Setup(x => x.FetchListAsync(It.IsAny<IDataFetchList<Enrollments_Row>>()))
            .ReturnsAsync(new List<Enrollments_Row>());

        var result = (await _service.GetAllAsync()).ToList();

        Assert.Empty(result);
    }

    [Fact]
    public async Task GetByStudentIdAsync_ReturnsMappedDtos()
    {
        var rows = new List<Enrollments_Row>
        {
            new Enrollments_Row { EnrollmentID = 1, StudentID = 2, TeacherID = 3, CourseID = 4, SchoolID = 5 },
            new Enrollments_Row { EnrollmentID = 2, StudentID = 2, TeacherID = 4, CourseID = 5, SchoolID = 6 }
        };
        _dataAccessMock
            .Setup(x => x.FetchListAsync(It.IsAny<IDataFetchList<Enrollments_Row>>()))
            .ReturnsAsync(rows);

        var result = (await _service.GetByStudentIdAsync(2)).ToList();

        Assert.Equal(2, result.Count);
        Assert.All(result, r => Assert.Equal(2, r.StudentID));
    }

    [Fact]
    public async Task GetByCourseIdAsync_ReturnsMappedDtos()
    {
        var rows = new List<Enrollments_Row>
        {
            new Enrollments_Row { EnrollmentID = 1, StudentID = 2, TeacherID = 3, CourseID = 4, SchoolID = 5 },
            new Enrollments_Row { EnrollmentID = 2, StudentID = 3, TeacherID = 4, CourseID = 4, SchoolID = 6 }
        };
        _dataAccessMock
            .Setup(x => x.FetchListAsync(It.IsAny<IDataFetchList<Enrollments_Row>>()))
            .ReturnsAsync(rows);

        var result = (await _service.GetByCourseIdAsync(4)).ToList();

        Assert.Equal(2, result.Count);
        Assert.All(result, r => Assert.Equal(4, r.CourseID));
    }

    [Fact]
    public async Task GetByTeacherIdAsync_ReturnsMappedDtos()
    {
        var rows = new List<Enrollments_Row>
        {
            new Enrollments_Row { EnrollmentID = 1, StudentID = 2, TeacherID = 3, CourseID = 4, SchoolID = 5 },
            new Enrollments_Row { EnrollmentID = 2, StudentID = 3, TeacherID = 3, CourseID = 5, SchoolID = 6 }
        };
        _dataAccessMock
            .Setup(x => x.FetchListAsync(It.IsAny<IDataFetchList<Enrollments_Row>>()))
            .ReturnsAsync(rows);

        var result = (await _service.GetByTeacherIdAsync(3)).ToList();

        Assert.Equal(2, result.Count);
        Assert.All(result, r => Assert.Equal(3, r.TeacherID));
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
        var dtos = new List<Enrollments_DTO>
        {
            new Enrollments_DTO { EnrollmentID = 1, StudentID = 2, TeacherID = 3, CourseID = 4, SchoolID = 5 },
            new Enrollments_DTO { EnrollmentID = 2, StudentID = 3, TeacherID = 4, CourseID = 5, SchoolID = 6 }
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
        var studentIds = new List<int> { 2, 3 };
        int teacherId = 4, courseId = 5, schoolId = 6;
        _dataAccessMock
            .Setup(x => x.ExecuteAsync(It.IsAny<IDataExecute>()))
            .ReturnsAsync(studentIds.Count);

        var result = await _service.InsertBulkAsync(studentIds, teacherId, courseId, schoolId);

        Assert.Equal(studentIds.Count, result);
        _dataAccessMock.Verify(x => x.ExecuteAsync(It.IsAny<IDataExecute>()), Times.Once);
    }

    [Fact]
    public async Task GetByStudentIdAsync_ReturnsEmpty_WhenNoRows()
    {
        _dataAccessMock
            .Setup(x => x.FetchListAsync(It.IsAny<IDataFetchList<Enrollments_Row>>()))
            .ReturnsAsync(new List<Enrollments_Row>());

        var result = (await _service.GetByStudentIdAsync(999)).ToList();

        Assert.Empty(result);
    }

    [Fact]
    public async Task GetByCourseIdAsync_ReturnsEmpty_WhenNoRows()
    {
        _dataAccessMock
            .Setup(x => x.FetchListAsync(It.IsAny<IDataFetchList<Enrollments_Row>>()))
            .ReturnsAsync(new List<Enrollments_Row>());

        var result = (await _service.GetByCourseIdAsync(999)).ToList();

        Assert.Empty(result);
    }

    [Fact]
    public async Task GetByTeacherIdAsync_ReturnsEmpty_WhenNoRows()
    {
        _dataAccessMock
            .Setup(x => x.FetchListAsync(It.IsAny<IDataFetchList<Enrollments_Row>>()))
            .ReturnsAsync(new List<Enrollments_Row>());

        var result = (await _service.GetByTeacherIdAsync(999)).ToList();

        Assert.Empty(result);
    }

}