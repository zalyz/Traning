using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseAccess.ResultProcessing
{
    public class ResultProcessing : IResultProcessing
    {
        /// <inheritdoc/>
        public IEnumerable<(string, double, string)> AverageScoreInSpecialty(SessionDataContext context)
        {
            var listOfTestResults = context.TestResults.ToList();
            var listOfExamResults = context.ExamResults.ToList();
            var averageScoreByTest = AverageScoreOfTestsBy(listOfTestResults, e => e.Test.GroupName);
            var averageScoreByExam = AverageScoreOfExamsBy(listOfExamResults, e => e.Exam.GroupName);
            return SumResultOfTestsAndExams(averageScoreByExam, averageScoreByTest);

        }

        /// <inheritdoc/>
        public IEnumerable<(string, double, string)> AverageScoreByTeacher(SessionDataContext context)
        {
            var listOfTestResults = context.TestResults.ToList();
            var listOfExamResults = context.ExamResults.ToList();

            var averageScoreOfTestsByTeacher = AverageScoreOfTestsBy(listOfTestResults, 
                e => e.Test.Teacher.FirstName + " " + e.Test.Teacher.MiddleName + " " + e.Test.Teacher.LastName);
            var averageScoreOfExamsByTeacher = AverageScoreOfExamsBy(listOfExamResults, 
                e => e.Exam.Teacher.FirstName + " " + e.Exam.Teacher.MiddleName + " " + e.Exam.Teacher.LastName);

            return SumResultOfTestsAndExams(averageScoreOfTestsByTeacher, averageScoreOfExamsByTeacher);
        }

        /// <inheritdoc/>
        public IEnumerable<(string, double, string)> DynamicsOfTheAverageScore(SessionDataContext context)
        {
            var listOfTestResults = context.TestResults.ToList();
            var listOfExamResults = context.ExamResults.ToList();

            var averageScoreOfTests = AverageScoreOfTestsBy(listOfTestResults, e => e.Test.Name);
            var averageScoreOfExams = AverageScoreOfExamsBy(listOfExamResults, e => e.Exam.Name);
            var averageScore = new List<(string, double, string)>(averageScoreOfTests);
            averageScore.AddRange(averageScoreOfExams);

            return averageScore.OrderBy(e => e.Item1).ThenBy(e => e.Item2).ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="firstList"></param>
        /// <param name="secondList"></param>
        /// <returns></returns>
        private IEnumerable<(string, double, string)> SumResultOfTestsAndExams(IEnumerable<(string, double, string)> firstList, IEnumerable<(string, double, string)> secondList)
        {
            if (secondList.Count() > firstList.Count())
            {
                var tmp = firstList;
                firstList = secondList;
                secondList = tmp;
            }

            var averageScoreInSpecialty = new List<(string, double, string)>();
            foreach (var result in firstList)
            {
                var resultFromSecondList = secondList.Where(e => e.Item3 == result.Item3 && e.Item1 == result.Item1);
                if (resultFromSecondList.Any())
                {
                    (string Year, double AverageScore, string Key) score;
                    score.Year = result.Item1;
                    score.AverageScore = (result.Item2 + resultFromSecondList.First().Item2) / 2;
                    score.Key = result.Item3;
                    secondList = secondList.Where(e => e != resultFromSecondList.First());
                    averageScoreInSpecialty.Add(score);
                }
                else
                {
                    averageScoreInSpecialty.Add(result);
                }
            }

            averageScoreInSpecialty.AddRange(secondList);
            return averageScoreInSpecialty.OrderBy(e => e.Item1).ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="testResults"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        private IEnumerable<(string, double, string)> AverageScoreOfTestsBy(IEnumerable<TestResult> testResults, Func<TestResult, string> func)
        {
            var september = 10;
            var april = 4;
            var groupedByYear = testResults.GroupBy(e => e.Test.Date.Value.Year);
            var averageScore = new List<(string, double, string)>();
            foreach (var year in groupedByYear)
            {
                var groupedByMonth = year.GroupBy(
                    e => (
                    (e.Test.Date.Value.Month > september && e.Test.Date.Value.Year == year.Key) || (e.Test.Date.Value.Month < april && e.Test.Date.Value.Year == year.Key + 1)
                    ) || (e.Test.Date.Value.Month > april && e.Test.Date.Value.Month < september));
                foreach (var isWinter in groupedByMonth)
                {
                    var groupedByGroup = isWinter.GroupBy(func);
                    foreach (var group in groupedByGroup)
                    {
                        (string Year, double AverageMark, string Group) score;
                        var timeOfYear = isWinter.Key == true ? "Winter" : "Summer";
                        score.Year = $"{timeOfYear}/{year.Key}";
                        score.AverageMark = group.Sum(e => e.Mark).Value / group.Count();
                        score.Group = group.Key;
                        averageScore.Add(score);
                    }
                }
            }

            return averageScore;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="examResults"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        private IEnumerable<(string, double, string)> AverageScoreOfExamsBy(IEnumerable<ExamResult> examResults, Func<ExamResult, string> func)
        {
            var september = 10;
            var april = 4;
            var groupedByYear = examResults.GroupBy(e => e.Exam.Date.Value.Year);
            var averageScore = new List<(string, double, string)>();
            foreach (var year in groupedByYear)
            {
                var groupedByMonth = year.GroupBy(
                    e => (
                    (e.Exam.Date.Value.Month > september && e.Exam.Date.Value.Year == year.Key) || (e.Exam.Date.Value.Month < april && e.Exam.Date.Value.Year == year.Key + 1)
                    ) || (e.Exam.Date.Value.Month > april && e.Exam.Date.Value.Month < september));
                foreach (var isWinter in groupedByMonth)
                {
                    var groupedByGroup = isWinter.GroupBy(func);
                    foreach (var group in groupedByGroup)
                    {
                        (string Year, double AverageMark, string Group) score;
                        var timeOfYear = isWinter.Key == true ? "Winter" : "Summer";
                        score.Year = $"{timeOfYear}/{year.Key}";
                        score.AverageMark = group.Sum(e => e.Mark).Value / group.Count();
                        score.Group = group.Key;
                        averageScore.Add(score);
                    }
                }
            }

            return averageScore;
        }
    }
}
