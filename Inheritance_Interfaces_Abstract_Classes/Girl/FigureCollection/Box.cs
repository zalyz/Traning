using Girl.Figures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Girl.FigureCollection
{
    class Box
    {
        private IEnumerable<Figure> _figures;

        public void Add(Figure figure)
        {
            if (!_figures.Contains(figure) && _figures.Count() < 20)
            {
                _figures.Append(figure);
            }
            else
            {
                throw new ArgumentException();
            }
        }

        public Figure FigureAt(int index)
        {
            return _figures.ElementAt(index);
        }

        public Figure Take(int index)
        {
            var figure = _figures.ElementAt(index);
            _figures = _figures.Where(e => e != figure);
            return figure;
        }

        public void Replace(Figure figure, int index)
        {
            var figureAtIndex = _figures.ElementAt(index);
            figureAtIndex = figure;
        }

        public Figure Find(Func<Figure, bool> selector)
        {
            foreach (var figure in _figures)
            {
                if (selector.Invoke(figure))
                {
                    return figure;
                }
            }

            return null;
        }

        public int Count()
        {
            return _figures.Count();
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

        public IEnumerable<Figure> GetAllCircles()
        {
            return _figures.Where(e => e is Circle);
        }

        public IEnumerable<Figure> GetAllFilmFigures()
        {
            return _figures.Where(e => e.Color == FigureColor.Transparent);
        }
    }
}
