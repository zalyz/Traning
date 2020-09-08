drop table if exists Exam;
drop table if exists ExamResult;
drop table if exists Student;
drop table if exists Test;
drop table if exists TestResult;

CREATE TABLE Exam (
    ExamId    INT           NULL,
    Name      VARCHAR (100) NULL,
    Date      DATE          NULL,
    GroupName VARCHAR (100) NULL
);

CREATE TABLE ExamResult (
    ExamResultId INT NULL,
    ExamId       INT NULL,
    StudentId    INT NULL,
    Mark         INT NULL
);

CREATE TABLE Student (
    StudentId      INT           NULL,
    FirstName      VARCHAR (100) NULL,
    MiddleName     VARCHAR (100) NULL,
    LastName       VARCHAR (100) NULL,
    Gender         VARCHAR (100) NULL,
    DateOfBirthday DATE          NULL,
    GroupName      VARCHAR (100) NULL
);

CREATE TABLE Test (
    TestId    INT           NULL,
    Name      VARCHAR (100) NULL,
    Date      DATE          NULL,
    GroupName VARCHAR (100) NULL
);

CREATE TABLE TestResult (
    TestResultId INT NULL,
    TestId       INT NULL,
    StudentId    INT NULL,
    Mark         INT NULL
);

INSERT INTO Test (TestId, Name, Date, GroupName)
VALUES (1, 'Philosophy', '22-mar-2019', 'FE-31');

INSERT INTO Test (TestId, Name, Date, GroupName)
VALUES (2, 'Art', '27-mar-2019', 'KS-11');

INSERT INTO Exam (ExamId, Name, Date, GroupName)
VALUES (1, 'Math', '10-may-2020', 'FE-31');

INSERT INTO Exam (ExamId, Name, Date, GroupName)
VALUES (2, 'Programming', '15-may-2020', 'KS-11');

INSERT INTO Student (StudentId, FirstName, MiddleName, LastName, Gender, DateOfBirthday, GroupName)
VALUES (1, 'Abdulov', 'Oleg', 'Aleksandrovich', 'Male', '13-may-2000', 'FE-31');

INSERT INTO Student (StudentId, FirstName, MiddleName, LastName, Gender, DateOfBirthday, GroupName)
VALUES (2, 'Korovai', 'Irina', 'Olegovna', 'Female', '10-may-2001', 'KS-11');

INSERT INTO TestResult (TestResultId, TestId, StudentId, Mark)
VALUES (1, 1, 1, 8);

INSERT INTO TestResult (TestResultId, TestId, StudentId, Mark)
VALUES (2, 2, 2, 5);

INSERT INTO ExamResult (ExamResultId, ExamId, StudentId, Mark)
VALUES (1, 1, 1, 2);

INSERT INTO ExamResult (ExamResultId, ExamId, StudentId, Mark)
VALUES (2, 2, 2, 10);