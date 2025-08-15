CREATE TABLE [dbo].[Students] (
	StudentID INT PRIMARY KEY,
	FirstName NVARCHAR (150) NOT NULL,
	LastName NVarChar(150) NOT NULL,
	Year INT NOT NULL,
	SchoolID INT FOREIGN KEY (SchoolID) REFERENCES School(SchoolID),
	);