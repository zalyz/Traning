using Girl.Figures;
using System;
using System.IO;
using System.Text;
using System.Xml;

namespace Girl.DataReading
{
    /// <summary>
    /// Allows reading and writing xml files by XmlRader or XmlWriter.
    /// </summary>
    public class XmlDataAccess : IDataAccess<Figure>
    {
        /// <inheritdoc/>
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

        /// <summary>
        /// Creates instance of Circle class.
        /// </summary>
        /// <param name="info"> Set of information about sides length and figure color.</param>
        /// <returns> Instance of Circle class.</returns>
        private Circle GetCircle((string sides, string color) info)
        {
            double[] sidesLength = GetArrayOfSidesLength(info.sides);
            var color = GetColor(info.color);
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

        /// <summary>
        /// Creates instance of Triangle class.
        /// </summary>
        /// <param name="info"> Set of information about sides length and figure color.</param>
        /// <returns> Instance of Triangle class.</returns>
        private Triangle GetTriangle((string sides, string color) info)
        {
            double[] sidesLength = GetArrayOfSidesLength(info.sides);
            var color = GetColor(info.color);
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

        /// <summary>
        /// Creates instance of Rectangle class.
        /// </summary>
        /// <param name="info"> Set of information about sides length and figure color.</param>
        /// <returns> Instance of Rectangle class.</returns>
        private Rectangle GetRectangle((string sides, string color) info)
        {
            double[] sidesLength = GetArrayOfSidesLength(info.sides);
            var color = GetColor(info.color);
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

        /// <summary>
        /// Transforms string represent of sides length to array.
        /// </summary>
        /// <param name="sides"> Sides length in string form.</param>
        /// <returns> Array of sides length of figure.</returns>
        private double[] GetArrayOfSidesLength(string sides)
        {
            var splitedSides = sides.Split(' ');
            double[] sidesLength = new double[splitedSides.Length];

            for (int i = 0; i < sidesLength.Length; i++)
            {
                sidesLength[i] = double.Parse(splitedSides[i]);
            }

            return sidesLength;
        }

        /// <summary>
        /// Gets color by his string form.
        /// </summary>
        /// <param name="color"> String form of Color.</param>
        /// <returns> Color of figure.</returns>
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

        /// <summary>
        /// Gets information about figure from file.
        /// </summary>
        /// <param name="xmlReader"> Reader for Xml file.</param>
        /// <returns> Set of information about figure.</returns>
        private (string sides, string color) GetInformation(XmlReader xmlReader)
        {
            xmlReader.MoveToContent();
            xmlReader.ReadToDescendant("SidesLength");
            var sidesLength = xmlReader.ReadElementContentAsString();
            var color = xmlReader.ReadElementContentAsString();
            return (sidesLength, color);
        }

        /// <inheritdoc/>
        public void WriteData(Figure[] source, string path)
        {
            using (var xmlWriter = XmlWriter.Create(path))
            {
                xmlWriter.WriteStartDocument();

                xmlWriter.WriteStartElement("Figures");

                foreach (var item in source)
                {
                    string sidesInStringFormat = SidesLengthToStringFormat(item.SidesLength);

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

        /// <summary>
        /// Transforms Sides length to string form.
        /// </summary>
        /// <param name="sides"> Figure sides.</param>
        /// <returns> Sides length in string form.</returns>
        private string SidesLengthToStringFormat(double[] sides)
        {
            var sidesStringFormat = new StringBuilder();
            for (int i = 0; i < sides.Length; i++)
            {
                sidesStringFormat.Append(sides[i]);
                if (i < sides.Length - 1)
                {
                    sidesStringFormat.Append(" ");
                }
            }

            return sidesStringFormat.ToString();
        }
    }
}
