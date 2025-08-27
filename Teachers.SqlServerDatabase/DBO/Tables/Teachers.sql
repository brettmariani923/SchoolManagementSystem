CREATE TABLE dbo.Teachers
(
    TeacherID INT IDENTITY(1,1) PRIMARY KEY,
    FirstName NVARCHAR(150) NOT NULL,
    LastName  NVARCHAR(150) NOT NULL,
    SchoolID  INT NOT NULL,
    CONSTRAINT FK_Teachers_School
        FOREIGN KEY (SchoolID) REFERENCES dbo.Schools(SchoolID),
    CONSTRAINT UQ_Teacher UNIQUE (FirstName, LastName, SchoolID)
);
