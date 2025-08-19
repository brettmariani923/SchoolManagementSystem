CREATE TABLE dbo.Courses
(
    CourseID   INT IDENTITY(1,1) PRIMARY KEY,
    CourseName NVARCHAR(100) NOT NULL,
    Credits    INT NOT NULL,
    SchoolID   INT NOT NULL,
    CONSTRAINT FK_Course_School 
        FOREIGN KEY (SchoolID) REFERENCES dbo.School(SchoolID)
);
