using Girl.Figures;
using System;
using System.Collections.Generic;
using Girl.DataReading;
using System.Text;

namespace Girl.FigureCollection
{
    class Box
    {
        private Figure[] _figures = new Figure[0];

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

        public Figure FigureAt(int index)
        {
            return _figures[index];
        }

        public Figure Take(int index)
        {
            var figure = _figures[index];
            DeleteElement(figure);
            return figure;
        }

        public void Replace(Figure figure, int index)
        {
            _figures[index] = figure;
        }

        public Figure Find(Figure simple)
        {
            foreach (var figure in _figures)
            {
                if (figure.Equals(simple))
                {
                    return figure;
                }
            }

            return null;
        }

        public int Count()
        {
            return _figures.Length;
        }

        public double TotalArear()
        {
            double sumOfArea = 0;
            foreach (var figure in _figures)
            {
                sumOfArea += figure.Area();
            }

            return sumOfArea;
        }

        public double TotalPerimeter()
        {
            double sumOfArea = 0;
            foreach (var figure in _figures)
            {
                sumOfArea += figure.Perimeter();
            }

            return sumOfArea;
        }

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

        public void ReadFromFile(IDataAccess<Figure> dataAccess, string path)
        {
            _figures = dataAccess.ReadData(path);
        }

        public void WriteToFile(IDataAccess<Figure> dataAccess, string path)
        {
            dataAccess.WriteData(_figures, path);
        }
        
        public void WriteToFile(IDataAccess<Figure> dataAccess, FigureMaterial materialOfFigures ,string path)
        {
            var selectedFigures = SelectByMaterial(materialOfFigures);
            dataAccess.WriteData(selectedFigures, path);
        }

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
    }
}
