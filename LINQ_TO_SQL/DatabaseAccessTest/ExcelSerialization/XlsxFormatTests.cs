using DatabaseAccess.ResultProcessing;
using NUnit.Framework;

namespace DatabaseAccess.ExcelSerialization.Tests
{
    /// <summary>
    /// Defines methods for testing XlsxFormat class.
    /// </summary>
    [TestFixture]
    public class XlsxFormatTests
    {
        /// <summary>
        /// Connection string to the database.
        /// </summary>
        private string _connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\slava\Desktop\Traning\LINQ_TO_SQL\DatabaseAccess\Session.mdf;Integrated Security=True";

        /// <summary>
        /// Saves to the xlsx file average score by spetialty.
        /// </summary>
        [Test]
        public void SaveToFile_AverageScoreInSpecialtySaved()
        {
            var xlsxFormat = new XlsxFormat<IReport>("AverageScoreBySpecialty.xlsx");
            var columnNames = new string[3] { "Year", "Average Mark", "Specialty" };
            var averageScore = new ResultProcessing.ResultProcessing().AverageScoreInSpecialty(new SessionDataContext(_connectionString));
            xlsxFormat.SaveToFile(columnNames, averageScore);
        }

        /// <summary>
        /// Saves to the xlsx file average score by teacher.
        /// </summary>
        [Test]
        public void SaveToFile_AverageScoreByTeacherSaved()
        {
            var xlsxFormat = new XlsxFormat<IReport>("AverageScoreByTeacher.xlsx");
            var columnNames = new string[3] { "Year", "Average Mark", "Teacher" };
            var averageScore = new ResultProcessing.ResultProcessing().AverageScoreByTeacher(new SessionDataContext(_connectionString));
            xlsxFormat.SaveToFile(columnNames, averageScore);
        }

        /// <summary>
        /// Saves to the xlsx file average score by test and exam names.
        /// </summary>
        [Test]
        public void SaveToFile_DynamicsOfTheAverageScoreSaved()
        {
            var xlsxFormat = new XlsxFormat<IReport>("DynamicsOfTheAverageScore.xlsx");
            var columnNames = new string[3] { "Year", "Average Mark", "Subject" };
            var averageScore = new ResultProcessing.ResultProcessing().DynamicsOfTheAverageScore(new SessionDataContext(_connectionString));
            xlsxFormat.SaveToFile(columnNames, averageScore);
        }
    }
}