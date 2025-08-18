CREATE TABLE dbo.Enrollments (
  EnrollmentID INT IDENTITY(1,1) PRIMARY KEY,
  TeacherID INT NOT NULL
    REFERENCES dbo.Teachers(TeacherID),
  StudentID INT NOT NULL
    REFERENCES dbo.Students(StudentID),
  CourseID INT NOT NULL
    REFERENCES dbo.Courses(CourseID),
  SchoolID INT NOT NULL
    REFERENCES dbo.Schools(SchoolID)
);

CREATE UNIQUE INDEX UX_Enrollments_Student_Course
  ON dbo.Enrollments(StudentID, CourseID);
