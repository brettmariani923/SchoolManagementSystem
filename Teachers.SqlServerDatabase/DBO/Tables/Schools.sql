CREATE TABLE dbo.Schools
(
    SchoolID INT IDENTITY(1,1) PRIMARY KEY,
    Name     NVARCHAR(75)  NOT NULL,
    Street1  NVARCHAR(200) NOT NULL,
    Street2  NVARCHAR(200) NULL,
    City     NVARCHAR(50)  NOT NULL,
    State    VARCHAR(50)   NOT NULL,
    CONSTRAINT UQ_School UNIQUE (Name, Street1, City, State)
);
