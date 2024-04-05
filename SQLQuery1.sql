CREATE TABLE student(
	studentID VARCHAR(255) NOT NULL UNIQUE,
	firstName VARCHAR(255) ,
	lastname VARCHAR(255),
	grade INT,
	school VARCHAR(255),
	gender VARCHAR(255),
	CONSTRAINT STUDENTPK PRIMARY KEY(studentID)
);
SELECT * FROM student;

CREATE TABLE class(
	subjectName VARCHAR(255) NOT NULL,
	grade int,
	teacherName VARCHAR(255),
	price DECIMAL(6,2),
	nameOfDay VARCHAR(255),
	CONSTRAINT CLASSPK PRIMARY KEY (subjectName,grade)
);



CREATE TABLE payment (
    studentID VARCHAR(255),
    year INT,
    month VARCHAR(255),
    subjectName VARCHAR(255),
    grade INT,
    paymentMethod VARCHAR(255),
    timestamp DATETIME DEFAULT GETDATE(), -- Default value set to current time
    CONSTRAINT FK_StudentID FOREIGN KEY (studentID) REFERENCES student(studentID) ON DELETE CASCADE, -- Cascading delete for studentID
    CONSTRAINT FK_Subject_Grade FOREIGN KEY (subjectName, grade) REFERENCES class(subjectName, grade) ON DELETE CASCADE, -- Cascading delete for subjectName and grade
	CONSTRAINT PAYMENTPK PRIMARY KEY (studentID,year,month,subjectName,grade)
);
