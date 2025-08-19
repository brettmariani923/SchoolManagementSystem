using Teachers.Domain.Interfaces;
using Teachers.Data.DTO;

namespace Teachers.Data.Requests.Teachers
{
    public class RemoveBulkTeachers
    {
        private readonly IEnumerable<Teachers_DTO> _teacherIDs;

        public RemoveBulkTeachers(IEnumerable<Teachers_DTO> teacherIDs)
        {
            _teacherIDs = teacherIDs;
        }

        public string GetSql() =>
    }
}
