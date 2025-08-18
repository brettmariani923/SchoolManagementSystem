CREATE TABLE dbo.Students
(
    StudentID INT IDENTITY(1,1) PRIMARY KEY,
    FirstName NVARCHAR(150) NOT NULL,
    LastName  NVARCHAR(150) NOT NULL,
    [Year]    INT NOT NULL,
    SchoolID  INT NOT NULL,
    CONSTRAINT FK_Students_School
        FOREIGN KEY (SchoolID) REFERENCES dbo.School(SchoolID)
);
