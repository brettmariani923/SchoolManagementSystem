using Teachers.Domain.Interfaces;

namespace Teachers.Data.Requests.Teachers
{
    public class RemoveBulkTeachers : IDataExecute
    {
        private readonly int[] _teacherIDs;

        public RemoveBulkTeachers(IEnumerable<int> teacherIds)
        {
            if (teacherIds is null) throw new ArgumentNullException(nameof(teacherIds));
            _teacherIDs = teacherIds.Distinct().ToArray();
            if (_teacherIDs.Length == 0)
                throw new ArgumentException("At least one TeacherID is required.", nameof(teacherIds));
        }

        public string GetSql() =>
            @"DELETE FROM dbo.Teachers
              WHERE TeacherID IN @TeacherIDs;";

        public object? GetParameters() => new { TeacherIDs = _teacherIDs };
    }
}
