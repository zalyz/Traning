using Girl.Figures;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;

namespace Girl.DataReading
{
    public class StreamDataAccess : IDataAccess<Figure>
    {
        public Figure[] ReadData(string path)
        {
            var arrayOfFigures = new Figure[0];
            using (var xmlReader = new StreamReader(path))
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(xmlReader);
                XmlElement xRoot = xmlDoc.DocumentElement;
                foreach (XmlNode xNode in xRoot)
                {
                    Array.Resize(ref arrayOfFigures, arrayOfFigures.Length + 1);
                    var sidesLength = new double[0];
                    var figureColor = FigureColor.White;
                    ReadingFieldsOfFigure(xNode, ref sidesLength, ref figureColor);
                    InsetFigureInArray(arrayOfFigures, xNode, sidesLength, figureColor);
                }
            }

            return arrayOfFigures;
        }

        private void InsetFigureInArray(Figure[] arrayOfFigures, XmlNode xNode, double[] sidesLength, FigureColor figureColor)
        {
            switch (xNode.Name)
            {
                case nameof(Circle):
                    arrayOfFigures[^1] = GetCircle(sidesLength, figureColor);
                    break;
                case nameof(Triangle):
                    arrayOfFigures[^1] = GetTriangle(sidesLength, figureColor);
                    break;
                case nameof(Rectangle):
                    arrayOfFigures[^1] = GetRectangle(sidesLength, figureColor);
                    break;
                default:
                    break;
            }
        }

        private void ReadingFieldsOfFigure(XmlNode xNode, ref double[] sidesLength, ref FigureColor figureColor)
        {
            foreach (XmlNode item in xNode.ChildNodes)
            {
                switch (item.Name)
                {
                    case "SidesLength":
                        sidesLength = GetArrayOfSidesLength(item.InnerText);
                        break;
                    case "Color":
                        figureColor = GetColor(item.InnerText);
                        break;
                }
            }
        }

        private Circle GetCircle(double[] sides, FigureColor color)
        {
            if (color == FigureColor.Transparent)
            {
                return new Circle(FigureMaterial.Film, sides[0]);
            }
            else
            {
                var circle = new Circle(FigureMaterial.Paper, sides[0]);
                circle.PaintFigureTo(color);
                return circle;
            }
        }

        private Triangle GetTriangle(double[] sides, FigureColor color)
        {
            if (color == FigureColor.Transparent)
            {
                return new Triangle(FigureMaterial.Film, sides[0], sides[1], sides[2]);
            }
            else
            {
                var circle = new Triangle(FigureMaterial.Paper, sides[0], sides[1], sides[2]);
                circle.PaintFigureTo(color);
                return circle;
            }
        }

        private Rectangle GetRectangle(double[] sides, FigureColor color)
        {
            if (color == FigureColor.Transparent)
            {
                return new Rectangle(FigureMaterial.Film, sides[0], sides[1]);
            }
            else
            {
                var circle = new Rectangle(FigureMaterial.Paper, sides[0], sides[1]);
                circle.PaintFigureTo(color);
                return circle;
            }
        }

        private double[] GetArrayOfSidesLength(string sidesLengthInStringFormat)
        {
            var splitedSides = sidesLengthInStringFormat.Split(' ');
            double[] sidesLength = new double[splitedSides.Length];

            for (int i = 0; i < sidesLength.Length; i++)
            {
                sidesLength[i] = double.Parse(splitedSides[i]);
            }

            return sidesLength;
        }

        private FigureColor GetColor(string color)
        {
            switch (color)
            {
                case "Transparent":
                    return FigureColor.Transparent;
                case "White":
                    return FigureColor.White;
                case "Red":
                    return FigureColor.Red;
                case "Blue":
                    return FigureColor.Blue;
                case "Yellow":
                    return FigureColor.Yellow;
                case "Green":
                    return FigureColor.Green;
                default:
                    return FigureColor.White;
            }
        }

        public void WriteData(Figure[] source, string path)
        {
            using (var writer = new StreamWriter(path))
            {
                writer.Write("<Figures>");
                string typeOfFigure;
                foreach (var figure in source)
                {
                    typeOfFigure = GetTypeOfFigure(figure);

                    writer.Write("<" + typeOfFigure + ">");
                    WriteSidesLengthToFile(figure, writer);
                    WriteColorToFile(figure, writer);
                    writer.Write("</" + typeOfFigure + ">");
                }

                writer.Write("</Figures>");
            }
        }

        private static string GetTypeOfFigure(Figure figure)
        {
            if (figure is Circle)
            {
                var typeOfFigure = nameof(Circle);
                return typeOfFigure;
            }

            if (figure is Triangle)
            {
                var typeOfFigure = nameof(Triangle);
                return typeOfFigure;
            }

            if (figure is Rectangle)
            {
                var typeOfFigure = nameof(Rectangle);
                return typeOfFigure;
            }

            throw new ArgumentException(nameof(Figure) + "is not valid.");
        }

        private void WriteSidesLengthToFile(Figure figure, StreamWriter stream)
        {
            var sidesLengthInStringFormat = SidesLengthToStringFormat(figure);
            stream.Write("<SidesLength>");
            stream.Write(sidesLengthInStringFormat);
            stream.Write("</SidesLength>");
        }

        private void WriteColorToFile(Figure figure, StreamWriter stream)
        {
            stream.Write("<Color>");
            stream.Write(figure.Color.ToString());
            stream.Write("</Color>");
        }

        private string SidesLengthToStringFormat(Figure figure)
        {
            var sidesStringFormat = new StringBuilder();
            for (int i = 0; i < figure.SidesLength.Length; i++)
            {
                sidesStringFormat.Append(figure.SidesLength[i]);
                if (i < figure.SidesLength.Length - 1)
                {
                    sidesStringFormat.Append(" ");
                }
            }

            return sidesStringFormat.ToString();
        }
    }
}
