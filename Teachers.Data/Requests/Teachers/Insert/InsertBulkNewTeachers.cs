using System;
using System.Linq;
using Teachers.Domain.Interfaces;
using Teachers.Data.Rows;

namespace Teachers.Data.Requests.Teachers.Insert
{
    public sealed class InsertBulkNewTeachers : IDataExecute
    {
        private readonly IEnumerable<Teachers_Row> _teachers;

        public InsertBulkNewTeachers(IEnumerable<Teachers_Row> teachers)
        {
            _teachers = teachers ?? throw new ArgumentNullException(nameof(teachers));
            if (!_teachers.Any())
                throw new ArgumentException("At least one teacher is required.", nameof(teachers));
        }

        public string GetSql() =>
            @"INSERT INTO dbo.Teachers (FirstName, LastName, SchoolID) " +
             "VALUES (@FirstName, @LastName, @SchoolID);";

        public object? GetParameters() =>
            _teachers.Select(t => new
            {
                t.FirstName,
                t.LastName,
                t.SchoolID
            });
    }
}
