using Teachers.Data.Interfaces;
using Teachers.Data.Rows;

namespace Teachers.Data.Requests.Teachers.Insert
{
    public sealed class InsertNewTeacher : IDataExecute
    {
        private readonly Teachers_Row _row;

        public InsertNewTeacher(Teachers_Row row)
        {
            _row = row ?? throw new ArgumentNullException(nameof(row));

            if (string.IsNullOrWhiteSpace(_row.FirstName))
                throw new ArgumentException("FirstName cannot be null or empty.", nameof(row));
            if (string.IsNullOrWhiteSpace(_row.LastName))
                throw new ArgumentException("LastName cannot be null or empty.", nameof(row));
            if (_row.SchoolID <= 0)
                throw new ArgumentOutOfRangeException(nameof(row.SchoolID), "SchoolID must be positive.");

            _row.FirstName = _row.FirstName.Trim();
            _row.LastName = _row.LastName.Trim();
        }

        public string GetSql() =>
            @"INSERT INTO dbo.Teachers (FirstName, LastName, SchoolID) " +
              "VALUES (@FirstName, @LastName, @SchoolID);";

        public object GetParameters() => new
        {
            _row.FirstName,
            _row.LastName,
            _row.SchoolID
        };
    }
}
