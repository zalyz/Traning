using System;
using System.Collections.Generic;
using System.Linq;

namespace DatabaseAccess.ResultProcessing
{
    /// <summary>
    /// Defines methods for processing test and exam results.
    /// </summary>
    public class ResultProcessing : IResultProcessing
    {
        /// <inheritdoc/>
        public IEnumerable<IReport> AverageScoreInSpecialty(SessionDataContext context)
        {
            var listOfTestResults = context.TestResults.ToList();
            var listOfExamResults = context.ExamResults.ToList();
            var averageScoreByTest = AverageScoreOfTestsBy(listOfTestResults, e => e.Test.GroupName);
            var averageScoreByExam = AverageScoreOfExamsBy(listOfExamResults, e => e.Exam.GroupName);
            return SumResultOfTestsAndExams(averageScoreByExam, averageScoreByTest);

        }

        /// <inheritdoc/>
        public IEnumerable<IReport> AverageScoreByTeacher(SessionDataContext context)
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
        public IEnumerable<IReport> DynamicsOfTheAverageScore(SessionDataContext context)
        {
            var listOfTestResults = context.TestResults.ToList();
            var listOfExamResults = context.ExamResults.ToList();

            var averageScoreOfTests = AverageScoreOfTestsBy(listOfTestResults, e => e.Test.Name);
            var averageScoreOfExams = AverageScoreOfExamsBy(listOfExamResults, e => e.Exam.Name);
            var averageScore = new List<IReport>(averageScoreOfTests);
            averageScore.AddRange(averageScoreOfExams);

            return averageScore.OrderBy(e => e.Criteria).ThenBy(e => e.SessionName).ToList();
        }

        /// <summary>
        /// Sums two collection of average score.
        /// </summary>
        /// <param name="firstList"></param>
        /// <param name="secondList"></param>
        /// <returns>Sum of two collection.</returns>
        private IEnumerable<IReport> SumResultOfTestsAndExams(IEnumerable<IReport> firstList, IEnumerable<IReport> secondList)
        {
            if (secondList.Count() > firstList.Count())
            {
                var tmp = firstList;
                firstList = secondList;
                secondList = tmp;
            }

            var averageScoreInSpecialty = new List<IReport>();
            foreach (var result in firstList)
            {
                var resultFromSecondList = secondList.Where(e => e.Criteria == result.Criteria && e.SessionName == result.SessionName);
                if (resultFromSecondList.Any())
                {
                    IReport score = new SessionReport();
                    score.SessionName = result.SessionName;
                    score.AverageMark = (result.AverageMark + resultFromSecondList.First().AverageMark) / 2;
                    score.Criteria = result.Criteria;
                    secondList = secondList.Where(e => e != resultFromSecondList.First());
                    averageScoreInSpecialty.Add(score);
                }
                else
                {
                    averageScoreInSpecialty.Add(result);
                }
            }

            averageScoreInSpecialty.AddRange(secondList);
            return averageScoreInSpecialty.OrderBy(e => e.SessionName).ToList();
        }

        /// <summary>
        /// Returns average score of tests.
        /// </summary>
        /// <param name="testResults">collection of test results.</param>
        /// <param name="func">The criterion for selection.</param>
        /// <returns>Collection that contains time of year, average mark and criterion name.</returns>
        private IEnumerable<IReport> AverageScoreOfTestsBy(IEnumerable<TestResult> testResults, Func<TestResult, string> func)
        {
            var september = 10;
            var april = 4;
            var groupedByYear = testResults.GroupBy(e => e.Test.Date.Value.Year);
            var averageScore = new List<IReport>();
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
                        IReport score = new SessionReport();
                        var timeOfYear = isWinter.Key == true ? "Winter" : "Summer";
                        score.SessionName = $"{timeOfYear}/{year.Key}";
                        score.AverageMark = group.Sum(e => e.Mark).Value / group.Count();
                        score.Criteria = group.Key;
                        averageScore.Add(score);
                    }
                }
            }

            return averageScore;
        }

        /// <summary>
        /// Returns average score of exams.
        /// </summary>
        /// <param name="examResults"></param>
        /// <param name="func">The criterion for selection.</param>
        /// <returns>Collection that contains time of year, average mark and criterion name.</returns>
        private IEnumerable<IReport> AverageScoreOfExamsBy(IEnumerable<ExamResult> examResults, Func<ExamResult, string> func)
        {
            var september = 10;
            var april = 4;
            var groupedByYear = examResults.GroupBy(e => e.Exam.Date.Value.Year);
            var averageScore = new List<IReport>();
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
                        IReport score = new SessionReport();
                        var timeOfYear = isWinter.Key == true ? "Winter" : "Summer";
                        score.SessionName = $"{timeOfYear}/{year.Key}";
                        score.AverageMark = group.Sum(e => e.Mark).Value / group.Count();
                        score.Criteria = group.Key;
                        averageScore.Add(score);
                    }
                }
            }

            return averageScore;
        }
    }
}
