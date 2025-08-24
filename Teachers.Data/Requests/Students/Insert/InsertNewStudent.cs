using Teachers.Domain.Interfaces;
using Teachers.Data.Rows;

namespace Teachers.Data.Requests.Students.Insert
{
    public sealed class InsertNewStudent : IDataExecute
    {
        private readonly Students_Row _row;

        public InsertNewStudent(Students_Row row)
        {
            _row = row ?? throw new ArgumentNullException(nameof(row));

            if (string.IsNullOrWhiteSpace(_row.FirstName))
                throw new ArgumentException("FirstName cannot be null or empty.", nameof(row));
            if (string.IsNullOrWhiteSpace(_row.LastName))
                throw new ArgumentException("LastName cannot be null or empty.", nameof(row));
            if (_row.Year <= 0)
                throw new ArgumentOutOfRangeException(nameof(row.Year), "Year must be positive.");
            if (_row.SchoolID <= 0)
                throw new ArgumentOutOfRangeException(nameof(row.SchoolID), "SchoolID must be positive.");

            _row.FirstName = _row.FirstName.Trim();
            _row.LastName = _row.LastName.Trim();
        }

        public string GetSql() =>
            @"INSERT INTO dbo.Students (FirstName, LastName, [Year], SchoolID) " +
              "VALUES (@FirstName, @LastName, @Year, @SchoolID);";

        public object GetParameters() => new
        {
            _row.FirstName,
            _row.LastName,
            _row.Year,
            _row.SchoolID
        };
    }
}
