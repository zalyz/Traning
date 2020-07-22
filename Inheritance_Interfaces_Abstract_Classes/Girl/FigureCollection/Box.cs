using Girl.Figures;
using System;
using System.Linq;
using Girl.DataReading;

namespace Girl.FigureCollection
{
    /// <summary>
    /// Represents Box which contains geometric figures.
    /// </summary>
    public class Box
    {
        /// <summary>
        /// Array of Figures.
        /// </summary>
        private Figure[] _figures = new Figure[0];

        /// <summary>
        /// Adds figures to array.
        /// </summary>
        /// <param name="figure"> Figure to adding.</param>
        public void Add(Figure figure)
        {
            if (!IsContains(figure) && _figures.Length < 20)
            {
                Array.Resize(ref _figures, _figures.Length + 1);
                _figures[^1] = figure;
            }
            else
            {
                throw new ArgumentException("You can't add an existing figure.");
            }
        }

        /// <summary>
        /// Gets figure by index.
        /// </summary>
        /// <param name="index"> Figure index.</param>
        /// <returns> Concreat figure by index.</returns>
        public Figure FigureAt(int index)
        {
            return _figures[index];
        }

        /// <summary>
        /// Gets figure by index and remove it from array.
        /// </summary>
        /// <param name="index"> Figure index.</param>
        /// <returns> Concrete figure by index.</returns>
        public Figure Take(int index)
        {
            var figure = _figures[index];
            DeleteElement(figure);
            return figure;
        }

        /// <summary>
        /// Replaces Figure from array to passed figure.
        /// </summary>
        /// <param name="figure">Figure to replace.</param>
        /// <param name="index"> Position in array.</param>
        public void Replace(Figure figure, int index)
        {
            _figures[index] = figure;
        }

        /// <summary>
        /// Find figure in array by passed example.
        /// </summary>
        /// <param name="sample">Example figure.</param>
        /// <returns> The same figure, null otherwise.</returns>
        public Figure Find(Figure sample)
        {
            foreach (var figure in _figures)
            {
                if (figure.Equals(sample))
                {
                    return figure;
                }
            }

            return null;
        }

        /// <summary>
        /// Gets number of figure in array.
        /// </summary>
        /// <returns> Number of figures.</returns>
        public int Count()
        {
            return _figures.Length;
        }

        /// <summary>
        /// Gets total arear of all figures.
        /// </summary>
        /// <returns> Total area of all figures.</returns>
        public double TotalArea()
        {
            double sumOfArea = 0;
            foreach (var figure in _figures)
            {
                sumOfArea += figure.Area();
            }

            return sumOfArea;
        }

        /// <summary>
        /// Gets total perimeter of all figures.
        /// </summary>
        /// <returns> Total perimeter of all figures.</returns>
        public double TotalPerimeter()
        {
            double sumOfArea = 0;
            foreach (var figure in _figures)
            {
                sumOfArea += figure.Perimeter();
            }

            return sumOfArea;
        }

        /// <summary>
        /// Gets array of circle from box.
        /// </summary>
        /// <returns> Array of circles.</returns>
        public Circle[] GetAllCircles()
        {
            var arrayOfCircle = new Circle[0];
            foreach (var item in _figures)
            {
                if (item is Circle)
                {
                    Array.Resize(ref arrayOfCircle, arrayOfCircle.Length + 1);
                    arrayOfCircle[^1] = (item as Circle);
                }
            }

            if (arrayOfCircle.Length > 0)
            {
                return arrayOfCircle;
            }

            return null;
        }

        /// <summary>
        /// Gets array of film figures from box.
        /// </summary>
        /// <returns> Array of film figures.</returns>
        public Figure[] GetAllFilmFigures()
        {
            var arrayOfFilmFigures = new Figure[0];
            foreach (var item in _figures)
            {
                if (item.Color == FigureColor.Transparent)
                {
                    Array.Resize(ref arrayOfFilmFigures, arrayOfFilmFigures.Length + 1);
                    arrayOfFilmFigures[^1] = item;
                }
            }

            if (arrayOfFilmFigures.Length > 0)
            {
                return arrayOfFilmFigures;
            }

            return null;
        }

        /// <summary>
        /// Reads figures from file.
        /// </summary>
        /// <param name="dataAccess"> File reader.</param>
        /// <param name="path"> File path.</param>
        public void ReadFromFile(IDataAccess<Figure> dataAccess, string path)
        {
            _figures = dataAccess.ReadData(path);
        }

        /// <summary>
        /// Writes all figures to file.
        /// </summary>
        /// <param name="dataAccess"> File writer.</param>
        /// <param name="path"> File path.</param>
        public void WriteToFile(IDataAccess<Figure> dataAccess, string path)
        {
            dataAccess.WriteData(_figures, path);
        }
        
        /// <summary>
        /// Writes figures to file by material.
        /// </summary>
        /// <param name="dataAccess"> File writer.</param>
        /// <param name="materialOfFigures"> Figure material.</param>
        /// <param name="path"> File path.</param>
        public void WriteToFile(IDataAccess<Figure> dataAccess, FigureMaterial materialOfFigures ,string path)
        {
            var selectedFigures = SelectByMaterial(materialOfFigures);
            dataAccess.WriteData(selectedFigures, path);
        }

        /// <summary>
        /// Select figures by his material.
        /// </summary>
        /// <param name="figureMaterial"> Figure material.</param>
        /// <returns> Array of figures whith same material.</returns>
        private Figure[] SelectByMaterial(FigureMaterial figureMaterial)
        {
            var arrayOfFigures = new Figure[0];
            foreach (var item in _figures)
            {
                if (figureMaterial == FigureMaterial.Paper)
                {
                    if (item.Color != FigureColor.Transparent)
                    {
                        Array.Resize(ref arrayOfFigures, arrayOfFigures.Length + 1);
                        arrayOfFigures[^1] = item;
                    }
                }

                if (figureMaterial == FigureMaterial.Film)
                {
                    if (item.Color == FigureColor.Transparent)
                    {
                        Array.Resize(ref arrayOfFigures, arrayOfFigures.Length + 1);
                        arrayOfFigures[^1] = item;
                    }
                }
            }

            return arrayOfFigures;
        }

        /// <summary>
        /// Checks before adding is array contain same figure.
        /// </summary>
        /// <param name="figure"> Figure to check.</param>
        /// <returns> True if contains, False otherwise.</returns>
        private bool IsContains(Figure figure)
        {
            foreach (var item in _figures)
            {
                if (item.Equals(figure))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Deletes element from array..
        /// </summary>
        /// <param name="figure"> Figure to removing.</param>
        private void DeleteElement(Figure figure)
        {
            var updatedArray = new Figure[_figures.Length - 1];
            var updatedArrayIndex = 0;
            foreach (var item in _figures)
            {
                if (item != figure)
                {
                    updatedArray[updatedArrayIndex] = item;
                    updatedArrayIndex++;
                }
            }

            _figures = updatedArray;
        }
        
        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            return Equals(obj as Box);
        }

        /// <summary>
        /// Determaines whether the passed box is equal to the current.
        /// </summary>
        /// <param name="box"> Box to check.</param>
        /// <returns> True if boxes are equal, False otherwise.</returns>
        public bool Equals(Box box)
        {
            return box != null &&
                box._figures.SequenceEqual(_figures);
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return (_figures).GetHashCode();
        }
    }
}
