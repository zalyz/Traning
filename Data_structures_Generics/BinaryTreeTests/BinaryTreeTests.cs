using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace BinaryTree.Tests
{
    /// <summary>
    /// Defines methods for the testing BinaryTree class.
    /// </summary>
    [TestClass()]
    public class BinaryTreeTests
    {
        /// <summary>
        /// Tree root of default tree.
        /// </summary>
        private BinaryTree<TestResult> _treeRoot;

        /// <summary>
        /// Creates default tree.
        /// </summary>
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

        /// <summary>
        /// Adds new test result to the tree.
        /// </summary>
        [TestMethod()]
        public void Add_InputTestResult_SuccessfulAdding()
        {
            var testResult = new TestResult("Gaben", "Game test", DateTime.Now, 8);
            _treeRoot.Add(testResult);
            var actual = _treeRoot.GetTreeValues()[3];
            Assert.AreEqual(testResult, actual);
        }

        /// <summary>
        /// Balancing default tree.
        /// </summary>
        [TestMethod()]
        public void TreeBalancing_BalancingTreeExpected()
        {
            var expectedTreeList = new List<TestResult>()
            {
                new TestResult("Rose", "Math", DateTime.Parse("12.12.2020"), 72),
                new TestResult("Kane", "History", DateTime.Parse("12.12.2020"), 10),
                new TestResult("Bob", "Russian Test", DateTime.Parse("12.12.2020"), 87),
            };
            _treeRoot.BalanceTheTree();
            var actualTreeList = _treeRoot.GetTreeValues();
            CollectionAssert.AreEqual(expectedTreeList, actualTreeList);
        }

        /// <summary>
        /// Writes binary tree to the file.
        /// </summary>
        [TestMethod()]
        public void WriteToXml_SuccessfulWRiting()
        {
            var expectedTree = _treeRoot.GetTreeValues();
            _treeRoot.WriteToXml("TreeXml.xml");
            _treeRoot.ReadFromXml("TreeXml.xml");
            var actualTree = _treeRoot.GetTreeValues();
            CollectionAssert.AreEqual(expectedTree, actualTree);
        }

        /// <summary>
        /// Reades binary tree from the file.
        /// </summary>
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