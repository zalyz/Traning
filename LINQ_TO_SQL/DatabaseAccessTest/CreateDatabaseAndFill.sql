drop table if exists ExamResult;
drop table if exists TestResult;
drop table if exists Exam;
drop table if exists Test;
drop table if exists Teacher;
drop table if exists Student;
drop table if exists StudentGroup;

CREATE TABLE StudentGroup (
    Id          INT           IDENTITY (1, 1) NOT NULL,
    Name        VARCHAR (30)  NOT NULL,
    Description VARCHAR (200) NOT NULL,
    PRIMARY KEY CLUSTERED (Id ASC),
    UNIQUE NONCLUSTERED (Name ASC)
);

CREATE TABLE Student (
    StudentId      INT           IDENTITY (1, 1) NOT NULL,
    FirstName      VARCHAR (100) NULL,
    MiddleName     VARCHAR (100) NULL,
    LastName       VARCHAR (100) NULL,
    Gender         VARCHAR (100) NULL,
    DateOfBirthday DATE          NULL,
    StudentGroupId INT           NOT NULL,
    PRIMARY KEY CLUSTERED (StudentId ASC),
    CONSTRAINT UC_Student UNIQUE NONCLUSTERED (FirstName ASC, MiddleName ASC, LastName ASC, DateOfBirthday ASC),
    FOREIGN KEY (StudentGroupId) REFERENCES StudentGroup(Id) ON DELETE CASCADE
);

CREATE TABLE Teacher (
    Id         INT          IDENTITY (1, 1) NOT NULL,
    FirstName  VARCHAR (30) NULL,
    MiddleName VARCHAR (30) NOT NULL,
    LastName   VARCHAR (30) NULL,
    Gender     VARCHAR (20) NULL,
    PRIMARY KEY CLUSTERED (Id ASC),
    CONSTRAINT UC_Teacher UNIQUE NONCLUSTERED (FirstName ASC, MiddleName ASC, LastName ASC)
);

CREATE TABLE Exam (
    ExamId    INT           IDENTITY (1, 1) NOT NULL,
    Name      VARCHAR (100) NULL,
    Date      DATE          NULL,
    GroupName VARCHAR (100) NULL,
    TeacherId INT           NOT NULL,
    PRIMARY KEY CLUSTERED (ExamId ASC),
    CONSTRAINT UC_Exam UNIQUE NONCLUSTERED (Name ASC, Date ASC, GroupName ASC),
    FOREIGN KEY (TeacherId) REFERENCES Teacher(Id) ON DELETE CASCADE
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
    TeacherId INT           NOT NULL,
    PRIMARY KEY CLUSTERED (TestId ASC),
    CONSTRAINT UC_Test UNIQUE NONCLUSTERED (Name ASC, Date ASC, GroupName ASC),
    FOREIGN KEY (TeacherId) REFERENCES Teacher(Id) ON DELETE CASCADE
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

INSERT INTO StudentGroup(Name, Description)
VALUES ('PI-21', 'FAIS');

INSERT INTO StudentGroup(Name, Description)
VALUES ('KS-41', 'FAIS');

INSERT INTO Student (FirstName, MiddleName, LastName, Gender, DateOfBirthday, StudentGroupId)
VALUES ('Korovai', 'Irina', 'Olegovna', 'Female', '10-may-2001', 1);

INSERT INTO Student (FirstName, MiddleName, LastName, Gender, DateOfBirthday, StudentGroupId)
VALUES ('Abdulov', 'Oleg', 'Aleksandrovich', 'Male', '13-may-2000', 2);

INSERT INTO Student (FirstName, MiddleName, LastName, Gender, DateOfBirthday, StudentGroupId)
VALUES ('Agli', 'Semen', 'Profilevich', 'Male', '10-june-2000', 2);

INSERT INTO Student (FirstName, MiddleName, LastName, Gender, DateOfBirthday, StudentGroupId)
VALUES ('Kulbit', 'Sasha', 'Vladivirovich', 'Male', '13-dec-1999', 1);

INSERT INTO Teacher (FirstName, MiddleName, LastName, Gender)
VALUES ('Mocart', 'Amadey', 'Olegovich', 'Male');

INSERT INTO Teacher (FirstName, MiddleName, LastName, Gender)
VALUES ('Chaikovski', 'Petr', 'Ilich', 'Male');

INSERT INTO Test (Name, Date, GroupName, TeacherId)
VALUES ('Art', '27-may-2019', 'IP-21', 1);

INSERT INTO Test (Name, Date, GroupName, TeacherId)
VALUES ('Art', '27-may-2020', 'IP-21', 2);

INSERT INTO Exam (Name, Date, GroupName, TeacherId)
VALUES ('Math', '10-june-2019', 'IP-21', 1);

INSERT INTO Exam (Name, Date, GroupName, TeacherId)
VALUES ('Programming', '15-june-2020', 'KS-41', 2);

INSERT INTO TestResult (TestId, StudentId, Mark)
VALUES (1, 1, 7);

INSERT INTO TestResult (TestId, StudentId, Mark)
VALUES (1, 2, 6);

INSERT INTO TestResult (TestId, StudentId, Mark)
VALUES (2, 3, 5);

INSERT INTO TestResult (TestId, StudentId, Mark)
VALUES (1, 4, 8);

INSERT INTO ExamResult (ExamId, StudentId, Mark)
VALUES (1, 1, 5);

INSERT INTO ExamResult (ExamId, StudentId, Mark)
VALUES (2, 2, 2);

INSERT INTO ExamResult (ExamId, StudentId, Mark)
VALUES (1, 3, 6);

INSERT INTO ExamResult (ExamId, StudentId, Mark)
VALUES (2, 3, 7);

INSERT INTO ExamResult (ExamId, StudentId, Mark)
VALUES (2, 4, 8);