using Microsoft.VisualStudio.TestTools.UnitTesting;
using Girl.FigureCollection;
using System;
using System.Collections.Generic;
using System.Text;
using Girl.Figures;
using Girl.DataReading;

namespace Girl.FigureCollection.Tests
{
    [TestClass()]
    public class BoxTests
    {
        private Box _box = new Box();

        [TestInitialize]
        public void TestInitialize()
        {
            _box.Add(new Circle(FigureMaterial.Film, 4));
            _box.Add(new Triangle(FigureMaterial.Paper, 6, 5, 6));
            var rectangle = new Rectangle(FigureMaterial.Paper, 4, 6);
            rectangle.PaintFigureTo(FigureColor.Red);
            _box.Add(rectangle);
        }

        [TestMethod()]
        public void Add_InputBlueCircle_SuccessfullyAdding()
        {
            var circle = new Circle(FigureMaterial.Paper, 4);
            circle.PaintFigureTo(FigureColor.Blue);
            _box.Add(circle);
        }

        [ExpectedException(typeof(ArgumentException))]
        [TestMethod()]
        public void Add_InputFilmCircle_ThrowsArgumentException()
        {
            var circle = new Circle(FigureMaterial.Film, 4);
            _box.Add(circle);
        }

        [TestMethod()]
        public void FigureAt_Input1_TriangleReturned()
        {
            var indexOfFigure = 1;
            var expected = new Triangle(FigureMaterial.Paper, 6, 5, 6);
            var actual = _box.FigureAt(indexOfFigure);
            Assert.AreEqual(expected, actual);
        }

        [ExpectedException(typeof(IndexOutOfRangeException))]
        [TestMethod()]
        public void FigureAt_Input5_ThrowsIndexOutOfRangeException()
        {
            var indexOfFigure = 5;
            var actual = _box.FigureAt(indexOfFigure);
        }

        [TestMethod()]
        public void Take_Input1_CollectionWhithTwoElement()
        {
            var indexOfFigure = 1;
            var expectedCount = 2; 
            _box.Take(indexOfFigure);
            var actual = _box.Count();
            Assert.AreEqual(expectedCount, actual);
        }

        [ExpectedException(typeof(IndexOutOfRangeException))]
        [TestMethod()]
        public void Take_Input5_ThrowsIndexOutOfRangeException()
        {
            var indexOfFigure = 5;
            _box.Take(indexOfFigure);
        }

        [TestMethod()]
        public void Replace_IncertRectangle_SuccessfullyReplacing()
        {
            var rectangle = new Rectangle(FigureMaterial.Paper, 4, 5);
            _box.Replace(rectangle, 0);
            var expected = rectangle;
            var actual = _box.FigureAt(0);
            Assert.AreEqual(expected, actual);
        }

        [ExpectedException(typeof(IndexOutOfRangeException))]
        [TestMethod()]
        public void Replace_IncertRectangle_ThrowsIndexOutOfRangeException()
        {
            var rectangle = new Rectangle(FigureMaterial.Paper, 4, 5);
            _box.Replace(rectangle, 5);
        }

        [TestMethod()]
        public void Find_SampleRectangle_Rectanglereturned()
        {
            var rectangle = new Rectangle(FigureMaterial.Paper, 4, 6);
            rectangle.PaintFigureTo(FigureColor.Red);
            var expected = rectangle;
            var actual = _box.Find(rectangle);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void Count_InvokeCountMethod_3Returned()
        {
            var expectedCount = 3;
            var actual = _box.Count();
            Assert.AreEqual(expectedCount, actual);
        }

        [TestMethod()]
        public void TotalArea_InvokeTotalAreaMethod_87Returned()
        {
            var expectedArea = 87.87;
            var actual = _box.TotalArea();
            Assert.AreEqual(expectedArea, actual, 0.1);
        }

        [TestMethod()]
        public void TotalPerimeter_InvokeTotalPerimeterMethod_62Returned()
        {
            var expectedPerimeter = 62.12;
            var actual = _box.TotalPerimeter();
            Assert.AreEqual(expectedPerimeter, actual, 0.1);
        }

        [TestMethod()]
        public void GetAllCircles_InvokeGetAllCirclecsMethod_UnitArray()
        {
            var expected = new Figure[] { new Circle(FigureMaterial.Film, 4) };
            var actual = _box.GetAllCircles();
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void GetAllFilmFigures_InvokeGetAllFilmFiguresMethod_UnitArray()
        {
            var expected = new Figure[] { new Circle(FigureMaterial.Film, 4) };
            var actual = _box.GetAllFilmFigures();
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void ReadFromFile_InputXmlDataAccess_SuccessfullyReading()
        {
            _box.WriteToFile(new XmlDataAccess(), "Figures.xml");
            var expectedCollection = new Box();
            expectedCollection.ReadFromFile(new XmlDataAccess(), "Figures.xml");
            Assert.AreEqual(expectedCollection, _box);
        }

        [TestMethod()]
        public void WriteToFileAllElements_InputXmlDataAccess_SuccessfullyWriting()
        {
            _box.WriteToFile(new XmlDataAccess(), "Figures.xml");
        }

        [TestMethod()]
        public void WriteToFileByMaterial_InputStreamDataAccess_SuccessfullyWriting()
        {
            _box.WriteToFile(new StreamDataAccess(), FigureMaterial.Paper, "Figures.xml");
        }
    }
}