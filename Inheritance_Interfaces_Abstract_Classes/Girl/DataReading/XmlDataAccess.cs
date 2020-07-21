using Girl.Figures;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;

namespace Girl.DataReading
{
    public class XmlDataAccess : IDataAccess<Figure>
    {
        public Figure[] ReadData(string path)
        {
            var arrayOfFigures = new Figure[0];
            using (var filestream = new FileStream(path, FileMode.OpenOrCreate))
            {
                var xmlReader = XmlReader.Create(filestream);

                while (xmlReader.Read())
                {
                    if (xmlReader.NodeType == XmlNodeType.Element)
                    {
                        switch (xmlReader.Name)
                        {
                            case "Circle":
                                var circleInformation = GetInformation(xmlReader);
                                Array.Resize(ref arrayOfFigures, arrayOfFigures.Length + 1);
                                arrayOfFigures[^1] = GetCircle(circleInformation);
                                break;
                            case "Triangle":
                                var triangleInformation = GetInformation(xmlReader);
                                Array.Resize(ref arrayOfFigures, arrayOfFigures.Length + 1);
                                arrayOfFigures[^1] = GetTriangle(triangleInformation);
                                break;
                            case "Rectangle":
                                var rectangleInformation = GetInformation(xmlReader);
                                Array.Resize(ref arrayOfFigures, arrayOfFigures.Length + 1);
                                arrayOfFigures[^1] = GetRectangle(rectangleInformation);
                                break;
                            default:
                                break;
                        }
                    }
                }
            }

            return arrayOfFigures;
        }

        private Circle GetCircle((string sides, string color) info)
        {
            double[] sidesLength = GetArrayOfSidesLength(info);
            var color = GetColor(info);
            if (color == FigureColor.Transparent)
            {
                return new Circle(FigureMaterial.Film, sidesLength[0]);
            }
            else
            {
                var circle = new Circle(FigureMaterial.Paper, sidesLength[0]);
                circle.PaintFigureTo(color);
                return circle;
            }
        }

        private Triangle GetTriangle((string sides, string color) info)
        {
            double[] sidesLength = GetArrayOfSidesLength(info);
            var color = GetColor(info);
            if (color == FigureColor.Transparent)
            {
                return new Triangle(FigureMaterial.Film, sidesLength[0], sidesLength[1], sidesLength[2]);
            }
            else
            {
                var circle = new Triangle(FigureMaterial.Paper, sidesLength[0], sidesLength[1], sidesLength[2]);
                circle.PaintFigureTo(color);
                return circle;
            }
        }

        private Rectangle GetRectangle((string sides, string color) info)
        {
            double[] sidesLength = GetArrayOfSidesLength(info);
            var color = GetColor(info);
            if (color == FigureColor.Transparent)
            {
                return new Rectangle(FigureMaterial.Film, sidesLength[0], sidesLength[1]);
            }
            else
            {
                var circle = new Rectangle(FigureMaterial.Paper, sidesLength[0], sidesLength[1]);
                circle.PaintFigureTo(color);
                return circle;
            }
        }

        private double[] GetArrayOfSidesLength((string sides, string color) info)
        {
            var splitedSides = info.sides.Split(' ');
            double[] sidesLength = new double[splitedSides.Length];

            for (int i = 0; i < sidesLength.Length; i++)
            {
                sidesLength[i] = double.Parse(splitedSides[i]);
            }

            return sidesLength;
        }

        private FigureColor GetColor((string sides, string color) info)
        {
            switch (info.color)
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

        private (string sides, string color) GetInformation(XmlReader xmlReader)
        {
            xmlReader.MoveToContent();
            xmlReader.ReadToDescendant("SidesLength");
            var sidesLength = xmlReader.ReadElementContentAsString();
            var color = xmlReader.ReadElementContentAsString();
            return (sidesLength, color);
        }

        public void WriteData(Figure[] source, string path)
        {
            using (var xmlWriter = XmlWriter.Create(path))
            {
                xmlWriter.WriteStartDocument();

                xmlWriter.WriteStartElement("Figures");

                foreach (var item in source)
                {
                    string sidesInStringFormat = SidesLengthToStringFormat(item);

                    if (item is Circle)
                    {
                        xmlWriter.WriteStartElement("Circle");
                    }

                    if (item is Triangle)
                    {
                        xmlWriter.WriteStartElement("Triangle");
                    }

                    if (item is Rectangle)
                    {
                        xmlWriter.WriteStartElement("Rectangle");
                    }

                    xmlWriter.WriteElementString("SidesLength", sidesInStringFormat);

                    xmlWriter.WriteElementString("Color", item.Color.ToString());

                    xmlWriter.WriteEndElement();
                }

                xmlWriter.WriteEndElement();

                xmlWriter.WriteEndDocument();
            }

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
