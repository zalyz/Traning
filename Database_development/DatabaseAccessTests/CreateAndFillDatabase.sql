drop table if exists TestResult;
drop table if exists ExamResult;
drop table if exists Exam;
drop table if exists Test;
drop table if exists Student;

CREATE TABLE Exam (
    ExamId    INT           IDENTITY (1, 1) NOT NULL,
    Name      VARCHAR (100) NULL,
    Date      DATE          NULL,
    GroupName VARCHAR (100) NULL,
    PRIMARY KEY CLUSTERED (ExamId ASC),
    CONSTRAINT UC_Exam UNIQUE NONCLUSTERED (Name ASC, Date ASC, GroupName ASC)
);

CREATE TABLE Student (
    StudentId      INT           IDENTITY (1, 1) NOT NULL,
    FirstName      VARCHAR (100) NULL,
    MiddleName     VARCHAR (100) NULL,
    LastName       VARCHAR (100) NULL,
    Gender         VARCHAR (100) NULL,
    DateOfBirthday DATE          NULL,
    GroupName      VARCHAR (100) NULL,
    PRIMARY KEY CLUSTERED (StudentId ASC),
    CONSTRAINT UC_Student UNIQUE NONCLUSTERED (FirstName ASC, MiddleName ASC, LastName ASC, DateOfBirthday ASC)
);

CREATE TABLE ExamResult (
    ExamResultId INT IDENTITY (1, 1) NOT NULL,
    ExamId       INT NULL,
    StudentId    INT NULL,
    Mark         INT NULL,
    PRIMARY KEY CLUSTERED (ExamResultId ASC),
    FOREIGN KEY (ExamId) REFERENCES Exam(ExamId) ON DELETE CASCADE,
    FOREIGN KEY (StudentId) REFERENCES Student(StudentId) ON DELETE CASCADE
);

CREATE TABLE Test (
    TestId    INT           IDENTITY (1, 1) NOT NULL,
    Name      VARCHAR (100) NULL,
    Date      DATE          NULL,
    GroupName VARCHAR (100) NULL,
    PRIMARY KEY CLUSTERED (TestId ASC),
    CONSTRAINT UC_Test UNIQUE NONCLUSTERED (Name ASC, Date ASC, GroupName ASC),
);

CREATE TABLE TestResult (
    TestResultId INT IDENTITY (1, 1) NOT NULL,
    TestId       INT NULL,
    StudentId    INT NULL,
    Mark         INT NULL,
    PRIMARY KEY CLUSTERED (TestResultId ASC),
    FOREIGN KEY (TestId) REFERENCES Test(TestId) ON DELETE CASCADE,
    FOREIGN KEY (StudentId) REFERENCES Student(StudentId) ON DELETE CASCADE
);


INSERT INTO Test (Name, Date, GroupName)
VALUES ('Philosophy', '22-mar-2019', 'FE-31');


INSERT INTO Test (Name, Date, GroupName)
VALUES ('Art', '27-mar-2019', 'KS-11');

INSERT INTO Exam (Name, Date, GroupName)
VALUES ('Math', '10-may-2020', 'FE-31');

INSERT INTO Exam (Name, Date, GroupName)
VALUES ('Programming', '15-may-2020', 'KS-11');


INSERT INTO Student (FirstName, MiddleName, LastName, Gender, DateOfBirthday, GroupName)
VALUES ('Abdulov', 'Oleg', 'Aleksandrovich', 'Male', '13-may-2000', 'FE-31');


INSERT INTO Student (FirstName, MiddleName, LastName, Gender, DateOfBirthday, GroupName)
VALUES ('Korovai', 'Irina', 'Olegovna', 'Female', '10-may-2001', 'KS-11');


INSERT INTO TestResult (TestId, StudentId, Mark)
VALUES (1, 1, 8);


INSERT INTO TestResult (TestId, StudentId, Mark)
VALUES (2, 2, 5);


INSERT INTO ExamResult (ExamId, StudentId, Mark)
VALUES (1, 1, 2);

INSERT INTO ExamResult (ExamId, StudentId, Mark)
VALUES (2, 2, 10);