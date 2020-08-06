using Microsoft.VisualStudio.TestTools.UnitTesting;
using BinaryTree;
using System;
using System.Collections.Generic;
using System.Text;

namespace BinaryTree.Tests
{
    [TestClass()]
    public class BinaryTreeTests
    {
        private BinaryTree<TestResult> _treeRoot;

        [TestInitialize]
        public void TestInitialize()
        {
            _treeRoot = new BinaryTree<TestResult>();
            var firstResult = new TestResult("Bob", "Russian Test", DateTime.Parse("12.12.2020"), 87); ;
            var secondResult = new TestResult("Rose", "Math", DateTime.Parse("12.12.2020"), 72);
            var thirdResult = new TestResult("Kane", "History", DateTime.Parse("12.12.2020"), 10);
            _treeRoot.Add(firstResult);
            _treeRoot.Add(secondResult);
            _treeRoot.Add(thirdResult);
        }

        [TestMethod()]
        public void Add_InputTestResult_SuccessfulAdding()
        {
            var testResult = new TestResult("Gaben", "Game test", DateTime.Now, 8);
            _treeRoot.Add(testResult);
            var actual = _treeRoot.GetTreeValues()[3];
            Assert.AreEqual(testResult, actual);
        }

        [TestMethod()]
        public void TreeBalancing_BalancingTreeExpected()
        {
            var expectedTreeList = new List<TestResult>()
            {
                new TestResult("Rose", "Math", DateTime.Parse("12.12.2020"), 72),
                new TestResult("Kane", "History", DateTime.Parse("12.12.2020"), 10),
                new TestResult("Bob", "Russian Test", DateTime.Parse("12.12.2020"), 87),
            };
            _treeRoot.TreeBalancing();
            var actualTreeList = _treeRoot.GetTreeValues();
            CollectionAssert.AreEqual(expectedTreeList, actualTreeList);
        }

        [TestMethod()]
        public void WriteToXml_SuccessfulWRiting()
        {
            var expectedTree = _treeRoot.GetTreeValues();
            _treeRoot.WriteToXml("TreeXml.xml");
            _treeRoot.ReadFromXml("TreeXml.xml");
            var actualTree = _treeRoot.GetTreeValues();
            CollectionAssert.AreEqual(expectedTree, actualTree);
        }

        [TestMethod()]
        public void ReadFromXml_SuccessfulReading()
        {
            var expectedTree = _treeRoot.GetTreeValues();
            _treeRoot.WriteToXml("TreeXml.xml");
            _treeRoot.ReadFromXml("TreeXml.xml");
            var actualTree = _treeRoot.GetTreeValues();
            CollectionAssert.AreEqual(expectedTree, actualTree);
        }
    }
}