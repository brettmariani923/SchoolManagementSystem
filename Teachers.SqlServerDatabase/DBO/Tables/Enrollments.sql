CREATE TABLE [dbo].[Enrollments] (
	EnrollmentID INT PRIMARY KEY NOT NULL,
	TeacherID INT FOREIGN KEY (TeacherID) REFERENCES TeacherID (TeacherID),
	StudentID INT FOREIGN KEY (StudentID) REFERENCES StudentID (StudentID),
	CourseID INT FOREIGN KEY (CourseID) REFERENCES CourseID (CourseID),
	SchoolID INT FOREIGN KEY (SchoolID) REFERENCES SchoolID (SchoolID),
	);
