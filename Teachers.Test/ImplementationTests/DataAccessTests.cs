using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Data.Sqlite;
using Teachers.Domain.Implementation;
using Teachers.Domain.Interfaces;
using Xunit;

namespace Teachers.Test.ImplementationTests
{
    /// <summary>
    /// Test-only SQLite connection factory that returns NEW connections
    /// to the same on-disk database file (so the schema persists across connections).
    /// </summary>
    internal sealed class TestSqliteConnectionFactory : IDbConnectionFactory
    {
        private readonly string _connectionString;

        public TestSqliteConnectionFactory(string dbFilePath)
        {
            // Use a file-based SQLite DB so multiple connections see the same schema/data.
            _connectionString = $"Data Source={dbFilePath};Cache=Shared";
        }

        public IDbConnection NewConnection() => new SqliteConnection(_connectionString);
    }

    public sealed class DataAccessTests : IDisposable
    {
        private readonly string _dbPath;
        private readonly IDbConnectionFactory _factory;
        private readonly DataAccess _data;

        public DataAccessTests()
        {
            // Create a unique temp file for this test class.
            _dbPath = Path.Combine(Path.GetTempPath(), $"teachers-tests-{Guid.NewGuid():N}.db");

            _factory = new TestSqliteConnectionFactory(_dbPath);
            _data = new DataAccess(_factory);

            InitializeSchemaAndSeed();
        }

        public void Dispose()
        {
            try
            {
                if (File.Exists(_dbPath))
                {
                    // Best-effort cleanup
                    File.Delete(_dbPath);
                }
            }
            catch { /* ignore */ }
        }

        private void InitializeSchemaAndSeed()
        {
            using var conn = _factory.NewConnection();
            conn.Open();

            conn.Execute(@"
                CREATE TABLE IF NOT EXISTS Courses(
                    CourseID   INTEGER PRIMARY KEY AUTOINCREMENT,
                    CourseName TEXT NOT NULL,
                    Credits    INTEGER NOT NULL,
                    SchoolID   INTEGER NOT NULL
                );

                DELETE FROM Courses; -- keep tests deterministic

                INSERT INTO Courses (CourseName, Credits, SchoolID)
                VALUES ('Algebra I', 3, 1),
                       ('Biology',   4, 2),
                       ('Chemistry', 4, 2);
            ");
        }

        // --------------- Basic ctor guard ---------------

        [Fact]
        public void Ctor_WithNullFactory_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new DataAccess(null!));
        }

        // --------------- ExecuteAsync ---------------

        private sealed class UpdateCreditsRequest : IDataExecute
        {
            private readonly int _courseId;
            private readonly int _newCredits;

            public UpdateCreditsRequest(int courseId, int newCredits)
            {
                _courseId = courseId;
                _newCredits = newCredits;
            }

            public string GetSql() =>
                "UPDATE Courses SET Credits = @Credits WHERE CourseID = @CourseID;";

            public object GetParameters() => new { Credits = _newCredits, CourseID = _courseId };
        }

        [Fact]
        public async Task ExecuteAsync_UpdatesExactlyOneRow()
        {
            // Arrange: update CourseID = 1
            var req = new UpdateCreditsRequest(courseId: 1, newCredits: 5);

            // Act
            var affected = await _data.ExecuteAsync(req);

            // Assert
            Assert.Equal(1, affected);

            using var conn = _factory.NewConnection();
            var credits = await conn.ExecuteScalarAsync<int>("SELECT Credits FROM Courses WHERE CourseID = 1;");
            Assert.Equal(5, credits);
        }

        // --------------- FetchAsync<T> ---------------

        private sealed class SelectCourseNameById : IDataFetch<string>
        {
            private readonly int _courseId;
            public SelectCourseNameById(int courseId) => _courseId = courseId;

            public string GetSql() => "SELECT CourseName FROM Courses WHERE CourseID = @CourseID;";
            public object GetParameters() => new { CourseID = _courseId };
        }

        [Fact]
        public async Task FetchAsync_ReturnsFirstRowOrNull()
        {
            var hitReq = new SelectCourseNameById(1);
            var missReq = new SelectCourseNameById(999);

            var hit = await _data.FetchAsync(hitReq);
            var miss = await _data.FetchAsync(missReq);

            Assert.Equal("Algebra I", hit);
            Assert.Null(miss);
        }

        // --------------- FetchListAsync<T> ---------------

        private sealed class SelectCourseNamesBySchool : IDataFetchList<string>
        {
            private readonly int _schoolId;
            public SelectCourseNamesBySchool(int schoolId) => _schoolId = schoolId;

            public string GetSql() =>
                "SELECT CourseName FROM Courses WHERE SchoolID = @SchoolID ORDER BY CourseID;";

            public object GetParameters() => new { SchoolID = _schoolId };
        }

        [Fact]
        public async Task FetchListAsync_ReturnsAllMatchingRows()
        {
            var req = new SelectCourseNamesBySchool(2);

            var rows = await _data.FetchListAsync(req);

            Assert.Collection(rows,
                s => Assert.Equal("Biology", s),
                s => Assert.Equal("Chemistry", s));
        }

        // --------------- Exception propagation ---------------

        private sealed class FailingDataExecute : IDataExecute
        {
            public string GetSql() => throw new Exception("Failing GetSql");
            public object GetParameters() => throw new Exception("Failing GetParameters");
        }

        [Fact]
        public async Task HandleRequest_PropagatesException_FromRequestObject()
        {
            await Assert.ThrowsAsync<Exception>(() => _data.ExecuteAsync(new FailingDataExecute()));
        }
    }
}
