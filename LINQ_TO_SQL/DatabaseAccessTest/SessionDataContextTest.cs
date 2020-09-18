using System;
using System.Linq;
using DatabaseAccess;
using DatabaseAccess.ScriptExecuter;
using NUnit.Framework;

namespace DatabaseAccessTest
{
    /// <summary>
    /// Defines methods for testing SessionDataContext class.
    /// </summary>
    [TestFixture]
    public class SessionDataContextTest
    {
        /// <summary>
        /// Connection string to the database.
        /// </summary>
        private string _connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\slava\Desktop\Traning\LINQ_TO_SQL\DatabaseAccess\Session.mdf;Integrated Security=True";

        /// <summary>
        /// Creates tables in the database and fill it.
        /// </summary>
        [OneTimeSetUp]
        public void ClassInitialize()
        {
            var filePath = @"C:\Users\slava\Desktop\LINQ_TO_SQL\CreateDatabaseAndFill.sql";
            ScriptExecuter.Execute(filePath, _connectionString);
        }

        /// <summary>
        /// Adds test result to the database.
        /// </summary>
        [TestCase("YAPVU", "21.12.2019", "IP-31", "Shibeko", "Viktor", "Nikolaevich", "Male", "Dranev", "Slava", "Alekseevich", "Male", "7.07.2000", "Semibold")]
        public void Add_TestResultSaved(string testName, string testDate, string groupName, string teacherFName, string teacherMName, string teacherLName, 
            string teacherGender, string studFName, string studMName, string studLName, string studGender, string studBirthday, string groupDescription)
        {
            var database = new SessionDataContext(_connectionString);
            var testResult = new DatabaseAccess.TestResult()
            {
                Test = new Test()
                {
                    Name = testName,
                    Date = DateTime.Parse(testDate),
                    GroupName = groupName,
                    Teacher = new Teacher()
                    {
                        FirstName = teacherFName,
                        MiddleName = teacherMName,
                        LastName = teacherLName,
                        Gender = teacherGender
                    },
                },
                Student = new Student()
                {
                    FirstName = studFName,
                    MiddleName = studMName,
                    LastName = studLName,
                    Gender = studGender,
                    DateOfBirthday = DateTime.Parse(studBirthday),
                    StudentGroup = new StudentGroup()
                    {
                        Name = groupName,
                        Description = groupDescription
                    }
                },
                Mark = 10
            };

            database.TestResults.InsertOnSubmit(testResult);
            database.SubmitChanges();
        }
        
        /// <summary>
        /// Adds exam result to the database.
        /// </summary>
        [Order(1)]
        [TestCase("Math", "29.06.2019", "IK-11", "Avaken", "Viktor", "Sergeevich", "Male", "Golovach", "Andrey", "Ivanovich", "Male", "15.10.2001", "Good choice")]
        public void Add_ExamResultSaved(string examName, string examDate, string groupName, string teacherFName, string teacherMName, string teacherLName, 
            string teacherGender, string studFName, string studMName, string studLName, string studGender, string studBirthday, string groupDescription)
        {
            var database = new SessionDataContext(_connectionString);
            var examResult = new ExamResult()
            {
                Exam = new Exam()
                {
                    Name = examName,
                    Date = DateTime.Parse(examDate),
                    GroupName = groupName,
                    Teacher = new Teacher()
                    {
                        FirstName = teacherFName,
                        MiddleName = teacherMName,
                        LastName = teacherLName,
                        Gender = teacherGender
                    },
                },
                Student = new Student()
                {
                    FirstName = studFName,
                    MiddleName = studMName,
                    LastName = studLName,
                    Gender = studGender,
                    DateOfBirthday = DateTime.Parse(studBirthday),
                    StudentGroup = new StudentGroup()
                    {
                        Name = groupName,
                        Description = groupDescription
                    }
                },
                Mark = 4
            };

            database.ExamResults.InsertOnSubmit(examResult);
            database.SubmitChanges();
        }

        /// <summary>
        /// Reads first test result from the database.
        /// </summary>
        [TestCase("Art", "Korovai", "Irina", "Olegovna")]
        public void Read_TestResultReturned(string testName, string studFName, string studMName, string studLName)
        {
            var dataAccess = new SessionDataContext(_connectionString);
            
            var actualTestResult = dataAccess.TestResults.Where(e => e.Test.Name == testName &&
            e.Student.FirstName == studFName &&
            e.Student.MiddleName == studMName &&
            e.Student.LastName == studLName).FirstOrDefault();
            Assert.IsNotNull(actualTestResult);
        }
        
        /// <summary>
        /// Reads first exam result from the database.
        /// </summary>
        [TestCase("Math", "Korovai", "Irina", "Olegovna")]
        public void Read_ExamResultReturned(string examName, string studFName, string studMName, string studLName)
        {
            var dataAccess = new SessionDataContext(_connectionString);
            
            var actualTestResult = dataAccess.ExamResults.Where(e => e.Exam.Name == examName &&
            e.Student.FirstName == studFName &&
            e.Student.MiddleName == studMName &&
            e.Student.LastName == studLName).FirstOrDefault();
            Assert.IsNotNull(actualTestResult);
        }

        /// <summary>
        /// Updates exam result in the database.
        /// </summary>
        [Test]
        public void Update_ExamResultUpdated()
        {
            var dataAccess = new SessionDataContext(_connectionString);
            
            var examResultForUpdate = dataAccess.ExamResults.FirstOrDefault();
            examResultForUpdate.Mark = 10;
            dataAccess.SubmitChanges();
        }
        
        /// <summary>
        /// Updates test result in the database.
        /// </summary>
        [Test]
        public void Update_TestResultUpdated()
        {
            var dataAccess = new SessionDataContext(_connectionString);
            
            var examResultForUpdate = dataAccess.TestResults.FirstOrDefault();
            examResultForUpdate.Mark = 1;
            dataAccess.SubmitChanges();
        }

        /// <summary>
        /// Deletes exam result from database.
        /// </summary>
        [TestCase("Golovach", "Andrey", "Ivanovich")]
        public void Delete_ExamResultRemoed(string studFName, string studMName, string studLName)
        {
            var dataAccess = new SessionDataContext(_connectionString);

            var examResult = dataAccess.ExamResults.Where(e => e.Student.FirstName == studFName &&
            e.Student.MiddleName == studMName &&
            e.Student.LastName == studLName).FirstOrDefault();
            dataAccess.ExamResults.DeleteOnSubmit(examResult);
            dataAccess.SubmitChanges();
        }
        
        /// <summary>
        /// Deletes test result from database.
        /// </summary>
        [Order(2)]
        [TestCase("Agli", "Semen", "Profilevich")]
        public void Delete_TestResultRemoed(string studFName, string studMName, string studLName)
        {
            var dataAccess = new SessionDataContext(_connectionString);

            var testResult = dataAccess.TestResults.Where(e => e.Student.FirstName == studFName &&
            e.Student.MiddleName == studMName &&
            e.Student.LastName == studLName).FirstOrDefault();
            dataAccess.TestResults.DeleteOnSubmit(testResult);
            dataAccess.SubmitChanges();
        }

        /// <summary>
        /// Adds exam to the database.
        /// </summary>
        [Order(3)]
        [TestCase("MOM", "15.06.2020", "IK-11", 3)]
        public void Add_ExamSaved(string name, string date, string groupName, int teacherId)
        {
            var dataAccess = new SessionDataContext(_connectionString);
            var exam = new Exam()
            {
                Name = name,
                Date = DateTime.Parse(date),
                GroupName = groupName,
                TeacherId = teacherId
            };

            dataAccess.Exams.InsertOnSubmit(exam);
            dataAccess.SubmitChanges();
        }
        
        /// <summary>
        /// Adds test to the database.
        /// </summary>
        [Order(4)]
        [TestCase("ABC", "15.06.2020", "IK-11", 3)]
        public void Add_TestSaved(string name, string date, string groupName, int teacherId)
        {
            var dataAccess = new SessionDataContext(_connectionString);
            var test = new Test()
            {
                Name = name,
                Date = DateTime.Parse(date),
                GroupName = groupName,
                TeacherId = teacherId
            };

            dataAccess.Tests.InsertOnSubmit(test);
            dataAccess.SubmitChanges();
        }

        /// <summary>
        /// Reads exam from the database.
        /// </summary>
        [Test]
        public void Read_ExamReturned()
        {
            var dataAccess = new SessionDataContext(_connectionString);
            var exam = dataAccess.Exams.FirstOrDefault();
            Assert.IsNotNull(exam);
        }
        
        /// <summary>
        /// Reads test from the database.
        /// </summary>
        [Test]
        public void Read_TestReturned()
        {
            var dataAccess = new SessionDataContext(_connectionString);
            var test = dataAccess.Tests.FirstOrDefault();
            Assert.IsNotNull(test);
        }

        /// <summary>
        /// Updates exam from the database.
        /// </summary>
        [Test]
        public void Update_ExamUpdated()
        {
            var dataAccess = new SessionDataContext(_connectionString);
            var exam = dataAccess.Exams.FirstOrDefault();
            exam.Name = "WinApi";
            dataAccess.SubmitChanges();
        }

        /// <summary>
        /// Updates test in the database.
        /// </summary>
        [Test]
        public void Update_TestUpdated()
        {
            var dataAccess = new SessionDataContext(_connectionString);
            var test = dataAccess.Tests.FirstOrDefault();
            test.Name = "Assembler";
            dataAccess.SubmitChanges();
        }

        /// <summary>
        /// Removes exam form the database.
        /// </summary>
        [Test]
        public void Delete_ExamDeleted()
        {
            var dataAccess = new SessionDataContext(_connectionString);
            var exam = dataAccess.Exams.ToList().LastOrDefault();
            dataAccess.Exams.DeleteOnSubmit(exam);
            dataAccess.SubmitChanges();
        }
        
        /// <summary>
        /// Removes test from the database.
        /// </summary>
        [Test]
        public void Delete_TestDeleted()
        {
            var dataAccess = new SessionDataContext(_connectionString);
            var test = dataAccess.Tests.ToList().LastOrDefault();
            dataAccess.Tests.DeleteOnSubmit(test);
            dataAccess.SubmitChanges();
        }

        /// <summary>
        /// Adds student to the database.
        /// </summary>
        [Order(5)]
        [TestCase("Ignatenko", "Stas", "Igorevich", "Middle", "12.07.1995", 3)]
        public void Add_StudentSaved(string studFName, string studMName, string studLName, string gender, string dateOfBirthday, int studGroupId)
        {
            var adataAccess = new SessionDataContext(_connectionString);
            var student = new Student()
            {
                FirstName = studFName,
                MiddleName = studMName,
                LastName = studLName,
                Gender = gender,
                DateOfBirthday = DateTime.Parse(dateOfBirthday),
                StudentGroupId = studGroupId
            };
        }

        /// <summary>
        /// Reads students from the database.
        /// </summary>
        [Test]
        public void Read_StudentReturned()
        {
            var dataAccess = new SessionDataContext(_connectionString);
            var student = dataAccess.Students.ToList().LastOrDefault();
            Assert.IsNotNull(student);
        }

        /// <summary>
        /// Updates students in the database.
        /// </summary>
        [Test]
        public void Update_StudentUpdated()
        {
            var dataAccess = new SessionDataContext(_connectionString);
            var student = dataAccess.Students.ToList().LastOrDefault();
            student.DateOfBirthday = DateTime.Parse("12.07.2001");
            dataAccess.SubmitChanges();
        }

        /// <summary>
        /// Removes student from the database.
        /// </summary>
        [Test]
        public void Delete_StudentDeeted()
        {
            var dataAccess = new SessionDataContext(_connectionString);
            var student = dataAccess.Students.ToList().LastOrDefault();
            dataAccess.Students.DeleteOnSubmit(student);
            dataAccess.SubmitChanges();
        }

        /// <summary>
        /// Adds teacher to the database.
        /// </summary>
        [Order(6)]
        [TestCase("Kosinov", "Gennadi", "Petrovich", "Male")]
        public void Add_TeacherSaved(string firstName, string middleName, string lastName, string gender)
        {
            var dataAccess = new SessionDataContext(_connectionString);
            var teacher = new Teacher()
            {
                FirstName = firstName,
                MiddleName = middleName,
                LastName = lastName,
                Gender = gender
            };
            dataAccess.Teachers.InsertOnSubmit(teacher);
            dataAccess.SubmitChanges();
        }
        
        /// <summary>
        /// Reads teacher from the database.
        /// </summary>
        [Test]
        public void Read_TeacherReturned()
        {
            var dataAccess = new SessionDataContext(_connectionString);
            var teacher = dataAccess.Teachers.FirstOrDefault();
            Assert.IsNotNull(teacher);
        }

        /// <summary>
        /// Updates teacher in the database.
        /// </summary>
        [TestCase("Batkovich")]
        public void Update_TeacherUpdated(string lastName)
        {
            var dataAccess = new SessionDataContext(_connectionString);
            var teacher = dataAccess.Teachers.FirstOrDefault();
            teacher.LastName = lastName;
            dataAccess.SubmitChanges();
        }

        /// <summary>
        /// Remove teacher from the database.
        /// </summary>
        [Test]
        public void Delete_TeacherDeleted()
        {
            var dataAccess = new SessionDataContext(_connectionString);
            var teacher = dataAccess.Teachers.ToList().LastOrDefault();
            dataAccess.Teachers.DeleteOnSubmit(teacher);
            dataAccess.SubmitChanges();
        }

        /// <summary>
        /// Adds student group to the database.
        /// </summary>
        [Order(7)]
        [TestCase("IS-41", "The best")]
        public void Add_StudentGroupSaved(string name, string description)
        {
            var dataAccess = new SessionDataContext(_connectionString);
            var studentGroup = new StudentGroup()
            {
                Name = name,
                Description = description
            };
            dataAccess.StudentGroups.InsertOnSubmit(studentGroup);
            dataAccess.SubmitChanges();
        }

        /// <summary>
        /// Reads studetn group from the database.
        /// </summary>
        [Test]
        public void Read_StudentGroupReturned()
        {
            var dataAccess = new SessionDataContext(_connectionString);
            var studentGroup = dataAccess.StudentGroups.FirstOrDefault();
            Assert.IsNotNull(studentGroup);
        }

        /// <summary>
        /// Updates student group in the database.
        /// </summary>
        [TestCase("Student group")]
        public void Update_StudentGroupUpdated(string description)
        {
            var dataAccess = new SessionDataContext(_connectionString);
            var studentGroup = dataAccess.StudentGroups.ToList().LastOrDefault();
            studentGroup.Description = description;
            dataAccess.SubmitChanges();
        }

        /// <summary>
        /// Removes student group from the database.
        /// </summary>
        [Test]
        public void Delete_StudentGroupDeleted()
        {
            var dataAccess = new SessionDataContext(_connectionString);
            var studentGroup = dataAccess.StudentGroups.ToList().LastOrDefault();
            dataAccess.StudentGroups.DeleteOnSubmit(studentGroup);
            dataAccess.SubmitChanges();
        }
    }
}
