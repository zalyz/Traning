using NUnit.Framework;

namespace DatabaseAccess.ResultProcessing.Tests
{
    /// <summary>
    /// Defines methods for testing ResultProcessing class.
    /// </summary>
    [TestFixture]
    public class ResultProcessingTests
    {
        /// <summary>
        /// Connection string to the database.
        /// </summary>
        private string _connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\slava\Desktop\Traning\LINQ_TO_SQL\DatabaseAccess\Session.mdf;Integrated Security=True";

        /// <summary>
        /// Returns average score by spetialty.
        /// </summary>
        [Test]
        public void AverageScoreInSpecialtyTest()
        {
            var context = new SessionDataContext(_connectionString);
            var resultProcessing = new ResultProcessing();
            var averageBySpecialty = resultProcessing.AverageScoreInSpecialty(context);
        }

        /// <summary>
        /// Returns average score by teacher.
        /// </summary>
        [Test]
        public void AverageScoreByTeacherTest()
        {
            var context = new SessionDataContext(_connectionString);
            var resultProcessing = new ResultProcessing();
            var averageByTeacher = resultProcessing.AverageScoreByTeacher(context);
        }

        /// <summary>
        /// Returns average score by all test and exam names.
        /// </summary>
        [Test]
        public void DynamicsOfTheAverageScoreTest()
        {
            var context = new SessionDataContext(_connectionString);
            var resultProcessing = new ResultProcessing();
            var dynamicsOfTheAverage = resultProcessing.DynamicsOfTheAverageScore(context);
        }
    }
}